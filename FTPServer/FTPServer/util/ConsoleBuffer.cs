using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTPServer.model;

namespace FTPServer.util
{
    public class ConsoleBuffer
    {
        internal static readonly List<string> ServerStatusLog = new List<string>();
        internal static readonly List<string> ServerActionLog = new List<string>();
        public static void AddToStatusLog(string text)
        {
            ServerStatusLog.Add("[" + DateTime.Now + "] " + text + Environment.NewLine);
        }

        public static void AddToActionLog(string text)
        {
            ServerActionLog.Add("[" + DateTime.Now + "] " + text + Environment.NewLine);
        }

        internal static void PrintUserDetails(ClientInformation clientInfo)
        {
            AddToActionLog("Username = " + clientInfo.Username);
            AddToActionLog("Password = " + clientInfo.Password);
            AddToActionLog("Salt = " + clientInfo.Salt);
            AddToActionLog("Available = " + clientInfo.LoggedIn);
        }
    }
}
