using System.ComponentModel.DataAnnotations.Schema;

namespace Hbs.Web.QuestionAnswer.Models.Attachments
{
    public class AnswerAttachment : Attachment
    {
        public int AnswerId { get; set; }

        [ForeignKey("AnswerId")]
        public virtual Answer Answer { get; set; }
    }
}