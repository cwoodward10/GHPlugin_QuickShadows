using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace pluginQuickShadows
{
    public class ghcSunPositionByYear : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the ghcSunPositionByYear class.
        /// </summary>
        public ghcSunPositionByYear()
          : base("Sun Position By Year", "SunXYear",
              "Takes a CSV of Sun Position information and returns a list of Sun Positions",
              "Quick Shadow", "Sun Position")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("Day of the Year", "Date",
                "a number that represents the current Date", GH_ParamAccess.list);
            pManager.AddNumberParameter("Time of Day", "Hour",
                "Time of Day on a given Date", GH_ParamAccess.list);
            pManager.AddNumberParameter("Altitude Angles", "Altitute",
                "Altitude of the sun at a given Hour on a given Date", GH_ParamAccess.list);
            pManager.AddNumberParameter("Azumith Angles", "Azumith",
                "Azumith angle of the sun at a given Hour on a given Date", GH_ParamAccess.list);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Sun Positions", "Positions", "Sun Positions", GH_ParamAccess.list);
            pManager.AddVectorParameter("Sun Position Vectors", "Vectors",
                "List of Sun Position Vectors", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            /* set up component input variables */
            List<double> iClockTimes = new List<double>();
            List<double> iAltitudeAngles = new List<double>();
            List<double> iAzumithAngles = new List<double>();

            DA.GetDataList(1, iClockTimes);
            DA.GetDataList(2, iAltitudeAngles);
            DA.GetDataList(3, iAzumithAngles);

            List<SunPosition> sunPositions = new List<SunPosition>();
            List<Vector3d> sunPositionVectors = new List<Vector3d>();

            /* check to make sure hours and angles are of equal lengths */
            if (iClockTimes.Count != iAltitudeAngles.Count || iAltitudeAngles.Count != iAzumithAngles.Count)
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error,
                    "The number of Altitude Angles, Azumith Angles, Hours is not the same.");

            /* check to make sure that there are the correct number of hours in the year */
            if (iClockTimes.Count != 8760 && iClockTimes.Count != 8761)
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error,
                    "Incorrect number of hours in this year");

            /* initiate the new SunPosition object with the current sun position data */
            for (int i = 0; i < iClockTimes.Count; i++)
                sunPositions.Add(new SunPosition(iClockTimes[i], iAltitudeAngles[i], iAzumithAngles[i]));

            foreach (SunPosition s in sunPositions)
                sunPositionVectors.Add(s.SunVector);

            DA.SetDataList("Sun Positions", sunPositions);
            DA.SetDataList("Sun Position Vectors", sunPositionVectors);
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("30e0b823-03ce-49eb-9820-fd5c33a131fb"); }
        }
    }
}