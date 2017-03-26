﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hbs.Web.QuestionAnswer.Data;
using Hbs.Web.QuestionAnswer.Models;
using Hbs.Web.QuestionAnswer.ViewModels;

namespace Hbs.Web.QuestionAnswer.Controllers
{
    public class QuestionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Questions
        public ActionResult Index()
        {
            var questions = db.Questions
                              .OrderByDescending(q => q.CreationDate)
                              .Select(q => new QuestionIndexViewModel
                              {
                                  Id = q.Id,
                                  Title = q.Title,
                                  Text = q.Text,
                                  IsSolved = q.Answers.Any(a => a.IsCorrectAnswer)
                              });

            return View(questions);
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
        public ActionResult Details([Bind(Include = "Id,NewAnswerText,Author,CreationDate,Text,Title")] QuestionViewModel questionView)
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

                db.Answers.Add(answer);
                db.SaveChanges();
                return RedirectToAction("Details", "Questions", new { id = questionView.Id });
            }

            return View(questionView);
        }

        private QuestionViewModel GenerateQuestionViewModel(int? id)
        {
            var question = db.Questions
                .Select(q => new QuestionViewModel
                {
                    Id = q.Id,
                    Title = q.Title,
                    Text = q.Text,
                    IsSolved = q.Answers.Any(a => a.IsCorrectAnswer),
                    Author = q.Author,
                    CreationDate = q.CreationDate,
                    ModifiedDate = q.ModifiedDate,
                    Answers = q.Answers
                })
                .SingleOrDefault(q => q.Id == id);

            if (question == null)
            {
                return null;
            }

            return question;
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
        public ActionResult Create([Bind(Include = "Id,Title,Text")] Question question)
        {
            question.CreationDate = DateTime.Now;
            question.Author = User.Identity.Name;

            if (ModelState.IsValid)
            {
                db.Questions.Add(question);
                db.SaveChanges();
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
            Question question = db.Questions.Find(id);
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
            Answer answer = db.Answers.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }

            answer.IsCorrectAnswer = isCorrect;
            db.Entry(answer).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details", new { id = answer.QuestionId });
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Author,Text,CreationDate")] Question question)
        {
            if (ModelState.IsValid)
            {
                question.ModifiedDate = DateTime.Now;
                db.Entry(question).State = EntityState.Modified;
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
