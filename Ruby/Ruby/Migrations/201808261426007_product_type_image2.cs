namespace Ruby.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class product_type_image2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductTypes", "Color", c => c.String());
            DropColumn("dbo.ProductTypes", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductTypes", "Image", c => c.Byte());
            DropColumn("dbo.ProductTypes", "Color");
        }
    }
}
