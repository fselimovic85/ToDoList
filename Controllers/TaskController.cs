using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TO_DO_LIST.Dtos.Daily_List;
using TO_DO_LIST.Dtos.Tasks_of_List;
using TO_DO_LIST.Models;
using TO_DO_LIST.Service.DailyListService;
using TO_DO_LIST.Service.TaskService;

namespace TO_DO_LIST.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITasksService _testService;
        public TaskController(ITasksService testService)
        {
            _testService = testService;

        }
               
        [HttpPost("CreateTasks")]
        public async Task<ActionResult<ServiceResponse<List<GetTasksDto>>>> CreateTaskList(CreateTasksDto newTaskList)
        {
            return Ok(await _testService.CreateTaskList(newTaskList));
        }

        [HttpGet("GetAllTasks")]
        public async Task<ActionResult<ServiceResponse<List<GetTasksDto>>>> GetTaskList()
        {

            return Ok(await _testService.GetAllTasks());
        }

        [HttpPut("Update/{Id}")]
        public async Task<ActionResult<ServiceResponse<GetTasksDto>>> UpdateTasks(int Id, UpdateTasksDto updateTaskListDto)
        {
            var responce = await _testService.UpdateTasks(Id, updateTaskListDto);

            if (responce.Data == null)
            {
                return NotFound(responce);
            }
            return Ok(responce);
            
        }
        
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetTasksDto>>>> Delete(int id)
        {
            var response = await _testService.DeleteTaskList(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
      
    }
}
