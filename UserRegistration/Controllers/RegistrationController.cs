using Microsoft.AspNetCore.Mvc;
using UserRegistration.Data;
using UserRegistration.Models;

namespace UserRegistration.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ApplicationDbContext _db;
        public RegistrationController(ApplicationDbContext db) 
        {
            _db= db;
        }
        [AcceptVerbs("Post","Get")]
        public IActionResult UserNameIsExist(string userName) 
        {
            var data=_db.Registrations.Where(e=>e.UserName == userName).SingleOrDefault();
            if(data!=null)
            {
                return Json($"Username{userName} is already in use!");
            }
            else
            {
                return Json(true);
            }
        }
		
		public IActionResult SignUp() 
        {
            return View();
        }
        [HttpPost]
		public IActionResult SignUp(Registration model)
		{
            if (ModelState.IsValid) 
            {
                var data = new Registration()
                {
                    UserName= model.UserName,
                    Password= model.Password,
                    Gender= model.Gender,
                    Dateofbirth=model.Dateofbirth,
                    SecurityQuestion=model.SecurityQuestion,
                    Answer=model.Answer 
                };
                _db.Registrations.Add(data);
                _db.SaveChanges();
                TempData["sucessMessage"] = "You are eligible to login ,Please fill the credentials and then login! ";
                return RedirectToAction("Login","Login"); 
            }
            else
            {
                TempData["errorMessage"] = "Empty form can not be submitted";
				return View();
			}
			
		}
	}
}
