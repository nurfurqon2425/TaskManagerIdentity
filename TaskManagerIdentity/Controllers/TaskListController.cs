using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public TaskListController(ApplicationDbContext db)
        {
            _db = db;
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

            const int pageSize = 5;
            if (pg < 1)
                pg = 1;

            int recsCount = objList.Count();            

            int recSkip = (pg - 1) * pageSize;

            IEnumerable<TaskList> retTaskList = objList.Skip(recSkip).Take(pageSize);

            Pager pager = new Pager(recsCount, pg, pageSize);

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

        [Authorize]
        public IActionResult Update()
        {
            return View();
        }

    }
}
