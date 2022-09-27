using SistemaGenericoRH.Dtos;
using SistemaGenericoRH.Helpers;
using SistemaGenericoRH.Models;
using SistemaGenericoRH.Repositorys;
using System.Transactions;

namespace SistemaGenericoRH.Services
{
    public class UserService
    {
        private IUserRepository userRepository;
        private UserValidatorService userValidatorService;
        private SimpleAES simpleAES;

        public UserService(IUserRepository userRepository,
            UserValidatorService userValidatorService,
            SimpleAES simpleAES)
        {
            this.userRepository = userRepository;
            this.userValidatorService = userValidatorService;
            this.simpleAES = simpleAES;
        }
        public IEnumerable<UserDto> Get()
        {
            return userRepository.GetDto();
        }
        public UserDto Get(int idUser)
        {
            return userRepository.GetDto(idUser);
        }
        public int Create(UserDto userDto)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                userValidatorService.ValidateCreate(userDto);
                string password = simpleAES.EncryptToString(userDto.Password);
                var newUser = new User
                {
                    Email = userDto.Email,
                    Password = password,
                    Gender = userDto.Gender,
                    User1 = userDto.User1,
                    Status = true
                };

                var idUser = userRepository.Create(newUser).IdUser;
                scope.Complete();

                return idUser;
            }
        }

        public void Update(UserDto userDto)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                userValidatorService.ValidateUpdate(userDto);
                var user = userRepository.Get(userDto.IdUser);

                user.Email = userDto.Email;
                user.Gender = userDto.Gender;
                user.User1 = userDto.User1;

                if (!(userDto.Password == null))
                {
                    user.Password = simpleAES.EncryptToString(userDto.Password);
                }
                userRepository.Update(user);
            }
        }

        public void Deactivate(int idUser)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                userValidatorService.ValidateDelete(idUser);
                var user = userRepository.Get(idUser);
                user.Status = false;
                userRepository.Update(user);
            }
        }
    }
}
