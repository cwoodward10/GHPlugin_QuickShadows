using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;

namespace pluginQuickShadows
{
    class Month
    {
        public readonly List<Hour> hoursOfMonth = new List<Hour>();
        public readonly List<Day> daysOfMonth = new List<Day>();
        public readonly int monthNumber;

        public Month(List<Day> newDays, int newMonthNumber)
        {
            daysOfMonth = newDays;
            monthNumber = newMonthNumber;
        }

        private void GetHoursOfMonth()
        {
            foreach (Day d in daysOfMonth)
            {
                foreach (Hour h in d.hoursOfDay)
                    hoursOfMonth.Add(h);
            }
        }

        public List<List<Vector3d>> GetSunVectorsByDay()
        {
            List<List<Vector3d>> sunVectors = new List<List<Vector3d>>();

            foreach (Day d in daysOfMonth)
                sunVectors.Add(d.GetSunVectors());

            return sunVectors;
        }

        public List<Vector3d> GetSunVectorsByHour()
        {
            List<Vector3d> sunVectors = new List<Vector3d>();

            foreach (Hour h in hoursOfMonth)
                sunVectors.Add(h.sunVector);

            return sunVectors;
        }
    }
}
