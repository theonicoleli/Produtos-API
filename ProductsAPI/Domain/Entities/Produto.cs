namespace Domain.Entities
{
    public class Produto
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public string Descricao { get; set; }
        public required decimal Preco { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}
