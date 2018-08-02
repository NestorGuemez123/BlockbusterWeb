namespace VideoOnDemand.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicial2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Generoes", newName: "Generos");
            RenameTable(name: "dbo.Favoritoes", newName: "Favorito");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Favorito", newName: "Favoritoes");
            RenameTable(name: "dbo.Generos", newName: "Generoes");
        }
    }
}
