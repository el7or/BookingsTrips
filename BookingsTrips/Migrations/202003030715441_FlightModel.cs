namespace BookingsTrips.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FlightModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Flights",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FromAirport = c.String(),
                        ToAirport = c.String(),
                        FromDate = c.DateTime(nullable: false),
                        ToDate = c.DateTime(nullable: false),
                        Seats = c.Int(nullable: false),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsActive = c.Boolean(),
                        IsDeleted = c.Boolean(),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        EditedBy = c.String(),
                        EditedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Flights");
        }
    }
}
