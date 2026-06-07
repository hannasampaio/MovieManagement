namespace MovieManagement.Domain.Entities
{
    public class Filme
    {
        // modelo de dados do sistema

        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public int Ano { get; set; }
        public string Lingua { get; set; } = string.Empty;
        public int Classificacao { get; set; }

    }
}
