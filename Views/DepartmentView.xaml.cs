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
using Team3.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Team3.ModelViews;
using Team3.DTOs;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Team3.Views
{
    public sealed partial class DepartmentView : Page
    {
        private readonly DepartmentModelView _viewModel;
        public DepartmentView()
        {
            this.InitializeComponent();
            _viewModel = new DepartmentModelView();
            LoadDepartments();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.Navigate(typeof(AuditOptionPage));

            }
        }


        private async void LoadDepartments()
        {
           // List<DepartmentDTO> departments = await _viewModel.GetDepartmentDetailsAsync();
           // DepartmentListView.ItemsSource = departments;
        }
    }
}
