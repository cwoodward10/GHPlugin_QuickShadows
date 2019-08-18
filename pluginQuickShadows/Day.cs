using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace pluginQuickShadows
{
    class Day
    {
        public readonly List<Hour> hoursOfDay = new List<Hour>();
        public readonly int dayNumber;
        private int monthNumber;
        private int dayInMonth;

        private bool leapYear;

        public int DayInMonth { get => dayInMonth; set => dayInMonth = value; }
        public int MonthNumber { get => monthNumber; set => monthNumber = value; }

        public Day(List<Hour> newHours, int newDayNumber, bool isLeapYear)
        {
            /* ensure there are the correct number of hours in this day */
            if (newHours.Count != 24)
                throw new Exception("Incorrect number of hours in this Day");

            hoursOfDay = newHours;
            dayNumber = newDayNumber;
            leapYear = isLeapYear;
            GetMonthNumber();            
        }

        private void GetMonthNumber()
        {
            List<int> daysInMonthCount = new List<int> { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            if (leapYear) daysInMonthCount[1] = 29;
            
            for (int m = 0; m < daysInMonthCount.Count; m++)
            {
                if (dayNumber <= daysInMonthCount.Take(m + 1).Sum())
                {
                    monthNumber = m + 1;
                    dayInMonth = dayNumber - daysInMonthCount.Take(m).Sum();
                }
                    
                else continue;
            }

            
        }

        public List<Vector3d> GetSunVectors()
        {
            List<Vector3d> sunVectorList = new List<Vector3d>();

            foreach (Hour h in hoursOfDay)
                sunVectorList.Add(h.sunVector);

            return sunVectorList;
        }
    }
}
