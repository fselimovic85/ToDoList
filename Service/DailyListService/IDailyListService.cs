using TO_DO_LIST.Dtos.Daily_List;
using TO_DO_LIST.Models;

namespace TO_DO_LIST.Service.DailyListService
{
    public interface IDailyListService
    {

        Task<ServiceResponse<List<GetDailyListDto>>> GetAllDailyList();
        Task<ServiceResponse<List<GetDailyListDto>>> GetAllDailyListWithPage(int page, DateTime date, string title);
        Task<ServiceResponse<List<GetDailyListDto>>> CreateDaliyList(CreateDailyListDto newDailyListDto);
        Task<ServiceResponse<GetDailyListDto>> UpdateDaliyList(int Id, UpdateDailyListDto updateDailyListDto);
        Task<ServiceResponse<List<GetDailyListDto>>> DeleteDaliyList(int id);
    }
}
