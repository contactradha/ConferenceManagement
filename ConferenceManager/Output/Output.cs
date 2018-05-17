using System;
using System.Collections.Generic;
using System.Text;
using ConferenceManager.Event;

namespace ConferenceManager.View
{
    /// <summary>
    /// The concrete class used to initiate different output types
    /// </summary>
    public class Output
    {
        private IOutputType OutputType { get; set; }
        public List<Conference> Data { get; set; }
        public Output(IOutputType type)
        {
            this.OutputType = type;
        }

        /// <summary>
        /// Method used to process data on different outputs
        /// </summary>
        public void ProcessOutput()
        {
            this.OutputType.DiplayData(this);
        }
    }
}
