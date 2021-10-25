namespace VidlyWithAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class populatingBirthdatesValuesToCustomers : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Customers SET Birthdate = '01/05/1990' WHERE Id = 1");
            Sql("UPDATE Customers SET Birthdate = '12/09/1980' WHERE Id = 2");
        }

        public override void Down()
        {
        }
    }
}
