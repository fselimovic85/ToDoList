using TO_DO_LIST.Dtos.Tasks_of_List;
using TO_DO_LIST.Models;

namespace TO_DO_LIST.Service.TastkTimeConverterService
{
    public interface ITastkTimeConverterService
    {
        Task<ServiceResponse<List<GetTasksDto>>> GetAllFilteredTasks(int list_id, bool done, DateTime dedline);
        Task<ServiceResponse<List<GetTasksDto>>> GetAllUserdDonedTasksForUser();
        Task<ServiceResponse<GetTasksDto>> UpdateTasksInsertDone(int Id, bool done, UpdateTasksDoneDto updateTaskListDto);
        Task<ServiceResponse<GetTasksDto>> UpdateTasksSelectDone(int Id, bool done);

    }
}
