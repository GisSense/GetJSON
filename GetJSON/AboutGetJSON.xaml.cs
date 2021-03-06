﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GetJSON
{
    /// <summary>
    /// Interaction logic for AboutGetJSON.xaml
    /// </summary>
    public partial class AboutGetJSON : Window
    {
        public AboutGetJSON()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            rtbAbout.Document.Blocks.Clear();
            rtbAbout.AppendText(Properties.Resources.AboutTxt);
            rtbAbout.AppendText("\r\n");

            Assembly assembly = Assembly.GetExecutingAssembly();
            AssemblyName assName = assembly.GetName();
            object[] attribs = assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), true);
            String company = "";
            String copyright = "";
            if (attribs.Length > 0)
            {
                company = ((AssemblyCompanyAttribute)attribs[0]).Company;
            }
            attribs = assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), true);
            if (attribs.Length > 0)
            {
                copyright = ((AssemblyCopyrightAttribute)attribs[0]).Copyright;
            }
            rtbAbout.AppendText("Company: " + company + "\r");
            rtbAbout.AppendText("Assembly: " + assName.Name + "\r");
            rtbAbout.AppendText("Version: " + assName.Version.ToString() + "\r");
            rtbAbout.AppendText("Copyright: " + "\r" + copyright + "\r");

        }

        private void image1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //open website
            System.Diagnostics.Process.Start("http://www.gissense.com");
        }
    }
}
