

using System.ComponentModel;

namespace Domain.Enums
{

    public enum LentStatus
    {
        [Description("Pending")]
        Pending = 1,

        [Description("Paid")]
        Paid = 2
    }
}