using ShowUsers.Interface;
using ShowUsers.Model.Custom;
using ShowUsers.Model.Models;
using ShowUsers.Model.ViewModels;
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
            Session["Logged"] = false;

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "");

                return View(login);
            }

            if (login.LoginUser != null && login.Password != null)
            {
                bool isValidUser = _accountRepo.ValidateLogin(login);
                if (isValidUser)
                {
                    Session["Logged"] = true;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "");
                }
            }
            return View(login);
        }

        public JsonResult GetAppUsers(JQueryDatatableParamModel param)
        {
            var json = Json(new object());
            AppUserDataTableViewModel data = _accountRepo.GetAppUsersList(param);

            var result = data.AppUserList.Select(u => new[]
            {
                u.Id.ToString(),
                u.UserName,
                u.Password,
                u.Email,
                u.Gender,
                u.Active
                }).ToList();

            json = Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = data.AppUsersTotal,
                iTotalDisplayRecords = data.AppUsersTotal,
                aaData = result
            },
                   JsonRequestBehavior.AllowGet);
            return json;
        }

        public JsonResult UpdateUserStatus(string email, bool status)
        {
            string newStatus = _accountRepo.UpdateUserStatus(email, !status);

            var result = new { newStatus = newStatus };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}