using RentalHome.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalHome.Core.Entities
{
    public class PropertyAmenity:BaseEntity
    {
        public int PropertyId { get; set; }
        public int AmenityId  { get; set; }
    }
}
