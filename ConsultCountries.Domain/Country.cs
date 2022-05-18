
namespace ConsultCountries.Domain
{
    public class Country
    {
        public Name? Name { get; set; }
        public List<string>? Capital { get; set; }
        public int Population { get; set; }
        public List<string>? TimeZones { get; set; }
        public List<string>? Borders { get; set; }
        public Flags? Flags { get; set; }   
        public Dictionary<string, Currencies>? Currencies { get; set; }
        public Dictionary<string, string>? Languages { get; set; }
        public string? Cca3 { get; set; } //Initials
        public List<decimal>? Latlng { get; set; }
        public string? Region { get; set; }
    }
}

