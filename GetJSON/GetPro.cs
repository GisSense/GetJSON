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

        #region Overrides
        
            /// <summary>
            /// Called by Framework when ArcGIS Pro is closing
            /// </summary>
            /// <returns>False to prevent Pro from closing, otherwise True</returns>
        protected override bool CanUnload()
        {
            //TODO - add your business logic
            //return false to ~cancel~ Application close
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

        #endregion Overrides
        


    }
}
