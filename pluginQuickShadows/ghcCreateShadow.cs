using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace pluginQuickShadows
{
    public class ghcCreateShadow : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the ghcCreateShadow class.
        /// </summary>
        public ghcCreateShadow()
          : base("Create Shadow", "Create Shadow",
              "Creates a Shadow Object",
              "Quick Shadow", "Shadow")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Sun Position", "Sun Position", "Sun Position", GH_ParamAccess.list);
            pManager.AddMeshParameter("Origin Object", "Origin Object", "Object from which to derive the shadow", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Shadow", "Shadow", "Shadow Object with meta data about the shadow curve", GH_ParamAccess.list);
            pManager.AddGenericParameter("Shadow Curve", "Shadow", "Curve that represents the shadow for particular sun vector", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<SunPosition> iSunPositions = new List<SunPosition>();
            Mesh iOriginObject = new Mesh();

            DA.GetDataList(0, iSunPositions);
            DA.GetData(1, ref iOriginObject);

            List<Shadow> myShadows = new List<Shadow>();
            List<Object> shadowCurves = new List<Object>();

            foreach (SunPosition p in iSunPositions)
                myShadows.Add(new Shadow(p, iOriginObject));

            foreach (Shadow s in myShadows)
                shadowCurves.Add(s.ShadowCurve);

            DA.SetDataList(0, myShadows);
            DA.SetDataList(1, shadowCurves);
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
            get { return new Guid("f2104bdc-eb73-423b-aeba-47bd7daf06dc"); }
        }
    }
}