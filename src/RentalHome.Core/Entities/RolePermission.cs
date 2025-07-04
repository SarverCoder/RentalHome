﻿using RentalHome.Core.Common;

namespace RentalHome.Core.Entities;

public class RolePermission : BaseEntity
{
    public int RoleId { get; set; }
    public Role Role { get; set; }
    public int PermissionId { get; set; }
    public Permission Permission { get; set; }
}