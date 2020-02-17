﻿namespace CangguEvents.SQLite.Entities
{
    public class LocationEntity : StorageItem
    {
        public string GoogleUrl { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        
//        public LocationEntity(string googleUrl, float latitude, float longitude)
//        {
//            GoogleUrl = googleUrl;
//            Latitude = latitude;
//            Longitude = longitude;
//        }
    }
}