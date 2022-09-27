using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaGenericoRH.Dtos;
using SistemaGenericoRH.Services;

namespace SistemaGenericoRH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("get")]
        public IEnumerable<UserDto> GetUser()
        {
            return userService.Get();
        }

        [HttpGet("get/{idUser}")]
        public UserDto GetUserByID(int idUser)
        {
            return userService.Get(idUser);
        }

        [HttpPost("create")]
        public int CreateUser(UserDto userDto)
        {
            return userService.Create(userDto);
        }

        [HttpPut("update")]
        public void UpdateUser(UserDto userDto)
        {
            userService.Update(userDto);
        }

        [HttpDelete("delete/{idUser}")]
        public void CreateUser(int idUser)
        {
            userService.Deactivate(idUser);
        }

    }
}
