using RentalHome.Core.Common;

namespace RentalHome.Core.Entities;

public  class Photo : BaseEntity
{
    public string Image { get; set; }
    public string Url { get; set; }

    public int PropertyId { get; set; }
    public Property Property { get; set; }
}
