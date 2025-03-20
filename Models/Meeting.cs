using System;
using System.Collections.Generic;

namespace MeetingAPI.Models;

public partial class Meeting
{
    public int MeetingId { get; set; }

    public int MeetingTypeId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime MeetingDate { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public string Location { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    //public virtual ICollection<MeetingParticipant> MeetingParticipants { get; set; } = new List<MeetingParticipant>();

    //public virtual MeetingType MeetingType { get; set; } = null!;
}
