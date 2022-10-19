using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TO_DO_LIST.Dtos.Daily_List;
using TO_DO_LIST.Dtos.User;
using TO_DO_LIST.Models;
using TO_DO_LIST.Service.DailyListService;

namespace TO_DO_LIST.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("[controller]")]
    public class DailyListController : ControllerBase
    {
        private readonly IDailyListService _dailyListService;
        public DailyListController(IDailyListService dailyListService)
        {
            _dailyListService = dailyListService;

        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetDailyListDto>>>> CreateDailyList(CreateDailyListDto newDailyList)
        {
            return Ok(await _dailyListService.CreateDaliyList(newDailyList));
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetDailyListDto>>>> GetDailyList()
        {

            return Ok(await _dailyListService.GetAllDailyList());
        }
       
        [HttpGet("GetAllUsersListDateTitleFeced/{page}/{date}/{title}")]
        public async Task<ActionResult<List<GetDailyListDto>>> GetAllDailyListWithPage(int page, DateTime date, string title)
        {
            return Ok(await _dailyListService.GetAllDailyListWithPage(page, date, title));
        }

        [HttpPut("Update/{Id}")]
        public async Task<ActionResult<ServiceResponse<GetDailyListDto>>> UpdateDailyList(int Id, UpdateDailyListDto updateDailyList)
        {
            var responce = await _dailyListService.UpdateDaliyList(Id, updateDailyList);

            if (responce.Data == null)
            {
                return NotFound(responce);
            }
            return Ok(responce);
        }
        
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetDailyListDto>>>> Delete(int id)
        {
            var response = await _dailyListService.DeleteDaliyList(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }



    }
}
