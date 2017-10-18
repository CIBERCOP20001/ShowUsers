using ShowUsers.Interface;
using ShowUsers.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShowUsers.Web.Controllers
{
    public class AccountController : Controller
    {
        IAccountRepository _accountRepo = null;

        public AccountController(IAccountRepository accountRepo)
        {
            _accountRepo = accountRepo;
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(Login login)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "");

                return View(login);
            }

            if (login.LoginUser != null && login.Password !=null )
            {
                bool isValidUser = _accountRepo.ValidateLogin(login);

                //ModelState.AddModelError("", "");
                //userProfile = _securityRepo.LogIn(model.Login, model.Password);
            }
            return View(login);
        }
    }
}