namespace courses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseModule : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Module", "CourseId", c => c.Int(nullable: false));
            DropColumn("dbo.Module", "ProductId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Module", "ProductId", c => c.Int(nullable: false));
            DropColumn("dbo.Module", "CourseId");
        }
    }
}
