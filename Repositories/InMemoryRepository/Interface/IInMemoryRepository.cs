using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserRecordKeepingSystem.Models.DTOs;

namespace UserRecordKeepingSystem.Repositories.InMemoryRepository.Interface
{
    public interface IInMemoryRepository
    {
        public Task<bool> AddUser(UserDto user);
        public List<UserDto> RetreiveAllUsers();
        
      
        public int RowCount();

    }
}
