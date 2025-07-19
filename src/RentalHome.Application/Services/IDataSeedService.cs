namespace RentalHome.Application.Services;

public interface IDataSeedService
{
    Task SeedRolesAndPermissionsAsync();
}