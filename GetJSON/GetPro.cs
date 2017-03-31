using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using System.Threading.Tasks;
using ArcGIS.Desktop.Framework.Events;
using ArcGIS.Desktop.Core.Events;
using ArcGIS.Desktop.Mapping;
using ArcGIS.Core.Data;
using GetJSON.Events;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Core.Geoprocessing;
using ArcGIS.Desktop.Framework.Threading.Tasks;

namespace GetJSON
{
    internal class GetPro : Module
    {
        private static GetPro _this = null;

        /// <summary>
        /// Retrieve the singleton instance to this module here
        /// </summary>
        public static GetPro Current
        {
            get
            {
                return _this ?? (_this = (GetPro)FrameworkApplication.FindModule("GetJSON_Module"));
            }
        }

        #region ----- Overrides -----

        protected override bool Initialize()
        {
            //subscribe on sel
            GetJsonSelectionFinishedEvent.Subscribe(OnGetJsonSelectionFinished);

            return base.Initialize();
        }

        /// <summary>
        /// Called by Framework when ArcGIS Pro is closing
        /// </summary>
        /// <returns>False to prevent Pro from closing, otherwise True</returns>
        protected override bool CanUnload()
        {
            GetJsonSelectionFinishedEvent.Unsubscribe(OnGetJsonSelectionFinished);

            return true;
        }

        protected override Func<Task> ExecuteCommand(string id)
        {
            var command = FrameworkApplication.GetPlugInWrapper(id) as ICommand;
            if (command == null)
                return () => Task.FromResult(0);
            if (!command.CanExecute(null))
                return () => Task.FromResult(0);

            return () =>
            {
                command.Execute(null); // if it is a tool, execute will set current tool
                return Task.FromResult(0);
            };

        }

        #endregion ----- Overrides -----

        private async void OnGetJsonSelectionFinished(GetJsonSelectionFinishedEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("0: " + DateTime.Now);
            //fille the textbox with info
            DockpaneGJViewModel.UpdateText("Processing...");
            Task<IGPResult> myTsk = QueuedTask.Run(() =>
            {
                BasicFeatureLayer bfl = args.BasicFL;
                var aap = new List<object>() { bfl, };
                System.Diagnostics.Debug.WriteLine("1: " + DateTime.Now);
                Task<IGPResult> taskRes =
                   Geoprocessing.ExecuteToolAsync("conversion.FeaturesToJSON",
                                                            Geoprocessing.MakeValueArray(aap, null, "FORMATTED"));
                System.Diagnostics.Debug.WriteLine("2: " + DateTime.Now);
                return taskRes;
            });
            System.Diagnostics.Debug.WriteLine("3: " + DateTime.Now);
            IGPResult resultaat = await myTsk;
            if (!(resultaat.IsFailed || resultaat.IsCanceled))
            {
                ////filename
                string filename = myTsk.Result.ReturnValue;
                ////goedgegaan
                System.Diagnostics.Debug.WriteLine("4: " + myTsk.Result.ReturnValue + " " + DateTime.Now);
                //read the file
                //todo
                //fill the textbox
                DockpaneGJViewModel.UpdateText(filename);
                //close and delete the file
                //todo
            }
        }

    }
}
