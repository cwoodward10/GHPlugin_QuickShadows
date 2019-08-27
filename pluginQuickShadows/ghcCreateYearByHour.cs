using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

// In order to load the result of this wizard, you will also need to
// add the output bin/ folder of this project to the list of loaded
// folder in Grasshopper.
// You can use the _GrasshopperDeveloperSettings Rhino command for that.

namespace pluginQuickShadows
{
    public class ghcCreateYearByHour : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public ghcCreateYearByHour()
          : base("Create Year By Hour", 
                 "YrXHr",
                 "Parses a Sun Position output CSV from SUSDesign into a Year Object and a list of Vectors",
                 "Quick Shadow", 
                 "Utility")
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
            pManager.AddGenericParameter("Year by Hour", "YearXHour", "Year Class Object by Hour", GH_ParamAccess.item);
            pManager.AddVectorParameter("Sun Position Vectors", "Vectors", 
                "List of Sun Position Vectors", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            /* set up component input variables */
            List<double> iClockTimes = new List<double>();
            List<double> iAltitudeAngles = new List<double>();
            List<double> iAzumithAngles = new List<double>();

            DA.GetDataList(1, iClockTimes);
            DA.GetDataList(2, iAltitudeAngles);
            DA.GetDataList(3, iAzumithAngles);


            /* check to make sure hours and angles are of equal lengths */
            if (iClockTimes.Count != iAltitudeAngles.Count || iAltitudeAngles.Count != iAzumithAngles.Count)
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error,
                    "The number of Altitude Angles, Azumith Angles, Hours is not the same.");

            /* check to make sure that there are the correct number of hours in the year */
            if (iClockTimes.Count != 8760 && iClockTimes.Count != 8761)
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error,
                    "Incorrect number of hours in this year");

            /* initiate the new year by hour object with the current sun position data */
            YearByHour currentYear = new YearByHour(iClockTimes, iAltitudeAngles, iAzumithAngles);
            DA.SetData("Year by Hour", currentYear);
            DA.SetDataList("Sun Position Vectors", currentYear.GetSunVectors());
        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("a6577792-5263-4a21-98bb-dd22b3ebb3ee"); }
        }
    }
}
