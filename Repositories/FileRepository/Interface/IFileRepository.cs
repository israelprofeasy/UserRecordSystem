using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserRecordKeepingSystem.Models.DTOs;

namespace UserRecordKeepingSystem.Repositories.FileRepository.Interface
{
    public interface IFileRepository
    {
        public Task<bool> AddUser(UserDto user);
        public Task<bool> UpdateUser(UserDto user);
        public Task<List<UserDto>> RetreiveAllUsers();
        public Task<List<UserDto>> SelectUserByName(string userName);
        public Task<List<UserDto>> SelectUserById(string id);
        public void DeleteAllUsers();
        public Task<bool> DeleteAuser(UserDto user);
        
        public int RowCount();
        public Task<List<UserDto>> ListOfUsersById(List<string> ids);
        public Task<bool> DeleteListOfUsers(List<string> ids);

    }
}