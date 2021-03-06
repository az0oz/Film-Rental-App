namespace VidlyWithAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'0aaa45cf-05ca-4a06-8b67-f979f27e7bdb', N'az0oz_19@hotmail.com', 0, N'AMZuZc6HxrJhdkD7B40xt3nGdbhY32wsuMnEGau4VDyIjB3TjsovXi0Ku+zH+cfW4g==', N'244c6aeb-3c2f-4aeb-ab3b-130f815b0a05', NULL, 0, 0, NULL, 1, 0, N'az0oz_19@hotmail.com')
                INSERT INTO[dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES(N'c1d70afc-84cd-4bb9-bc83-153aa2a8d4fa', N'admin@vidly.com', 0, N'AJqy2pkFGeOKN5PlWLMVH+KYd9OMydOfxhifwzlF5vW7Cfc5o0YgYAn9iKmf4fmqFw==', N'0638c355-8dba-4299-9082-ea556379b849', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'b91b7fb1-098f-4378-ac90-2a722006e1f8', N'CanManageMovies')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c1d70afc-84cd-4bb9-bc83-153aa2a8d4fa', N'b91b7fb1-098f-4378-ac90-2a722006e1f8')
            ");
        }
        
        public override void Down()
        {
        }
    }
}
