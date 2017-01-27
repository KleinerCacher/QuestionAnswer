namespace Hbs.Web.QuestionAnswer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingModifiedDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answers", "ModifiedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Answers", "ModifiedDate");
        }
    }
}
