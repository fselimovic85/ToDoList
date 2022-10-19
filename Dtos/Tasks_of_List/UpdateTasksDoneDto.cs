using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace TO_DO_LIST.Dtos.Tasks_of_List
{
    public class UpdateTasksDoneDto
    {
        public bool Done { get; set; }
        public DateTime AddedToDone { get; set; }
    }
}
