using System.ComponentModel.DataAnnotations.Schema;

namespace Hbs.Web.QuestionAnswer.Models.Attachments
{
    public class AnswerAttachment : Attachment
    {
        public AnswerAttachment() { }

        public AnswerAttachment(int answerId, Attachment attachment)
        {
            AnswerId = answerId;
            Data = attachment.Data;
            Id = attachment.Id;
            FileType = attachment.FileType;
            Name = attachment.Name;
        }

        public int AnswerId { get; set; }

        [ForeignKey("AnswerId")]
        public virtual Answer Answer { get; set; }
    }
}