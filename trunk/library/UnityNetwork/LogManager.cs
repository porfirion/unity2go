using System;
using System.IO;
using System.Globalization;
using UnityEngine;

namespace UnityNetwork
{
    public class LogManager
    {
        private string name = "";

        public LogManager()
        {
            this.name = "log" + TimeNow();
            Log("============================================");
            Log("Logging started");
        }

        public LogManager(string name)
        {
            this.name = name + TimeNow();
            Log("============================================");
            Log("Logging started");
        }

        ~LogManager()
        {
            Log("Logging finished");
        }

        public void Log(string message)
        {
            Debug.Log(message);

            FileStream fs = new FileStream(name + ".txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);

            sw.WriteLine(DateTime.Now.ToString() + " : " + message);
            sw.Close();
            fs.Dispose();
        }

        string TimeNow()
        {
            return DateTime.Now.ToString("_yyyy_MM_dd", CultureInfo.CreateSpecificCulture("ru-RU")) + DateTime.Now.ToString("(HH.mm.ss)", CultureInfo.CreateSpecificCulture("ru-RU"));
        }
    }
}
