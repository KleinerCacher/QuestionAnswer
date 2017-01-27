using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hbs.Web.QuestionAnswer.Models;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Hbs.Web.QuestionAnswer.ViewModels
{
    public class QuestionViewModel
    {
        public ICollection<Answer> Answers { get; internal set; }

        [UIHint("tinymce_jquery_full_compressed"), AllowHtml]
        public string NewAnswerText { get; set; }
        public string Author { get;  set; }

        [DisplayFormat(DataFormatString ="{0:dd.MM.yyyy}")]
        public DateTime CreationDate { get;  set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime ModifiedDate { get; set; }
        public int Id { get;  set; }

        [UIHint("tinymce_jquery_full_compressed"), AllowHtml]
        public string Text { get;  set; }
        public string Title { get;  set; }

        public QuestionViewModel()
        {
            Answers = new List<Answer>();
        }

        public QuestionViewModel(Question question)
        {
            this.Author = question.Author;
            this.CreationDate = question.CreationDate;
            this.ModifiedDate = question.ModifiedDate;
            this.Id = question.Id;
            this.Text = question.Text;
            this.Title = question.Title;
            this.Answers = new List<Answer>();
        }
    }
}