using System;

namespace CangguEvents.Asp.Services.Implementation
{
    public static class DateTimeService
    {
        private static readonly TimeZoneInfo
            CangguTimeZone = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");

        public static DateTime CangguTimeNow => TimeZoneInfo.ConvertTime(DateTime.Now, CangguTimeZone);
    }
}