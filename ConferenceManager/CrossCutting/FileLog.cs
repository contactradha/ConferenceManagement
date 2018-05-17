using System;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;
/// <summary>
/// This file helps to log onto a file 
/// </summary>
namespace ConferenceManager.CrossCutting
{
    public class FileLog : Log
    {
        // Internal log file name value
        private string _StackTrace = "";
        private string _FileLocation;
        private static ResourceManager resourceManager = new ResourceManager(typeof(FileLog).Namespace + ".ExceptionManagerText", Assembly.GetAssembly(typeof(FileLog)));

        public FileLog()
        {
        }
        /// <summary>
        /// Record the Message acception exception
        /// </summary>
        /// <param name="mexcMessage"></param>
        /// <param name="Severity"></param>
        public override void RecordMessage(Exception mexcMessage, Log.MessageType Severity)
        {
            this._FileLocation = "C:\\RK\\Labs\\TW\\Assignment\\ConferenceManager\\ConferenceManager\\LogFile.txt";
            this._StackTrace = mexcMessage.StackTrace;
            this._StackTrace = mexcMessage.Message + this._StackTrace;
            this.RecordMessage(mexcMessage.Message, Severity);
        }

        /// <summary>
        /// RecordMessage acception string
        /// </summary>
        /// <param name="mstrMessage"></param>
        /// <param name="Severity"></param>
        public override void RecordMessage(string mstrMessage, Log.MessageType Severity)
        {
            FileStream fileStream = null;
            StreamWriter writer = null;
            this._FileLocation = "LogFile.txt";
            StringBuilder message = new StringBuilder();
            try
            {
                fileStream = new FileStream(this._FileLocation, FileMode.OpenOrCreate, FileAccess.Write);
                writer = new StreamWriter(fileStream);

                // Set the file pointer to the end of the file
                writer.BaseStream.Seek(0, SeekOrigin.End);

                // Create the message               
                message.Append(Environment.NewLine);
                message.Append(Environment.NewLine);
                message.Append("*******************Error Details***********************");                
                message.Append(Environment.NewLine);
                message.Append("Details : " + mstrMessage);
                message.Append(Environment.NewLine);
                message.Append("Severity : " + Severity);
                message.Append(Environment.NewLine);
                message.Append("Assembly Version : " + System.Reflection.Assembly.GetAssembly(this.GetType()).GetName().Version.ToString());
                message.Append(Environment.NewLine);
                message.Append("TimeStamp : " + DateTime.Now.ToString());
                message.Append(Environment.NewLine);
                message.Append("StackTrace : " + _StackTrace);
                message.Append(Environment.NewLine);
                message.Append("********************************************************");
                // Force the write to the underlying file
                writer.WriteLine(message.ToString());
                writer.Flush();


            }
            finally
            {
                if (writer != null) writer.Close();
            }

        }

       
    }
}
