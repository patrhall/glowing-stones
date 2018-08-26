namespace Ruby.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullParentId : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Pages", new[] { "ParentId" });
            AlterColumn("dbo.Pages", "ParentId", c => c.Int());
            CreateIndex("dbo.Pages", "ParentId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Pages", new[] { "ParentId" });
            AlterColumn("dbo.Pages", "ParentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Pages", "ParentId");
        }
    }
}
