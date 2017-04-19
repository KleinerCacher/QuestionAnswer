using Hbs.Web.QuestionAnswer.Models.Attachments;
using ImageResizer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Web;

namespace Hbs.Web.QuestionAnswer.Common
{
    public static class AttachmentHelper
    {
        public static List<AnswerAttachment> TransformToAnswerAttachment(int answerId, IEnumerable<HttpPostedFileBase> files)
        {
            var attachments = new List<AnswerAttachment>();
            foreach (var file in files)
            {
                if (file == null) continue;

                Attachment attachment = GenerateAttachment(file);
                var answerAttachment = new AnswerAttachment(answerId, attachment);
                attachments.Add(answerAttachment);
            }

            return attachments;
        }

        public static List<QuestionAttachment> TransformToQuestionAttachments(int questionId ,IEnumerable<HttpPostedFileBase> files)
        {
            var attachments = new List<QuestionAttachment>(); 
            foreach (var file in files)
            {
                if (file == null) continue;

                Attachment attachment = GenerateAttachment(file);
                var questionAttachment = new QuestionAttachment(questionId, attachment);
                attachments.Add(questionAttachment);
            }

            return attachments;
        }

        private static Attachment GenerateAttachment(HttpPostedFileBase file)
        {
            string[] filesegments = file.FileName.Split('\\');
            byte[] arr = new byte[file.InputStream.Length];
            file.InputStream.Read(arr, 0, (int)file.InputStream.Length);

            AttachmentType fType = GetTypeByName(file.FileName);

            //resize image if larger then configered size
            int configMaxHeight = Int32.Parse(ConfigurationManager.AppSettings[Constants.ResizeImageMaxHeight], CultureInfo.InvariantCulture);
            int configQuality = Int32.Parse(ConfigurationManager.AppSettings[Constants.ResizeQuality], CultureInfo.InvariantCulture);
            arr = ResizeImage(file.InputStream, fType, configMaxHeight, configQuality);

            var attachment = new Attachment
            {
                Data = arr,
                FileType = fType,
                Name = filesegments[filesegments.Length - 1]
            };
            return attachment;
        }

        private static byte[] ResizeImage(Stream input, AttachmentType fileType, int configMaxHeight, int configQuality)
        {
            input.Seek(0, SeekOrigin.Begin);

            ResizeSettings resizeSetting = new ResizeSettings
            {
                MaxHeight = configMaxHeight,
                Quality = configQuality
            };
            //handle supported attachement typs
            if (fileType == AttachmentType.Jpg)
            {
                resizeSetting.Format = "jpg";
            }
            else if (fileType == AttachmentType.Png)
            {
                resizeSetting.Format = "png";
            }
            else
            {
                resizeSetting.Format = "gif";
            }

            using (MemoryStream ms = new MemoryStream())
            {
                ImageBuilder.Current.Build(input, ms, resizeSetting);
                return ms.ToArray();
            }
        }

        public static AttachmentType GetTypeByName(string name)
        {
            if (name.EndsWith(".gif", StringComparison.OrdinalIgnoreCase))
            {
                return AttachmentType.Gif;
            }
            else if (name.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
            {
                return AttachmentType.Png;
            }
            else if (name.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
            {
                return AttachmentType.Jpg;
            }
            return AttachmentType.Unknown;
        }
    }
}