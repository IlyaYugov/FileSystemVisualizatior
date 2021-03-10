using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using FileSystemVisualization.Annotations;

namespace FileSystemVisualization.Models
{
    public class FileSystemItem : INotifyPropertyChanged
    {
        private string content;
        private double width;
        private double height;

        public FileSystemItemType Type { get; set; }
        public List<FileSystemItem> Children { get; set; }
        public FileSystemItem Parent { get; set; }

        public string Content
        {
            get => content;
            set
            {
                content = value;
                OnPropertyChanged("Content");
            }
        }

        public double Width
        {
            get => width;
            set
            {
                width = value;
                OnPropertyChanged("Width");
            }
        }

        public double Height
        {
            get => height;
            set
            {
                height = value;
                OnPropertyChanged("Height");
            }
        }

        public Color BackgroundColor { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}