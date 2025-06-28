using RentalHome.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalHome.Core.Entities;

public class PropertyType : BaseEntity 
{
    public string Name { get; set; }
    public string Description { get; set; }
    
    public string IconClass { get; set; }

    public ICollection<Property> Properties { get; set; }
}
