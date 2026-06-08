using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Data.Sqlite;
using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;

namespace MovieManagement.Data.Repositories
{
    public class CategoriaSQLiteRepository : ICategoriaRepository
    {
        private string _connectionString = "Data Source=moviemanagement.db";

        public CategoriaSQLiteRepository()
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
                CREATE TABLE IF NOT EXISTS Categorias (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nome TEXT NOT NULL
                );
            ";

            command.ExecuteNonQuery();
        }

        public void Adicionar(Categoria categoria)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Categorias (Nome) VALUES ($nome)";
            command.Parameters.AddWithValue("$nome", categoria.Nome);

            command.ExecuteNonQuery();
        }

        public List<Categoria> ObterTodos()
        {
            List<Categoria> categorias = new List<Categoria>();

            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, Nome FROM Categorias";

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                categorias.Add(new Categoria
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1)
                });
            }

            return categorias;
        }

        public Categoria? ObterPorNome(string nome)
        {
            return ObterTodos()
                .FirstOrDefault(c => c.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
        }

        public bool Remover(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Categorias WHERE Id = $id";
            command.Parameters.AddWithValue("$id", id);

            return command.ExecuteNonQuery() > 0;
        }

        public bool ExisteCategoriaComNome(string nome)
        {
            return ObterPorNome(nome) != null;
        }
    }
}
