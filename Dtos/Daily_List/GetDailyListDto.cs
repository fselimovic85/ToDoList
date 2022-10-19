using TO_DO_LIST.Models;

namespace TO_DO_LIST.Dtos.Daily_List
{
    public class GetDailyListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        
    }
}
