using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Data.Sqlite;
using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;

namespace MovieManagement.Data.Repositories
{
    public class RealizadorSQLiteRepository : IRealizadorRepository
    {
        private string _connectionString = "Data Source=moviemanagement.db";

        public RealizadorSQLiteRepository()
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
                CREATE TABLE IF NOT EXISTS Realizadores (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nome TEXT NOT NULL,
                    Pais TEXT NOT NULL
                );
            ";

            command.ExecuteNonQuery();
        }

        public void Adicionar(Realizador realizador)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Realizadores (Nome, Pais) VALUES ($nome, $pais)";
            command.Parameters.AddWithValue("$nome", realizador.Nome);
            command.Parameters.AddWithValue("$pais", realizador.Pais);

            command.ExecuteNonQuery();
        }

        public List<Realizador> ObterTodos()
        {
            List<Realizador> realizadores = new List<Realizador>();

            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, Nome, Pais FROM Realizadores";

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                realizadores.Add(new Realizador
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Pais = reader.GetString(2)
                });
            }

            return realizadores;
        }

        public Realizador? ObterPorNome(string nome)
        {
            return ObterTodos()
                .FirstOrDefault(r => r.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
        }

        public bool Remover(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Realizadores WHERE Id = $id";
            command.Parameters.AddWithValue("$id", id);

            return command.ExecuteNonQuery() > 0;
        }

        public bool ExisteRealizadorComNome(string nome)
        {
            return ObterPorNome(nome) != null;
        }
    }
}
