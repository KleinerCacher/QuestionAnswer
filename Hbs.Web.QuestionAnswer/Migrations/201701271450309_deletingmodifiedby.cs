namespace Hbs.Web.QuestionAnswer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletingmodifiedby : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Questions", "ModifiedBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "ModifiedBy", c => c.String());
        }
    }
}
