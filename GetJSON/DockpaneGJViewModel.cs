using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using System.ComponentModel;

namespace GetJSON
{
    internal class DockpaneGJViewModel : DockPane
    {
        private const string _dockPaneID = "GetJSON_DockpaneGJ";
        private const string _menuID = "GetJSON_DockpaneGJ_Menu";
        private string txtJson = "";

        protected DockpaneGJViewModel() { }

        /// <summary>
        /// Show the DockPane.
        /// </summary>
        internal static void Show()
        {
            DockPane pane = FrameworkApplication.DockPaneManager.Find(_dockPaneID);
            if (pane == null)
                return;
            pane.Activate();

            return;
        }

        /// <summary>
        /// Text shown near the top of the DockPane.
        /// </summary>
        private string _heading = "My DockPane";
        public string Heading
        {
            get { return _heading; }
            set
            {
                SetProperty(ref _heading, value, () => Heading);
            }
        }

        #region Burger Button
        /// <summary>
        /// Menu shown when burger button is clicked.
        /// </summary>
        public System.Windows.Controls.ContextMenu BurgerButtonMenu
        {
            get { return FrameworkApplication.CreateContextMenu(_menuID); }
        }

        internal static void UpdateText(string v)
        {
            DockPane pane = FrameworkApplication.DockPaneManager.Find(_dockPaneID);
            
            if (pane != null)
            {
                ((DockpaneGJViewModel)pane).TextJson = v;
            }

        }
        #endregion
        public string TextJson
        {
            get
            {
                return this.txtJson;
            }
            set
            {
                SetProperty(ref this.txtJson, value, () => TextJson);
            }
        }

    }

    /// <summary>
    /// Button implementation to show the DockPane.
    /// </summary>
    internal class DockpaneGJ_ShowButton : Button
    {
        protected override void OnClick()
        {
            DockpaneGJViewModel.Show();
        }
    }

    /// <summary>
    /// Button implementation for the button on the menu of the burger button.
    /// </summary>
    internal class DockpaneGJ_MenuButton : Button
    {
        protected override void OnClick()
        {
            //About...
            new AboutGetJSON().ShowDialog();
        }
    }
}
