namespace ISR.Domain.Entities
{
    public class HomeAddress
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        private HomeAddress() { }

        public HomeAddress(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public void Update(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
