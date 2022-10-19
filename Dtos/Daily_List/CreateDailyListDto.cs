namespace TO_DO_LIST.Dtos.Daily_List
{
    public class CreateDailyListDto
    {
        public string Title { get; set; } = "My List";
        public string Description { get; set; } = " That is my List";
        public DateTime CreatedDate { get; set; }=DateTime.Now;
    
    }
}
