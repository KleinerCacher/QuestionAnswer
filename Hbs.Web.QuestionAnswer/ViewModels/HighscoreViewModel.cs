using System.Collections.Generic;

namespace Hbs.Web.QuestionAnswer.ViewModels
{
    public class HighscoreViewModel
    {
        public List<UserViewModel> HighscoreList { get; set; }

        public string ReturnUrl { get; set; }

        public HighscoreViewModel()
        {
            HighscoreList = new List<UserViewModel>();
        }
    }
}