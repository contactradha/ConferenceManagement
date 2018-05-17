using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceManager.CrossCutting
{
    public abstract class Log
    {
        /// <value>Few message severities</value>
        public enum MessageType
        {
            /// <value>Fatal Error message</value>
            FatalException = 1,
            /// <value>Error audit message</value>
            Exception = 2,
            /// <value>Warning message</value>
            Warning = 3,
            /// <value>Information message</value>
            Information = 4
        }
        public abstract void RecordMessage(Exception mexcMessage, MessageType Severity);
        public abstract void RecordMessage(string mstrMessage, MessageType Severity);      
    }
}
