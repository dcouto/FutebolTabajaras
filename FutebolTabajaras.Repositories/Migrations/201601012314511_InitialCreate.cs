namespace FutebolTabajaras.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accomplishments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        AwayTeam_ID = c.Int(),
                        HomeTeam_ID = c.Int(),
                        Team_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Teams", t => t.AwayTeam_ID)
                .ForeignKey("dbo.Teams", t => t.HomeTeam_ID)
                .ForeignKey("dbo.Teams", t => t.Team_ID)
                .Index(t => t.AwayTeam_ID)
                .Index(t => t.HomeTeam_ID)
                .Index(t => t.Team_ID);
            
            CreateTable(
                "dbo.PlayerAccomplishments",
                c => new
                    {
                        Player_ID = c.Int(nullable: false),
                        Accomplishment_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Player_ID, t.Accomplishment_ID })
                .ForeignKey("dbo.Players", t => t.Player_ID, cascadeDelete: true)
                .ForeignKey("dbo.Accomplishments", t => t.Accomplishment_ID, cascadeDelete: true)
                .Index(t => t.Player_ID)
                .Index(t => t.Accomplishment_ID);
            
            CreateTable(
                "dbo.TeamPlayers",
                c => new
                    {
                        Team_ID = c.Int(nullable: false),
                        Player_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Team_ID, t.Player_ID })
                .ForeignKey("dbo.Teams", t => t.Team_ID, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.Player_ID, cascadeDelete: true)
                .Index(t => t.Team_ID)
                .Index(t => t.Player_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamPlayers", "Player_ID", "dbo.Players");
            DropForeignKey("dbo.TeamPlayers", "Team_ID", "dbo.Teams");
            DropForeignKey("dbo.Matches", "Team_ID", "dbo.Teams");
            DropForeignKey("dbo.Matches", "HomeTeam_ID", "dbo.Teams");
            DropForeignKey("dbo.Matches", "AwayTeam_ID", "dbo.Teams");
            DropForeignKey("dbo.PlayerAccomplishments", "Accomplishment_ID", "dbo.Accomplishments");
            DropForeignKey("dbo.PlayerAccomplishments", "Player_ID", "dbo.Players");
            DropIndex("dbo.TeamPlayers", new[] { "Player_ID" });
            DropIndex("dbo.TeamPlayers", new[] { "Team_ID" });
            DropIndex("dbo.PlayerAccomplishments", new[] { "Accomplishment_ID" });
            DropIndex("dbo.PlayerAccomplishments", new[] { "Player_ID" });
            DropIndex("dbo.Matches", new[] { "Team_ID" });
            DropIndex("dbo.Matches", new[] { "HomeTeam_ID" });
            DropIndex("dbo.Matches", new[] { "AwayTeam_ID" });
            DropTable("dbo.TeamPlayers");
            DropTable("dbo.PlayerAccomplishments");
            DropTable("dbo.Matches");
            DropTable("dbo.Teams");
            DropTable("dbo.Players");
            DropTable("dbo.Accomplishments");
        }
    }
}
