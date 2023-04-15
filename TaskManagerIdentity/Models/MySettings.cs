using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagerIdentity.Models
{
    public class MySettings
    {
        public const string SectionName = "MySettings";

        public int PageSize { get; set; }
        public int PageSpan { get; set; }
        public int StartPageSpan { get; set; }
        public int EndPageSpan { get; set; }

    }
}
