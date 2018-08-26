namespace Ruby.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class product_type_image : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Comment", c => c.String());
            DropColumn("dbo.Products", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Image", c => c.Byte(nullable: false));
            DropColumn("dbo.Products", "Comment");
        }
    }
}
