using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagerIdentity.Models
{
    public class TaskList
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string TaskName { get; set; }
        [Required]
        [Range(1, 5, ErrorMessage = "Priority only 1 until 5")]
        public int Priority { get; set; }
        [Required]
        public bool Active { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

    }
}
