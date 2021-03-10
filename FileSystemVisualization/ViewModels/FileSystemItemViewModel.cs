using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FileSystemVisualization.Annotations;
using FileSystemVisualization.Commands;
using FileSystemVisualization.Extensions;
using FileSystemVisualization.Models;

namespace FileSystemVisualization.ViewModels
{
    public class FileSystemItemViewModel : INotifyPropertyChanged
    {
        private FileSystemItem openedFileSystemItem;

        private FileSystemItemCommand goToParentCommand;

        private FileSystemItemCommand openFileSystemItemCommand;
        private FileSystemItemCommand removeFileSystemItemCommand;

        private FileSystemItemCreator fileSystemItemCreator;

        public ObservableCollection<FileSystemItem> FileSystemItems { get; set; }

        public FileSystemItemViewModel()
        {
            FileSystemItems = new ObservableCollection<FileSystemItem>();

            openedFileSystemItem = new FileSystemItem
            {
                Children = new List<FileSystemItem>()
            };

            fileSystemItemCreator = new FileSystemItemCreator();
        }

        public FileSystemItemCommand GoToParentCommand
        {
            get
            {
                return goToParentCommand ??= new FileSystemItemCommand(_ =>
                {
                    if (openedFileSystemItem.Parent == null)
                        return;

                    openedFileSystemItem.ModifyArea(FileSystemItemAreaType.Miniature);
                    openedFileSystemItem = openedFileSystemItem.Parent;

                    FileSystemItems.Clear();
                    FileSystemItems.AddRange(openedFileSystemItem.Children);
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
                        openedFileSystemItem = fileSystemItem;

                        FileSystemItems.Clear();

                        ChangeOpenedItemsByNewItemType();
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
                            openedFileSystemItem.Children.Remove(fileSystemItem);

                            FileSystemItems.Remove(fileSystemItem);
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

        private void ChangeOpenedItemsByNewItemType()
        {
            switch (openedFileSystemItem.Type)
            {
                case FileSystemItemType.Directory:
                    FileSystemItems.AddRange(openedFileSystemItem.Children);
                    break;

                case FileSystemItemType.File:
                    openedFileSystemItem.ModifyArea(FileSystemItemAreaType.MaximumArea);

                    FileSystemItems.Add(openedFileSystemItem);
                    break;
            }
        }

        public void CreateFileSystemItem(FileSystemItemType type, string content)
        {
            var newItem = fileSystemItemCreator.Create(type, content, openedFileSystemItem, FileSystemItemAreaType.Miniature);
            openedFileSystemItem.AddChild(newItem);

            FileSystemItems.Insert(0, newItem);
        }
    }
}