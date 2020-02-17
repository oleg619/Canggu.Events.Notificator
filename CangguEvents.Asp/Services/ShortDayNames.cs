using System;
using System.Globalization;

namespace CangguEvents.Asp.Services
{
    public static class ShortDayNames
    {
        private static readonly DateTimeFormatInfo Culture = CultureInfo.GetCultureInfo("en-US").DateTimeFormat;

        public static string Get(DayOfWeek dayOfWeek)
        {
            var shortName = Culture.AbbreviatedDayNames[(int) dayOfWeek];
            return shortName;
        }
    }
}