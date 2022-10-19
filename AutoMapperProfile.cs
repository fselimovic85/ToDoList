
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TO_DO_LIST.Dtos.Daily_List;
using TO_DO_LIST.Dtos.Tasks_of_List;
using TO_DO_LIST.Dtos.User;
using TO_DO_LIST.Models;

namespace TO_DO_LIST
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Map for DailyList
            CreateMap<DailyList, GetDailyListDto>();
            CreateMap<CreateDailyListDto, DailyList>();
            CreateMap<UpdateDailyListDto, DailyList>();

            //Map for Tasks
            CreateMap<Tasks, GetTasksDto>();
            CreateMap<CreateTasksDto, Tasks>();
            CreateMap<UpdateTasksDto, Tasks>();
            CreateMap<UpdateTasksDoneDto, Tasks>();

            //Map for DailyList on  GetTasksDto
            CreateMap<DailyList, GetTasksDto>();


        }
    }
}
