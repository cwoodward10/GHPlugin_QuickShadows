using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;
using Rhino.NodeInCode;

namespace pluginQuickShadows
{
    class Shadow
    {
        private SunPosition currentSunPosition;
        private Mesh originObject;

        public SunPosition CurrentSunPosition { get => currentSunPosition; set => currentSunPosition = value; }
        public Mesh OriginObject { get => originObject; set => originObject = value; }
        public readonly Object ShadowCurve;

        private ComponentFunctionInfo ghMeshShadow = Components.FindComponent("MeshShadow");

        public Shadow(SunPosition newSunPosition, Mesh newOriginObject)
        {
            currentSunPosition = newSunPosition;
            originObject = newOriginObject;

            ShadowCurve = CreateShadowCurve();
        }

        private Object CreateShadowCurve()
        {
            string[] warnings;
            Plane planeXY = new Plane(new Point3d(0.0, 0.0, 0.0), new Vector3d(0.0, 0.0, 1.0));

            object[] result = ghMeshShadow.Evaluate(new Object[] { originObject, currentSunPosition.SunVector, planeXY }, false, out warnings);

            return result[0];
        }
    }
}
