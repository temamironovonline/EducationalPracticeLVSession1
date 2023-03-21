using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SF2022User_NN_Lib;
using SF2022User_NN_Lib.dll;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void time_is_notNull()
        {
            TimeSpan[] startTimes = new TimeSpan[] { new TimeSpan(10,0,0),
            new TimeSpan(11,0,0),
            new TimeSpan(15,0,0),
            new TimeSpan(15,30,0),
            new TimeSpan(16,50,0)};
            int[] durations = new int[] { 60, 30, 10, 10, 40 };
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(18, 0, 0);
            int consultationTime = 30;
            string[] s = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);

            Assert.IsNotNull(s);
        }

        [TestMethod]
        public void EasyCalculateLowDuration()
        {
            TimeSpan[] startTimes = new TimeSpan[] { new TimeSpan(10,0,0),
            new TimeSpan(11,0,0),
            new TimeSpan(15,0,0),
            new TimeSpan(15,30,0),
            new TimeSpan(16,50,0)};
            int[] durations = new int[] { 60, 30 };
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(18, 0, 0);
            int consultationTime = 30;
            string[] s = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);

            Assert.IsNotNull(s);
        }

        [TestMethod]
        public void HardCalculateMoreduration()
        {
            TimeSpan[] startTimes = new TimeSpan[] { new TimeSpan(10,0,0),
            new TimeSpan(11,0,0),
            new TimeSpan(15,10,0),
            new TimeSpan(15,30,0),
            new TimeSpan(17,30,0)};
            int[] durations = new int[] { 60, 15, 10, 15, 40 };
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(18, 0, 0);
            int consultationTime = 20;
            string[] s = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);

            Assert.IsTrue(s.Length == 22);
        }

        [TestMethod]
        public void TestMethod5()
        {
            TimeSpan[] startTimes = new TimeSpan[] { new TimeSpan(6,0,0),
            new TimeSpan(11,0,0),
            new TimeSpan(15,10,0),
            new TimeSpan(15,30,0),
            new TimeSpan(18,30,0)};
            int[] durations = new int[] { 60, 15, 10, 15, 40 };
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(18, 0, 0);
            int consultationTime = 20;
            string[] s = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);

            Assert.IsTrue(s.Length == 26);
        }

        [TestMethod]
        public void TestMethod6()
        {
            TimeSpan[] startTimes = new TimeSpan[] { new TimeSpan(6,0,0),
            new TimeSpan(11,0,0),
            new TimeSpan(18,30,0)};
            int[] durations = new int[] { 60, 15, 10, 15, 40 };
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(18, 0, 0);
            int consultationTime = 20;
            string[] s = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);

            Assert.IsInstanceOfType(s, typeof(string[]));
        }


        [TestMethod]
        public void TestMethod7()
        {
            TimeSpan[] startTimes = new TimeSpan[] { new TimeSpan(10,0,0),
            new TimeSpan(11,0,0),
            new TimeSpan(15,0,0),
            new TimeSpan(15,30,0),
            new TimeSpan(16,50,0)};
            int[] durations = new int[] { 60, 30, 10, 10, 40 };
            TimeSpan beginWorkingTime = new TimeSpan(18, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(8, 0, 0);
            int consultationTime = 30;
            string[] s = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);

            Assert.IsNull(s);
        }
        [TestMethod]
        public void TestMethod1()
        {
            TimeSpan[] startTimes = new TimeSpan[] { new TimeSpan(10,0,0),
                new TimeSpan(11,0,0),
                new TimeSpan(15,0,0),
                new TimeSpan(15,30,0),
                new TimeSpan(16,50,0)};
            int[] durations = new int[] { 60, 30, 10, 10, 40 };
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(18, 0, 0);
            int consultationTime = 30;
            string[] s = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);
            string[] ss = new string[]
            {
                "08:00-08:30",
                "08:30-09:00",
                "09:00-09:30",
                "09:30-10:00",
                "11:30-12:00",
                "12:00-12:30",
                "12:30-13:00",
                "13:00-13:30",
                "13:30-14:00",
                "14:00-14:30",
                "14:30-15:00",
                "15:40-16:10",
                "16:10-16:40",
                "17:30-18:00"
            };
            Assert.AreEqual(s.Length, ss.Length);
        }
        [TestMethod]
        public void TestMethod8()
        {
            TimeSpan[] startTimes = new TimeSpan[] { new TimeSpan(10,0,0),
            new TimeSpan(15,30,0),
            new TimeSpan(17,40,0)};
            int[] durations = new int[] { 60, 30, 10, 10, 40 };
            TimeSpan beginWorkingTime = new TimeSpan(10, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(19, 0, 0);
            int consultationTime = 20;
            string[] s = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);

            Assert.IsNotNull(s);
        }

        [TestMethod]
        public void TestMethod9()
        {
            TimeSpan[] startTimes = new TimeSpan[] { new TimeSpan(10,0,0),
            new TimeSpan(15,30,0),
            new TimeSpan(24,40,0)};
            int[] durations = new int[] { 60, 30, 10, 10, 40 };
            TimeSpan beginWorkingTime = new TimeSpan(24, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(26, 0, 0);
            int consultationTime = 20;
            string[] s = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);

            Assert.IsNotNull(s);
        }

        [TestMethod]
        public void TestMethod10()
        {
            TimeSpan[] startTimes = new TimeSpan[] { new TimeSpan(10,0,0),
            new TimeSpan(11,39,0),
            new TimeSpan(18,40,0)};
            int[] durations = new int[] { 11, 16, 7, 10, 40 };
            TimeSpan beginWorkingTime = new TimeSpan(10, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(20, 0, 0);
            int consultationTime = 14;
            string[] s = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);

            Assert.IsNotNull(s);
        }
    }
}
   
