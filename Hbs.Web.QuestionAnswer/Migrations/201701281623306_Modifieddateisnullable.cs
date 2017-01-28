namespace Hbs.Web.QuestionAnswer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modifieddateisnullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Answers", "ModifiedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Answers", "ModifiedDate", c => c.DateTime(nullable: false));
        }
    }
}
