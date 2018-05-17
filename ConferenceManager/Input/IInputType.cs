using System;
using System.Collections.Generic;
using System.Text;
using ConferenceManager.Event;

namespace ConferenceManager.Input
{

    /// <summary>
    /// abstract class to be implemented by different inputs
    /// </summary>
    public interface IInputType 
    {
        IEnumerable<Slot> Parse(Input input);
    }
}
