using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using VehicleRental.Models.Identity;

namespace VehicleRental.Models
{
    public class Booking
    {
        public Guid ID { get; set; }

        public Guid VehicleID { get; set; }

        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime BookingDate { get; set; }

        public string UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("VehicleID")]
        public virtual Vehicle Vehicle { get; set; }
    }
}
