namespace Hbs.Web.QuestionAnswer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renamingModifiedDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "ModifiedDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Questions", "ModifiedData");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "ModifiedData", c => c.DateTime(nullable: false));
            DropColumn("dbo.Questions", "ModifiedDate");
        }
    }
}
