using RentalHome.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalHome.Core.Entities;

public  class District : BaseEntity
{
    public string Name { get; set; }

    public long RegionId { get; set; }

    public Region Region { get; set; }

    public ICollection<Property> Properties { get; set; }

}
