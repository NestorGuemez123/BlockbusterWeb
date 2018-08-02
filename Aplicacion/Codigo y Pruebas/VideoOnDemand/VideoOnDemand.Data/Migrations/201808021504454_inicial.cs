namespace VideoOnDemand.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Media",
                c => new
                    {
                        MediaId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Descripcion = c.String(maxLength: 500),
                        DuracionMin = c.Int(),
                        FechaRegistro = c.DateTime(),
                        FechaLanzamiento = c.DateTime(),
                        Activo = c.Boolean(nullable: false),
                        EstadosMedia = c.Int(),
                    })
                .PrimaryKey(t => t.MediaId);
            
            CreateTable(
                "dbo.Personas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Descripcion = c.String(),
                        FechaNacimiento = c.DateTime(),
                        Status = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Generoes",
                c => new
                    {
                        GeneroId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        Descripcion = c.String(maxLength: 500),
                        Activo = c.Boolean(),
                    })
                .PrimaryKey(t => t.GeneroId);
            
            CreateTable(
                "dbo.Opinions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Puntuacion = c.Int(),
                        Descripcion = c.String(),
                        FechaRegistro = c.DateTime(),
                        UsuarioId = c.Int(),
                        MediaId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Media", t => t.MediaId)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId)
                .Index(t => t.UsuarioId)
                .Index(t => t.MediaId);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 200),
                        IdentityId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Favoritoes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        FechaAgregado = c.DateTime(nullable: false),
                        usuarioId = c.Int(nullable: false),
                        mediaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Media", t => t.mediaId, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.usuarioId, cascadeDelete: true)
                .Index(t => t.usuarioId)
                .Index(t => t.mediaId);
            
            CreateTable(
                "dbo.MediaOnPlays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Milisegundo = c.Long(),
                        MediaId = c.Int(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Media", t => t.MediaId, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.MediaId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Media-Actor",
                c => new
                    {
                        MediaId = c.Int(nullable: false),
                        ActoresId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MediaId, t.ActoresId })
                .ForeignKey("dbo.Media", t => t.MediaId, cascadeDelete: true)
                .ForeignKey("dbo.Personas", t => t.ActoresId, cascadeDelete: true)
                .Index(t => t.MediaId)
                .Index(t => t.ActoresId);
            
            CreateTable(
                "dbo.Media-Genero",
                c => new
                    {
                        MediaId = c.Int(nullable: false),
                        GeneroId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MediaId, t.GeneroId })
                .ForeignKey("dbo.Media", t => t.MediaId, cascadeDelete: true)
                .ForeignKey("dbo.Generoes", t => t.GeneroId, cascadeDelete: true)
                .Index(t => t.MediaId)
                .Index(t => t.GeneroId);
            
            CreateTable(
                "dbo.Episodios",
                c => new
                    {
                        MediaId = c.Int(nullable: false),
                        Temporada = c.Int(),
                        SerieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MediaId)
                .ForeignKey("dbo.Media", t => t.MediaId)
                .ForeignKey("dbo.Series", t => t.SerieId)
                .Index(t => t.MediaId)
                .Index(t => t.SerieId);
            
            CreateTable(
                "dbo.Series",
                c => new
                    {
                        MediaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MediaId)
                .ForeignKey("dbo.Media", t => t.MediaId)
                .Index(t => t.MediaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Series", "MediaId", "dbo.Media");
            DropForeignKey("dbo.Episodios", "SerieId", "dbo.Series");
            DropForeignKey("dbo.Episodios", "MediaId", "dbo.Media");
            DropForeignKey("dbo.MediaOnPlays", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.MediaOnPlays", "MediaId", "dbo.Media");
            DropForeignKey("dbo.Favoritoes", "usuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.Favoritoes", "mediaId", "dbo.Media");
            DropForeignKey("dbo.Opinions", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.Opinions", "MediaId", "dbo.Media");
            DropForeignKey("dbo.Media-Genero", "GeneroId", "dbo.Generoes");
            DropForeignKey("dbo.Media-Genero", "MediaId", "dbo.Media");
            DropForeignKey("dbo.Media-Actor", "ActoresId", "dbo.Personas");
            DropForeignKey("dbo.Media-Actor", "MediaId", "dbo.Media");
            DropIndex("dbo.Series", new[] { "MediaId" });
            DropIndex("dbo.Episodios", new[] { "SerieId" });
            DropIndex("dbo.Episodios", new[] { "MediaId" });
            DropIndex("dbo.Media-Genero", new[] { "GeneroId" });
            DropIndex("dbo.Media-Genero", new[] { "MediaId" });
            DropIndex("dbo.Media-Actor", new[] { "ActoresId" });
            DropIndex("dbo.Media-Actor", new[] { "MediaId" });
            DropIndex("dbo.MediaOnPlays", new[] { "UsuarioId" });
            DropIndex("dbo.MediaOnPlays", new[] { "MediaId" });
            DropIndex("dbo.Favoritoes", new[] { "mediaId" });
            DropIndex("dbo.Favoritoes", new[] { "usuarioId" });
            DropIndex("dbo.Opinions", new[] { "MediaId" });
            DropIndex("dbo.Opinions", new[] { "UsuarioId" });
            DropTable("dbo.Series");
            DropTable("dbo.Episodios");
            DropTable("dbo.Media-Genero");
            DropTable("dbo.Media-Actor");
            DropTable("dbo.MediaOnPlays");
            DropTable("dbo.Favoritoes");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Opinions");
            DropTable("dbo.Generoes");
            DropTable("dbo.Personas");
            DropTable("dbo.Media");
        }
    }
}
