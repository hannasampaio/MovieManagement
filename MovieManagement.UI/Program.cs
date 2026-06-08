using MovieManagement.Data.Repositories;
using MovieManagement.Business.Services;

namespace MovieManagementUI
{
    internal class Program
    {
        static FilmeService filmeService;
        static CategoriaService categoriaService;
        static RealizadorService realizadorService;
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("===================================");
            Console.WriteLine("     MOVIE MANAGEMENT SYSTEM");
            Console.WriteLine("===================================");
            Console.WriteLine();
            Console.WriteLine("Escolha o tipo de persistência:");
            Console.WriteLine("1 - Memória");
            Console.WriteLine("2 - SQLite");
            Console.Write("\nOpção: ");

            string opcaoPersistencia = Console.ReadLine() ?? "";

            if (opcaoPersistencia == "2")
            {
                var filmeRepo = new FilmeSQLiteRepository();
                var categoriaRepo = new CategoriaSQLiteRepository();
                var realizadorRepo = new RealizadorSQLiteRepository();

                categoriaService = new CategoriaService(categoriaRepo);
                realizadorService = new RealizadorService(realizadorRepo);
                filmeService = new FilmeService(filmeRepo, categoriaRepo, realizadorRepo);

                Console.WriteLine("\nPersistência SQLite selecionada.");
            }
            else
            {
                var filmeRepo = new FilmeRepository();
                var categoriaRepo = new CategoriaRepository();
                var realizadorRepo = new RealizadorRepository();

                categoriaService = new CategoriaService(categoriaRepo);
                realizadorService = new RealizadorService(realizadorRepo);
                filmeService = new FilmeService(filmeRepo, categoriaRepo, realizadorRepo);

                Console.WriteLine("\nPersistência em memória selecionada.");
            }

            Console.WriteLine("\nPrima qualquer tecla para continuar...");
            Console.ReadKey();

            MenuPrincipal();

        }

