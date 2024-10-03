using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhanQuyenAPI.DTOs;

namespace PhanQuyenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        public readonly DataContext _dataContext;
        public RoleController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Role>>> GetAllRole()
        {
            var roles = await _dataContext.Roles.ToListAsync();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRoleById([FromRoute] int id)
        {
            var role = await _dataContext.Roles.SingleOrDefaultAsync(x => x.Id == id);
            if (role == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(role);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Role>> CreateRole(RoleDTO roleDTO)
        {
            var role = new Role
            {
                Name = roleDTO.Name
            };
            await _dataContext.Roles.AddAsync(role);
            await _dataContext.SaveChangesAsync();
            return Ok(role);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Role>> UpdateRole([FromRoute] int id, RoleDTO roleDTO)
        {
            var role = _dataContext.Roles.SingleOrDefault(x => x.Id == id);
            if (role == null)
            {
                return NotFound();
            }
            else
            {
                role.Name = roleDTO.Name;
                await _dataContext.SaveChangesAsync();
                return Ok(role);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Role>> RemoveRole([FromRoute] int id)
        {
            var role = _dataContext.Roles.SingleOrDefault(x => x.Id == id);
            if (role == null)
            {
                return NotFound();
            }
            else
            {
                _dataContext.Roles.Remove(role);
                await _dataContext.SaveChangesAsync();
                return Ok(role);
            }
        }
    }
}
