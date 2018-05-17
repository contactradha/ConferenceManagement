using System;
using ConferenceManager.CrossCutting;

namespace ConferenceManager.Event
{
    /// <summary>
    /// This class is used to set regular event
    /// </summary>
    public class RegularSlot : ISlotType
    {
        Slot regularSlot;
        public DateTime CurrentSlotEndTime{ get; set; }
        public DateTime SlotStartTime { get; set; }
       
        /// <summary>
        /// Set Regular Event
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
        public Slot SetSlot(Slot slot)
        {
            Logger.RecordMessage("Entered Regular.SetSlot", Log.MessageType.Information, Logger.LogTypes.File);
            this.regularSlot = slot;
            DateTime sTime = this.SlotStartTime;
            this.CurrentSlotEndTime = this.SlotStartTime.AddMinutes(this.regularSlot.Duration);
            //Check the morning session time between 9am to 12pm
            if (this.SlotStartTime.Hour >= 9 && this.CurrentSlotEndTime.Hour <= 12 && (this.CurrentSlotEndTime.Hour == 12 ? (this.CurrentSlotEndTime.Minute == 0 ? true : false) : true))
            {
                sTime = this.SlotStartTime;
            }
            else
            {
                //check whether slot is not going beyond 12pm
                SetPostLunchSession();
                
                // Postlunch sessions 1:00 PM to 5:00 PM 
                if (this.SlotStartTime.Hour >= 13 && this.CurrentSlotEndTime.Hour <= 17 && 
                    (this.CurrentSlotEndTime.Hour == 17 ? (this.CurrentSlotEndTime.Minute == 0 ? true : false) : true)
                    )
                {
                    sTime = this.SlotStartTime;
                }
            }
            this.regularSlot.StartTime = sTime;

            Logger.RecordMessage("Exit RegularSlot.SetSlot", Log.MessageType.Information, Logger.LogTypes.File);
            return this.regularSlot;
        }

        /// <summary>
        /// This method used to identify lunch session
        /// </summary>
        void SetPostLunchSession()
        {
            Logger.RecordMessage("Entered RegularSlot.SetPostLunchSession", Log.MessageType.Information, Logger.LogTypes.File);
            //check whether slot is  going beyond 12pm
            if (this.CurrentSlotEndTime.Hour < 13)   //and greater than 12
            {
                // Set Time = 1:00 PM (13 hour in 24 watch system)
                TimeSpan SlotDuration = new TimeSpan(13, 00, 0);
                this.SlotStartTime = this.SlotStartTime.Date + SlotDuration; //Reset time to 1pm
                this.CurrentSlotEndTime = this.SlotStartTime.AddMinutes(this.regularSlot.Duration);
            }
            Logger.RecordMessage("Exit RegularSlot.SetPostLunchSession", Log.MessageType.Information, Logger.LogTypes.File);
        }
    }
}
