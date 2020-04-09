namespace CangguEvents.Domain
{
    public class Location
    {
        public string GoogleUrl { get; }
        public float Latitude { get; }
        public float Longitude { get; }

        public Location(string googleUrl, float latitude, float longitude)
        {
            GoogleUrl = googleUrl;
            Latitude = latitude;
            Longitude = longitude;
        }

        public override string ToString()
        {
            return $"{nameof(GoogleUrl)}: {GoogleUrl}, {nameof(Latitude)}: {Latitude}, {nameof(Longitude)}: {Longitude}";
        }
    }
}