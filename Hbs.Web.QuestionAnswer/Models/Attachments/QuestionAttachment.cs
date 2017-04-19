using System.ComponentModel.DataAnnotations.Schema;

namespace Hbs.Web.QuestionAnswer.Models.Attachments
{
    public class QuestionAttachment : Attachment
    {
        public QuestionAttachment() { }

        public QuestionAttachment(int questionId, Attachment attachment)
        {
            QuestionId = questionId;
            Data = attachment.Data;
            Id = attachment.Id;
            FileType = attachment.FileType;
            Name = attachment.Name;
        }

        public int QuestionId { get; set; }

        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }
    }
}