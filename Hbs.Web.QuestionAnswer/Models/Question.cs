﻿using System;
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

        [UIHint("tinymce_jquery_full_compressed"), AllowHtml]
        public string Text { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime CreationDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ShortText
        {
            get
            {
                return Text.Substring(0, Text.Length > shortTextLength ? shortTextLength : Text.Length);
            }
        }

        public ICollection<Answer> Answers { get; set; }

        public Question()
        {
            Answers = new List<Answer>();
        }
    }
}