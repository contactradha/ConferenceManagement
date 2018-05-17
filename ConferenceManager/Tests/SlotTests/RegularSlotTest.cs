using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace ConferenceManager.Tests.SlotTests
{
    [TestFixture]
    public class RegularSlotTest
    {

        [TestCase(09, 00, 45, ExpectedResult = "09:00")]
        [TestCase(10, 15, 75, ExpectedResult = "10:15")]
        [TestCase(11, 30, 40, ExpectedResult = "12:10")]  //To Be Failed
        [TestCase(10, 50, 70, ExpectedResult = "12:00")]  //To Be Failed
        [TestCase(11, 30, 40, ExpectedResult = "Afternoon")]
        public string MorningSession_Test (int Hr,int Min, int Duration)
        {
            DateTime sTime = new DateTime();
            DateTime SlotStartTime = new DateTime(0001, 01, 01, Hr, Min, 00);
            DateTime CurrentSlotEndTime = SlotStartTime.AddMinutes(Duration);
            if (SlotStartTime.Hour >= 9 && CurrentSlotEndTime.Hour <= 12 && (CurrentSlotEndTime.Hour == 12 ? (CurrentSlotEndTime.Minute == 0 ? true : false) : true))
            {
                sTime = SlotStartTime;
                return sTime.ToString("HH:mm");
            }
            else
            {
                return "Afternoon Session";
            }            
        }

        [TestCase(13, 00, 45, ExpectedResult = "13:45")]
        [TestCase(15, 15, 45, ExpectedResult = "16:00")]
        [TestCase(12, 15, 15, ExpectedResult = "13:15")]
        [TestCase(12, 15, 45, ExpectedResult = "12:00")] //To be Failed
        public string AfternoonSession_Test(int Hr, int Min, int Duration)
        {
            DateTime sTime = new DateTime();
            DateTime SlotStartTime = new DateTime(0001, 01, 01, Hr, Min, 00);
            DateTime CurrentSlotEndTime = SlotStartTime.AddMinutes(Duration);

            if (CurrentSlotEndTime.Hour < 13)   //and great than 12 for post lunch
            {
                TimeSpan SlotDuration = new TimeSpan(13, 00, 0);
                SlotStartTime = SlotStartTime.Date + SlotDuration;               
                CurrentSlotEndTime = SlotStartTime.AddMinutes(Duration);
            }

            if (SlotStartTime.Hour >= 13 && CurrentSlotEndTime.Hour <= 17 &&
                    (CurrentSlotEndTime.Hour == 17 ? (CurrentSlotEndTime.Minute == 0 ? true : false) : true)
                    )
            {
                sTime = CurrentSlotEndTime;
            }
            return sTime.ToString("HH:mm");

        }
    }
}
