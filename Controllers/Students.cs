using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using System.Collections.Generic;
using System.Collections.Immutable;
using X.PagedList;
using System;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;
using WebApplication2.School_dbModels;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace WebApplication2.Controllers
{
    public class Students : Controller
    {
        private static SchoolDbContext _schoolarDbContext = new SchoolDbContext();


        // GET: Students
        public IActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;
            var totalStudent = _schoolarDbContext.People.Where(p => p.Roles == 1);
            /*var totalStudent = from people in _schoolarDbContext.People
                        join role in _schoolarDbContext.Roles on people.Roles equals role.Id
                        where role.Labels.Equals("Srudent") 
                        select people;*/
            return View(totalStudent.ToPagedList(pageNumber, pageSize));
        }

        // GET: Students/Details/5
        public ActionResult Details(int id)
        {
            var student = _schoolarDbContext.People.FirstOrDefault(s => s.Id == id);

            return View(student);
        }

        [Authorize(Roles = "Administrator, Manager")]
        // GET: Students/Create
        public ActionResult Create()
        {
            DbSet<Role> roles = _schoolarDbContext.Roles;
            List<Role> rolesList = roles.ToList();

            ViewBag.Roles = new SelectList(rolesList, "Id", "Labels");
            return View();
        }

        [Authorize(Roles = "Administrator, Manager")]
        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Person student)
        {
            DbSet<Role> roles = _schoolarDbContext.Roles;
            List<Role> rolesList = roles.ToList();

            ViewBag.Roles = new SelectList(rolesList, "Id", "Labels");

            if (ModelState.IsValid)
            {
                _schoolarDbContext.People.Add(student);
                _schoolarDbContext.SaveChanges();  // Use SaveChanges directly without Update
                return RedirectToAction("Index");
            }

            return View(student);
        }

        [Authorize(Roles = "Administrator, Manager")]
        // GET: Students/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var person = await _schoolarDbContext.People.FindAsync(id);
            DbSet<Role> roles = _schoolarDbContext.Roles;
            List<Role> rolesList = roles.ToList();

            ViewBag.Roles = new SelectList(rolesList, "Id", "Labels");

            return View(person);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Birth,Roles")] Person updatedPerson)
        {
            if (ModelState.IsValid)
            {

                var existingPerson = await _schoolarDbContext.People.FindAsync(id);


                _schoolarDbContext.Entry(existingPerson).CurrentValues.SetValues(updatedPerson);

                await _schoolarDbContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }

            DbSet<Role> roles = _schoolarDbContext.Roles;
            List<Role> rolesList = await roles.ToListAsync();
            ViewBag.Roles = new SelectList(rolesList, "Id", "Labels");

            return View(updatedPerson);
        }

        [Authorize(Roles = "Administrator, Manager")]
        // GET: Students/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var studentToDelete = _schoolarDbContext.People.Find(id);
            _schoolarDbContext.Entry(studentToDelete).State = EntityState.Detached;
            return View(studentToDelete);
        }


        // POST: Students/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Person studentToDelete)
        {
            _schoolarDbContext.People.Remove(studentToDelete);
            _schoolarDbContext.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost]
        [HttpGet]
        public ActionResult Search(string searchterm, int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;
            var searchedStudents = _schoolarDbContext.People.Where(s => s.LastName == searchterm || s.FirstName == searchterm);

            ViewBag.SearchTerm = searchterm;

            return View(searchedStudents.ToPagedList(pageNumber, pageSize));
        }
    }
}
