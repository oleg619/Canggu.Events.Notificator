using System;

namespace CangguEvents.Application.Services.Implementation
{
    public static class DateTimeService
    {
        private static readonly TimeZoneInfo
            CangguTimeZone = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");

        public static DateTime CangguTimeNow => TimeZoneInfo.ConvertTime(DateTime.Now, CangguTimeZone);
    }
}