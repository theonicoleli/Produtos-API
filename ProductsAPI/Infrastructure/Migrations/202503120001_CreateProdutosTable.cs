using FluentMigrator;

namespace Infrastructure.Migrations
{
    [Migration(202503120001)]
    public class CreateProdutosTable : Migration
    {
        public override void Up()
        {
            Create.Table("Produtos")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Nome").AsString(100).NotNullable()
                .WithColumn("Descricao").AsString().Nullable()
                .WithColumn("Preco").AsDecimal(18, 2).NotNullable()
                .WithColumn("DataCadastro").AsDateTime().NotNullable()
                    .WithDefault(SystemMethods.CurrentDateTime);
        }

        public override void Down()
        {
            Delete.Table("Produtos");
        }
    }
}
