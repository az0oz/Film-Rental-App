namespace VidlyWithAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class populatingGenres : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres VALUES ('Action', '1')");
            Sql("INSERT INTO Genres VALUES ('Crime', '1')");
            Sql("INSERT INTO Genres VALUES ('Action', '2')");
            Sql("INSERT INTO Genres VALUES ('Crime', '2')");
            Sql("INSERT INTO Genres VALUES ('Drama', '3')");
            Sql("INSERT INTO Genres VALUES ('Horror', '3')");
            Sql("INSERT INTO Genres VALUES ('Mystery', '3')");
            Sql("INSERT INTO Genres VALUES ('Thriller', '3')");

        }

        public override void Down()
        {
        }
    }
}
