using Microsoft.UI.Xaml.Controls;
using Team3.ModelViews;

namespace Team3
{
    public sealed partial class MainPage : Page
    {


        public UsersViewModel ViewModel { get; } = new UsersViewModel();
        public MainPage()
        {
            this.InitializeComponent();
            this.UsersListView.DataContext = ViewModel;
        }
    }
}
