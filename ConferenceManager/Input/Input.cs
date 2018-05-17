using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceManager.Input
{
    /// <summary>
    /// Concrete class tobe used to call different input types
    /// </summary>
    public class Input
    {
        private IInputType InputType { get; set; }

        public string FilePath { get; set; }

        public Input (IInputType type)
        {
            this.InputType = type;
        }
        /// <summary>
        /// THis method is used to process different inputs
        /// </summary>
        public void ProcessInput()
        {

            ConferenceScheduler cs = new ConferenceScheduler();
            cs.ParseInput(InputType.Parse(this));
           
        }
    }
}
