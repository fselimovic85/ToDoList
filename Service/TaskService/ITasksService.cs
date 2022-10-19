using TO_DO_LIST.Dtos.Daily_List;
using TO_DO_LIST.Dtos.Tasks_of_List;
using TO_DO_LIST.Models;

namespace TO_DO_LIST.Service.TaskService
{
    public interface ITasksService
    {

        Task<ServiceResponse<List<GetTasksDto>>> GetAllTasks();
   
        Task<ServiceResponse<List<GetTasksDto>>> CreateTaskList(CreateTasksDto newTaskListDto);
       
        Task<ServiceResponse<GetTasksDto>> UpdateTasks(int Id, UpdateTasksDto updateTaskListDto);
       
        Task<ServiceResponse<List<GetTasksDto>>> DeleteTaskList(int id);
    }
}
