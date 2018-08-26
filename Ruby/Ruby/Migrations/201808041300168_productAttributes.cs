namespace Ruby.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productAttributes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "InternalId", c => c.String());
            AddColumn("dbo.Products", "Color", c => c.String());
            AddColumn("dbo.Products", "Weight", c => c.Double());
            AddColumn("dbo.Products", "Size", c => c.String());
            AddColumn("dbo.Products", "Clean", c => c.String());
            AddColumn("dbo.Products", "Pieces", c => c.Int());
            AddColumn("dbo.Products", "Shape", c => c.String());
            AddColumn("dbo.Products", "Cut", c => c.String());
            AddColumn("dbo.Products", "Treatment", c => c.String());
            AddColumn("dbo.Products", "Origin", c => c.String());
            AlterColumn("dbo.Products", "Price", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Price", c => c.Double(nullable: false));
            DropColumn("dbo.Products", "Origin");
            DropColumn("dbo.Products", "Treatment");
            DropColumn("dbo.Products", "Cut");
            DropColumn("dbo.Products", "Shape");
            DropColumn("dbo.Products", "Pieces");
            DropColumn("dbo.Products", "Clean");
            DropColumn("dbo.Products", "Size");
            DropColumn("dbo.Products", "Weight");
            DropColumn("dbo.Products", "Color");
            DropColumn("dbo.Products", "InternalId");
        }
    }
}
