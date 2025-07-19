using RentalHome.Core.Common;

namespace RentalHome.Core.Entities;

public  class Photo:BaseEntity
{
    public int PropertyId { get; set; }
    public string Url { get; set; }
    public bool IsMain { get; set; }

    public Property Property { get; set; }
}
