namespace Ruby.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class product_type_remove2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "ProductTypeId", "dbo.ProductTypes");
            DropIndex("dbo.Products", new[] { "ProductTypeId" });
            DropColumn("dbo.Products", "ProductTypeId");
            DropTable("dbo.ProductTypes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Color = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Products", "ProductTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "ProductTypeId");
            AddForeignKey("dbo.Products", "ProductTypeId", "dbo.ProductTypes", "Id", cascadeDelete: true);
        }
    }
}
