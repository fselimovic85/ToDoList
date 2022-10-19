using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TO_DO_LIST.Data;
using TO_DO_LIST.Dtos.Daily_List;
using TO_DO_LIST.Models;

namespace TO_DO_LIST.Service.DailyListService
{
    public class DailyListService : IDailyListService
    {

        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public DailyListService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        //Get UserId after authorised        
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User
                .FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<List<GetDailyListDto>>> CreateDaliyList(CreateDailyListDto newDailyListDto)
        {
            var serviceResponse = new ServiceResponse<List<GetDailyListDto>>();
            DailyList dailylist = _mapper.Map<DailyList>(newDailyListDto);

            dailylist.UserId = GetUserId();
            
            _context.DailyList.Add(dailylist);
            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.DailyList
                                 .Where(c=>c.User.Id==GetUserId())
                                 .Select(c => _mapper.Map<GetDailyListDto>(c))
                                 .ToListAsync();
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetDailyListDto>>> DeleteDaliyList(int id)
        {
            ServiceResponse<List<GetDailyListDto>> response = new ServiceResponse<List<GetDailyListDto>>();

            try
            {
                var _dailyList = await _context.DailyList.FirstOrDefaultAsync(c => c.Id == id && c.UserId==GetUserId());
               
                if (_dailyList == null)
                    throw new Exception("UserId from logined user is Incorect");

                _context.DailyList.Remove(_dailyList);
                await _context.SaveChangesAsync();

                response.Data = _context.DailyList.Select(c => _mapper.Map<GetDailyListDto>(c)).ToList();

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetDailyListDto>>> GetAllDailyList()
        {
            var response=new ServiceResponse<List<GetDailyListDto>>();
            var dbDailyList = await _context.DailyList.ToListAsync();
            response.Data=dbDailyList.Select(d => _mapper.Map<GetDailyListDto>(d)).ToList();
            return response;
        }

        public async Task<ServiceResponse<GetDailyListDto>> UpdateDaliyList(int Id, UpdateDailyListDto updateDailyListDto)
        {
            ServiceResponse<GetDailyListDto> response = new ServiceResponse<GetDailyListDto>();
          
            try
            {  
                var _dailylist = await _context.DailyList.FirstOrDefaultAsync(c => c.Id == Id && c.UserId == GetUserId());

                if (_dailylist == null)
                    throw new Exception("UserId from logined user is Incorect");

                _dailylist.Title = updateDailyListDto.Title;
                _dailylist.Description = updateDailyListDto.Description;
                _dailylist.CreatedDate = updateDailyListDto.CreatedDate;
                
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetDailyListDto>(_dailylist);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
        public async Task<ServiceResponse<List<GetDailyListDto>>> GetAllDailyListWithPage(int page, DateTime date, string title)
        {
            var serviceResponse = new ServiceResponse<List<GetDailyListDto>>();
           
            var skippedLists = (page - 1) * 10;
            var numberPerPage = int.Parse(_configuration.GetSection("Pagination:NumberPerPage").Value); // take from configuration;

            serviceResponse.Data = await _context.DailyList
                                     .Where(c => c.UserId == GetUserId() && c.CreatedDate <= date && c.Title==title)
                                   //.Where(c => c.UserId == GetUserId() && c.CreatedDate <date && c.Title.Contains(title,StringComparison.InvariantCultureIgnoreCase))
                                     .Skip(skippedLists)
                                     .Take(numberPerPage)
                                     .Select(c => _mapper.Map<GetDailyListDto>(c)) .ToListAsync();

            return serviceResponse;
        }
    }
}
