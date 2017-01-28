namespace Hbs.Web.QuestionAnswer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MarkAnswerAsCorrect : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answers", "IsCorrectAnswer", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Answers", "IsCorrectAnswer");
        }
    }
}
