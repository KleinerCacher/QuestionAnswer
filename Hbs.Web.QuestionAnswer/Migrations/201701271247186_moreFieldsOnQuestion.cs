namespace Hbs.Web.QuestionAnswer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class moreFieldsOnQuestion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "ModifiedData", c => c.DateTime(nullable: false));
            AddColumn("dbo.Questions", "ModifiedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "ModifiedBy");
            DropColumn("dbo.Questions", "ModifiedData");
        }
    }
}
