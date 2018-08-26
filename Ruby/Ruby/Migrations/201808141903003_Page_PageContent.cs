namespace Ruby.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Page_PageContent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pages", "PageContent", c => c.String());
            DropColumn("dbo.Pages", "Content");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pages", "Content", c => c.String());
            DropColumn("dbo.Pages", "PageContent");
        }
    }
}
