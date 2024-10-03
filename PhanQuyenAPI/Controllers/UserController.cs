using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhanQuyenAPI.DTOs;

namespace PhanQuyenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly DataContext _dataContext;
        public UserController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUser() 
        {
            var users = await _dataContext.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<User>> GetUserById([FromRoute] int Id)
        {
            var user = await _dataContext.Users.SingleOrDefaultAsync(x => x.Id == Id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user);
            }
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(UserDTO userDTO)
        {
            var user = new User
            {
                Name = userDTO.Name
            };
            await _dataContext.Users.AddAsync(user);
            await _dataContext.SaveChangesAsync();  
            return Ok(user);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<User>> UpdateUser([FromRoute] int Id, UserDTO userDTO)
        {
            var user = _dataContext.Users.SingleOrDefault(x => x.Id == Id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                user.Name = userDTO.Name;
                await _dataContext.SaveChangesAsync();
                return Ok(user);
            }
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<User>> RemoveUser([FromRoute] int Id)
        {
            var user = _dataContext.Users.SingleOrDefault(x => x.Id == Id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                _dataContext.Users.Remove(user);
                await _dataContext.SaveChangesAsync();
                return Ok(user);
            }
        }
    }
}
