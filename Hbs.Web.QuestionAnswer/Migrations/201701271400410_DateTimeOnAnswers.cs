namespace Hbs.Web.QuestionAnswer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTimeOnAnswers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answers", "CreationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Answers", "CreationDate");
        }
    }
}
