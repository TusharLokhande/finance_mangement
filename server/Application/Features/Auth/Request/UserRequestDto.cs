using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Auth.Request
{
    public record UserRequestDto
    {
        public string Name { get; set; }    
        public string Email { get; set; }
        public string Oid { get; set; }
    }
}
