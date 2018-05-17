using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConferenceManager.Event;

namespace ConferenceManager.View
{
    /// <summary>
    /// This class is derived from IOutputDisplay
    /// THis class is used to diplay output on a console
    /// </summary>
    public class ConsoleDisplay :IOutputType
    {
        public void DiplayData(string Data)
        {
            Console.WriteLine(Data);            
        }

        public void DiplayData(Output output)
        {
            List<Conference> ConferenceDetails = output.Data;
            for (int i = 0; i < ConferenceDetails.Count; i++)
            {
                Console.WriteLine("Track " + (i + 1) + ":");
                
                var list = ConferenceDetails[i].SlotList.OrderBy(x => x.StartTime);
                foreach (Slot s in list)
                {
                    Console.WriteLine(s.StartTime.ToString("t").PadLeft(8, '0').PadRight(10, ' ') + s.Description);
                    
                }
                Console.WriteLine("\n");
                
            }
        }
    }
}
