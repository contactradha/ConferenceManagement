using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceManager.Event
{
    /// <summary>
    /// This class is used to store Network Slot
    /// </summary>
    public class NetworkSlot : ISlotType
    {
        Slot networkSlot;
        public Slot SetSlot(Slot slot)
        {
            this.networkSlot = slot;            
            this.networkSlot.Description = "Network";
            return this.networkSlot;
        }
    }

    
}
