using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Events;
using ArcGIS.Desktop.Mapping.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using ArcGIS.Desktop.Mapping;

namespace GetJSON
{
    /// <summary>
    /// Interaction logic for DockpaneGJView.xaml
    /// </summary>
    public partial class DockpaneGJView : UserControl
    {
        DispatcherTimer timer = new DispatcherTimer();

        public DockpaneGJView()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(ClearMessageEventHandler);
            ActiveToolChangedEvent.Subscribe(onToolChanged);
            //Active Pane Check
            ActivePaneChangedEvent.Subscribe(onPaneChanged);

        }

        ~DockpaneGJView()
        {
            ActiveToolChangedEvent.Unsubscribe(onToolChanged);
            ActivePaneChangedEvent.Unsubscribe(onPaneChanged);
        }

        private void onToolChanged(ToolEventArgs obj)
        {
            btnPointer.IsChecked = false;
            if (obj.CurrentID == "GetJSON_PointerTool")
            {
                btnPointer.IsChecked = true;
            }
        }
        private void onPaneChanged(PaneEventArgs obj)
        {
            btnPointer.IsEnabled = true;
            if (obj.IncomingPane == null || !(obj.IncomingPane is IMapPane))
            {
                btnPointer.IsEnabled = false;
            }
        }


        private void btnPointer_Click(object sender, RoutedEventArgs e)
        {
            IPlugInWrapper piw = FrameworkApplication.GetPlugInWrapper("GetJSON_PointerTool");
            ICommand myCmd = piw as ICommand;
            if (myCmd != null)
            {
                if (piw.Enabled) {
                    if (myCmd.CanExecute(null)) myCmd.Execute(null);
                }
                else if (FrameworkApplication.GetPlugInWrapper("GetJSON_PointerTool").Enabled)
                {
                    if (myCmd.CanExecute(null)) myCmd.Execute(null);
                } else
                {
                    if (myCmd.CanExecute(null)) myCmd.Execute(null);
                }
            }

        }

        private void btnClearText_Click(object sender, RoutedEventArgs e)
        {
            this.txbJSON.Clear();
            this.txbJSON.Focus();
            ShowMessage("Text cleared", Brushes.Blue, 1);
        }

        private void ShowMessage(string message, Brush br, double seconds)
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
            }

            lblMessage.Content = message;
            lblMessage.Foreground = br;

            //The message will disappear after x seconds.
            timer.Interval = TimeSpan.FromSeconds(seconds);
            timer.Start();
        }

        private void ClearMessageEventHandler(object sender, EventArgs e)
        {
            lblMessage.Content = string.Empty;
            (sender as DispatcherTimer).Stop();
            lblMessage.Foreground = Brushes.Blue;
        }

        private void btnCopyText_Click(object sender, RoutedEventArgs e)
        {
            //copy to clipboard
            this.txbJSON.SelectAll();
            this.txbJSON.Copy();
            this.txbJSON.Select(0, 0);
            this.txbJSON.Focus();
            ShowMessage("Text copied", Brushes.Blue, 1);
        }
    }
}
