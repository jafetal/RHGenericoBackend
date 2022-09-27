using Microsoft.EntityFrameworkCore;
using SistemaGenericoRH.Dtos;
using SistemaGenericoRH.Models;
using System.Collections.Generic;
using System.Linq;

namespace SistemaGenericoRH.Repositorys
{
    public class UserRepository: Repository<User>, IUserRepository
    {
        public UserRepository(ModelContext context) : base(context)
        {
            base.context = context;
        }

        public IEnumerable<UserDto> GetDto(){
            return context.User
                    .Where(a => a.Status)
                    .Select(a => new UserDto
                        {
                            Email = a.Email,
                            CreateDate = a.CreateDate,
                            Gender = a.Gender,
                            IdUser = a.IdUser,
                            Status = a.Status,
                            User1 = a.User1
                        }
                    );
        }
        public UserDto GetDto(int idUser){
            return context.User
                    .Where(a => a.IdUser == idUser)
                    .Select(a => new UserDto{
                        Email = a.Email,
                        CreateDate = a.CreateDate,
                        Gender = a.Gender,
                        IdUser = a.IdUser,
                        Status = a.Status,
                        User1 = a.User1
                        }
                    ).FirstOrDefault();
        }
        public IEnumerable<User> Get(){
            return context.User
                    .Where(a => a.Status);
        }
        public User Get(int idUser){
            return context.User
                    .Where(a => a.IdUser == idUser)
                    .FirstOrDefault();
        }
        public User Get(UserDto userDto){
            return context.User
                    .Where(a => ( a.User1.ToLower() == userDto.User1.ToLower()
                        || a.Email.ToLower() == userDto.Email.ToLower())
                     )
                    .FirstOrDefault();
        }
    }
}
