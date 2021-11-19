using System;
using System.Collections.Generic;
using System.Text;

namespace UserRecordKeepingSystem.Models.DTOs
{
    public class UserDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string GitHubUrl { get; set; }

    }
}
