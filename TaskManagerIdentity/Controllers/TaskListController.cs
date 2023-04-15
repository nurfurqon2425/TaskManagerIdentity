using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerIdentity.Data;
using TaskManagerIdentity.Models;

namespace TaskManagerIdentity.Controllers
{
    public class TaskListController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;
        //private readonly int _pageSize;
        //private readonly int _pageSpan;
        //private readonly int _startPageSpan;
        //private readonly int _endPageSpan;

        //public TaskListController(IOptions<MySettings> mySettings)
        //{
        //    _pageSize = mySettings.Value.PageSize;
        //    _pageSpan = mySettings.Value.PageSpan;
        //    _startPageSpan = mySettings.Value.StartPageSpan;
        //    _endPageSpan = mySettings.Value.EndPageSpan;
        //}

        //public TaskListController(ApplicationDbContext db, IOptions<MySettings> mySettings)
        //{
        //    _db = db;
        //    _pageSize = mySettings.Value.PageSize;
        //    _pageSpan = mySettings.Value.PageSpan;
        //    _startPageSpan = mySettings.Value.StartPageSpan;
        //    _endPageSpan = mySettings.Value.EndPageSpan;
        //}

        public TaskListController(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        [Authorize]
        public IActionResult List(string SearchText = "", int pg = 1)
        {
            IEnumerable<TaskList> objList;

            if(SearchText != "" && SearchText != null)
            {
                objList = _db.TaskList.Where(p => p.TaskName.Contains(SearchText));
            }
            else
            {
                objList = _db.TaskList;
            }

            int pageSize = _configuration.GetValue<int>("MySettings:PageSize");
            //int pageSize = _pageSize;
            if (pg < 1)
                pg = 1;

            int recsCount = objList.Count();            

            int recSkip = (pg - 1) * pageSize;

            IEnumerable<TaskList> retTaskList = objList.Skip(recSkip).Take(pageSize);

            Pager pager = new Pager(recsCount, pg, pageSize, _configuration);

            this.ViewBag.Pager = pager;

            return View(objList);
        }

        //GET - CREATE
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        //POST - CREATE
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TaskList obj)
        {
            if (ModelState.IsValid)
            {
                _db.TaskList.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("List");
            }
            return View(obj);
        }

        //GET - EDIT
        [Authorize]
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.TaskList.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            
            return View(obj);
        }

        //POST - EDIT
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(TaskList obj)
        {
            if (ModelState.IsValid)
            {
                _db.TaskList.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("List");
            }
            return View(obj);
        }

        //POST - DONE
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Done(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.TaskList.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            obj.Active = false;
            _db.TaskList.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("List");
        }

    }
}
