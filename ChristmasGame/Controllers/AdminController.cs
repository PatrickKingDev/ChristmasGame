using ChristmasGame.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChristmasGame.Controllers
{
    public class AdminController : Controller
    {
        Repository repo = new Repository();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string link)
        {
            repo.AddLink(link);

            return View();
        }

        public ActionResult NewGame() {
            repo.AddVersion();

            return RedirectToAction("index");
        }

        public ActionResult ResetGame()
        {
            repo.ResetPhrases();
            repo.AddVersion();

            return RedirectToAction("index");
        }
    }
}