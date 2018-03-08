namespace ElementsWeb.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class initialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BaseAtributes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    MaxHealth = c.Single(nullable: false),
                    HealthRegen = c.Single(nullable: false),
                    MaxResource = c.Single(nullable: false),
                    ResourceRegen = c.Single(nullable: false),
                    AttackDamage = c.Single(nullable: false),
                    MagicDamage = c.Single(nullable: false),
                    Armor = c.Int(nullable: false),
                    Stamina = c.Int(nullable: false),
                    Strength = c.Int(nullable: false),
                    Intellect = c.Int(nullable: false),
                    Agility = c.Int(nullable: false),
                    CharacterClass_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CharacterClasses", t => t.CharacterClass_Id)
                .Index(t => t.CharacterClass_Id);

            CreateTable(
                "dbo.CharacterClasses",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false),
                    Description = c.String(),
                    CreatedOn = c.DateTime(nullable: false),
                    ModifiedOn = c.DateTime(),
                    IsDeleted = c.Boolean(nullable: false),
                    DeletedOn = c.DateTime(),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.IsDeleted);

            CreateTable(
                "dbo.Characters",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false),
                    Level = c.Int(nullable: false),
                    CurentExperiense = c.Long(nullable: false),
                    Location = c.String(),
                    Rotation = c.String(),
                    CreatedOn = c.DateTime(nullable: false),
                    ModifiedOn = c.DateTime(),
                    IsDeleted = c.Boolean(nullable: false),
                    DeletedOn = c.DateTime(),
                    Class_Id = c.Int(),
                    User_Id = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CharacterClasses", t => t.Class_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.IsDeleted)
                .Index(t => t.Class_Id)
                .Index(t => t.User_Id);

            CreateTable(
                "dbo.AspNetUsers",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    IsDeleted = c.Boolean(nullable: false),
                    DeletedOn = c.DateTime(),
                    CreatedOn = c.DateTime(nullable: false),
                    ModifiedOn = c.DateTime(),
                    Email = c.String(maxLength: 256),
                    EmailConfirmed = c.Boolean(nullable: false),
                    PasswordHash = c.String(),
                    SecurityStamp = c.String(),
                    PhoneNumber = c.String(),
                    PhoneNumberConfirmed = c.Boolean(nullable: false),
                    TwoFactorEnabled = c.Boolean(nullable: false),
                    LockoutEndDateUtc = c.DateTime(),
                    LockoutEnabled = c.Boolean(nullable: false),
                    AccessFailedCount = c.Int(nullable: false),
                    UserName = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");

            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.String(nullable: false, maxLength: 128),
                    ClaimType = c.String(),
                    ClaimValue = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                {
                    LoginProvider = c.String(nullable: false, maxLength: 128),
                    ProviderKey = c.String(nullable: false, maxLength: 128),
                    UserId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128),
                    RoleId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            CreateTable(
                "dbo.AspNetRoles",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");

            CreateTable(
                "dbo.ServerSettings",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Version = c.String(nullable: false),
                    URL = c.String(nullable: false),
                    CreatedOn = c.DateTime(nullable: false),
                    ModifiedOn = c.DateTime(),
                    IsDeleted = c.Boolean(nullable: false),
                    DeletedOn = c.DateTime(),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.IsDeleted);

        }

        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Characters", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Characters", "Class_Id", "dbo.CharacterClasses");
            DropForeignKey("dbo.BaseAtributes", "CharacterClass_Id", "dbo.CharacterClasses");
            DropIndex("dbo.ServerSettings", new[] { "IsDeleted" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Characters", new[] { "User_Id" });
            DropIndex("dbo.Characters", new[] { "Class_Id" });
            DropIndex("dbo.Characters", new[] { "IsDeleted" });
            DropIndex("dbo.CharacterClasses", new[] { "IsDeleted" });
            DropIndex("dbo.BaseAtributes", new[] { "CharacterClass_Id" });
            DropTable("dbo.ServerSettings");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Characters");
            DropTable("dbo.CharacterClasses");
            DropTable("dbo.BaseAtributes");
        }
    }
}
