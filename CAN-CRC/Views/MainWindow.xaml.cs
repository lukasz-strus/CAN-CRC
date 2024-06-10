using CAN_CRC.ViewModels;

namespace CAN_CRC.Views;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();

        DataContext = new MainViewModel();  
    }
}