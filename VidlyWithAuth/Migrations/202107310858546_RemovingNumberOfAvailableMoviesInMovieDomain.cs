namespace VidlyWithAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovingNumberOfAvailableMoviesInMovieDomain : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Movies", "NumberAvailable");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "NumberAvailable", c => c.Int(nullable: false));
        }
    }
}
