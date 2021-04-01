using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace CurrencyConvertor.Utility
{
    public class Logging
    {
        public static string strLogFolder = ConfigurationManager.AppSettings["LogFolder"].ToString();


        public static void writteLog(string logText, String fileName)
        {

            String strLogFileName = fileName + "_" + System.DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            string filePath = strLogFolder + strLogFileName;

            if (!File.Exists(filePath))
            {
                FileStream fs = File.Create(filePath);
                fs.Close();

            }
            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine("{0} {1} :{2}", DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString(), logText);
                sw.WriteLine("====================================================================================");
                sw.Flush();
                sw.Close();
            }
        }
    }
}