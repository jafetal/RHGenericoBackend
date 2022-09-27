using SistemaGenericoRH.Dtos;
using SistemaGenericoRH.Helpers;
using SistemaGenericoRH.Models;
using SistemaGenericoRH.Repositorys;

namespace SistemaGenericoRH.Services
{
    public class UserValidatorService
    {
        private IUserRepository UserRepository;

        public UserValidatorService(IUserRepository UserRepository)
        {
            this.UserRepository = UserRepository;
        }

        private readonly string EmailRequiredMessage = "El correo electrónico es requerido";
        private readonly string UserNameRequiredMessage = "El nombre de usuario es requerido";
        private readonly string PasswordRequiredMessage = "La contraseña es requerida";
        private readonly string GenderRequiredMessage = "El género es requerido";

        private readonly string DoesntExistMessage = "El usuario no existe";
        private readonly string AlreadyExistMessage = "Ya existe un usuario registrado con los mismos datos";

        private static readonly int EmailLength = 100;
        private static readonly int UserNameLength = 50;
        private static readonly int PasswordLength = 50;
        private static readonly int GenderLength = 50;

        private readonly string EmailLengthMessage = $"La longitud máxima del correo electrónico son { EmailLength } caracteres";
        private readonly string UserNameLengthMessage = $"La longitud máxima del nombre de usuario son { UserNameLength } caracteres";
        private readonly string InvalidPasswordMessage = $"La contraseña no cumple con los requisitos de seguridad, debe contener por lo menos: 1 mayúscula, 1 minúscula, 1 símbolo y 1 número, y 10 caracteres en total";
        private readonly string InvalidEmailMessage = $"El correo electrónico no es válido";

        public void ValidateCreate(UserDto userDto)
        {
            ValidateRequired(userDto);
            ValidateFormat(userDto);
            ValidarRango(userDto);
            ValidateDuplicated(userDto);
        }

        public void ValidateUpdate(UserDto userDto)
        {
            ValidateRequiredUpdate(userDto);
            ValidateFormat(userDto);
            ValidarRango(userDto);
            ValidateExistence(userDto.IdUser);
            ValidateDuplicated(userDto);
        }

        public void ValidateDelete(int idUser)
        {
            ValidateDependecies(idUser);
        }

        public void ValidateRequired(UserDto userDto)
        {
            Validator.ValidateRequired(userDto.Password, PasswordRequiredMessage);
            Validator.ValidateRequired(userDto.Gender, GenderRequiredMessage);
            Validator.ValidateRequired(userDto.Email, EmailRequiredMessage);
            Validator.ValidateRequired(userDto.User1, UserNameRequiredMessage);
        }

        public void ValidateRequiredUpdate(UserDto userDto)
        {
            Validator.ValidateRequired(userDto.Gender, GenderRequiredMessage);
            Validator.ValidateRequired(userDto.Email, EmailRequiredMessage);
            Validator.ValidateRequired(userDto.User1, UserNameRequiredMessage);
        }

        public void ValidateFormat(UserDto userDto)
        {
            Validator.ValidateEmail(userDto.Email, InvalidEmailMessage);
            if (userDto.Password?.Length > 0 )
            {
                Validator.ValidatePassword(userDto.Password, InvalidPasswordMessage);
            }
        }

        public void ValidarRango(UserDto userDto)
        {
            Validator.ValidateMaxString(userDto.Email, EmailLength, EmailLengthMessage);
            Validator.ValidateMaxString(userDto.Gender, GenderLength, GenderRequiredMessage);
            Validator.ValidateMaxString(userDto.User1, UserNameLength, UserNameRequiredMessage);
            Validator.ValidateMaxString(userDto.Password, PasswordLength, PasswordRequiredMessage);
        }

        public void ValidateExistence(int idUser)
        {
            User user = UserRepository.Get(idUser);

            if (user == null)
            {
                throw new GenericException(DoesntExistMessage);
            }
        }

        public void ValidateDuplicated(UserDto userDto)
        {
            var duplicatedUser = UserRepository.Get(userDto);

            if (duplicatedUser != null && userDto.IdUser != duplicatedUser.IdUser)
            {
                throw new GenericException(AlreadyExistMessage);
            }
        }

        public void ValidateDependecies(int idUser)
        {
            
        }

        public void ValidatePassword(string password)
        {

        }
    }
}
