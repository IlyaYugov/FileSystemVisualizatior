using System.Windows;
using FileSystemVisualization.ViewModels;

namespace FileSystemVisualization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new FileSystemItemViewModel();
        }
    }
}
