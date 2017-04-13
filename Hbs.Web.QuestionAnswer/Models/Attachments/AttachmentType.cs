using System.ComponentModel;

namespace Hbs.Web.QuestionAnswer.Models.Attachments
{
    public enum AttachmentType
    {
        [Description("image/jpeg")]
        Jpg,
        [Description("image/png")]
        Png,
        [Description("image/gif")]
        Gif,
        [Description("binary/octet-stream")]
        Unknown
    }
}