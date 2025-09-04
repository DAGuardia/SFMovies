namespace SFMovies.Domain.Entities
{
    public class MovieLocation
    {
        public string Address { get; }
        public string? FunFact { get; }
        public double? Latitude { get; }
        public double? Longitude { get; }

        public MovieLocation(string address, string? funFact = null, double? latitude = null, double? longitude = null)
        {
            Address = address;
            FunFact = funFact;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
