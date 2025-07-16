namespace RentalHome.Application.Security.AuthEnums;

[ApplicationEnumDescription]
public enum ApplicationPermissionCode
{
    #region Document

    #region User

    [ApplicationPermissionDescription(ApplicationPermissionGroupCode.User, "User Create")]
    UserCreate,

    [ApplicationPermissionDescription(ApplicationPermissionGroupCode.User, "User Read")]
    UserRead,

    [ApplicationPermissionDescription(ApplicationPermissionGroupCode.User, "User Update")]
    UserUpdate,

    #endregion

    #region Role

    [ApplicationPermissionDescription(ApplicationPermissionGroupCode.Role, "Create Role")]
    CreateRole,

    [ApplicationPermissionDescription(ApplicationPermissionGroupCode.Role, "Get Roles")]
    GetRoles,

    [ApplicationPermissionDescription(ApplicationPermissionGroupCode.Role, "Update RolePermissions")]
    UpdateRolePermissions,

    #endregion

    #region Permission

    [ApplicationPermissionDescription(ApplicationPermissionGroupCode.Permission, "Get Permissions")]
    GetPermissions,

    #endregion

    #region Amenity

    [ApplicationPermissionDescription(ApplicationPermissionGroupCode.Amenity, "Get Amenities")]
    GetAmenities,

    [ApplicationPermissionDescription(ApplicationPermissionGroupCode.Amenity, "Get Amenity")]
    GetAmenity,

    [ApplicationPermissionDescription(ApplicationPermissionGroupCode.Amenity, "Create Amenity")]
    CreateAmenity,

    [ApplicationPermissionDescription(ApplicationPermissionGroupCode.Amenity, "Update Amenity")]
    UpdateAmenity,

    [ApplicationPermissionDescription(ApplicationPermissionGroupCode.Amenity, "Delete Amenity")]
    DeleteAmenity,

    #endregion

    #region Booking

    [ApplicationPermissionDescription(ApplicationPermissionGroupCode.Booking, "Get Booking")]
    GetBooking,

    [ApplicationPermissionDescription(ApplicationPermissionGroupCode.Booking, "Get Bookings")]
    GetBookings,

    [ApplicationPermissionDescription(ApplicationPermissionGroupCode.Booking, "Create Booking")]
    CreateBooking,

    [ApplicationPermissionDescription(ApplicationPermissionGroupCode.Booking, "Delete Booking")]
    DeleteBooking,

    #endregion

    #region District

    [ApplicationPermissionDescription(ApplicationPermissionGroupCode.District, "Get District")]
    GetDistrict,

    [ApplicationPermissionDescription(ApplicationPermissionGroupCode.District, "Get Districts")]
    GetDistricts,

    [ApplicationPermissionDescription(ApplicationPermissionGroupCode.District, "Create District")]
    CreateDistrict,

    [ApplicationPermissionDescription(ApplicationPermissionGroupCode.District, "Update District")]
    UpdateDistrict,

    [ApplicationPermissionDescription(ApplicationPermissionGroupCode.District, "Delete District")]
    DeleteDistrict,


    #endregion

    #region Property

    [ApplicationPermissionDescription(ApplicationPermissionGroupCode.Property, "Get Property")]
    GetProperty,

    [ApplicationPermissionDescription(ApplicationPermissionGroupCode.Property, "Get Properties")]
    GetProperties,

    [ApplicationPermissionDescription(ApplicationPermissionGroupCode.Property, "Create Property")]
    CreateProperty,

    [ApplicationPermissionDescription(ApplicationPermissionGroupCode.Property, "Update Property")]
    UpdateProperty,

    [ApplicationPermissionDescription(ApplicationPermissionGroupCode.Property, "Delete Property")]
    DeleteProperty,

    #endregion

    #region Region

    [ApplicationPermissionDescription(ApplicationPermissionGroupCode.Region, "Get Region")]
    GetRegion,

    [ApplicationPermissionDescription(ApplicationPermissionGroupCode.Region, "Get Regions")]
    GetRegions,

    [ApplicationPermissionDescription(ApplicationPermissionGroupCode.Region, "Create Region")]
    CreateRegion,

    [ApplicationPermissionDescription(ApplicationPermissionGroupCode.Region, "Update Region")]
    UpdateRegion,

    [ApplicationPermissionDescription(ApplicationPermissionGroupCode.Region, "Delete Region")]
    DeleteRegion,


    #endregion


    #endregion
}