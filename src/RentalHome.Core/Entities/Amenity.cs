using RentalHome.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalHome.Core.Entities
{
    public  class Amenity:BaseEntity
    {
        public string Name { get; set; }
        public string IconClass { get; set; }
    }
}
