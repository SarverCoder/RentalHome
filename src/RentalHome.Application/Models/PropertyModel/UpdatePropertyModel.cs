﻿namespace RentalHome.Application.Models.PropertyModel;
public class UpdatePropertyModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }

    public List<int> PropertyAmenityIds { get; set; } = new();
}