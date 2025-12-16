using ISR.Domain.Enums;

namespace ISR.Domain.Entities
{
    public class Lead
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; } = default!;
        public string Address { get; private set; } = default!;
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public LeadSource Source { get; private set; }

        private Lead() { }

        public Lead(
            string name,
            string address,
            double latitude,
            double longitude,
            LeadSource source)
        {
            Name = name;
            Address = address;
            Latitude = latitude;
            Longitude = longitude;
            Source = source;
        }
    }
}
