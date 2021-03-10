using System.Collections.Generic;
using FileSystemVisualization.Extensions;
using FileSystemVisualization.Models;

namespace FileSystemVisualization
{
    public class FileSystemItemCreator
    {
        private FileSystemItemColorGetter colorGetter;

        public FileSystemItemCreator()
        {
            colorGetter = new FileSystemItemColorGetter();
        }

        public FileSystemItem Create(FileSystemItemType type, string content, FileSystemItem parent, FileSystemItemAreaType areaType)
        {
            var child = new FileSystemItem
            {
                Children = new List<FileSystemItem>(),
                Content = content,
                Parent = parent,
                Type = type,
                BackgroundColor = colorGetter.GetNextColor()
            };
            child.ModifyArea(areaType);

            return child;
        }
    }
}