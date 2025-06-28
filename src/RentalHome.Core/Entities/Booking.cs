using RentalHome.Core.Common;
using RentalHome.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalHome.Core.Entities
{
    public class Booking : BaseEntity
    {
        public int PropertyId { get; set; }
        public int TenantId { get; set; }
        public int LandlordId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
   
}
