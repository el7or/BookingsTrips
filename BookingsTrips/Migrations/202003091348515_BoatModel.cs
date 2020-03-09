namespace BookingsTrips.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BoatModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Boats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FromDate = c.DateTime(nullable: false),
                        ToDate = c.DateTime(nullable: false),
                        FloorsCount = c.Int(nullable: false),
                        IsActive = c.Boolean(),
                        IsDeleted = c.Boolean(),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        EditedBy = c.String(),
                        EditedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Floors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BoatId = c.Int(nullable: false),
                        FloorNumber = c.Int(nullable: false),
                        AllCabinsCount = c.Int(),
                        SingleCabinsCount = c.Int(),
                        DoubleCabinsCount = c.Int(),
                        TripleCabinsCount = c.Int(),
                        IsActive = c.Boolean(),
                        IsDeleted = c.Boolean(),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        EditedBy = c.String(),
                        EditedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Boats", t => t.BoatId, cascadeDelete: true)
                .Index(t => t.BoatId);
            
            CreateTable(
                "dbo.Cabins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FloorId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        Type = c.Int(nullable: false),
                        IsActive = c.Boolean(),
                        IsDeleted = c.Boolean(),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        EditedBy = c.String(),
                        EditedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Floors", t => t.FloorId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.FloorId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cabins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Cabins", "FloorId", "dbo.Floors");
            DropForeignKey("dbo.Floors", "BoatId", "dbo.Boats");
            DropIndex("dbo.Cabins", new[] { "UserId" });
            DropIndex("dbo.Cabins", new[] { "FloorId" });
            DropIndex("dbo.Floors", new[] { "BoatId" });
            DropTable("dbo.Cabins");
            DropTable("dbo.Floors");
            DropTable("dbo.Boats");
        }
    }
}
