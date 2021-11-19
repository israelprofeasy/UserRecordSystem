using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserRecordKeepingSystem.Models.DTOs;
using UserRecordKeepingSystem.Repositories.InMemoryRepository.Interface;

namespace UserRecordKeepingSystem.Repositories.InMemoryRepository.Implementation
{
    class InMemoryRepository : IInMemoryRepository

    {
        public Task<bool> AddUser(UserDto users)
        {
            int numberOfRowsBefore = this.RowCount();

            
            
                
                InMemoryStore.Users.Add(users);
            

            int numberOfRowsAfter = this.RowCount();

            if (numberOfRowsAfter <= numberOfRowsBefore)
                return Task.Run(() => false);

            return Task.Run(() => true);
        }


       

        public List<UserDto> RetreiveAllUsers()
        {
            throw new NotImplementedException();
        }

        public int RowCount()
        {
            return InMemoryStore.Users.Count;
        }

      
        }
    }

