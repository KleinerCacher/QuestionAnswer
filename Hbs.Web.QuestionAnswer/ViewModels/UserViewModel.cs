using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hbs.Web.QuestionAnswer.ViewModels
{
    public class UserViewModel
    {
        public string Login { get; set; }
        public string DisplayName { get; set; }
        public int AnsweredQuestions { get; set; }
    }
}