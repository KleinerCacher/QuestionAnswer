﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hbs.Web.QuestionAnswer.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }
        // TODO Question ID muss Indeziert werden
        public int QuestionId { get; set; }
        public Question Question { get; set; }

        [UIHint("tinymce_jquery_full_compressed"), AllowHtml]
        public string Text { get; set; }
        public string Author { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime CreationDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime ModifiedDate { get; set; }
    }
}