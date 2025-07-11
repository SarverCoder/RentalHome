﻿using RentalHome.Core.Common;
using RentalHome.Core.Enums;

namespace RentalHome.Core.Entities;

public class Notification : BaseEntity
{

    public int UserId { get; set; }

    public string Content { get; set; }

    public NotificationType NotificationType { get; set; }

    public bool IsRead { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int RelatedEntityId { get; set; }

    public User User { get; set; }
}

