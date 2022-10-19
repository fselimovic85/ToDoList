using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Policy;
using TO_DO_LIST.Data;
using TO_DO_LIST.Dtos.Tasks_of_List;
using TO_DO_LIST.Dtos.User;
using TO_DO_LIST.Models;

namespace TO_DO_LIST.Service.TastkTimeConverterService
{
    public class TastkTimeConverterService: ITastkTimeConverterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TastkTimeConverterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User
        .FindFirstValue(ClaimTypes.NameIdentifier));

        private int GetCurentUserTimeZone(int UserId)
        {
            var currentUser = _context.User
                               .SingleOrDefault(d => d.Id == UserId);

            return currentUser.TimeZone;
        }
        public async Task<ServiceResponse<List<GetTasksDto>>> GetAllFilteredTasks(int list_id, bool done, DateTime dedline)
        {
            var response = new ServiceResponse<List<GetTasksDto>>();

            response.Data = await _context.Task
                                  .Where(c => c.DailyListId == list_id && c.Done == done && c.Deadline == dedline)
                                  .Select(c => _mapper.Map<GetTasksDto>(c)).ToListAsync();
            return response;
        }
        public async Task<ServiceResponse<GetTasksDto>> UpdateTasksInsertDone(int Id, bool done, UpdateTasksDoneDto updateTaskListDoneDto)
        {
            ServiceResponse<GetTasksDto> response = new ServiceResponse<GetTasksDto>();

            try
            {
                var _tasklist = await _context.Task.FirstOrDefaultAsync(c => c.Id == Id);

                int myZone = GetCurentUserTimeZone(GetUserId());

                _tasklist.Done = done;
                _tasklist.AddedToDone = DateTime.UtcNow.AddHours(-myZone);

                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetTasksDto>(_tasklist);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
        public async Task<ServiceResponse<GetTasksDto>> UpdateTasksSelectDone(int Id, bool done)
        {
            ServiceResponse<GetTasksDto> response = new ServiceResponse<GetTasksDto>();

            try
            {
                var _tasklistRestore = await _context.Task.FirstOrDefaultAsync(c => c.Id == Id);

                int myZoneRestore = GetCurentUserTimeZone(GetUserId());

                DateTime getAddedToDan= Convert.ToDateTime(_tasklistRestore.AddedToDone);
                _tasklistRestore.Done = done;
                _tasklistRestore.Deadline = getAddedToDan.AddHours(myZoneRestore);

                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetTasksDto>(_tasklistRestore);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetTasksDto>>> GetAllUserdDonedTasksForUser()
        {
            var LoginedUserTask = new ServiceResponse<List<GetTasksDto>>();
            int userTimeZone= GetCurentUserTimeZone(GetUserId());

            //Get All task which is user created
            LoginedUserTask.Data = await _context.Task
                             .Include(r => r.DailyList)
                             .Where(k => k.DailyList.UserId == GetUserId())
                             .Select(d => _mapper.Map<GetTasksDto>(d)).ToListAsync();

            foreach (var taskofUser in LoginedUserTask.Data)
            {
                taskofUser.Dedline = taskofUser.AddedToDone.AddHours(userTimeZone);
            }
            await _context.SaveChangesAsync();

            return LoginedUserTask;
        }
    }
}
