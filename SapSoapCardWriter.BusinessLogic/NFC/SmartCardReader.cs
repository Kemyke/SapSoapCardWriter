using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SapSoapCardWriter.BusinessLogic.NFC
{
    public class SmartCardReader
    {
        uint _last_error;
        uint _scope = SmartCard.SCOPE_SYSTEM;
        string _reader_name;
        uint _reader_state = SmartCard.STATE_UNAWARE;
        CardBuffer _card_atr = null;
        StatusChangeCallback _status_change_callback = null;
        Thread _status_change_thread = null;
        volatile bool _status_change_running = false;

        public SmartCardReader(uint Scope, string ReaderName)
        {
            _scope = Scope;
            _reader_name = ReaderName;
        }

        public SmartCardReader(string ReaderName)
        {
            _reader_name = ReaderName;
        }

        ~SmartCardReader()
        {
            StopMonitor();
        }

        public uint Scope
        {
            get
            {
                return _scope;
            }
        }

        /**v* SCardReader/Name
		 *
		 * NAME
		 *   SCardReader.Name
		 *
		 * SYNOPSIS
		 *   string Name
		 *
		 * OUTPUT
		 *   Return the name of the reader specified when instanciating the object.
		 *
		 **/

        public string Name
        {
            get
            {
                return _reader_name;
            }
        }

        /**t* SCardReader/StatusChangeCallback
		 *
		 * NAME
		 *   SCardReader.StatusChangeCallback
		 *
		 * SYNOPSIS
		 *   delegate void StatusChangeCallback(uint ReaderState, CardBuffer CardAtr);
		 *
		 * DESCRIPTION
		 *   Typedef for the callback that will be called by the background thread launched
		 *   by SCardReader.StartMonitor(), everytime the status of the reader is changed.
		 *
		 * NOTES
		 *   The callback is invoked in the context of a background thread. This implies that
		 *   it is not allowed to access the GUI's components directly.
		 *
		 **/
        public delegate void StatusChangeCallback(uint ReaderState,
                                                  CardBuffer CardAtr);

        /**m* SCardReader/StartMonitor
		 *
		 * NAME
		 *   SCardReader.StartMonitor()
		 *
		 * SYNOPSIS
		 *   SCardReader.StartMonitor(SCardReader.StatusChangeCallback callback);
		 *
		 * DESCRIPTION
		 *   Create a background thread to monitor the reader associated to the object.
		 *   Everytime the status of the reader is changed, the callback is invoked.
		 *
		 * SEE ALSO
		 *   SCardReader.StatusChangeCallback
		 *   SCardReader.StopMonitor()
		 *
		 **/

        public void StartMonitor(StatusChangeCallback callback)
        {
            StopMonitor();

            if (callback != null)
            {
                _status_change_callback = callback;
                _status_change_thread = new Thread(StatusChangeMonitor);
                _status_change_running = true;
                _status_change_thread.Start();
            }
        }

        /**m* SCardReader/StopMonitor
		 *
		 * NAME
		 *   SCardReader.StopMonitor()
		 *
		 * DESCRIPTION
		 *   Stop the background thread previously launched by SCardReader.StartMonitor().
		 *
		 **/

        public void StopMonitor()
        {
            _status_change_callback = null;
            _status_change_running = false;

            if (_status_change_thread != null)
            {
                _status_change_thread.Abort();
                _status_change_thread.Join();
                _status_change_thread = null;
            }
        }

        private void StatusChangeMonitor()
        {
            uint rc;

            IntPtr hContext = IntPtr.Zero;

            _reader_state = SmartCard.STATE_UNAWARE;
            _card_atr = null;

            rc =
                SmartCard.EstablishContext(_scope, IntPtr.Zero, IntPtr.Zero, ref hContext);
            if (rc != SmartCard.S_SUCCESS)
                return;

            SmartCard.READERSTATE[] states = new SmartCard.READERSTATE[1];

            states[0] = new SmartCard.READERSTATE();
            states[0].szReader = _reader_name;
            states[0].pvUserData = IntPtr.Zero;
            states[0].dwCurrentState = 0;
            states[0].dwEventState = 0;
            states[0].cbAtr = 0;
            states[0].rgbAtr = null;

            while (_status_change_running)
            {
                rc = SmartCard.GetStatusChange(hContext, 250, states, 1);

                if (!_status_change_running)
                    break;

                if (rc == SmartCard.E_TIMEOUT)
                    continue;

                if (rc != SmartCard.S_SUCCESS)
                {
                    _last_error = rc;

                    SmartCard.ReleaseContext(hContext);
                    if (_status_change_callback != null)
                        _status_change_callback(0, null);
                    break;
                }

                if ((states[0].dwEventState & SmartCard.STATE_CHANGED) != 0)
                {
                    states[0].dwCurrentState = states[0].dwEventState;

                    if (_status_change_callback != null)
                    {
                        CardBuffer card_atr = null;

                        if ((states[0].dwEventState & SmartCard.STATE_PRESENT) != 0)
                            card_atr =
                                new CardBuffer(states[0].rgbAtr, (int)states[0].cbAtr);

                        _status_change_callback(states[0].dwEventState & ~SmartCard.
                                                STATE_CHANGED, card_atr);
                    }
                }
            }

            SmartCard.ReleaseContext(hContext);
        }

        private void UpdateState()
        {
            uint rc;

            IntPtr hContext = IntPtr.Zero;

            _reader_state = SmartCard.STATE_UNAWARE;
            _card_atr = null;

            rc =
                SmartCard.EstablishContext(_scope, IntPtr.Zero, IntPtr.Zero, ref hContext);
            if (rc != SmartCard.S_SUCCESS)
            {
                _last_error = rc;
                return;
            }

            SmartCard.READERSTATE[] states = new SmartCard.READERSTATE[1];

            states[0] = new SmartCard.READERSTATE();
            states[0].szReader = _reader_name;
            states[0].pvUserData = IntPtr.Zero;
            states[0].dwCurrentState = 0;
            states[0].dwEventState = 0;
            states[0].cbAtr = 0;
            states[0].rgbAtr = null;

            rc = SmartCard.GetStatusChange(hContext, 0, states, 1);
            if (rc != SmartCard.S_SUCCESS)
            {
                SmartCard.ReleaseContext(hContext);
                return;
            }

            SmartCard.ReleaseContext(hContext);

            _reader_state = states[0].dwEventState;

            if ((_reader_state & SmartCard.STATE_PRESENT) != 0)
            {
                _card_atr = new CardBuffer(states[0].rgbAtr, (int)states[0].cbAtr);
            }
        }

        /**v* SCardReader/Status
		 *
		 * NAME
		 *   SCardReader.Status
		 * 
		 * SYNOPSIS
		 *   uint Status
		 *
		 * OUTPUT
		 *   Returns the current status of the reader.
		 *
		 * SEE ALSO
		 *   SCardReader.CardPresent
		 *   SCardReader.StatusAsString
		 *
		 **/

        public uint Status
        {
            get
            {
                UpdateState();
                return _reader_state;
            }
        }

        /**v* SCardReader/StatusAsString
		 *
		 * NAME
		 *   SCardReader.StatusAsString
		 * 
		 * SYNOPSIS
		 *   string StatusAsString
		 *
		 * OUTPUT
		 *   Returns the current status of the reader, using SCARD.ReaderStatusToString as formatter.
		 *
		 * SEE ALSO
		 *   SCardReader.Status
		 *
		 **/

        public string StatusAsString
        {
            get
            {
                UpdateState();
                return SmartCard.ReaderStatusToString(_reader_state);
            }
        }

        /**v* SCardReader/CardPresent
		 *
		 * NAME
		 *   SCardReader.CardPresent
		 * 
		 * SYNOPSIS
		 *   bool CardPresent
		 *
		 * OUTPUT
		 *   Returns true if a card is present in the reader.
		 *   Returns false if there's no smartcard in the reader.
		 *
		 * SEE ALSO
		 *   SCardReader.CardAtr
		 *   SCardReader.CardAvailable
		 *   SCardReader.Status
		 *
		 **/

        public bool CardPresent
        {
            get
            {
                UpdateState();
                if ((_reader_state & SmartCard.STATE_PRESENT) != 0)
                    return true;
                return false;
            }
        }

        /**v* SCardReader/CardAvailable
		 *
		 * NAME
		 *   SCardReader.CardAvailable
		 *
		 * SYNOPSIS
		 *   bool CardAvailable
		 *
		 * OUTPUT
		 *   Returns true if a card is available in the reader.
		 *   Returns false if there's no smartcard in the reader, or if it is already used by another process/thread.
		 * 
		 * SEE ALSO
		 *   SCardReader.CardAtr
		 *   SCardReader.CardPresent
		 *   SCardReader.Status
		 *
		 **/

        public bool CardAvailable
        {
            get
            {
                UpdateState();
                if (((_reader_state & SmartCard.STATE_PRESENT) != 0)
                    && ((_reader_state & SmartCard.STATE_MUTE) == 0)
                    && ((_reader_state & SmartCard.STATE_INUSE) == 0))
                    return true;
                return false;
            }
        }

        /**v* SCardReader/CardAtr
		 *
		 * NAME
		 *   SCardReader.CardAtr
		 *
		 * SYNOPSIS
		 *   CardBuffer CardAtr
		 *
		 * OUTPUT
		 *   If a smartcard is present in the reader (SCardReader.CardPresent == true), returns the ATR of the card.
		 *   Returns null overwise.
		 * 
		 * SEE ALSO
		 *   SCardReader.CardPresent
		 *   SCardReader.Status
		 *
		 **/

        public CardBuffer CardAtr
        {
            get
            {
                UpdateState();
                return _card_atr;
            }
        }

        /**v* SCardReader/LastError
		 *
		 * NAME
		 *   uint SCardReader.LastError
		 * 
		 * OUTPUT
		 *   Returns the last error encountered by the object when working with SCARD functions.
		 *
		 * SEE ALSO
		 *   SCardReader.LastErrorAsString
		 *
		 **/
        public uint LastError
        {
            get
            {
                return _last_error;
            }
        }

        /**v* SCardReader/LastErrorAsString
		 *
		 * NAME
		 *   string SCardReader.LastErrorAsString
		 * 
		 * OUTPUT
		 *   Returns the last error encountered by the object when working with SCARD functions.
		 *
		 * SEE ALSO
		 *   SCardReader.LastError
		 *
		 **/
        public string LastErrorAsString
        {
            get
            {
                return SmartCard.ErrorToString(_last_error);
            }
        }

    }
}
