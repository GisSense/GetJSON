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
    internal class DockpaneGJViewModel : DockPane//, INotifyPropertyChanged
    {
        private const string _dockPaneID = "GetJSON_DockpaneGJ";
        private const string _menuID = "GetJSON_DockpaneGJ_Menu";
        private string txtJson = "";
        //public event PropertyChangedEventHandler PropertyChanged;

        protected DockpaneGJViewModel() { }

        /// <summary>
        /// Show the DockPane.
        /// </summary>
        internal static void Show()
        {
            DockPane pane = FrameworkApplication.DockPaneManager.Find(_dockPaneID);
            if (pane == null)
                return;
            //((DockpaneGJViewModel)pane).TextJson = "Poep";
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
                //string test = ((DockpaneGJViewModel)pane).TextJson;
                ((DockpaneGJViewModel)pane).TextJson = v;
                //test = ((DockpaneGJViewModel)pane).TextJson;
                //((DockpaneGJViewModel)pane).BurgerButtonTooltip = v;
                //DataContextTest.Instance.TextJson = DateTime.Now.ToString();
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

        //private string test = "";
        ///// <summary>
        ///// Tooltip shown when hovering over the burger button.
        ///// </summary>
        //public string BurgerButtonTooltip
        //{
        //    get { return test; }//"Options"; }
        //    set
        //    {
        //        SetProperty(ref test, value, () => BurgerButtonTooltip);
        //    }
        //}

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
        }
    }
}
