using System.Collections.Generic;
using ConferenceManager.CrossCutting;
using ConferenceManager.View;
using ConferenceManager.Event;

namespace ConferenceManager
{
    /// <summary>
    /// This Program is used to schedule the tracks and diplay output to window. 
    /// </summary>
    public class ConferenceScheduler
    {

        /// <summary>
        /// Parse the input
        /// </summary>
        /// <param name="RawSlots"></param>
        public void ParseInput(IEnumerable<Slot> RawSlots)
        {            
            Logger.RecordMessage("Entered ConferenceScheduler.ParseInput", Log.MessageType.Information, Logger.LogTypes.File);
            if (RawSlots == null)
            {
                Logger.RecordMessage("There is an error with input", Log.MessageType.Information, Logger.LogTypes.File);
                return;
            }
            SlotManager sm = new SlotManager();
            //Create list of different Slots for the conferece
            List<Conference> lstConference = sm.Create(RawSlots);
            Output op = new Output(new ConsoleDisplay())
            {
                Data = lstConference
            };
            op.ProcessOutput();
            Logger.RecordMessage("Exited ConferenceScheduler.ParseInput", Log.MessageType.Information, Logger.LogTypes.File);
        }

        
    }
}
