using Newtonsoft.Json.Converters;

namespace VehicleRental.Models
{
    public class CustomDateTimeConverter : IsoDateTimeConverter
    {
        public CustomDateTimeConverter()
        {
            base.DateTimeFormat = "dd-MM-yyyy";
        }
    }
}
