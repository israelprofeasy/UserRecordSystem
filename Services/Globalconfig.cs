using System;
using System.Collections.Generic;
using System.Text;
using UserRecordKeepingSystem.Commons;
using UserRecordKeepingSystem.Repositories.FileRepository.Implementation;
using UserRecordKeepingSystem.Repositories.FileRepository.Interface;
using UserRecordKeepingSystem.Repositories.InMemoryRepository.Implementation;
using UserRecordKeepingSystem.Repositories.InMemoryRepository.Interface;
using UserRecordKeepingSystem.Services.Interface;

namespace UserRecordKeepingSystem.Services
{
    public static class Global_config
    {
        public static IInMemoryRepository _inMemoryRepo;
        public static IFileRepository _fileRepo;
        public static IUserInput _userInput;
        public static ILogger logger;
        public static void Instantiate()
        {
            _inMemoryRepo = new InMemoryRepository();
            _fileRepo = new FileRepository();
            logger = new Logger();
            _userInput = new UserInput(_fileRepo, _inMemoryRepo, logger);

        }
    }
}
