using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hbs.Web.QuestionAnswer.ViewModels
{
    public class QuestionIndexViewModelContainer
    {
        public IEnumerable<QuestionIndexViewModel> Questions { get; set; }

        public string SearchText { get; set; }

        public int Filter { get; set; }
    }
}