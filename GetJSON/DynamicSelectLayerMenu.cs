using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Mapping;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Core.Data;
using System.Windows.Controls;
using ArcGIS.Core.Geometry;
using System.IO;
using System.Runtime.Serialization.Json;
using ArcGIS.Desktop.Core.Geoprocessing;
using GetJSON.Events;

namespace GetJSON
{
    internal class DynamicSelectLayerMenu : DynamicMenu
    {
        internal delegate void ClickAction(KeyValuePair<BasicFeatureLayer, List<long>> allSelectedfeatures);
        private static Dictionary<BasicFeatureLayer, List<long>> allSelectedfeatures;

        public static void SetItems(Dictionary<BasicFeatureLayer, List<long>> asf)
        {
            allSelectedfeatures = new Dictionary<BasicFeatureLayer, List<long>>(asf);
        }

        protected override void OnPopup()
        {
            this.Add("Select layer", "", false, true, true);
            this.AddSeparator();
            if (allSelectedfeatures == null || allSelectedfeatures.Count == 0)
            {
                this.Add("No features found", "", false, false, true);
                return;
            }

            ClickAction theAction = OnMenuItemClicked;
            foreach (KeyValuePair<BasicFeatureLayer, List<long>> entry in allSelectedfeatures)
            {
                string layername = entry.Key.Name;
                int selCount = entry.Value.Count;
                bool enableIt = false;
                if (selCount <= 1000)
                {
                    enableIt = true;
                }
                this.Add($"{layername} ({selCount})", "", false, enableIt, false, theAction, entry);
            }
        }

        private static void OnMenuItemClicked(KeyValuePair<BasicFeatureLayer, List<long>> selectedLayer)
        {
            //reselect and start converting
            QueuedTask.Run(() =>
            {
                foreach (BasicFeatureLayer bfl in allSelectedfeatures.Keys)
                {
                    if (bfl.Name != selectedLayer.Key.Name)
                    {
                        //clear this selection
                        bfl.ClearSelection();
                    }
                }

                //publish event GetJsonSelectionFinishedEvent with basiclayerfeature
                GetJsonSelectionFinishedEvent.Publish(new GetJsonSelectionFinishedEventArgs(selectedLayer.Key));
            });
        }

    }
}
