using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MonstermakarenWPF
{
    static class Logger
    {
        public static void Log(string message)
        {
            StreamWriter SW = null;
            try
            {
                SW = new StreamWriter("C:\\Temp\\MM_Logg.txt", true);

                SW.WriteLine(DateTime.Now + " " + message);
            }
            finally
            {
                if (SW != null)
                    SW.Close();
            }
            
        }

    }
}
