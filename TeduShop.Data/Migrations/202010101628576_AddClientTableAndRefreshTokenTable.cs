namespace TeduShop.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddClientTableAndRefreshTokenTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    ClientId = c.String(nullable: false, maxLength: 500, unicode: false),
                    ClientSecret = c.String(nullable: false, maxLength: 500, unicode: false),
                    ClientName = c.String(nullable: false, maxLength: 100, unicode: false),
                    CreatedDate = c.DateTime(nullable: false),
                    RefreshTokenLifeTime = c.Int(nullable: false),
                    AllowedOrigin = c.String(nullable: false, maxLength: 500, unicode: false),
                    IsActive = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.RefreshTokens",
                c => new
                {
                    ID = c.String(nullable: false, maxLength: 500, unicode: false),
                    ClientId = c.String(nullable: false, maxLength: 500, unicode: false),
                    UserName = c.String(nullable: false, maxLength: 500, unicode: false),
                    IssuedTime = c.DateTime(nullable: false),
                    ExpiredTime = c.DateTime(nullable: false),
                    ProtectedTicket = c.String(nullable: false, maxLength: 500, unicode: false),
                })
                .PrimaryKey(t => t.ID);
        }

        public override void Down()
        {
            DropTable("dbo.RefreshTokens");
            DropTable("dbo.Clients");
        }
    }
}