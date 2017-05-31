using System;
using System.Collections.Generic;
using System.Windows.Input;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using System.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using GetJSON.Events;
using ArcGIS.Desktop.Core.Geoprocessing;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using System.IO;

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
            //fill the textbox with info
            DockpaneGJViewModel.UpdateText("Processing...");
            //execute the geoprocessing tool for creating json
            Task<IGPResult> myTsk = QueuedTask.Run(() =>
            {
                BasicFeatureLayer bfl = args.BasicFL;
                var flist = new List<object>() { bfl, };
                Task<IGPResult> taskRes =
                   Geoprocessing.ExecuteToolAsync("conversion.FeaturesToJSON", Geoprocessing.MakeValueArray(flist, null, "FORMATTED"));
                return taskRes;
            });
            IGPResult resultaat = await myTsk;
            if (!(resultaat.IsFailed || resultaat.IsCanceled))
            {
                ////filename
                string filename = myTsk.Result.ReturnValue;
                //read the file
                string contents = File.ReadAllText(@filename);
                //fill the textbox
                DockpaneGJViewModel.UpdateText(contents);
            } else
            {
                DockpaneGJViewModel.UpdateText("Sorry, but features can't be converted to JSON. " + Environment.NewLine + "Response: " + resultaat.ReturnValue);
            }
        }

    }
}
