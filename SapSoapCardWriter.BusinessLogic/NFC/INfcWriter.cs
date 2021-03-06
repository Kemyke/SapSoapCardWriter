﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapSoapCardWriter.BusinessLogic.NFC
{
    public enum ReaderState
    {
        CardNotPresent,
        CardPresent,
    }

    public interface INfcWriter
    {
        void StartMonitor();
        void StopMonitor();
        bool Erase(string key);
        bool Prepare(string key);
        bool Lock(string key);
        bool WriteNfcTag(List<string> dataList);
        List<string> ReadNfcTags();
        string GetSerialNumber();

        event EventHandler<ReaderState> ReaderStateChanged;
        byte[] GetCardUID(string key);
    }
}
