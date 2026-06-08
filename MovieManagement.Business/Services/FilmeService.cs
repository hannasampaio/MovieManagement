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
        private ICategoriaRepository _categoriaRepositorio;
        private  IRealizadorRepository _realizadorRepositorio;

        // Injeção de dependência
        public FilmeService(IFilmeRepository repositorio, ICategoriaRepository categoriaRepositorio, IRealizadorRepository realizadorRepositorio)
        {
            _repositorio = repositorio;
            _categoriaRepositorio = categoriaRepositorio;
            _realizadorRepositorio = realizadorRepositorio;
        }

        public void AdicionarFilme(string titulo, int ano, string lingua, int classificacao, string nomeCategoria, string nomeRealizador)
        {
            if (string.IsNullOrWhiteSpace(titulo))
            {
                throw new ArgumentException("O título não pode ser vazio");
            }

            if (ano <= 0)
            {
                throw new ArgumentException("O ano deve ser válido");
            }

            if (string.IsNullOrWhiteSpace(lingua))
            {
                throw new ArgumentException("A língua não pode ser vazia");
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

            Categoria? categoria = _categoriaRepositorio.ObterPorNome(nomeCategoria);

            if (categoria == null)
            {
                throw new Exception("Categoria não encontrada");
            }

            Realizador? realizador = _realizadorRepositorio.ObterPorNome(nomeRealizador);

            if (realizador == null)
            {
                throw new Exception("Realizador não encontrado");
            }

            Filme novo = new Filme
            {
                Titulo = titulo,
                Ano = ano,
                Lingua = lingua,
                Classificacao = classificacao,
                CategoriaId = categoria.Id,
                RealizadorId = realizador.Id,
                Categoria = categoria,
                Realizador = realizador
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