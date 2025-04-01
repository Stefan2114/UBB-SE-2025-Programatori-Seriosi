using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;



namespace Team3.Views
{
 
    public sealed partial class AuditOptionPage : Page
    {
        public AuditOptionPage()
        {
            this.InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.Navigate(typeof(UserView));

            }
        }

        private void DoctorStatsButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(DoctorView));
        }

        private void DepartmentStatsButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(DepartmentView));
        }
    }
}
