namespace SnackBarServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstM : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        SnackId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateImplement = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.Snacks", t => t.SnackId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.SnackId);
            
            CreateTable(
                "dbo.Snacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SnackName = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SnackProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SnackId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Snacks", t => t.SnackId, cascadeDelete: true)
                .Index(t => t.SnackId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StockProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StockId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Stocks", t => t.StockId, cascadeDelete: true)
                .Index(t => t.StockId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StockName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SnackProducts", "SnackId", "dbo.Snacks");
            DropForeignKey("dbo.StockProducts", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.StockProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.SnackProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Orders", "SnackId", "dbo.Snacks");
            DropForeignKey("dbo.Orders", "ClientId", "dbo.Clients");
            DropIndex("dbo.StockProducts", new[] { "ProductId" });
            DropIndex("dbo.StockProducts", new[] { "StockId" });
            DropIndex("dbo.SnackProducts", new[] { "ProductId" });
            DropIndex("dbo.SnackProducts", new[] { "SnackId" });
            DropIndex("dbo.Orders", new[] { "SnackId" });
            DropIndex("dbo.Orders", new[] { "ClientId" });
            DropTable("dbo.Stocks");
            DropTable("dbo.StockProducts");
            DropTable("dbo.Products");
            DropTable("dbo.SnackProducts");
            DropTable("dbo.Snacks");
            DropTable("dbo.Orders");
            DropTable("dbo.Clients");
        }
    }
}
