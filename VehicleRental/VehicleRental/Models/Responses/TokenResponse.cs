using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRental.Models.Responses
{
    public class TokenResponse
    {
        public string Token { get; set; }

        public string Name { get; set; }

        public DateTime ValidUpto { get; set; }

        public string Role { get; set; }

        public string UserID { get; set; }
    }
}
