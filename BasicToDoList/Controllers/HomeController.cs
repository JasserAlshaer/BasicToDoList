﻿using BasicToDoList.Models;
using BasicToDoList.Models.Context;
using BasicToDoList.Models.DTO;
using BasicToDoList.Models.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BasicToDoList.Controllers
{
    public class HomeController : Controller
    {

        private readonly BasicToDoListDbContext _basicToDoListDbContext;
        public HomeController(BasicToDoListDbContext basicToDoListDbContext)
        {
            _basicToDoListDbContext = basicToDoListDbContext;
        }
        public IActionResult Index()
        {
            //showing Registration Form 
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration(string name, string email, string password)
        {
            //validation
            if (string.IsNullOrEmpty(name))
                return BadRequest("Name is Required");
            if (string.IsNullOrEmpty(email))
                return BadRequest("Email is Required");
            if (string.IsNullOrEmpty(password))
                return BadRequest("Password is Required");
            //advance validetion
            var isThereUser = await _basicToDoListDbContext.Users.AnyAsync(u => u.Email == email);
            if (isThereUser)
            {
                return BadRequest("Email Already Is Used");
            }
            User customer = new User()
            {
                Name = name,
                Email = email,
                Password = password,
                UserName = email
            };
            customer.IsLoggedIn = false;
            customer.IsActive = true;
            customer.CreationDate = DateTime.Now;
            await _basicToDoListDbContext.AddAsync(customer);
            await _basicToDoListDbContext.SaveChangesAsync();
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string pass)
        {
            if (string.IsNullOrEmpty(username))
                return BadRequest("Username/Email is Required");
            if (string.IsNullOrEmpty(pass))
                return BadRequest("Password is Required");

            var user = await _basicToDoListDbContext.Users.SingleOrDefaultAsync
                (u => u.UserName == username && u.Password == pass
                && u.IsLoggedIn == false);
            if (user == null)
            {
                return BadRequest("Email or Pass Is Wrong / You're Already Have Opening Session");
            }
            else
            {
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                user.IsLoggedIn = true;
                _basicToDoListDbContext.Update(user);
                await _basicToDoListDbContext.SaveChangesAsync();
                return RedirectToAction("MyMission");
            }
        }
        public IActionResult MyMission()
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            
            var res = from mission in _basicToDoListDbContext.Missions
                      join status in _basicToDoListDbContext.MissionStatus
                      on mission.MissionStatus.Id equals status.Id
                      where mission.User.Id == userId
                      select new MissionInformationDTO
                      {

                      };
            return View();
        }

        public IActionResult MissionForm()
        {
            return View("MissionForm");
        }
        public IActionResult Update(int id)
        {
            var entity = _basicToDoListDbContext.Missions.FirstOrDefault(x=>x.Id == id);
            if (entity == null)
            {
                return BadRequest("Entity Dose not Exisit");
            }
            return View(entity);
        }
        [HttpPost]
        public IActionResult Update(int id,string title,string description,string priority,int statusId)
        {
            var entity = _basicToDoListDbContext.Missions.FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                return BadRequest("Entity Dose not Exisit");
            }
            entity.Title = title;
            entity.Description = description;
            entity.PriorityLevel = priority;
            entity.MissionStatus = _basicToDoListDbContext.MissionStatus.Find(statusId);
            _basicToDoListDbContext.Update(entity);
            _basicToDoListDbContext.SaveChanges();
            return RedirectToAction("MyMission");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var entity = _basicToDoListDbContext.Missions.FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                return BadRequest("Entity Dose not Exisit");
            }
            entity.IsActive = false;
            _basicToDoListDbContext.Update(entity);
            _basicToDoListDbContext.SaveChanges();
            return RedirectToAction("MyMission");
        }
    }
}
