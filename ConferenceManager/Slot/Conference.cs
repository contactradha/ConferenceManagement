using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceManager.Event
{
    /// <summary>
    /// To Store different slots for the conference
    /// </summary>
   public class Conference
    {
        public List<Slot> SlotList { get; set; }
    }
}
