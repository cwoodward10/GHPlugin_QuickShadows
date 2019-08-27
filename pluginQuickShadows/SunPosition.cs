using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;

namespace pluginQuickShadows
{
    class SunPosition
    {
        private int hour;
        private int month = 0;
        private int dayInYear = 0;
        private int dayInMonth = 0;
        private double azumithAngle;
        private double altitudeAngle;

        public int Hour { get => hour; set => hour = value; }
        public int DayInYear { get => dayInYear; set => dayInYear = value; }
        public int Month { get => month; set => month = value; }
        public int DayInMonth { get => dayInMonth; set => dayInMonth = value; }
        public double AzumithAngle { get => azumithAngle; set => azumithAngle = value; }
        public double AltitudeAngle { get => altitudeAngle; set => altitudeAngle = value; }

        public readonly Vector3d SunVector;
        public bool IsLeapYear = false;

        public SunPosition(int newHour, double newAltitudeAngle, double newAzumithAngle)
        {
            hour = newHour;
            altitudeAngle = newAltitudeAngle;
            azumithAngle = newAzumithAngle;

            SunVector = CreateSunVector();
        }

        public SunPosition(int newHour, int newDayInYear, double newAltitudeAngle, double newAzumithAngle)
        {
            hour = newHour;
            DayInYear = newDayInYear;
            altitudeAngle = newAltitudeAngle;
            azumithAngle = newAzumithAngle;

            SunVector = CreateSunVector();
            ConvertDayToMonth();
        }

        public SunPosition(int newHour, int newDayInYear, double newAltitudeAngle, double newAzumithAngle, bool isThisLeapYear)
        {
            hour = newHour;
            DayInYear = newDayInYear;
            altitudeAngle = newAltitudeAngle;
            azumithAngle = newAzumithAngle;
            IsLeapYear = isThisLeapYear;

            SunVector = CreateSunVector();
            ConvertDayToMonth();
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

        private void ConvertDayToMonth()
        {
            List<int> daysInMonthCount = new List<int> { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            if (IsLeapYear) daysInMonthCount[1] = 29;

            for (int m = 0; m < daysInMonthCount.Count; m++)
            {
                if (dayInYear <= daysInMonthCount.Take(m + 1).Sum())
                {
                    month = m + 1;
                    if (m == 0) dayInMonth = dayInYear;
                    else dayInMonth = dayInYear - daysInMonthCount.Take(m).Sum();
                }

                else continue;
            }
        }
    }
}
