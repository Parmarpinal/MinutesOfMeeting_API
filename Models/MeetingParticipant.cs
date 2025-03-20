using System;
using System.Collections.Generic;

namespace MeetingAPI.Models;

public partial class MeetingParticipant
{
    public int ParticipantId { get; set; }

    public int UserId { get; set; }

    public int MeetingId { get; set; }

    public bool? IsInvited { get; set; }

    public bool? IsActualPresent { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual Meeting Meeting { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
