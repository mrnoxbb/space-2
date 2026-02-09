using System.Windows;
using SpaceVenueApp.ViewModels;

namespace SpaceVenueApp;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }
}
