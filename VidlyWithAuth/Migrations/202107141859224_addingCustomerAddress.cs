namespace VidlyWithAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingCustomerAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Address", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "Address");
        }
    }
}