        static void MenuPrincipal()
        {
          bool continuar = true;

            while (continuar)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("╔══════════════════════════╗");
                Console.WriteLine("║       MOVIE MANAGEMENT   ║");
                Console.WriteLine("╚══════════════════════════╝");
                Console.ResetColor();

                Console.WriteLine("1 - Menu Filmes");
                Console.WriteLine("2 - Menu Categorias");
                Console.WriteLine("3 - Menu Realizadores");
                Console.WriteLine("0 - Sair");

                Console.Write("\nOpção: ");
                string opcao = Console.ReadLine();

                if (opcao == "1")
                {
                    MenuFilmes();
                }
                else if (opcao == "2")
                {
                    MenuCategorias();
                }
                else if (opcao == "3")
                {
                    MenuRealizadores();
                }
                else if (opcao == "0")
                {
                    Console.WriteLine("\nPrograma encerrado.");
                    continuar = false;
                }
                else
                {
                    Console.WriteLine("\nOpção inválida.");
                    Console.ReadKey();
                }
            }
        }

        static void MenuFilmes()
        {
            bool voltar = false;

            while (!voltar)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("╔══════════════════════════╗");
                Console.WriteLine("║       MENU FILMES        ║");
                Console.WriteLine("╚══════════════════════════╝");
                Console.ResetColor();

                Console.WriteLine("1 - Adicionar Filme");
                Console.WriteLine("2 - Listar Filmes");
                Console.WriteLine("3 - Procurar Filme");
                Console.WriteLine("4 - Remover Filme");
                Console.WriteLine("0 - Voltar");

                Console.Write("\nOpção: ");
                string opcao = Console.ReadLine();

                if (opcao == "1")
                {
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("═══ ADICIONAR FILME ═══\n");
                    Console.ResetColor();

                    var categorias = categoriaService.ListarTodos();
                    var realizadores = realizadorService.ListarTodos();

                    if (categorias.Count == 0 || realizadores.Count == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Antes de adicionar um filme, é necessário cadastrar pelo menos:");
                        Console.WriteLine("- 1 categoria");
                        Console.WriteLine("- 1 realizador");
                        Console.ResetColor();

                        Console.ReadKey();
                        continue;
                    }

                    Console.Write("Título: ");
                    string titulo = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(titulo))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nErro: O título não pode ser vazio.");
                        Console.ResetColor();
                        Console.ReadKey();
                        continue;
                    }

                    if (titulo.All(c => !char.IsLetterOrDigit(c)))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nErro: O título deve conter letras ou números.");
                        Console.ResetColor();
                        Console.ReadKey();
                        continue;
                    }

                    if (filmeService.ProcurarFilme(titulo) != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nErro: Já existe um filme com esse título.");
                        Console.ResetColor();
                        Console.ReadKey();
                        continue;
                    }

                    int ano;

                    while (true)
                    {
                        Console.Write("Ano: ");

                        if (int.TryParse(Console.ReadLine(), out ano))
                            break;

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ano inválido. Tenta novamente.");
                        Console.ResetColor();
                    }

                    Console.Write("Língua: ");
                    string lingua = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(lingua))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nErro: A língua é obrigatória.");
                        Console.ResetColor();
                        Console.ReadKey();
                        continue;
                    }

                    int classificacao;

                    while (true)
                    {
                        Console.Write("Classificação (0-5) : ");

                        if (int.TryParse(Console.ReadLine(), out classificacao) && classificacao >= 0 && classificacao <= 5)
                            break;

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Classificação inválida. Deve ser um número entre 0 e 5.");
                        Console.ResetColor();
                    }

                    Console.Write("Categoria: ");
                    string categoria = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(categoria))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nErro: A categoria é obrigatória.");
                        Console.ResetColor();
                        Console.ReadKey();
                        continue;
                    }

                    Console.Write("Realizador: ");
                    string realizador = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(realizador))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nErro: O realizador é obrigatório.");
                        Console.ResetColor();
                        Console.ReadKey();
                        continue;
                    }

                    try
                    {
                        filmeService.AdicionarFilme(titulo, ano, lingua, classificacao, categoria, realizador);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nFilme adicionado com sucesso!");
                        Console.ResetColor();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nErro: " + ex.Message);
                        Console.ResetColor();
                    }

                    Console.ReadKey();
                }
                else if (opcao == "2")
                {
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("═══ LISTA DE FILMES ═══\n");
                    Console.ResetColor();

                    var lista = filmeService.ListarTodos();

                    if (lista.Count == 0)
                    {
                        Console.WriteLine("Nenhum filme registado.");
                    }
                    else
                    {
                        foreach (var f in lista)
                        {
                            Console.WriteLine(
                                $"{f.Id} | {f.Titulo} | {f.Ano} | {f.Lingua} | {f.Classificacao} | {f.Categoria?.Nome} | {f.Realizador?.Nome}"
                            );
                        }
                    }

                    Console.ReadKey();
                }
                else if (opcao == "3")
                {
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("═══ PROCURAR FILME ═══\n");
                    Console.ResetColor();

                    Console.Write("Título: ");
                    string titulo = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(titulo))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nErro: O título não pode ser vazio.");
                        Console.ResetColor();
                        Console.ReadKey();
                        continue;
                    }

                    var filme = filmeService.ProcurarFilme(titulo);

                    if (filme == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nFilme não encontrado.");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nFilme encontrado:\n");
                        Console.ResetColor();

                        Console.WriteLine($"ID: {filme.Id}");
                        Console.WriteLine($"Título: {filme.Titulo}");
                        Console.WriteLine($"Ano: {filme.Ano}");
                        Console.WriteLine($"Língua: {filme.Lingua}");
                        Console.WriteLine($"Classificação: {filme.Classificacao}");
                        Console.WriteLine($"Categoria: {filme.Categoria?.Nome}");
                        Console.WriteLine($"Realizador: {filme.Realizador?.Nome}");
                    }

                    Console.ReadKey();
                }
                else if (opcao == "4")
                {
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("═══ REMOVER FILME ═══\n");
                    Console.ResetColor();

                    int id;

                    while (true)
                    {
                        Console.Write("ID do filme: ");

                        if (int.TryParse(Console.ReadLine(), out id))
                            break;

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ID inválido. Tenta novamente.");
                        Console.ResetColor();
                    }

                    try
                    {
                        filmeService.RemoverFilme(id);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nFilme removido com sucesso!");
                        Console.ResetColor();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nErro: " + ex.Message);
                        Console.ResetColor();
                    }

                    Console.ReadKey();
                }
                else if (opcao == "0")
                {
                    voltar = true;

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nOpção inválida.");
                    Console.ResetColor();

                    Console.ReadKey();
                }
            }
        }

        static void MenuCategorias()
        {
            bool voltar = false;

            while (!voltar)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("╔══════════════════════════╗");
                Console.WriteLine("║     MENU CATEGORIAS      ║");
                Console.WriteLine("╚══════════════════════════╝");
                Console.ResetColor();

                Console.WriteLine("1 - Adicionar Categoria");
                Console.WriteLine("2 - Listar Categorias");
                Console.WriteLine("3 - Procurar Categoria");
                Console.WriteLine("4 - Remover Categoria");
                Console.WriteLine("0 - Voltar");

                Console.Write("\nOpção: ");
                string opcao = Console.ReadLine();

                if (opcao == "1")
                {
                    Console.Clear();
                    Console.WriteLine("═══ ADICIONAR CATEGORIA ═══\n");

                    Console.Write("Nome: ");
                    string nome = Console.ReadLine();

                    try
                    {
                        categoriaService.AdicionarCategoria(nome);
                        Console.WriteLine("\nCategoria adicionada com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("\nErro: " + ex.Message);
                    }

                    Console.ReadKey();
                }
                else if (opcao == "2")
                {
                    Console.Clear();
                    Console.WriteLine("═══ LISTA DE CATEGORIAS ═══\n");

                    var lista = categoriaService.ListarTodos();

                    if (lista.Count == 0)
                    {
                        Console.WriteLine("Nenhuma categoria registada.");
                    }
                    else
                    {
                        foreach (var c in lista)
                        {
                            Console.WriteLine($"{c.Id} | {c.Nome}");
                        }
                    }

                    Console.ReadKey();
                }
                else if (opcao == "3")
                {
                    Console.Clear();
                    Console.WriteLine("═══ PROCURAR CATEGORIA ═══\n");

                    Console.Write("Nome: ");
                    string nome = Console.ReadLine();

                    var categoria = categoriaService.ProcurarCategoria(nome);

                    if (categoria == null)
                    {
                        Console.WriteLine("\nCategoria não encontrada.");
                    }
                    else
                    {
                        Console.WriteLine($"\nID: {categoria.Id}");
                        Console.WriteLine($"Nome: {categoria.Nome}");
                    }

                    Console.ReadKey();
                }
                else if (opcao == "4")
                {
                    Console.Clear();
                    Console.WriteLine("═══ REMOVER CATEGORIA ═══\n");

                    int id;

                    while (true)
                    {
                        Console.Write("ID da categoria: ");

                        if (int.TryParse(Console.ReadLine(), out id))
                            break;

                        Console.WriteLine("ID inválido. Tenta novamente.");
                    }

                    try
                    {
                        categoriaService.RemoverCategoria(id);
                        Console.WriteLine("\nCategoria removida com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("\nErro: " + ex.Message);
                    }

                    Console.ReadKey();
                }
                else if (opcao == "0")
                {
                    voltar = true;
                }
                else
                {
                    Console.WriteLine("\nOpção inválida.");
                    Console.ReadKey();
                }
            }
        }

        static void MenuRealizadores()
        {
            bool voltar = false;

            while (!voltar)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("╔══════════════════════════╗");
                Console.WriteLine("║    MENU REALIZADORES     ║");
                Console.WriteLine("╚══════════════════════════╝");
                Console.ResetColor();

                Console.WriteLine("1 - Adicionar Realizador");
                Console.WriteLine("2 - Listar Realizadores");
                Console.WriteLine("3 - Procurar Realizador");
                Console.WriteLine("4 - Remover Realizador");
                Console.WriteLine("0 - Voltar");

                Console.Write("\nOpção: ");
                string opcao = Console.ReadLine();

                if (opcao == "1")
                {
                    Console.Clear();
                    Console.WriteLine("═══ ADICIONAR REALIZADOR ═══\n");

                    Console.Write("Nome: ");
                    string nome = Console.ReadLine();

                    Console.Write("País: ");
                    string pais = Console.ReadLine();

                    try
                    {
                        realizadorService.AdicionarRealizador(nome, pais);
                        Console.WriteLine("\nRealizador adicionado com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("\nErro: " + ex.Message);
                    }

                    Console.ReadKey();
                }
                else if (opcao == "2")
                {
                    Console.Clear();
                    Console.WriteLine("═══ LISTA DE REALIZADORES ═══\n");

                    var lista = realizadorService.ListarTodos();

                    if (lista.Count == 0)
                    {
                        Console.WriteLine("Nenhum realizador registado.");
                    }
                    else
                    {
                        foreach (var r in lista)
                        {
                            Console.WriteLine($"{r.Id} | {r.Nome} | {r.Pais}");
                        }
                    }

                    Console.ReadKey();
                }
                else if (opcao == "3")
                {
                    Console.Clear();
                    Console.WriteLine("═══ PROCURAR REALIZADOR ═══\n");

                    Console.Write("Nome: ");
                    string nome = Console.ReadLine();

                    var realizador = realizadorService.ProcurarRealizador(nome);

                    if (realizador == null)
                    {
                        Console.WriteLine("\nRealizador não encontrado.");
                    }
                    else
                    {
                        Console.WriteLine($"\nID: {realizador.Id}");
                        Console.WriteLine($"Nome: {realizador.Nome}");
                        Console.WriteLine($"País: {realizador.Pais}");
                    }

                    Console.ReadKey();
                }
                else if (opcao == "4")
                {
                    Console.Clear();
                    Console.WriteLine("═══ REMOVER REALIZADOR ═══\n");

                    int id;

                    while (true)
                    {
                        Console.Write("ID do realizador: ");

                        if (int.TryParse(Console.ReadLine(), out id))
                            break;

                        Console.WriteLine("ID inválido. Tenta novamente.");
                    }

                    try
                    {
                        realizadorService.RemoverRealizador(id);
                        Console.WriteLine("\nRealizador removido com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("\nErro: " + ex.Message);
                    }

                    Console.ReadKey();
                }
                else if (opcao == "0")
                {
                    voltar = true;
                }
                else
                {
                    Console.WriteLine("\nOpção inválida.");
                    Console.ReadKey();
                }
            }
        }
    }
}