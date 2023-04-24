using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserRegistration.Data;
using UserRegistration.Models;

namespace UserRegistration.Controllers
{
	public class LoginController : Controller
	{
		private readonly ApplicationDbContext _db;
		public LoginController(ApplicationDbContext db)
		{
			_db = db;
		}
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Login(Login model)
		{
			if(ModelState.IsValid) 
			{
                var data=_db.Registrations.Where(e=>e.UserName== model.UserName).SingleOrDefault();
				if(data!=null) 
				{
					bool isValid=(data.UserName==model.UserName && data.Password==model.Password);
					if(isValid)
					{    // user info is saved in identity variable
						var identity= new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, model.UserName)},
							CookieAuthenticationDefaults.AuthenticationScheme);
						//here identity is passed to principal
						var principal=new ClaimsPrincipal(identity);
						// here authentication is passed to principal
		                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
						//session is used to store sensitive info like username ,password
						HttpContext.Session.SetString("Username",model.UserName);
						return RedirectToAction("Index", "Home");

					}
					else 
					{
						TempData["errorPassword"] = "Invalid password !";
						return View(model);
					}
				}
				else
				{
					TempData["errorUsername"] = "Username is not found";
					return View(model);
				}
			}
			else
			{
				return View(model);
			}
			
		}
		public IActionResult LogOut()
		{
			HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);	
			return RedirectToAction("Login", "Login");
		}
	}

}
