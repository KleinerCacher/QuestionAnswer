using System.ComponentModel.DataAnnotations.Schema;

namespace Hbs.Web.QuestionAnswer.Models.Attachments
{
    public class QuestionAttachment : Attachment
    {
        public int QuestionId { get; set; }

        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }
    }
}