# FileSystemVisualizatior

.NET Core 5.0, WPF

Deletetig/opening file/directory - right click on file/directory
Go back, creating file/directory - Menu

TODO:
0. MultiLine file viwer
1. Styles
2. Visual difference between file and directory
3. Sorting/Editing files
4. ContextMenu in empty area for
5. Better kind of visualization information about modifying file/directory




using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using FileSystemVisualization.Models;
using FileSystemVisualization.ViewModels;

namespace FileSystemVisualization
{
    public class RectangleItem
    {
        public RectangleItem Parent {get;set;}

        public Rectangle Current {get;set;}

        public List<RectangleItem> Children {get;set;}

        public double Left { get; set; }
        public double Top { get; set; }

        public double FileLength { get; set; }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double rootFileLenght;

        public MainWindow()
        {
            InitializeComponent();

             DirectoryInfo di = new DirectoryInfo(@"C:\Users\Test\Downloads\FileSystemVisualizatior-main");
            // Get a reference to each file in that directory.
            List<FileInfo> files = di.GetFiles().ToList();

            foreach (FileInfo f in files)
                if(f.Length > rootFileLenght)
                {
                    rootFileLenght = f.Length;
                }

            var root = new RectangleItem 
            { 
                Children = new List<RectangleItem>(),
                Current = new Rectangle()
                    {
                        Width = 100,
                        Height = 100,
                        Stroke = new SolidColorBrush(Colors.Black),
                        Fill = new SolidColorBrush(Colors.Orange)
                    },
                FileLength = rootFileLenght,
                Left = 0,
                Top = 0
            };

            Canvas.SetLeft(root.Current, root.Left);
            Canvas.SetTop(root.Current, root.Top);
            FileSystemCanvas.Children.Add(root.Current);

            foreach (FileInfo file in files)
            { 
                var item = InsertItem(root, file.Length);
                if(item == null)
                    break;
                Canvas.SetLeft(item.Current, 0);
                Canvas.SetTop(item.Current, 0);
                FileSystemCanvas.Children.Add(item.Current);
            }
        }

        public RectangleItem InsertItem(RectangleItem root, double insertedItemWidth)
        {
            if(insertedItemWidth >= root.FileLength / 2 &&
                root.Children.All( s=> s.FileLength <= root.FileLength / 2))
            {
                var newItem = new RectangleItem
                {
                    Children = root.Children,
                    Parent = root,
                    FileLength = insertedItemWidth,
                    Current = new Rectangle()
                    {
                        Width = insertedItemWidth/rootFileLenght*100,
                        Height = insertedItemWidth/rootFileLenght*100,
                        Stroke = new SolidColorBrush(Colors.Black),
                        Fill = new SolidColorBrush(Colors.Orange)
                    },
                    Left = root.Left + insertedItemWidth/rootFileLenght*100,
                    Top = root.Top + insertedItemWidth/rootFileLenght*100
                };
                foreach(var child in newItem.Children)
                {
                    child.Parent = newItem;
                };
                root.Children = new List<RectangleItem>{newItem}; 
                return newItem;
            } 
            else if(insertedItemWidth >= root.FileLength / 2 &&
                root.Children.All( s=> s.FileLength >= root.FileLength / 2))
            {
                var newItem = new RectangleItem
                {
                    Children = new List<RectangleItem>(),
                    Parent = root,
                    FileLength = insertedItemWidth,
                    Current = new Rectangle()
                    {
                        Width = insertedItemWidth/rootFileLenght*100,
                        Height = insertedItemWidth/rootFileLenght*100,
                        Stroke = new SolidColorBrush(Colors.Black),
                        Fill = new SolidColorBrush(Colors.Orange)
                    },
                    Left = root.Left + insertedItemWidth/rootFileLenght*100,
                    Top = root.Top + insertedItemWidth/rootFileLenght*100
                };

                root.Children.Add(newItem);
                return newItem;
            }
            else if(insertedItemWidth <= root.FileLength/2 
                && root.Children.All( s=> s.FileLength <= root.FileLength / 2))
            {
                 var newItem = new RectangleItem
                {
                    Children = new List<RectangleItem>(),
                    Parent = root,
                    FileLength = insertedItemWidth,
                    Current = new Rectangle()
                    {
                        Width = insertedItemWidth/rootFileLenght*100,
                        Height = insertedItemWidth/rootFileLenght*100,
                        Stroke = new SolidColorBrush(Colors.Black),
                        Fill = new SolidColorBrush(Colors.Orange)
                    },
                    Left = root.Left + insertedItemWidth/rootFileLenght*100,
                    Top = root.Top + insertedItemWidth/rootFileLenght*100
                };

                root.Children.Add(newItem);
                return newItem;
            }
            else if(insertedItemWidth <= root.FileLength/2 
                && root.Children.All( s=> s.FileLength >= root.FileLength / 2))
            {
                return root.Children[0];
            }
            else
                return null;
        }
    }
}
