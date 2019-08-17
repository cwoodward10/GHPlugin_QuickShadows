using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;
using Grasshopper.Kernel;

namespace pluginQuickShadows
{
    class Hour
    {
        /* probably go through and set these as properties at some point */
        public readonly double clockTime;
        public readonly double altitudeAngle;
        public readonly double azumithAngle;

        public Hour(double newClockTime, double newAltitudeAngle, double newAzumithAngle)
        {
            clockTime = newClockTime;
            altitudeAngle = newAltitudeAngle;
            azumithAngle = newAzumithAngle;
        }

        public string returnRegularTime()
        {
            string amPM;
            string regularTime = clockTime.ToString();

            if (clockTime < 12)
                amPM = "am";
            else amPM = "pm";

            regularTime += amPM;
            return regularTime;
        }
    }
}
