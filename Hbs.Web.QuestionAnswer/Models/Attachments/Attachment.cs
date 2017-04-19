using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Hbs.Web.QuestionAnswer.Models.Attachments
{
    public class Attachment
    {
        public int Id { get; set; }
        public byte[] Data { get; set; }
        public AttachmentType FileType { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public bool Delete { get; set; }
    }
}