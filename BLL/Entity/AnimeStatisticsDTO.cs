using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entity
{
    public class AnimeStatisticsDTO
    {
        public int PlannedCount { get; set; }
        public int WatchingCount { get; set; }
        public int WatchedCount { get; set; }
        public int PostponedCount { get; set; }
        public int DroppedCount { get; set; }

        public double PlannedPercentage => CalculatePercentage(PlannedCount);
        public double WatchingPercentage => CalculatePercentage(WatchingCount);
        public double WatchedPercentage => CalculatePercentage(WatchedCount);
        public double PostponedPercentage => CalculatePercentage(PostponedCount);
        public double DroppedPercentage => CalculatePercentage(DroppedCount);

        private double CalculatePercentage(int count)
        {
            // Проверка на деление на ноль
            if (count == 0)
            {
                return 0;
            }
            else
            {
                // Расчет процента от общего числа
                return (double)count / (PlannedCount + WatchingCount + WatchedCount + PostponedCount + DroppedCount) * 100;
            }
        }
    }
}
