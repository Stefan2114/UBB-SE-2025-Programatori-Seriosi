using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;
using Team3.Domain;
using Team3.ModelViews;

namespace Team3.Views
{
    public sealed partial class DoctorView : Window  // Change from Page to Window
    {
        private readonly DoctorModelView _doctorModelView;

        public DoctorView()
        {
            this.InitializeComponent();
            _doctorModelView = new DoctorModelView();
            LoadDoctors();
        }

        // Load doctors into the ListView
        private void LoadDoctors()
        {
            doctorInfoList.ItemsSource = _doctorModelView.DoctorsInfo;
        }

        // Handle when the date in the ComboBox is changed
        private void SelectDatePeriod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectDatePeriod.SelectedItem != null)
            {
                _doctorModelView.DateSelectedComboBoxHandler(selectDatePeriod.SelectedItem.ToString()); //help here pls ,cred ca mi trb de la cata
                LoadDoctors();
            }
        }

        // Handle when the back button is clicked
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _doctorModelView.BackButtonHandler();  //same here
            this.Close();  // Closes the window when back is pressed
        }
    }
}
