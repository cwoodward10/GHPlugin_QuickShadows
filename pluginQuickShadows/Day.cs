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

        public Day(List<Hour> newHours, int dayNumber)
        {
            /* ensure there are the correct number of hours in this day */
            if (newHours.Count != 24)
                throw new Exception("Incorrect number of hours in this Day");

            hoursOfDay = newHours;
        }

        public List<Vector3d> GetSunVectors()
        {
            List<Vector3d> sunVectorList = new List<Vector3d>();

            foreach (Hour h in hoursOfDay)
                sunVectorList.Add(h.GetSunVector());

            return sunVectorList;
        }
    }
}
