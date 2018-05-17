using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceManager.Event
{
    /// <summary>
    /// This is the Data class having minimum required attributes
    /// </summary>
    public class Slot
    {
        public int Duration { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }

        ISlotType SlotType { get; set; }

        public Slot (ISlotType type)
        {
            this.SlotType = type;
        }
        //This default constructor is enforced to handle File Input Scenario
        public Slot ()
        { }

        /// <summary>
        /// This method is used to set the slot start timings
        /// </summary>
        /// <returns></returns>
        public Slot SetSlot()
        {
            return this.SlotType.SetSlot(this);
        }
    }

}
