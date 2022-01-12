using System;
using System.IO;

namespace ShowBridge.Services.CommonService
{
    public static class ErrorLog
    {
        public static void LogErrorToFile(Exception ex, string controllerName, string methodname, string path)
        {
            try
            {
                string message = string.Empty;
                string EntityErrorMessages = string.Empty;
                try
                {
                    using (StreamWriter writer = new StreamWriter(path, true))
                    {
                        message = "*************************************************************************************";
                        message += Environment.NewLine;
                        message += CreateErrorMessage(ex, controllerName, methodname);
                        message += "*************************************************************************************";
                        message += Environment.NewLine; message += Environment.NewLine;
                        writer.WriteLine(message);
                    }
                }
                catch
                {
                    message = string.Empty;
                }

            }
            catch
            {
            }
        }
        private static string CreateErrorMessage(Exception ex, string controllerName, string methodname)
        {
            string message = string.Empty;
            string EntityErrorMessages = string.Empty;
            message += string.Format("Message: {0}", ex.Message);
            message += Environment.NewLine;
            message += string.Format("StackTrace: {0}", ex.StackTrace);
            message += Environment.NewLine;
            message += string.Format("Source: {0}", ex.Source);
            message += Environment.NewLine;
            message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
            message += Environment.NewLine;
            message += "Inner Exception : " + ((ex.InnerException != null) ? ex.InnerException.Message : "");
            message += Environment.NewLine;
            message += "Inner Exception Message : " + ((ex.InnerException != null) ? (ex.InnerException.InnerException != null ? ex.InnerException.InnerException.Message : "") : "");
            message += Environment.NewLine;
            message += !string.IsNullOrEmpty(EntityErrorMessages) ? EntityErrorMessages : "";
            message += Environment.NewLine;

            return message;
        }
    }
}
