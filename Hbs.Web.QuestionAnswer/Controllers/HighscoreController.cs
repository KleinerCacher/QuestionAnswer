using Hbs.Web.QuestionAnswer.Common;
using Hbs.Web.QuestionAnswer.Data;
using Hbs.Web.QuestionAnswer.ViewModels;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Hbs.Web.QuestionAnswer.Controllers
{
    public class HighscoreController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Highscore
        public ActionResult Index(string sourceUrl)
        {
            var x = db.Answers
                    .Where(a => a.IsCorrectAnswer)
                    .GroupBy(a => a.Author);

            var model = new HighscoreViewModel();
            foreach (var item in x)
            {
                var highscore = new UserViewModel();
                highscore.AnsweredQuestions = item.Count();
                highscore.DisplayName = UserHelper.GetUserDisplayname(item.ElementAt(0).Author);
                highscore.Login = item.ElementAt(0).Author;
                model.HighscoreList.Add(highscore);
            }

            model.HighscoreList.OrderByDescending(t => t.AnsweredQuestions);
            model.ReturnUrl = sourceUrl;

            return View(model);
        }
    }
}