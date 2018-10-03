namespace Ruby.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thumb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ThumbImage", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ThumbImage");
        }
    }
}
