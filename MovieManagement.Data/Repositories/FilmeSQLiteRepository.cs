using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Data.Sqlite;
using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;

namespace MovieManagement.Data.Repositories
{
    public class FilmeSQLiteRepository : IFilmeRepository
    {
        private string _connectionString = "Data Source=moviemanagement.db";

        public FilmeSQLiteRepository()
        {
            CriarTabela();
        }

        private void CriarTabela()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
                CREATE TABLE IF NOT EXISTS Filmes (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Titulo TEXT NOT NULL,
                    Ano INTEGER NOT NULL,
                    Lingua TEXT NOT NULL,
                    Classificacao INTEGER NOT NULL,
                    CategoriaNome TEXT,
                    RealizadorNome TEXT
                );
            ";

            command.ExecuteNonQuery();
        }

        public void Adicionar(Filme filme)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
                INSERT INTO Filmes 
                (Titulo, Ano, Lingua, Classificacao, CategoriaNome, RealizadorNome)
                VALUES 
                ($titulo, $ano, $lingua, $classificacao, $categoria, $realizador);
            ";

            command.Parameters.AddWithValue("$titulo", filme.Titulo);
            command.Parameters.AddWithValue("$ano", filme.Ano);
            command.Parameters.AddWithValue("$lingua", filme.Lingua);
            command.Parameters.AddWithValue("$classificacao", filme.Classificacao);
            command.Parameters.AddWithValue("$categoria", filme.Categoria?.Nome ?? "");
            command.Parameters.AddWithValue("$realizador", filme.Realizador?.Nome ?? "");

            command.ExecuteNonQuery();
        }

        public List<Filme> ObterTodos()
        {
            List<Filme> filmes = new List<Filme>();

            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
                SELECT Id, Titulo, Ano, Lingua, Classificacao, CategoriaNome, RealizadorNome 
                FROM Filmes
            ";

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                filmes.Add(new Filme
                {
                    Id = reader.GetInt32(0),
                    Titulo = reader.GetString(1),
                    Ano = reader.GetInt32(2),
                    Lingua = reader.GetString(3),
                    Classificacao = reader.GetInt32(4),
                    Categoria = new Categoria { Nome = reader.GetString(5) },
                    Realizador = new Realizador { Nome = reader.GetString(6) }
                });
            }

            return filmes;
        }

        public Filme? ObterPorTitulo(string titulo)
        {
            return ObterTodos()
                .FirstOrDefault(f => f.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase));
        }

        public bool Remover(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Filmes WHERE Id = $id";
            command.Parameters.AddWithValue("$id", id);

            return command.ExecuteNonQuery() > 0;
        }

        public bool ExisteFilmeComTitulo(string titulo)
        {
            return ObterPorTitulo(titulo) != null;
        }
    }
}