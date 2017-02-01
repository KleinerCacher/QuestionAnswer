using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hbs.Web.QuestionAnswer.Models
{
    public class Question
    {
        private const int shortTextLength = 400;

        [Key]
        public int Id { get; set; }
        [DisplayName("Titel")]
        public string Title { get; set; }
        public string Author { get; set; }

        [UIHint("ckeditor_jquery"), AllowHtml]
        public string Text { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime CreationDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime? ModifiedDate { get; set; }
        public string ShortText
        {
            get
            {
                return Text.Substring(0, Text.Length > shortTextLength ? shortTextLength : Text.Length);
            }
        }

        public ICollection<Answer> Answers { get; set; }

        [NotMapped]
        public bool IsSolved {
            get { return Answers.Any(a => a.IsCorrectAnswer); }
        }

        public Question()
        {
            Answers = new List<Answer>();
        }
    }
}