using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TO_DO_LIST.Dtos.Tasks_of_List;
using TO_DO_LIST.Models;
using TO_DO_LIST.Service.TaskService;
using TO_DO_LIST.Service.TastkTimeConverterService;

namespace TO_DO_LIST.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("[controller]")]
    public class TastkTimeConverterController : ControllerBase
    {
        private readonly ITastkTimeConverterService _testService;
        public TastkTimeConverterController(ITastkTimeConverterService testService)
        {
            _testService = testService;

        }

        [HttpPut("UpdateInsertDone/{Id}/{done}")]
        public async Task<ActionResult<ServiceResponse<GetTasksDto>>> UpdateTasks(int Id, bool done, UpdateTasksDoneDto updateTaskListDoneDto)
        {
            var response = await _testService.UpdateTasksInsertDone(Id, done, updateTaskListDoneDto);

            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);

        }

        [HttpPut("UpdateTasksSelectDone/{Id}/{done}")]
        public async Task<ActionResult<ServiceResponse<GetTasksDto>>> UpdateTasksSelectDone(int Id, bool done)
        {
            var response = await _testService.UpdateTasksSelectDone(Id, done);

            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);

        }

        [HttpGet("GetAllFetshedTasks/{list_id}/{done}/{dedline}")]
        public async Task<ActionResult<ServiceResponse<List<GetTasksDto>>>> GetAllFilteredTasks(int list_id, bool done, DateTime dedline)
        {

            return Ok(await _testService.GetAllFilteredTasks(list_id, done, dedline));
        }

        [HttpGet("GetAllUserdDonedTasksForUser")]
        public async Task<ActionResult<ServiceResponse<List<GetTasksDto>>>> GetAllUserdDonedTasksForUser()
        {
            return Ok(await _testService.GetAllUserdDonedTasksForUser());
        }

    }
}
