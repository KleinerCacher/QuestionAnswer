using Hbs.Web.QuestionAnswer.Common;
using Hbs.Web.QuestionAnswer.Data;
using Hbs.Web.QuestionAnswer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using Hbs.Web.QuestionAnswer.Models.Attachments;

namespace Hbs.Web.QuestionAnswer.Controllers
{
    public class AnswersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Answers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answer answer = db.Answers.Where(x => x.Id == id)
                                      .Include(x => x.Attachments)
                                      .FirstOrDefault();
            if (answer == null)
            {
                return HttpNotFound();
            }

            if (!answer.Author.Equals(User.Identity.Name))
            {
                return RedirectToAction("Index", "Questions");
            }

            ViewBag.QuestionId = new SelectList(db.Questions, "Id", "Title", answer.QuestionId);
            return View(answer);
        }

        // POST: Answers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Answer answer, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                answer.ModifiedDate = DateTime.Now;
                db.Entry(answer).State = EntityState.Modified;

                var attachmentsToDelete = answer.Attachments.Where(x => x.Delete).ToList();
                foreach (var attachment in attachmentsToDelete)
                {
                    var item = db.AnswerAttachments.Find(attachment.Id);
                    db.AnswerAttachments.Remove(item);
                }

                var attachments = AttachmentHelper.TransformToAnswerAttachment(answer.Id, files);
                db.AnswerAttachments.AddRange(attachments);

                db.SaveChanges();
                return RedirectToAction("Details", "Questions", new { id = answer.QuestionId });
            }

            return View(answer);
        }

        // GET: Answers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Answer answer = db.Answers.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }

            if (!answer.Author.Equals(User.Identity.Name))
            {
                return RedirectToAction("Index", "Questions");
            }

            var questionId = answer.QuestionId;
            db.Answers.Remove(answer);
            db.SaveChanges();
            return RedirectToAction("Details", "Questions", new { id = questionId });
        }

        // POST: Answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Answer answer = db.Answers.Find(id);
            db.Answers.Remove(answer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Attachment(int id)
        {
            AnswerAttachment img = db.AnswerAttachments.Find(id);
            return File(img.Data, EnumDescription.Get(img.FileType));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
