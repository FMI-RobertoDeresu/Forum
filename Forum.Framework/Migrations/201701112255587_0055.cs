namespace Forum.Framework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0055 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Categories", name: "UserId", newName: "CreatedBy_Id");
            RenameColumn(table: "dbo.Subjects", name: "UserId", newName: "CreatedBy_Id");
            RenameColumn(table: "dbo.Topics", name: "UserId", newName: "CreatedBy_Id");
            RenameColumn(table: "dbo.Posts", name: "UserId", newName: "CreatedBy_Id");
            RenameIndex(table: "dbo.Categories", name: "IX_UserId", newName: "IX_CreatedBy_Id");
            RenameIndex(table: "dbo.Subjects", name: "IX_UserId", newName: "IX_CreatedBy_Id");
            RenameIndex(table: "dbo.Topics", name: "IX_UserId", newName: "IX_CreatedBy_Id");
            RenameIndex(table: "dbo.Posts", name: "IX_UserId", newName: "IX_CreatedBy_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Posts", name: "IX_CreatedBy_Id", newName: "IX_UserId");
            RenameIndex(table: "dbo.Topics", name: "IX_CreatedBy_Id", newName: "IX_UserId");
            RenameIndex(table: "dbo.Subjects", name: "IX_CreatedBy_Id", newName: "IX_UserId");
            RenameIndex(table: "dbo.Categories", name: "IX_CreatedBy_Id", newName: "IX_UserId");
            RenameColumn(table: "dbo.Posts", name: "CreatedBy_Id", newName: "UserId");
            RenameColumn(table: "dbo.Topics", name: "CreatedBy_Id", newName: "UserId");
            RenameColumn(table: "dbo.Subjects", name: "CreatedBy_Id", newName: "UserId");
            RenameColumn(table: "dbo.Categories", name: "CreatedBy_Id", newName: "UserId");
        }
    }
}
