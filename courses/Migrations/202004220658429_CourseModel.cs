namespace courses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Course", "CourseModuleId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Course", "CourseModuleId");
        }
    }
}
