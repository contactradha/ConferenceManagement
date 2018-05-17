using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceManager.Event
{
    /// <summary>
    /// Abstract class to be implemented by different slot types
    /// </summary>
    public interface ISlotType
    {
        Slot SetSlot(Slot slot);
    }
}
