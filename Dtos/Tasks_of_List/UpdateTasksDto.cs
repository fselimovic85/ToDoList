using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace TO_DO_LIST.Dtos.Tasks_of_List
{
    public class UpdateTasksDto
    {
        
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Dedline { get; set; }
        public DateTime AddedToDone { get; set; }

    }
}
