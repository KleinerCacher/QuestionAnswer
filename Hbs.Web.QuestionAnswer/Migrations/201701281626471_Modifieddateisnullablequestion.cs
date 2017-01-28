namespace Hbs.Web.QuestionAnswer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modifieddateisnullablequestion : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Questions", "ModifiedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Questions", "ModifiedDate", c => c.DateTime(nullable: false));
        }
    }
}
