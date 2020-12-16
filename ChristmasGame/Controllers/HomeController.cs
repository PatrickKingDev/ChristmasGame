using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChristmasGame.Models.ViewModels;
using ChristmasGame.DAL;

namespace ChristmasGame.Controllers
{
    public class HomeController : Controller
    {
        Repository repo = new Repository();

        public ActionResult Index()
        {
            var vm = new IndexVM();
            var phrase = string.Empty;

            phrase = readCookie();

            if (string.IsNullOrWhiteSpace(phrase)) {
                phrase = repo.GetRandomPhrase();

                if (!string.IsNullOrWhiteSpace(phrase)) {
                    AddCookie(phrase);
                }
            }

            vm.Phrase = phrase;
            vm.Link = getLink();

            return View(vm);
        }

        private void AddCookie(string phrase) {
            var currentVersion = repo.GetCurrentVersion();

            HttpCookie currentCookie = new HttpCookie("CurrentPhrase");
            currentCookie.Value = Server.UrlEncode(currentVersion + "|=|" + phrase);

            Response.Cookies.Add(currentCookie);
        }

        private string readCookie() {
            var currentVersion = repo.GetCurrentVersion();

            var currentValue = Server.UrlDecode(Request.Cookies["CurrentPhrase"]?.Value);

            if (currentValue == null) { 
                return string.Empty;
            }

            var splitted = currentValue.Split(new string[] { "|=|"}, StringSplitOptions.None);

            if (splitted[0] == currentVersion.ToString()) {
                return splitted[1];
            }

            return string.Empty;
        }

        private string getLink() {
            var currentLink = repo.GetLink();

            if (string.IsNullOrWhiteSpace(currentLink)) {
                currentLink = "https://cosel.io/";
            }

            if (!currentLink.StartsWith("http")) {
                currentLink = "http://" + currentLink;
            }

            return currentLink;
        }
    }
}