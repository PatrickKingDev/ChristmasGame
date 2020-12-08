using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChristmasGame.Models;

namespace ChristmasGame.DAL
{
    public class Repository
    {
        private ChristmasGameEntities db = new ChristmasGameEntities();

        public string GetRandomPhrase() {

            var rnd = new Random();
            var list = db.Phrases.Where(p => p.Given == false).ToList();

            if (list.Count == 0) {
                return string.Empty;
            }

            var r = rnd.Next(list.Count);

            var item = list[r];
            item.Given = true;

            db.SaveChanges();

            return item.Desc;
        }

        public void ResetPhrases() {
            db.Phrases.ToList().ForEach(p => p.Given = false);
            db.SaveChanges();
        }

        public void AddVersion() {
            db.CurrentVersions.Add(new CurrentVersion());
            db.SaveChanges();
        }

        public int GetCurrentVersion() {
            return db.CurrentVersions.OrderByDescending(v => v.Version).First().Version;
        }
    }
}