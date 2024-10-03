using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhanQuyenAPI.DTOs;

namespace PhanQuyenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FunctionController : ControllerBase
    {
        public readonly DataContext _dataContext;
        public FunctionController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Function>>> GetAllFunction()
        {
            var functions = await _dataContext.Functions.ToListAsync();
            return Ok(functions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Function>> GetFunctionById([FromRoute] int id)
        {
            var function = await _dataContext.Functions
                .FirstOrDefaultAsync(x => x.Id == id);
            if (function == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(function);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Function>> CreateFunction(FunctionDTO functionDTO)
        {
            var function = new Function
            {
                Name = functionDTO.Name,
            };

            await _dataContext.Functions.AddAsync(function);
            await _dataContext.SaveChangesAsync();
            return Ok(function);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Function>> UpdateFunction([FromRoute]int id,  [FromBody]FunctionDTO functionDTO)
        {
            var function = await _dataContext.Functions.FirstOrDefaultAsync(x => x.Id==id);
            if(function == null)
            {
                return BadRequest();
            } else
            {
                function.Name = functionDTO.Name;
                await _dataContext.SaveChangesAsync();
                return Ok(function);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Function>> RemoveFunctionById([FromRoute]int id)
        {
            var function = await _dataContext.Functions.FirstOrDefaultAsync(x =>x.Id==id);
            if (function == null)
            {
                return BadRequest();
            }
            else
            {
                _dataContext.Functions.Remove(function);
                await _dataContext.SaveChangesAsync();
                return Ok(function);
            }
        }
    }
}
