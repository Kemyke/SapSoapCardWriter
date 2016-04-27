using SapSoapCardWriter.BusinessLogic;
using SapSoapCardWriter.BusinessLogic.NFC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapSoapCardWriter.TestConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                string path = "CardData.txt";

                if (args.Length == 1)
                {
                    path = args[0];
                }

                var cardWriter = new NfcCardWriter(new ConsoleLogger());

                Console.WriteLine("Try to read NFC card...");
                var tags = cardWriter.ReadNfcTags().Result;
                Console.WriteLine("NFC card read completed!");

                Console.WriteLine("Writing file to {0} ...", path);
                using (var stream = new StreamWriter(path))
                {
                    foreach (var tag in tags)
                    {
                        stream.WriteLine(tag);
                    }
                }

                Console.WriteLine("Write completed!");
            }
            catch(Exception ex)
            {
                var oldColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Exception occured: {0}.", ex);
                Console.ForegroundColor = oldColor;
            }
            finally
            {
                Console.WriteLine("Press any key to exit!");
                Console.ReadLine();
            }
        }
    }
}
