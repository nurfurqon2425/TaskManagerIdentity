using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagerIdentity.Models
{
    public class Sorting
    {
        public bool DescName { get; set; }
        public bool DescPriority { get; set; }
        public bool DescStartTime { get; set; }
        public bool DescEndTime { get; set; }

        public Sorting()
        {

        }

    }
}
