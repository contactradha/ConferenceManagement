using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceManager.Event
{
    /// <summary>
    /// This class is used to set the Lunch Slot
    /// </summary>
    public class LunchSlot : ISlotType
    {
        Slot lunchSlot;
        public Slot SetSlot(Slot slot)
        {
            this.lunchSlot = slot;
            this.lunchSlot.Description = "Lunch";
            return this.lunchSlot;
        }
    }
}
