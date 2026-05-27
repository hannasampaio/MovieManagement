using MovieManagement.Data.Repositories;
using MovieManagement.Business.Services;

namespace MovieManagementUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FilmeRepository repo = new FilmeRepository();
            FilmeService servico = new FilmeService(repo);

            bool continuar = true;

            while (continuar)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("╔══════════════════════════╗");
                Console.WriteLine("║       MENU FILMES       ║");
                Console.WriteLine("╚══════════════════════════╝");
                Console.ResetColor();

                Console.WriteLine("1 - Adicionar Filme");
                Console.WriteLine("2 - Listar Filmes");
                Console.WriteLine("3 - Procurar Filme");
                Console.WriteLine("4 - Remover Filme");
                Console.WriteLine("0 - Sair");

                Console.Write("\nOpção: ");
                string opcao = Console.ReadLine();

                if (opcao == "1")
                {
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("═══ ADICIONAR FILME ═══\n");
                    Console.ResetColor();

                    Console.Write("Título: ");
                    string titulo = Console.ReadLine();

                    if (servico.ProcurarFilme(titulo) != null)
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

                    int classificacao;

                    while (true)
                    {
                        Console.Write("Classificação: ");

                        if (int.TryParse(Console.ReadLine(), out classificacao))
                            break;

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Classificação inválida. Tenta novamente.");
                        Console.ResetColor();
                    }

                    try
                    {
                        servico.AdicionarFilme(titulo, ano, lingua, classificacao);

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

                    var lista = servico.ListarTodos();

                    if (lista.Count == 0)
                    {
                        Console.WriteLine("Nenhum filme registado.");
                    }
                    else
                    {
                        foreach (var f in lista)
                        {
                            Console.WriteLine(
                                $"{f.Id} | {f.Titulo} | {f.Ano} | {f.Lingua} | {f.Classificacao}"
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

                    var filme = servico.ProcurarFilme(titulo);

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
                        servico.RemoverFilme(id);

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
                    continuar = false;

                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("\nPrograma encerrado.");
                    Console.ResetColor();
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
    }
}