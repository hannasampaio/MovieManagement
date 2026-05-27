using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace MovieManagement.Business.Services
{
    public class FilmeService
    {
        private IFilmeRepository _repositorio;

        // Injeção de dependência
        public FilmeService(IFilmeRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public void AdicionarFilme(string titulo, int ano, string lingua, int classificacao)
        {
            if (string.IsNullOrWhiteSpace(titulo))
            {
                throw new ArgumentException("O título não pode ser vazio");
            }

            if (classificacao < 0 || classificacao > 5)
            {
                throw new ArgumentException("A classificação deve estar entre 0 e 5");
            }

            bool existe = _repositorio.ExisteFilmeComTitulo(titulo);

            if (existe)
            {
                throw new Exception("Já existe um filme com esse título");
            }

            Filme novo = new Filme
            {
                Titulo = titulo,
                Ano = ano,
                Lingua = lingua,
                Classificacao = classificacao
            };

            _repositorio.Adicionar(novo);
        }

        public List<Filme> ListarTodos()
        {
            return _repositorio.ObterTodos();
        }

        public Filme? ProcurarFilme(string titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo))
            {
                throw new ArgumentException("O título não pode ser vazio");
            }

            return _repositorio.ObterPorTitulo(titulo);
        }

        public void RemoverFilme(int id)
        {
            bool removido = _repositorio.Remover(id);

            if (!removido)
            {
                throw new Exception("Filme não encontrado");
            }
        }

        public bool ExisteFilmeComTitulo(string titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo))
            {
                throw new ArgumentException("O título não pode ser vazio");
            }

            return _repositorio.ExisteFilmeComTitulo(titulo);
        }
    }
}