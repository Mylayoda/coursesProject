namespace courses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserSubscriptions", newName: "StudentSubscriptions");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.StudentSubscriptions", newName: "UserSubscriptions");
        }
    }
}
