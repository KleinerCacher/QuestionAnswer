using Hbs.Web.QuestionAnswer.Common;
using Hbs.Web.QuestionAnswer.Data;
using Hbs.Web.QuestionAnswer.ViewModels;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Hbs.Web.QuestionAnswer.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: User
        [ChildActionOnly]
        public ActionResult UserInfo()
        {
            var answeredQuestionCount = db.Questions
                      .Where(q => q.Answers
                               .Any(a => a.Author.ToLower() == User.Identity.Name.ToLower()
                                && a.IsCorrectAnswer))
                      .Count();

            var user = new UserViewModel
            {
                AnsweredQuestions = answeredQuestionCount,
                DisplayName = UserHelper.GetUserDisplayname(User.Identity.Name),
                Login = User.Identity.Name
            };

            return PartialView(user);
        }
    }
}