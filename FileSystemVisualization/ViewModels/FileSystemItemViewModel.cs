using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using FileSystemVisualization.Annotations;
using FileSystemVisualization.Commands;
using FileSystemVisualization.Models;

namespace FileSystemVisualization.ViewModels
{
    public class FileSystemItemViewModel : INotifyPropertyChanged
    {
        public FileSystemItem currentFileSystemItem;

        private byte lastGreenLevel = 100;
        private byte lastBlueLevel = 0;

        private FileSystemItemCommand goToParentCommand;
        private FileSystemItemCommand сreateFileCommand;
        private FileSystemItemCommand сreateDirectoryCommand;

        private FileSystemItemCommand openFileSystemItemCommand;
        private FileSystemItemCommand removeFileSystemItemCommand;

        public ObservableCollection<FileSystemItem> FileSystemItems { get; set; }


        public FileSystemItemViewModel()
        {
            currentFileSystemItem = new FileSystemItem();
            FileSystemItems = new ObservableCollection<FileSystemItem>();


            for (int i = 0; i < 50; i++)
            {
                FileSystemItems.Add(GetLabels());
            }

            currentFileSystemItem.Children = FileSystemItems.ToList();
        }

        public FileSystemItemCommand CreateFileCommand => 
            сreateFileCommand ??= new FileSystemItemCommand(_ =>
            {
                CreateFileSystemItemByType(FileSystemItemType.File);
            });

        public FileSystemItemCommand CreateDirectoryCommand =>
            сreateDirectoryCommand ??= new FileSystemItemCommand(_ =>
            {
                CreateFileSystemItemByType(FileSystemItemType.Directory);
            });


        public FileSystemItemCommand GoToParentCommand
        {
            get
            {
                return goToParentCommand ??= new FileSystemItemCommand(_ =>
                {
                    if (currentFileSystemItem.Parent == null)
                        return;

                    FileSystemItems.Clear();
                    currentFileSystemItem = currentFileSystemItem.Parent;
                    foreach (var item in currentFileSystemItem.Children)
                    {
                        FileSystemItems.Add(item);
                    }
                });
            }
        }

        public FileSystemItemCommand OpenFileSystemItemCommand
        {
            get
            {
                return openFileSystemItemCommand ??= new FileSystemItemCommand(inputItem =>
                {
                    if (inputItem is FileSystemItem fileSystemItem)
                    {
                        FileSystemItems.Clear();
                        currentFileSystemItem = fileSystemItem;

                        switch (fileSystemItem.Type)
                        {
                            case FileSystemItemType.Directory:
                                foreach (var item in currentFileSystemItem.Children)
                                {
                                    FileSystemItems.Add(item);
                                }
                                break;

                            case FileSystemItemType.File:
                                FileSystemItems.Add(currentFileSystemItem);
                                break;
                        }
                    }
                });
            }
        }

        public FileSystemItemCommand RemoveFileSystemItemCommand
        {
            get
            {
                return removeFileSystemItemCommand ??= new FileSystemItemCommand(obj =>
                    {
                        if (obj is FileSystemItem fileSystemItem)
                        {
                            FileSystemItems.Remove(fileSystemItem);
                            currentFileSystemItem.Children.Remove(fileSystemItem);
                        }
                    },
                    (obj) => FileSystemItems.Count > 0);
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private FileSystemItem GetLabels()
        {
            return new FileSystemItem
            {
                Content = "qweqweqweqwe",
                Parent = currentFileSystemItem,
                Type = FileSystemItemType.Directory,
                Children = new List<FileSystemItem>(),
                BackgroundColor = Color.FromRgb(0, 100, 0)
            };
        }

        private void CreateFileSystemItemByType(FileSystemItemType type)
        {
            lastBlueLevel += 20;

            if (lastBlueLevel >= 100)
            {
                lastGreenLevel += 10;
                lastBlueLevel = 0;
            }

            FileSystemItem item = new FileSystemItem
            {
                Children = new List<FileSystemItem>(),
                Content = "trololo",
                Parent = currentFileSystemItem,
                Type = type,
                BackgroundColor = Color.FromRgb(0, lastGreenLevel, lastBlueLevel)
            };
            FileSystemItems.Insert(0, item);
            currentFileSystemItem.Children.Add(currentFileSystemItem);
        }
    }
}