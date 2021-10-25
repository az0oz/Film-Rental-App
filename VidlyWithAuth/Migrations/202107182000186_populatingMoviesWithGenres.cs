namespace VidlyWithAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class populatingMoviesWithGenres : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Movies (Id, Name, ReleaseDate, DateAdded, NumberInStock) VALUES (1, 'Pulp Fiction', '19941021', '20210718', 5)");
            Sql("INSERT INTO Movies (Id, Name, ReleaseDate, DateAdded, NumberInStock) VALUES (2, 'No Sudden Move', '20210624', '20210718', 10)");
            Sql("INSERT INTO Movies (Id, Name, ReleaseDate, DateAdded, NumberInStock) VALUES (3, 'Midsommar', '20190703', '20210718', 50)");

        }

        public override void Down()
        {
        }
    }
}
