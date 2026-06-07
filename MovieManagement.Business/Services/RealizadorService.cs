using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace MovieManagement.Business.Services
{
    public class RealizadorService
    {
        private IRealizadorRepository _repositorio;

        public RealizadorService(IRealizadorRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public void AdicionarRealizador(string nome, string pais)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome do realizador é obrigatório.");

            if (string.IsNullOrWhiteSpace(pais))
                throw new ArgumentException("O país do realizador é obrigatório.");

            if (_repositorio.ExisteRealizadorComNome(nome))
                throw new Exception("Já existe um realizador com esse nome.");

            Realizador realizador = new Realizador
            {
                Nome = nome,
                Pais = pais
            };

            _repositorio.Adicionar(realizador);
        }

        public List<Realizador> ListarTodos()
        {
            return _repositorio.ObterTodos();
        }

        public Realizador? ProcurarRealizador(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome do realizador é obrigatório.");

            return _repositorio.ObterPorNome(nome);
        }

        public void RemoverRealizador(int id)
        {
            bool removido = _repositorio.Remover(id);

            if (!removido)
                throw new Exception("Realizador não encontrado.");
        }
    }
}