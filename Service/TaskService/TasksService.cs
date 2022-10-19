using Microsoft.EntityFrameworkCore;
using TO_DO_LIST.Dtos.Daily_List;
using TO_DO_LIST.Dtos.Tasks_of_List;
using TO_DO_LIST.Models;
using AutoMapper;
using TO_DO_LIST.Data;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TO_DO_LIST.Dtos.User;
using Microsoft.EntityFrameworkCore.Internal;

namespace TO_DO_LIST.Service.TaskService
{
    public class TasksService : ITasksService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TasksService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
       
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User
                .FindFirstValue(ClaimTypes.NameIdentifier));
        
        public async Task<ServiceResponse<List<GetTasksDto>>> CreateTaskList(CreateTasksDto newTaskListDto)
        {
           
            Tasks tasks = _mapper.Map<Tasks>(newTaskListDto);

            var ValidDailyiId = _context.DailyList
                             .Where(d => d.UserId == GetUserId() && d.Id== newTaskListDto.DailyListId)
                             .Select(d => _mapper.Map<GetDailyListDto>(d)).ToList();

            var serviceResponse = new ServiceResponse<List<GetTasksDto>>();

            try
            {
                if (ValidDailyiId.Count==1)
                {
                    tasks.Deadline = DateTime.UtcNow;

                    _context.Task.Add(tasks);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = await _context.Task
                                         .Select(c => _mapper.Map<GetTasksDto>(c))
                                       .ToListAsync();
                }
                else
                {
                    throw new Exception("A logged-in User cannot create a task from someone else's list");
                }
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetTasksDto>>> DeleteTaskList(int id)
        {
            ServiceResponse<List<GetTasksDto>> response = new ServiceResponse<List<GetTasksDto>>();

            try
            {
                Tasks _taskList = await _context.Task.FirstAsync(c => c.Id == id);
                _context.Task.Remove(_taskList);
                await _context.SaveChangesAsync();

                response.Data = _context.Task.Select(c => _mapper.Map<GetTasksDto>(c)).ToList();

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetTasksDto>>> GetAllTasks()
        {
            var response = new ServiceResponse<List<GetTasksDto>>();

            response.Data = await _context.Task
                                  .Select(d => _mapper.Map<GetTasksDto>(d)).ToListAsync();
            return response;
        }

        public async  Task<ServiceResponse<GetTasksDto>> UpdateTasks(int Id, UpdateTasksDto updateTaskListDto)
        {
            ServiceResponse<GetTasksDto> response = new ServiceResponse<GetTasksDto>();
            
            try
            {
                var _tastlist = await _context.Task.FirstOrDefaultAsync(c => c.Id == Id);

                _tastlist.Title = updateTaskListDto.Title;
                _tastlist.Description = updateTaskListDto.Description;
                _tastlist.Deadline = updateTaskListDto.Dedline;

                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetTasksDto>(_tastlist);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

    }
}
