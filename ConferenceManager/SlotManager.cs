using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConferenceManager.CrossCutting;
using ConferenceManager.Event;

namespace ConferenceManager
{
    /// <summary>
    /// Class used to Manage Slot Details.
    /// </summary>
    public class SlotManager
    {
        //Daily start time at 9am
        DateTime DayStartTime
        {
            get { return new DateTime(0001, 01, 01, 09, 00, 00); }
        }

        //Used to store daily slots
        Conference SlotsPerDay { get; set; }

        //used to store previous item
        Slot PrevItem { get; set; }

        //to store and calculate start time and duaration of slot
        TimeSpan SlotDuration { get; set; }

        //used to store slot time
        DateTime CurrentSlotTime { get; set; }

        //current slot start time
        DateTime SlotStTime { get; set; }

        /// <summary>
        /// Create Conference Track
        /// </summary>
        /// <param name="SlotInfo"></param>
        /// <returns></returns>
        public List<Conference> Create(IEnumerable<Slot> SlotInfo)
        {
            Logger.RecordMessage("Entered SlotManager.Create", Log.MessageType.Information, Logger.LogTypes.File);
            List<Conference> lstConference = new List<Conference>();
            this.SlotDuration = new TimeSpan();
            this.SlotsPerDay = new Conference { SlotList = new List<Slot>() };
            this.CurrentSlotTime = new DateTime();            
            this.SlotStTime = this.DayStartTime;

            SetLunchEvent(12); //Lunch Starts at 12pm

            foreach(Slot s in SlotInfo.Where(slot => slot.Description.Trim().Length > 0))
            {
                //Condition to check End Of Day
                if (this.SlotStTime.AddMinutes(s.Duration).Hour >= 17)
                {
                    int StartHr =  FindNetworkEventStartHour(this.PrevItem);
                    SetNetworkEvent(StartHr);
                    lstConference.Add(this.SlotsPerDay); //Add the current Day conference to List
                    FallOnNextDay();
                }
                SetRegularEvent(s);                                
            }            
            //Networking Event for Final Day           
            int startHr = this.CurrentSlotTime.Hour >= 16 ? 17 : PrevItem.Duration > 60 ? 17 : 16;
            SetNetworkEvent(startHr);

            //Add the final conference to the list
            lstConference.Add(this.SlotsPerDay);
            Logger.RecordMessage("Exit SlotManager.Create", Log.MessageType.Information, Logger.LogTypes.File);
            return lstConference;
        }

        /// <summary>
        /// Perform default events for next day
        /// </summary>
        void FallOnNextDay()
        {            
            this.SlotsPerDay = new Conference { SlotList = new List<Slot>() }; //Set New conference for next day
            this.SlotStTime = this.DayStartTime;
            SetLunchEvent(12); //Lunch Starts at 12pm next day
        }

        /// <summary>
        /// Method to set the start time for current slot
        /// </summary>
        /// <param name="CurrentSlot"></param>
        void SetRegularEvent(Slot CurrentSlot)
        {
            Logger.RecordMessage("Entered SlotManager.SetConferenceTrack: Track ime For: " + CurrentSlot.Description, Log.MessageType.Information, Logger.LogTypes.File);
           
            Slot RegSlot = new Slot(new RegularSlot()
                                         {
                                              CurrentSlotEndTime = this.CurrentSlotTime,                                             
                                              SlotStartTime = this.SlotStTime                                              
                                          }
                                      )
             {
                 Description = CurrentSlot.Description,
                 Duration = CurrentSlot.Duration
             };
            //add the slot to slot list
            this.SlotsPerDay.SlotList.Add(RegSlot.SetSlot());
            this.SlotStTime = RegSlot.StartTime.AddMinutes(RegSlot.Duration); //Next Slot Start Time
            this.CurrentSlotTime = this.SlotStTime.AddMinutes(CurrentSlot.Duration); //CurrentSlot End Time
            this.PrevItem = RegSlot; //Set this as Previous SLot

            Logger.RecordMessage("Exit SlotManager.SetConferenceTrack", Log.MessageType.Information, Logger.LogTypes.File);
        }
        
       /// <summary>
       /// Used to Find the Network starting Hour
       /// </summary>
       /// <param name="CurrSlot"></param>
       /// <param name="PrevSlot"></param>
        int FindNetworkEventStartHour(Slot PrevSlot)
        {
            //if current slot ends before 15:59, then Network is 4pm otherwise 5pm
            int StartHr = ((PrevSlot.StartTime.AddMinutes(PrevSlot.Duration).Hour <= 15) && 
                           (PrevSlot.StartTime.AddMinutes(PrevSlot.Duration).Minute <= 59)
                          ) ? 16 : 17;
            return StartHr;
        }

        /// <summary>
        /// Set Nework Event
        /// </summary>
        /// <param name="StartHour"></param>
        void SetNetworkEvent(int StartHour)
        {
            //set Morning Slot
            Slot SlotNetwork = new Slot(new NetworkSlot())
            {
                StartTime = new DateTime(0001, 1, 1, StartHour, 0, 0)
            };
            this.SlotsPerDay.SlotList.Add(SlotNetwork.SetSlot());
        }

        /// <summary>
        /// Set Lunch Event
        /// </summary>
        /// <param name="StartHour"></param>
        void SetLunchEvent(int StartHour)
        {
            //set Morning Slot
            Slot SlotLunch = new Slot(new LunchSlot())
            {
                StartTime = new DateTime(0001, 1, 1, StartHour, 0, 0)               
            };
            this.SlotsPerDay.SlotList.Add(SlotLunch.SetSlot());
        }
    }
}
