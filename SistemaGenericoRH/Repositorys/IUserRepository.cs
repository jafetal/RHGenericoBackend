using SistemaGenericoRH.Dtos;
using SistemaGenericoRH.Models;

namespace SistemaGenericoRH.Repositorys
{
    public interface IUserRepository : IRepository<User>
    {
        public IEnumerable<UserDto> GetDto();
        public UserDto GetDto(int idUser);
        public IEnumerable<User> Get();
        public User Get(int idUser);
        public User Get(UserDto userDto);
    }
}
