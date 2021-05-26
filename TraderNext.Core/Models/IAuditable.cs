using System;

namespace TraderNext.Core.Models
{
    public interface IAuditable
    {
        DateTime CreatedTime { get; set; }

        long CreatedBy { get; set; }

        DateTime ModifiedTime { get; set; }

        long ModifiedBy { get; set; }
    }
}
