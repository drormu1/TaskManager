using Microsoft.AspNetCore.Mvc;
using TaskManager.Data.Entities;
using TaskManager.Data.Repositories;
using TaskManager.Logic.DTOs;

namespace TaskManager.APIServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<User> users = await _userRepository.GetAllAsync();
            IEnumerable<UserDto> dtos = users.Select(u => new UserDto
            {
                Id = u.Id,
                UserName = u.UserName
            });
            return Ok(dtos);
        }
    }
}