using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRecordKeepingSystem.Commons;
using UserRecordKeepingSystem.Models.DTOs;
using UserRecordKeepingSystem.Repositories.FileRepository.Interface;

namespace UserRecordKeepingSystem.Repositories.FileRepository.Implementation
{
    class FileRepository : IFileRepository

    {

        readonly string path = Utilities.GetApsolutePath("/UserRecords.txt");
        public Task<bool> AddUser(UserDto record)
        {

            using (StreamWriter sw = new StreamWriter(path, true))
            {
                

                    var line = "";
                    line += record.Id + ",";
                    line += record.Name + ",";
                    line += record.PhoneNumber + ",";
                    line += record.Email + ",";
                    line += record.GitHubUrl;

                    sw.WriteLine(line);
                

                sw.WriteLine(sw.NewLine);

            }
            return Task.Run(()=>true);

        }

        public void DeleteAllUsers()
        {
            File.Delete(path);
        }

        public Task<bool> DeleteAuser(UserDto user)
        {
            
            var records = File.ReadAllLines(path).ToList();
            for (int i = 0; i < records.Count; i++)
            {
                var line = records[i].Split(",");
                if (line[0] == user.Id)
                {
                    records.Remove(records[i]);

                }

            }
            File.WriteAllLines(path, records);
            return Task.Run(()=>true);
        }

      

        public Task<List<UserDto>> RetreiveAllUsers()
        {
            using (StreamReader sr = new StreamReader(path))
            {
             
                List<UserDto> users = new List<UserDto>();
 
                var result = sr.ReadToEnd();

                var splittedByNewLine = result.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var item in splittedByNewLine)
                {
                    if (!item.Equals(""))
                    {
                        var splittedItem = item.Split(",");


                        users.Add(
                            new UserDto { Id = splittedItem[0], Name = splittedItem[1], PhoneNumber = splittedItem[2], Email = splittedItem[3], GitHubUrl = splittedItem[4] });
                            
                        
                    }
                }
                return Task.Run(()=>users);
            }
        }

        public async Task<List<UserDto>> SelectUserById(string id)
        {
            var users= await RetreiveAllUsers();
            int numberOfRows = RowCount();
            if (numberOfRows < 1)
                throw new Exception("No record found, table is empty!");

            var recordsWithSameId= new List<UserDto>();
        
            for (int i = 0; i < numberOfRows; i++)
            {
               
                    if (users[i].Id.Equals(id))
                    {
                        recordsWithSameId.Add(users[i]);

                    }
                   
                
            }


            if (recordsWithSameId.Equals(null))
                throw new Exception($"No record found with {id}");

            return recordsWithSameId;
        }

        public async Task<List<UserDto>> SelectUserByName(string userName)
        {
           var users = await RetreiveAllUsers();
            int numberOfRows = RowCount();
            if (numberOfRows < 1)
                throw new Exception("No record found, table is empty!");

            var recordsWithSameName = new List<UserDto>();
            int backCounter = (numberOfRows - 1);
            for (int i = 0; i < numberOfRows; i++)
            {
                if (users[i].Name.Contains(userName))
                {
                    recordsWithSameName.Add(users[i]);
                }

                if (users[backCounter].Name.Equals(userName))
                {
                    recordsWithSameName.Add(users[backCounter]);
                }
                backCounter--;
            }


            if (recordsWithSameName.Equals(null))
                throw new Exception($"No record found with {userName}");

            return recordsWithSameName;
                   }
        public  int RowCount()
        {
            return RetreiveAllUsers().Result.Count;
        }
        public async Task<bool> DeleteListOfUsers(List<string> ids)
        {
            var data = await RetreiveAllUsers();
            for (int i = 0; i < data.Count; i++)
            {
                foreach (var id in ids)
                {
                    if (data[i].Id == id)
                    {
                        await DeleteAuser(data[i]);
                    }
                }
            }
            return true;
        }


        public async Task<List<UserDto>> ListOfUsersById(List<string> ids)
        {
            var records = new List<UserDto>();
            var data = await RetreiveAllUsers();
            for (int i = 0; i < data.Count; i++)
            {
                foreach (var id in ids)
                {
                    if (data[i].Id == id)
                    {
                        records.Add(data[i]);
                    }
                }
            }
            return records;
        }
        public Task<bool> UpdateUser(UserDto user)
        {
            int count = RowCount();
            if (count < 1) throw new Exception("No record found, table is empty!");

            var records = File.ReadAllLines(path);
            for(int i = 0; i < records.Length; i++)
            {
                var line = records[i].Split(",");
                if(line[0]== user.Id)
                {
                    records[i] = $"{user.Id}, {user.Name}, {user.PhoneNumber}, {user.Email}, {user.GitHubUrl}";

                }

            }
            File.WriteAllLines(path, records);


            return Task.Run(()=>true);
        }

       
       
    }
}
