using RentalHome.Core.Common;
using RentalHome.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalHome.Core.Entities
{
    public class Review : BaseEntity
    {
        public int BookingId { get; set; }
        public int PropertyId { get; set; }
        public int TenantId { get; set; }
        public int LandlordId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public  ReviewType ReviewType  { get; set; }
    }
   
}
