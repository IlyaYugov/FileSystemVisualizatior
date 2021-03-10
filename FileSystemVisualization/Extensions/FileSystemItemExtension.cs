using FileSystemVisualization.Models;

namespace FileSystemVisualization.Extensions
{
    public static class FileSystemItemExtension
    {
        public static FileSystemItem AddChild(this FileSystemItem item, FileSystemItem child)
        {
            item.BackgroundColor = child.BackgroundColor;
            item.Children.Add(child);

            return item;
        }

        public static FileSystemItem ModifyArea(this FileSystemItem item, FileSystemItemAreaType areaType)
        {
            var areaCalculator = new FileSystemItemAreaCalculator();
            var area = areaCalculator.CalculateByType(areaType, item.Content);

            item.Height = area.Height;
            item.Width = area.Width;

            return item;
        }
    }
}