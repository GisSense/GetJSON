using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Mapping;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Events;

namespace GetJSON
{
    internal class PointerTool : MapTool
    {
        public PointerTool()
        {
            IsSketchTool = true;
            SketchType = SketchGeometryType.Rectangle;
            SketchOutputMode = SketchOutputMode.Map;

        }


        protected override Task OnToolActivateAsync(bool active)
        {
            return base.OnToolActivateAsync(active);
        }


        protected override Task<bool> OnSketchCompleteAsync(Geometry geometry)
        {
            //end of sketch
            return base.OnSketchCompleteAsync(geometry);
        }
        protected override Task OnToolDeactivateAsync(bool hasMapViewChanged)
        {
            //deactivation of the tool
            return base.OnToolDeactivateAsync(hasMapViewChanged);
        }
        
    }
}
