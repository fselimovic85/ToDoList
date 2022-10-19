using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TO_DO_LIST.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime Deadline { get; set; }
        public bool Done { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? AddedToDone { get; set; }


        [ForeignKey(nameof(DailyList))]
        public int DailyListId { get; set; }

        public DailyList DailyList { get; set; }        
    }
}
