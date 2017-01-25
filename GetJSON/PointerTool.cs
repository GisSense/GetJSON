using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Mapping;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Events;
using ArcGIS.Desktop.Framework.Threading.Tasks;


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

        protected override async Task<bool> OnSketchCompleteAsync(Geometry geometry)
        {
            //1 select features based on geometry
            System.Windows.Point bottomRight = new System.Windows.Point();
            Dictionary<BasicFeatureLayer, List<long>> allfeatures = new Dictionary<BasicFeatureLayer, List<long>>();
            await QueuedTask.Run(() => 
            {
                allfeatures = ActiveMapView.SelectFeatures(geometry, SelectionCombinationMethod.New, false, false);
            });

            //2 build a context menu with the layers and (re)select by user's layer-choice
            ShowContextMenu(bottomRight, allfeatures);

            return true;
        }

        private void ShowContextMenu(System.Windows.Point screenLocation, Dictionary<BasicFeatureLayer, List<long>> allSelectedfeatures)
        {
            var contextMenu = FrameworkApplication.CreateContextMenu("DynamicMenu_SelectLayer", () => screenLocation);
            if (contextMenu == null) return;
            DynamicSelectLayerMenu.SetItems(allSelectedfeatures);
            contextMenu.IsOpen = true;
        }

    }
}
