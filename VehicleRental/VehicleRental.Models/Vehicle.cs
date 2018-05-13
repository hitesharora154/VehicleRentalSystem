using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleRental.Models
{
    public class Vehicle
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public Guid TypeID { get; set; }

        [ForeignKey("TypeID")]
        public virtual VehicleType VehicleType { get; set; }
    }
}
