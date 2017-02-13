using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hbs.Web.QuestionAnswer.ViewModels
{
    public class QuestionIndexViewModel
    {
        private const int shortTextLength = 400;

        public int Id { get; set; }
        public string Title { get; set; }

        [UIHint("ckeditor_jquery"), AllowHtml]
        public string Text { get; set; }
        public bool IsSolved { get; set; }
        public string ShortText
        {
            get
            {
                return Text.Substring(0, Text.Length > shortTextLength ? shortTextLength : Text.Length);
            }
        }

    }
}