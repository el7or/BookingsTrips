namespace BookingsTrips.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TripModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TripCabinsPrices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TripId = c.Int(nullable: false),
                        FloorId = c.Int(nullable: false),
                        TripSingleCabinsPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TripDoubleCabinsPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TripTripleCabinsPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsActive = c.Boolean(),
                        IsDeleted = c.Boolean(),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        EditedBy = c.String(),
                        EditedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Floors", t => t.FloorId, cascadeDelete: true)
                .ForeignKey("dbo.Trips", t => t.TripId, cascadeDelete: true)
                .Index(t => t.TripId)
                .Index(t => t.FloorId);
            
            CreateTable(
                "dbo.Trips",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartPoint = c.String(),
                        EndPoint = c.String(),
                        FromDate = c.DateTime(nullable: false),
                        ToDate = c.DateTime(nullable: false),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AdultPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TeenPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ChildPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BabyPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsActive = c.Boolean(),
                        IsDeleted = c.Boolean(),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        EditedBy = c.String(),
                        EditedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Boats", "TripId", c => c.Int());
            AddColumn("dbo.Flights", "TripId", c => c.Int());
            CreateIndex("dbo.Boats", "TripId");
            CreateIndex("dbo.Flights", "TripId");
            AddForeignKey("dbo.Flights", "TripId", "dbo.Trips", "Id");
            AddForeignKey("dbo.Boats", "TripId", "dbo.Trips", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Boats", "TripId", "dbo.Trips");
            DropForeignKey("dbo.TripCabinsPrices", "TripId", "dbo.Trips");
            DropForeignKey("dbo.Flights", "TripId", "dbo.Trips");
            DropForeignKey("dbo.TripCabinsPrices", "FloorId", "dbo.Floors");
            DropIndex("dbo.Flights", new[] { "TripId" });
            DropIndex("dbo.TripCabinsPrices", new[] { "FloorId" });
            DropIndex("dbo.TripCabinsPrices", new[] { "TripId" });
            DropIndex("dbo.Boats", new[] { "TripId" });
            DropColumn("dbo.Flights", "TripId");
            DropColumn("dbo.Boats", "TripId");
            DropTable("dbo.Trips");
            DropTable("dbo.TripCabinsPrices");
        }
    }
}
