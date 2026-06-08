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

        public int CategoriaId { get; set; }
        public int RealizadorId { get; set; }

        // relações adicionadas Categoria e Realizador
        public Categoria? Categoria { get; set; }
        public Realizador? Realizador { get; set; }

    }
}
