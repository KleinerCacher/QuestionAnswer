using Hbs.Web.QuestionAnswer.Models.Attachments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hbs.Web.QuestionAnswer.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime CreationDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime? ModifiedDate { get; set; }
        public bool IsCorrectAnswer { get; set; }
        public List<AnswerAttachment> Attachments { get; set; }

        public Answer()
        {
            Attachments = new List<AnswerAttachment>();
        }
    }
}