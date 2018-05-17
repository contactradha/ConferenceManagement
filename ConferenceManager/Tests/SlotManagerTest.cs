using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ConferenceManager.Event;
namespace ConferenceManager.Tests
{
    [TestFixture]
   public class SlotManagerTest
    {
        [Test]
        public void SetRegularEvent_Input_ToFail_Test()
        {
            DateTime currenstSlotEndTime = new DateTime(0001, 01, 01, 10, 00, 00); //CurrentSlot End Time
            DateTime slotStartTime = new DateTime(0001, 01, 01, 10, 00, 00); //CurrentSlot End Time
            

            Slot regSlot =     new Slot(new RegularSlot()
                                            {
                                                CurrentSlotEndTime = currenstSlotEndTime,
                                                SlotStartTime = slotStartTime
                                             }
                                                        
                                       )
            { };

            RegularSlot rs = new RegularSlot()
            {
                CurrentSlotEndTime = currenstSlotEndTime,
                SlotStartTime = slotStartTime
            };

            Assert.AreEqual(regSlot, rs);
            
        }

        [Test]
        public void SetRegularEvent_CheckSlotStartTime_Test()
        {
            DateTime currenstSlotEndTime = new DateTime(0001, 01, 01, 10, 00, 00); //CurrentSlot End Time
            DateTime slotStartTime = new DateTime(0001, 01, 01, 10, 00, 00); //CurrentSlot End Time
            TimeSpan slotDuration = new TimeSpan(0, 45, 0);

            Slot regSlot = new Slot(new RegularSlot()
                                    {
                                        CurrentSlotEndTime = currenstSlotEndTime,
                                        SlotStartTime = slotStartTime
                                     }

                                    )
            {};

            Slot expected = regSlot.SetSlot();

             Assert.AreEqual(expected.StartTime, new DateTime(0001, 01, 01, 10, 00, 00));

        }

        [Test]
        public void NetworkEventStartHour_4PMHourOutput_Test()
        {
            Slot prevSlot = new Slot()
            {
                StartTime = new DateTime(0001, 01, 01, 14, 30, 00),
                Duration = 30
            };
            //if current slot ends before 15:59, then Network is 4pm otherwise 5pm
            int StartHr = ((prevSlot.StartTime.AddMinutes(prevSlot.Duration).Hour <= 15) &&
                           (prevSlot.StartTime.AddMinutes(prevSlot.Duration).Minute <= 59)
                          ) ? 16 : 17;

            Assert.AreEqual(StartHr, 16);
        }

        [TestCase(14, 30, 30, ExpectedResult = 16)]
        [TestCase(15, 30, 29, ExpectedResult = 16)]
        [TestCase(15, 30, 40, ExpectedResult = 17)]
        [TestCase(15, 30, 35, ExpectedResult = 16)]  //To BE Failed
        [TestCase(16, 00, 29, ExpectedResult = 16)]  //To BE Failed
        [TestCase(16, 00, 29, ExpectedResult = 17)]
        public int NetworkEventStartHour_HourOutput_Test(int Hr, int Min,int Duration)
        {
            DateTime PrevItem = new DateTime(0001,01,01,Hr,Min,00);
            
            int StartHr = ((PrevItem.AddMinutes(Duration).Hour <= 15) &&
                           (PrevItem.AddMinutes(Duration).Minute <= 59)
                          ) ? 16 : 17;

            return StartHr;
        }

    }
}
