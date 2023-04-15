using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagerIdentity.Models
{
    public class Pager
    {
        public string SearchText { get; set; }
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }

        private readonly IConfiguration _configuration;

        public Pager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Pager()
        {

        }

        public Pager(int totalItems, int page, int pageSize)
        {
            int pageSpan = _configuration.GetValue<int>("PageSpan");
            int startPageSpan = _configuration.GetValue<int>("StartPageSpan");
            int endPageSpan = _configuration.GetValue<int>("EndPageSpan");

            int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            int currentPage = page;

            int startPage = currentPage - startPageSpan;
            int endPage = currentPage + endPageSpan;

            if(startPage <= 0)
            {
                endPage = endPage - (startPage - 1);
                startPage = 1;
            }

            if(endPage > totalPages)
            {
                endPage = totalPages;
                if(endPage > pageSpan)
                {
                    startPage = endPage - pageSpan + 1;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;

        }


    }
}
