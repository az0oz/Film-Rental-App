// <auto-generated />
namespace VidlyWithAuth.Migrations
{
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    [GeneratedCode("EntityFramework.Migrations", "6.1.3-40302")]
    public sealed partial class RequiringMin18YearsOldCustomerToGetMembership : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(RequiringMin18YearsOldCustomerToGetMembership));
        
        string IMigrationMetadata.Id
        {
            get { return "202107220011213_RequiringMin18YearsOldCustomerToGetMembership"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}
