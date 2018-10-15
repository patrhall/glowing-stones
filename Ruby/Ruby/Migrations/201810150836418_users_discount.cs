namespace Ruby.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class users_discount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Discount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Discount");
        }
    }
}
