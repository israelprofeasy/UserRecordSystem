using System;
using UserRecordKeepingSystem.Services;

namespace UserRecordKeepingSystem
{
    class Program
    {
        static void Main(string[] args)
        {

            Global_config.Instantiate();
            var user = new UserInput(Global_config._fileRepo, Global_config._inMemoryRepo, Global_config.logger);
            Console.WriteLine("**************************** USERS RECORD KEEPING**********************");
            bool choice = true;
            while (choice)
            {
               // Console.Clear();
                Console.WriteLine("Press 1 to Register user.");
                Console.WriteLine("Press 2 to View all  user.");
                Console.WriteLine("Press 3 to Edit user.");
                Console.WriteLine("Press 4 to Filter users by name.");
                Console.WriteLine("Press 5 to Filter user by id.");
                Console.WriteLine("Press 6 to Delete All user.");
                Console.WriteLine("Press 7 to Delete a user.");
                Console.WriteLine("Press 8 to Delete List of Users by id.");
                Console.WriteLine("Press 9 to  List of Users by id.");
                Console.WriteLine("Press 0 to exit.");
                Console.Write("Your Input: ");
                int input = Convert.ToInt32(Console.ReadLine());

                //  if (input == 1) UserInput.AddUser();
                if (input == 0) choice = false;
                if (input == 1) user.AddUser();
                if (input == 2) user.RetrieveUser();
                if (input == 3) user.EditUser();
                if (input == 4) user.FilterUserByName();
                if (input == 5) user.FilterUserById();
                if (input == 6) user.DeleteAllUser();
                if (input == 7) user.DeleteUser();
                if (input == 8) user.DeleteListOfUsers();
                if (input == 9) user.DisplayListofUser();


               
            }
        }
    }
}
