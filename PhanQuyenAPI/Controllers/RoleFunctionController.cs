using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PhanQuyenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleFunctionController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public RoleFunctionController(DataContext dataContext) {
            _dataContext= dataContext;
        }

        [HttpGet("{RoleId}")]
        public async Task<ActionResult<int[]>> GetFucntionIdByRoleId([FromRoute] int RoleId)
        {
            var role = await _dataContext.Roles.FirstOrDefaultAsync(x => x.Id == RoleId);
            if (role == null)
            {
                return NotFound();
            }
            else
            {
                var array = await _dataContext
                    .RoleFunctions
                    .Where(x => x.RoleId == RoleId)
                    .Select(x => x.FunctionId)
                    .ToArrayAsync();
                return Ok(array);
            }
        }

        [HttpPut("{RoleId}")]
        public async Task<ActionResult<Role_Function>> UpdateRoleFunction([FromRoute]int RoleId, [FromBody] int[] array)
        {
            var roles = await _dataContext.RoleFunctions.Where(x => x.RoleId == RoleId).ToArrayAsync();
            if (roles == null)
            {
                return NotFound();
            }
            else
            {
                for (var i = 0; i < roles.Length; i++)
                {
                    _dataContext.RoleFunctions.Remove(roles[i]);
                    await _dataContext.SaveChangesAsync();
                }
                for (var i = 0; (i < array.Length); i++)
                {
                    var rolefunction = new Role_Function
                    {
                        RoleId = RoleId,
                        FunctionId = array[i]
                    };
                    await _dataContext.RoleFunctions.AddAsync(rolefunction);
                    await _dataContext.SaveChangesAsync();
                }
                return Ok();
            }
        }
    }
}
