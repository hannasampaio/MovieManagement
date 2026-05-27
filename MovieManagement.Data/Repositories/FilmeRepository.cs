using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.Data.Repositories
{
    public class FilmeRepository : IFilmeRepository
    {
        private List<Filme> _filmes;
        private int _proximoId;

        public FilmeRepository()
        {
            _filmes = new List<Filme>();
            _proximoId = 1;
        }

        public void Adicionar(Filme filme)
        {
            filme.Id = _proximoId;
            _proximoId++;

            _filmes.Add(filme);
        }

        public List<Filme> ObterTodos()
        {
            return _filmes;
        }

        public Filme? ObterPorTitulo(string titulo)
        {
            foreach (Filme f in _filmes)
            {
                if (f.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase))
                {
                    return f;
                }
            }
            return null;
        }

        public bool Remover(int id)
        {
            Filme? filme = null;

            foreach (Filme f in _filmes)
            {
                if (f.Id == id)
                {
                    filme = f;
                    break;
                }
            }

            if (filme != null)
            {
                _filmes.Remove(filme);
                return true;
            }

            return false;
        }

        public bool ExisteFilmeComTitulo(string titulo)
        {
            foreach (Filme f in _filmes)
            {
                if (!string.IsNullOrEmpty(f.Titulo) &&
                    f.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
