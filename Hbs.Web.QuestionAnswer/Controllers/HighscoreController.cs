using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hbs.Web.QuestionAnswer.Data;
using Hbs.Web.QuestionAnswer.Models;
using Hbs.Web.QuestionAnswer.ViewModels;

namespace Hbs.Web.QuestionAnswer.Controllers
{
    public class HighscoreController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Highscore
        public ActionResult Index()
        {
            var x = db.Answers
                    .Where(a => a.IsCorrectAnswer)
                    .GroupBy(a => a.Author);


                    
                    //.SelectMany(q => q.Author).Distinct().ToList();

            //var answeredQuestionCount = db.Questions
            //       .Where(q => q.Answers
            //                .Any(a => a.Author.ToLower() == User.Identity.Name.ToLower()
            //                 && a.IsCorrectAnswer))
            //       .Count();

            return View();
        }
    }
}