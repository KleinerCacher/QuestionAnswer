using Hbs.Web.QuestionAnswer.Common;
using Hbs.Web.QuestionAnswer.Data;
using Hbs.Web.QuestionAnswer.Models;
using Hbs.Web.QuestionAnswer.Models.Attachments;
using Hbs.Web.QuestionAnswer.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Hbs.Web.QuestionAnswer.Controllers
{
    public class QuestionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Questions
        public ActionResult Index(string searchText, int filter = 0)
        {
            var questions = db.Questions
                              .Where(q => string.IsNullOrEmpty(searchText) || q.Title.ToUpper().Contains(searchText.ToUpper())
                              || q.Text.ToUpper().Contains(searchText.ToUpper()))
                              .OrderByDescending(q => q.CreationDate)
                              .Select(q => new QuestionIndexViewModel
                              {
                                  Id = q.Id,
                                  Title = q.Title,
                                  Text = q.Text,
                                  IsSolved = q.Answers.Any(a => a.IsCorrectAnswer)
                              });

            switch (filter)
            {
                case 1:
                    questions = questions.Where(q => q.IsSolved);
                    break;
                case 2:
                    questions = questions.Where(q => !q.IsSolved);
                    break;
                case 0:
                default:
                    break;
            }

            var model = new QuestionIndexViewModelContainer
            {
                Questions = questions,
                SearchText = searchText
            };

            return View(model);
        }

        // GET: Questions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var questionView = GenerateQuestionViewModel(id);
            if (questionView == null)
            {
                return HttpNotFound();
            }

            return View(questionView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(QuestionViewModel questionView, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                var answer = new Answer()
                {
                    QuestionId = questionView.Id,
                    Text = questionView.NewAnswerText,
                    Author = User.Identity.Name,
                    CreationDate = DateTime.Now
                };

                answer = db.Answers.Add(answer);

                var attachments = AttachmentHelper.TransformToAnswerAttachment(answer.Id, files);
                db.AnswerAttachments.AddRange(attachments);

                db.SaveChanges();
                var emailManager = new EmailManager(Request.Url.GetLeftPart(UriPartial.Authority));
                emailManager.NewAnswerCreated(questionView.Id, questionView.Title, questionView.Author);
                return RedirectToAction("Details", "Questions", new { id = questionView.Id });
            }

            return View(questionView);
        }

        private QuestionViewModel GenerateQuestionViewModel(int? id)
        {
            var question = db.Questions
                .Include(q => q.Attachments)
                .Include(q => q.Answers.Select(a => a.Attachments))
                .SingleOrDefault(q => q.Id == id);            

            if (question == null)
            {
                return null;
            }

            return new QuestionViewModel(question);
        }

        public ActionResult Attachment(int id)
        {
            QuestionAttachment img = db.QuestionAttachments.Find(id);
            return File(img.Data, EnumDescription.Get(img.FileType));
        }

        // GET: Questions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Question question, IEnumerable<HttpPostedFileBase> files)
        {
            question.CreationDate = DateTime.Now;
            question.Author = User.Identity.Name;

            if (ModelState.IsValid)
            {
                question = db.Questions.Add(question);

                var attachments = AttachmentHelper.TransformToQuestionAttachments(question.Id, files);
                db.QuestionAttachments.AddRange(attachments);

                db.SaveChanges();
                var emailManager = new EmailManager(Request.Url.GetLeftPart(UriPartial.Authority));
                emailManager.NewQuestionCreated(question.Id, question.Title);
                return RedirectToAction("Index");
            }

            return View(question);
        }

        // GET: Questions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Where(x => x.Id == id)
                                            .Include(x => x.Attachments)
                                            .FirstOrDefault();
            if (question == null)
            {
                return HttpNotFound();
            }

            if (!question.Author.Equals(User.Identity.Name))
            {
                return RedirectToAction("Index");
            }

            return View(question);
        }

        // GET: Questions/MarkAsCorrectAnswer/5
        public ActionResult MarkAsCorrectAnswer(int? id, bool isCorrect)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answer answer = db.Answers.Where(a => a.Id == id).Include(a => a.Question).FirstOrDefault();
            if (answer == null)
            {
                return HttpNotFound();
            }

            answer.IsCorrectAnswer = isCorrect;
            db.Entry(answer).State = EntityState.Modified;
            db.SaveChanges();

            if (isCorrect)
            {
                var emailManager = new EmailManager(Request.Url.GetLeftPart(UriPartial.Authority));
                emailManager.QuestionIsAnswered(answer.QuestionId, answer.Question.Title, answer.Author);
            }
            return RedirectToAction("Details", new { id = answer.QuestionId });
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Question question, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                question.ModifiedDate = DateTime.Now;
                db.Entry(question).State = EntityState.Modified;

                var questionsToDelete = question.Attachments.Where(x => x.Delete).ToList();
                foreach (var attachment in questionsToDelete)
                {
                    var item = db.QuestionAttachments.Find(attachment.Id);
                    db.QuestionAttachments.Remove(item);
                }

                var attachments = AttachmentHelper.TransformToQuestionAttachments(question.Id, files);
                db.QuestionAttachments.AddRange(attachments);

                db.SaveChanges();
                return RedirectToAction("Details", "Questions", new { id = question.Id });
            }
            return View(question);
        }

        // GET: Questions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var question = GenerateQuestionViewModel(id);
            if (question == null)
            {
                return HttpNotFound();
            }

            if (!question.Author.Equals(User.Identity.Name))
            {
                return RedirectToAction("Index");
            }

            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);

            var answers = db.Answers.Where(a => a.QuestionId == id);
            db.Answers.RemoveRange(answers);

            db.SaveChanges();
            return RedirectToAction("Index");
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
