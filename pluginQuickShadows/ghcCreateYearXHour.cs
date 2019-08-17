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
    public class ghcCreateYearXHour : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public ghcCreateYearXHour()
          : base("Create Year By Hour", 
                 "YearXHour",
                 "Organizes a Sun Position output CSV from SUSDesign by year",
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
            pManager.AddNumberParameter("Time of Day in Hours", "Hour", 
                "Time of Day on a given Date", GH_ParamAccess.list);
            pManager.AddNumberParameter("Altitude Angle", "Altitute", 
                "Altitude of the sun at a given Hour on a given Date", GH_ParamAccess.list);
            pManager.AddNumberParameter("Azimuth Angle", "Azumith", 
                "Azumith angle of the sun at a given Hour on a given Date", GH_ParamAccess.list);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Year by Hour", "Year", "Year Class Object by Hour", GH_ParamAccess.item);
            pManager.AddVectorParameter("Vectors", "Vectors", 
                "Vector Tree by sorted by Hour and Month in the Year", GH_ParamAccess.tree);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            /// construct a line which will be rotated and become the basis for our sun vectors
            Point3d myStartPoint = new Point3d(0.0, 0.0, 0.0);
            Point3d myEndPoint = new Point3d(0.0, 10.0, 0.0);
            LineCurve myInitialCurve = new LineCurve();
            myInitialCurve.SetStartPoint(myStartPoint);
            myInitialCurve.SetEndPoint(myEndPoint);



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
