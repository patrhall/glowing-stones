namespace Ruby.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class typeImageNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductTypes", "Image", c => c.Byte());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductTypes", "Image", c => c.Byte(nullable: false));
        }
    }
}
