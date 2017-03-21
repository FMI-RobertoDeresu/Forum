namespace Forum.Framework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        IsEdited = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .Index(t => t.CreatedBy_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedAt = c.DateTime(nullable: false),
                        UserName = c.String(),
                        Password_Hash = c.Binary(),
                        Password_Salt = c.Binary(),
                        Password_ChangedAt = c.DateTime(),
                        Role = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        ProfileImage32Url = c.String(),
                        ProfileImage64Url = c.String(),
                        ProfileImage128Url = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        IsEdited = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy_Id = c.Int(nullable: false),
                        Category_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        IsEdited = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy_Id = c.Int(nullable: false),
                        Subject_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Subjects", t => t.Subject_Id, cascadeDelete: true)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.Subject_Id);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        IsEdited = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy_Id = c.Int(nullable: false),
                        Topic_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Topics", t => t.Topic_Id, cascadeDelete: true)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.Topic_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subjects", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Topics", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.Posts", "Topic_Id", "dbo.Topics");
            DropForeignKey("dbo.Posts", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Topics", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Subjects", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Categories", "CreatedBy_Id", "dbo.Users");
            DropIndex("dbo.Posts", new[] { "Topic_Id" });
            DropIndex("dbo.Posts", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Topics", new[] { "Subject_Id" });
            DropIndex("dbo.Topics", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Subjects", new[] { "Category_Id" });
            DropIndex("dbo.Subjects", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Categories", new[] { "CreatedBy_Id" });
            DropTable("dbo.Posts");
            DropTable("dbo.Topics");
            DropTable("dbo.Subjects");
            DropTable("dbo.Users");
            DropTable("dbo.Categories");
        }
    }
}
