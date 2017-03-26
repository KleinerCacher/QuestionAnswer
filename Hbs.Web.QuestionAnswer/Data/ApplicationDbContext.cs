using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Hbs.Web.QuestionAnswer.Data
{
    public class ApplicationDbContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public ApplicationDbContext() : base("name=ApplicationDbContext")
        {
            // do not change this configuration!!!
            Configuration.AutoDetectChangesEnabled = true;
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Configuration.ValidateOnSaveEnabled = true;
        }

        public System.Data.Entity.DbSet<Hbs.Web.QuestionAnswer.Models.Question> Questions { get; set; }

        public System.Data.Entity.DbSet<Hbs.Web.QuestionAnswer.Models.Answer> Answers { get; set; }
    }
}
