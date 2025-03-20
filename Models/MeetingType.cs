using System;
using System.Collections.Generic;

namespace MeetingAPI.Models;

public partial class MeetingType
{
    public int MeetingTypeId { get; set; }

    public string MeetingTypeName { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    //public virtual ICollection<Meeting> Meetings { get; set; } = new List<Meeting>();
}
