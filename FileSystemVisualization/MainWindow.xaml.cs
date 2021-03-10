using System.Windows;
using FileSystemVisualization.ViewModels;

namespace FileSystemVisualization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FileSystemItemViewModel dataContext = new FileSystemItemViewModel();
        private FileSystemItemType creatingFileSystemItemType;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = dataContext;
        }

        private void CreateFile_OnClick(object sender, RoutedEventArgs e)
        {
            ContentFillingPopup.IsOpen = true;
            creatingFileSystemItemType = FileSystemItemType.File;
        }

        private void CreateDirectory_OnClick(object sender, RoutedEventArgs e)
        {
            ContentFillingPopup.IsOpen = true;
            creatingFileSystemItemType = FileSystemItemType.Directory;
        }

        private void ClosePopup_OnClick(object sender, RoutedEventArgs e)
        {
            ContentFillingPopup.IsOpen = false;
        }

        private void CreateFileSystemItemPopup_OnClick(object sender, RoutedEventArgs e)
        {
            ContentFillingPopup.IsOpen = false;
            dataContext.CreateFileSystemItem(creatingFileSystemItemType, FileSystemItemContent.Text);
        }
    }
}
