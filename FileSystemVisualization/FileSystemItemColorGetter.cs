using System.Windows.Media;

namespace FileSystemVisualization
{
    public class FileSystemItemColorGetter
    {
        private byte curentGreenLevel = 100;
        private byte curentBlueLevel;

        public Color GetNextColor()
        {
            curentBlueLevel += 20;

            if (curentBlueLevel >= 100)
            {
                curentGreenLevel += 10;
                curentBlueLevel = 0;
            }

            return Color.FromRgb(0, curentGreenLevel, curentBlueLevel);
        }
    }
}