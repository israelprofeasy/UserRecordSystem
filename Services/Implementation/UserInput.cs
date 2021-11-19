using System;
using System.Collections.Generic;
using System.Text;
using UserRecordKeepingSystem.Commons;
using UserRecordKeepingSystem.Models;
using UserRecordKeepingSystem.Models.DTOs;
using UserRecordKeepingSystem.Repositories.FileRepository.Interface;
using UserRecordKeepingSystem.Repositories.InMemoryRepository.Interface;
using UserRecordKeepingSystem.Services.Interface;

namespace UserRecordKeepingSystem.Services
{
   public class UserInput : IUserInput
    {
        private readonly IFileRepository _fileRepo;
        private readonly IInMemoryRepository _inMemoryRepo;
        private readonly ILogger _logger;
        
        
        public UserInput(IFileRepository fileRepo, IInMemoryRepository inMemoryRepo, ILogger logger)
        {
            _fileRepo = fileRepo;
            _inMemoryRepo = inMemoryRepo;
            _logger = logger;
        }

        public void AddUser()
        {
            
            bool count = true;
            while (count)
            {
                User input = GetInput();
                

                   var record = new UserDto()
                    {
                        Name = input.FirstName + " " + input.LastName,
                        PhoneNumber = input.PhoneNumber,
                        Email = input.Email,
                        GitHubUrl = input.GitHubUrl
                    };
                try
                {

                    var result = _fileRepo.AddUser(record).Result;

                    _inMemoryRepo.AddUser(record);




                    if (result)
                    {

                        Console.WriteLine("user Added successfully!");
                        Console.Write("Do you want to add another course? Y/N : ");
                        string choice = Console.ReadLine();
                        if (choice == "Y" || choice == "y" || choice == "yes" || choice == "Yes")
                        {
                            count = true;
                        }
                        else
                        {
                            count = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.Log(ex.Message);

                    Console.WriteLine("Operation to add new user failed!");
                }
            }
            Console.Clear();
        }

        private static User GetInput()
        {
           // bool res = false;
            User input = new User();
            Console.Write("Enter First Name : ");
            input.FirstName = Utilities.RemoveDigitFromStart(Console.ReadLine());
            input.FirstName = Utilities.FirstCharacterToUpper(input.FirstName);

            Console.Write("Enter LastName: ");
            input.LastName =  Utilities.RemoveDigitFromStart(Console.ReadLine());
            input.LastName = Utilities.FirstCharacterToUpper(input.LastName);

            PhoneNumber:
            Console.Write("Enter Phone number: ");
            input.PhoneNumber = Console.ReadLine();
            if (!Utilities.ValidatePhoneNumber(input.PhoneNumber))
            {
                Console.WriteLine("Phone number must 11 digits");
                goto PhoneNumber;
            }

            Email:
            Console.Write("Enter Email Address: ");
            input.Email = Console.ReadLine();
            if (!Utilities.ValidateEmail(input.Email))
            {
                Console.WriteLine("Invalid Email format");
                goto Email;
            }

            GitHub:
            Console.Write("Enter GitHub URL: ");
            input.GitHubUrl = Console.ReadLine();
            if (!Utilities.IsValidGitURL(input.GitHubUrl))
            {
                Console.WriteLine("Invalid GitHub Url");
                goto GitHub;
            }
            return input;
        }

        public void DeleteUser()
        {
            try
            {
                Console.WriteLine("Enter the user Id to be deleted");
                string id = Console.ReadLine();
                bool sucess = _fileRepo.DeleteAuser(_fileRepo.SelectUserById(id).Result[0]).Result;
                if (sucess)
                    Console.WriteLine("User Deleted!");
            }
            catch (Exception ex) { _logger.Log(ex.Message); }
        }

        public void RetrieveUser()
        {
            try
            {

            var userRecords = _fileRepo.RetreiveAllUsers().Result;
            Print(userRecords);
            }
            catch(Exception ex)
            {
                _logger.Log(ex.Message);
            }
        }

        public static void Print(List<UserDto> users)
        {

            if (users.Equals(null))
                throw new Exception("Report is enpty!");

            int widthOfTable = 85;
            Console.Clear();

            Utilities.PrintLine(widthOfTable);
            Utilities.PrintRow(widthOfTable, "NAME", "PHONE NUMBER", "EMAIL", "GITHUB URL");
            Utilities.PrintLine(widthOfTable);

            
            
                foreach (var record in users)
                {

                    Utilities.PrintRow(widthOfTable, record.Name, record.PhoneNumber,
                        record.Email, record.GitHubUrl);
                }
            

            Utilities.PrintLine(widthOfTable);

           
            //Console.ReadLine();
        }

        public void FilterUserByName()
        {
            Console.WriteLine("Enter the Name");
            string name = Console.ReadLine();
            try
            {

            var result = _fileRepo.SelectUserByName(name).Result;
            Print(result);
            }
            catch(Exception ex)
            {
                _logger.Log(ex.Message);
            }
        }

        public void FilterUserById()
        {
            Console.WriteLine("Enter the user Id");
            string id = Console.ReadLine();
            try
            {

            var result = _fileRepo.SelectUserById(id).Result;
            Print(result);
            }
            catch(Exception ex)
            {
                _logger.Log(ex.Message);
            }
        }

        public void EditUser()
        {
            Console.WriteLine("Enter the user Id");
            string id = Console.ReadLine();
            try
            {

            var result = _fileRepo.SelectUserById(id).Result;
            var input = GetInput();
            result[0].Name = input.FirstName + " " + input.LastName;
            result[0].Email = input.Email;
            result[0].PhoneNumber = input.PhoneNumber;
            result[0].GitHubUrl = input.GitHubUrl;
            _fileRepo.UpdateUser(result[0]);
            Console.WriteLine("Update successful");
            }
            catch(Exception ex)
            {
                _logger.Log(ex.Message);
            }
        }

        public void DeleteAllUser()
        {
            try
            {

            _fileRepo.DeleteAllUsers();
            Console.WriteLine("All Users Deleted!");
            }
            catch (Exception ex) 
            {
                _logger.Log(ex.Message);
            }
        }
        public void DeleteListOfUsers()
        {
            var list = DisplayListofUser();
            Console.WriteLine(" Do you want to permanently delete the above list of user? Y/N ");
            string delete = Console.ReadLine();
            if (delete == "Y" || delete == "y" || delete == "yes" || delete == "Yes")
            {
                try
                {

                _fileRepo.DeleteListOfUsers(list);
                }
                catch(Exception ex)
                {
                    _logger.Log(ex.Message);
                }
            }
            Console.WriteLine("List Successfully Deleted");

        }
        
        public List<string> DisplayListofUser()
        {
            List<string> list = new List<string>();
            bool count = true;
            while (count)
            {
                Console.WriteLine("Enter the list of user IDs to be displayed");
                Console.Write("Your input: ");
                list.Add(Console.ReadLine());
                Console.WriteLine("Do you want to add to the list?Y/N");
                string choice = Console.ReadLine();
                if (choice == "Y" || choice == "y" || choice == "yes" || choice == "Yes")
                {
                    count = true;
                }
                else
                {
                    count = false;
                }

            }
            try
            {
                 Print(_fileRepo.ListOfUsersById(list).Result);

            }
            catch (Exception ex)
            {
                _logger.Log(ex.Message);
            }
            return list;


        }
    }
}
