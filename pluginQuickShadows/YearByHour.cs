using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace pluginQuickShadows
{
    class YearByHour
    {
        public List<double> clockTimes;
        public List<double> altitudeAngles;
        public List<double> azumithAngles;

        List<Hour> hoursInYear = new List<Hour>();
        List<Day> daysInYear = new List<Day>();

        public YearByHour(List<double> newTimeList, List<double> newAltitudeList, List<double> newAzumithList)
        {
            /* create an Hour object for each hour in list */
            for (int t = 0; t < clockTimes.Count; t++)
                hoursInYear.Add((new Hour(clockTimes[t], altitudeAngles[t], azumithAngles[t])));

            /* create day objects from the hours in the year */
            createDaysFromHoursList();

            /* set the number of days in a month */
            List<int> dayInMonthCount = new List<int> { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            /* change February if need be */
            if (clockTimes.Count == 8761) dayInMonthCount[1] = 29;



        }

        private void createDaysFromHoursList()
        {
            
            /* create a Day objects for each 24 hour chunk in list */
            /* start with list IEnumerables in which to store the hours */
            List<IEnumerable<Hour>> listOfDays = new List<IEnumerable<Hour>>();
            /* iterate through the hours of the year in 24 hour chunks */
            for (int h = 0; h < hoursInYear.Count(); h += 24)
            {
                /* skip over i hours to then create a 24 chunk from the next 24 hours */
                listOfDays.Add(hoursInYear.Skip(h).Take(24));
            }

            /* send each 24 hour chunk of hours to a Day object along with it's day of year d*/
            for (int d = 0; d < listOfDays.Count; d++)
            {
                daysInYear.Add(new Day(listOfDays[d].ToList(), d));
            }
        }

        public List<Vector3d> GetSunVectors()
        {
            List<Vector3d> sunVectorList = new List<Vector3d>();

            foreach (Hour h in hoursInYear)
                sunVectorList.Add(h.GetSunVector());

            return sunVectorList;
        }
    }
}
