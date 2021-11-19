using System;
using System.Collections.Generic;
using System.Text;
using UserRecordKeepingSystem.Models.DTOs;

namespace UserRecordKeepingSystem.Services.Interface
{
    public interface IUserInput
    {
        public void AddUser();
        public void RetrieveUser();
        public void DeleteUser();
        public void FilterUserByName();
        public void FilterUserById();
        public void EditUser();
        public void DeleteAllUser();


    }
}
