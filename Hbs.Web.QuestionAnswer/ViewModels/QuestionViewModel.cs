using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hbs.Web.QuestionAnswer.Models;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Hbs.Web.QuestionAnswer.Models.Attachments;

namespace Hbs.Web.QuestionAnswer.ViewModels
{
    public class QuestionViewModel : QuestionIndexViewModel
    {
        public ICollection<Answer> Answers { get; internal set; }

        [UIHint("ckeditor_jquery"), AllowHtml]
        public string NewAnswerText { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime? ModifiedDate { get; set; }


        public string Author { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime CreationDate { get; set; }

        public List<QuestionAttachment> Attachments { get; set; }

        public QuestionViewModel()
        {
            Answers = new List<Answer>();
            Attachments = new List<QuestionAttachment>();
        }

        public QuestionViewModel(Question question)
        {
            Id = question.Id;
            Title = question.Title;
            Text = question.Text;
            IsSolved = question.Answers.Any(a => a.IsCorrectAnswer);
            Author = question.Author;
            CreationDate = question.CreationDate;
            ModifiedDate = question.ModifiedDate;
            Answers = question.Answers;
            Attachments = question.Attachments;
        }
    }
}