using AutoMapper;
using RentalHome.Application.Models.Tenant;
using RentalHome.Core.Entities;

namespace RentalHome.Application.MappingProfiles;

public class TenantProfiles : Profile
{
    public TenantProfiles()
    {
        CreateMap<CreateTenantModel, Tenant>();
        CreateMap<Tenant,CreateTenantModel>();
    }

}
