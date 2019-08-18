using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;
using Grasshopper.Kernel;
using Rhino;

namespace pluginQuickShadows
{
    class Hour
    {
        /* probably go through and set these as properties at some point */
        public readonly double clockTime;
        public readonly double altitudeAngle;
        public readonly double azumithAngle;

        public readonly Vector3d sunVector;

        public Hour(double newClockTime, double newAltitudeAngle, double newAzumithAngle)
        {
            clockTime = newClockTime;
            altitudeAngle = RhinoMath.ToRadians(newAltitudeAngle);
            azumithAngle = RhinoMath.ToRadians(newAzumithAngle);

            sunVector = CreateSunVector();
        }

        private Vector3d CreateSunVector()
        {
            /* set up a horizontal curve in the y direction that can be rotated */
            Point3d myStartPoint = new Point3d(0.0, 0.0, 0.0);
            Point3d myEndPoint = new Point3d(0.0, -100.0, 0.0);
            LineCurve myCurve = new LineCurve();

            myCurve.SetStartPoint(myStartPoint);
            myCurve.SetEndPoint(myEndPoint);

            /* set up axis of rotation for both altitude and azumith angles */
            Vector3d altitudeAxis = new Vector3d(1.0, 0.0, 0.0);
            Vector3d azumithAxis = new Vector3d(0.0, 0.0, 1.0);

            /* rotate by curve */
            myCurve.Rotate(altitudeAngle, altitudeAxis, myStartPoint);
            myCurve.Rotate(azumithAngle, azumithAxis, myStartPoint);

            /* set up the line from the start and end points of the curve so that the direction vector can be derived */
            Line vectorLine = new Line(myCurve.PointAtStart, myCurve.PointAtEnd);
            Vector3d mySunVector = new Vector3d(vectorLine.Direction);
                      
            return mySunVector;
        }

        public string ReturnRegularTime()
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
