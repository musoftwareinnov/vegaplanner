using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace vega.Extensions.DateTime
{

    public static class BusinessDays
    {
        public static System.DateTime AddBusinessDays(this System.DateTime source, int businessDays)
        {
            var dayOfWeek = businessDays < 0
                                ? ((int)source.DayOfWeek - 12) % 7
                                : ((int)source.DayOfWeek + 6) % 7;

            switch (dayOfWeek)
            {
                case 6:
                    businessDays--;
                    break;
                case -6:
                    businessDays++;
                    break;
            }
            return source.AddDays(businessDays + ((businessDays + dayOfWeek) / 5) * 2);
        }

        public static int GetBusinessDays(this System.DateTime current, System.DateTime finishDateExclusive, List<System.DateTime> excludedDates)
        {
            Func<int, bool> isWorkingDay = days =>
            {
                var currentDate = current.AddDays(days);
                var isNonWorkingDay =
                    currentDate.DayOfWeek == DayOfWeek.Saturday ||
                    currentDate.DayOfWeek == DayOfWeek.Sunday ||
                    excludedDates.Exists(excludedDate => excludedDate.Date.Equals(currentDate.Date));
                return !isNonWorkingDay;
            };

            return Enumerable.Range(0, (finishDateExclusive - current).Days).Count(isWorkingDay);
        }
    }
}