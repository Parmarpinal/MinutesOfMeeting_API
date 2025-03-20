using System;
using System.Collections.Generic;

namespace MeetingAPI.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string MobileNo { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public int Age { get; set; }

    public string City { get; set; } = null!;

    public string? ImagePath { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    //public virtual ICollection<MeetingParticipant> MeetingParticipants { get; set; } = new List<MeetingParticipant>();
}
