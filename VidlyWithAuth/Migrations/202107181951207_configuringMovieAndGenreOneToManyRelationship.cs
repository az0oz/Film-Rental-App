namespace VidlyWithAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class configuringMovieAndGenreOneToManyRelationship : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.MovieId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Genres", "MovieId", "dbo.Movies");
            DropIndex("dbo.Genres", new[] { "MovieId" });
            DropTable("dbo.Genres");
        }
    }
}
