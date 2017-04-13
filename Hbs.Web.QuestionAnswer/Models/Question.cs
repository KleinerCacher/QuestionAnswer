using Hbs.Web.QuestionAnswer.Models.Attachments;
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

        [Key]
        public int Id { get; set; }
        [DisplayName("Titel")]
        public string Title { get; set; }
        public string Author { get; set; }

        // TODO Textarea richtig ziehen in Create und Edit
        public string Text { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime CreationDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime? ModifiedDate { get; set; }

        public ICollection<Answer> Answers { get; set; }
        public List<QuestionAttachment> Attachments { get; set; }

        public Question()
        {
            Answers = new List<Answer>();
        }
    }
}