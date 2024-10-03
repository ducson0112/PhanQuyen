using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PhanQuyenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public UserRoleController(DataContext dataContext) 
        {
            _dataContext = dataContext;
        }

        [HttpGet("{userId}")]
        public async Task<Array> GetRoles([FromRoute] int userId)
        {
            var roleIds = await _dataContext.UserRoles.Where(x => x.UserId == userId).Select(x => x.RoleId).ToArrayAsync();
            return roleIds;
        }

        [HttpPut("{userId}")]
        public async Task<ActionResult<User_Role>> changeRole([FromRoute] int userId, int[] roleIds)
        {
            var userroles = await _dataContext.UserRoles.Where(x => x.UserId == userId).ToArrayAsync();
            if (userroles.Any())
            {
                for (int i = 0; i < userroles.Length; i++)
                {
                    _dataContext.UserRoles.Remove(userroles[i]);
                    await _dataContext.SaveChangesAsync();
                }
            }

            for (int i = 0; i < roleIds.Length; i++)
            {
                var newuserrole = new User_Role
                {
                    UserId = userId,
                    RoleId = roleIds[i]
                };
                await _dataContext.UserRoles.AddAsync(newuserrole);
                await _dataContext.SaveChangesAsync();
            }
            return Ok();
        }
    }
}
