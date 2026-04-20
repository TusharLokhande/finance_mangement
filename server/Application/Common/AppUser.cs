using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common
{
    public class AppUser
    {
        public Guid Id { get; set; } 
        public string AzureOid { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}
