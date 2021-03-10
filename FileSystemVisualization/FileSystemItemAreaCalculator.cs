using System.Windows;

namespace FileSystemVisualization
{
    public class FileSystemItemAreaCalculator
    {
        //TODO: (double Width, double Height) -> FileSystemItemArea
        public (double Width, double Height) CalculateOptimalArea(string content) 
            => ((content?.Length ?? 0) * 9, 30);

        public (double Width, double Height) CalculateMaximumArea() 
            => (SystemParameters.PrimaryScreenWidth, SystemParameters.PrimaryScreenHeight);

        public (double Width, double Height) CalculateByType(FileSystemItemAreaType type, string content)
        {
            switch (type)
            {
                case FileSystemItemAreaType.Miniature:
                    return CalculateOptimalArea(content);
                case FileSystemItemAreaType.MaximumArea:
                    return CalculateMaximumArea();
                default:
                    return (0, 0);
            }
        }
    }
}