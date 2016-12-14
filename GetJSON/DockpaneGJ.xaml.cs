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
        }

        private void btnPointer_Click(object sender, RoutedEventArgs e)
        {
            //PointerTool pt = new PointerTool();
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

    }
}
