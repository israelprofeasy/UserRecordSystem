using System;
using System.Collections.Generic;
using System.Text;
using UserRecordKeepingSystem.Models.DTOs;

namespace UserRecordKeepingSystem.Repositories.InMemoryRepository
{
    class InMemoryStore
    {
        public static List<UserDto> Users { get; set; } = new List<UserDto>();
    }
}
