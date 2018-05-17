using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ConferenceManager.Event;
namespace ConferenceManager.Tests.SlotTests
{
    [TestFixture]
    public class NetworkSlotTest
    {
        Slot ns;
        [Test]
        public void TestSetSlot_ToCheck_4pm()
        {
            ns = new Slot(new NetworkSlot())
            {
                Description = "Network",
                StartTime = new DateTime(0001, 01, 01, 16, 00, 00)
            };
            Assert.AreEqual(ns.StartTime.Hour, 16);
        }

        [Test]
        public void TestSetSlot_ToCheck_5pm()
        {
            ns = new Slot(new NetworkSlot())
            {
                Description = "Network",
                StartTime = new DateTime(0001, 01, 01, 17, 00, 00)
            };
            Assert.AreEqual(ns.StartTime.Hour, 17);
        }

    }
}
