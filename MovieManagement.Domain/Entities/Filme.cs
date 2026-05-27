namespace MovieManagement.Domain.Entities
{
    public class Filme
    {
        // modelo de dados do sistema

        public int Id { get; set; }
        public string Titulo { get; set; }
        public int Ano { get; set; }
        public string Lingua { get; set; }
        public int Classificacao { get; set; }

        public Filme()
        {
            Titulo = string.Empty; // evitar null
            Lingua = string.Empty;
        }
    }
}
