using Incentivapp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Incentivapp.Utils
{
    public class Logger
    {
        private static string _path = @"C:\Users\Public\Logs";
        private static string _projectName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        /// <summary>
        /// Guarda las excepciones
        /// </summary>
        /// <param name="ex"></param>
        public static void LogException(Exception ex)
        {
            var exceptionPath = $@"{_path}\{_projectName}\Exceptions";
            if (!Directory.Exists(exceptionPath))
                Directory.CreateDirectory(exceptionPath);
            string filePath = $"{exceptionPath}/exceptions-{DateTime.Now.ToString("yyyy-MM-dd")}.json";
            var excptList = new List<Exception>();
            if (File.Exists(filePath))
                excptList = JsonConvert.DeserializeObject<List<Exception>>(File.ReadAllText(filePath));

            excptList.Add(ex);
            File.WriteAllText(filePath,JsonConvert.SerializeObject(excptList));


        }
        /// <summary>
        /// Loggea algunos comportamientos
        /// </summary>
        /// <param name="obj"></param>
        public static void LogJson(LogModel obj)
        {
            var logPath = $@"{_path}\{_projectName}\Logs";
            if (!Directory.Exists(logPath))
                Directory.CreateDirectory(logPath);
            string filePath = $"{logPath}/logs-{DateTime.Now.ToString("yyyy-MM-dd")}.json";
            var logList = new List<LogModel>();
            if (File.Exists(filePath))
                logList = JsonConvert.DeserializeObject<List<LogModel>>(File.ReadAllText(filePath));
            logList.Add(obj);
            File.WriteAllText(filePath, JsonConvert.SerializeObject(logList));
        }
    }
}