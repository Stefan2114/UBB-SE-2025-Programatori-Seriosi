using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Team3
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.ExtendsContentIntoTitleBar = false;


            // Navigate to MainPage on startup
            MainFrame.Navigate(typeof(MainPage));
        }
    }
}
