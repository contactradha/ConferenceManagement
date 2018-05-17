using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Logger to log the content 
/// </summary>

namespace ConferenceManager.CrossCutting
{
    public sealed class Logger
    {
        /// <value>Available log types.</value>
        public enum LogTypes
        {
            /// <value>Log to the event log.</value>
            Event = 0,
            /// <value>Log to a file location.</value>
            File = 1
        }

        // Internal logging object
        private static Log ObjLogger;

        // Internal log type
        private static LogTypes _LogType = LogTypes.File;
        /// <value></value>
        public static LogTypes LogType
        {
            get { return _LogType; }
            set
            {
               // ObjLogger = new FileLog();
                // Set the Logger to the appropriate log when type changes.
                switch (value)
                {
                    case LogTypes.File:
                        ObjLogger = new FileLog();
                        break;
                    default:
                        ObjLogger = new FileLog();
                        break;
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Logger()
        {
           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mexcMessage"></param>
        /// <param name="Severity"></param>
        /// <param name="LogTo"></param>
        public static void RecordMessage(Exception mexcMessage, Log.MessageType Severity, LogTypes LogTo)
        {
            LogType = LogTo;
            ObjLogger.RecordMessage(mexcMessage, Severity);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mstrMessage"></param>
        /// <param name="Severity"></param>
        /// <param name="LogTo"></param>
        public static void RecordMessage(string mstrMessage, Log.MessageType Severity, LogTypes LogTo)
        {
            LogType = LogTo;
            ObjLogger.RecordMessage(mstrMessage, Severity);
        }
    }
}

