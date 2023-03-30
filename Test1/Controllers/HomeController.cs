using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Test1.Data;
using Test1.Models;

namespace Test1.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserRepository _userRepository;

        public HomeController()
        {
            _userRepository = new UserRepository();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Get()
        {
            List<UserViewModel> users = _userRepository.Get().Select(u => (UserViewModel)u).ToList();
            return View(users);
        }

        [HttpGet]
        public ActionResult GetById(Guid id)
        {
            UserViewModel user = _userRepository.GetById(id);
            return View("Details", user);
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            UserViewModel user = _userRepository.GetById(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel user)
        {
            _userRepository.Edit(new User
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                CellPhoneNumber = user.CellPhoneNumber
            });

            return RedirectToAction("GetById", new {id = user.Id});
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            _userRepository.Delete(id);
            return RedirectToAction("Get");
        }
        
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(List<UserBindingModel> users)
        {
            _userRepository.Add(users.Select(u => (User)u));
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}