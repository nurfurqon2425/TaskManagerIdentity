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
        public IActionResult List()
        {
            IEnumerable<TaskList> objList = _db.TaskList;
            return View(objList);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize]
        public IActionResult Update()
        {
            return View();
        }

    }
}
