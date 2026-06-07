using System;
using System.Collections.Generic;
using System.Linq;
using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;

namespace MovieManagement.Data.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private List<Categoria> _categorias = new List<Categoria>();
        private int _proximoId = 1;

        public void Adicionar(Categoria categoria)
        {
            categoria.Id = _proximoId++;
            _categorias.Add(categoria);
        }

        public List<Categoria> ObterTodos()
        {
            return _categorias;
        }

        public Categoria? ObterPorNome(string nome)
        {
            foreach (var c in _categorias)
            {
                if (c.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase))
                    return c;
            }

            return null;
        }

        public bool Remover(int id)
        {
            var categoria = _categorias.FirstOrDefault(c => c.Id == id);

            if (categoria == null)
                return false;

            _categorias.Remove(categoria);
            return true;
        }

        public bool ExisteCategoriaComNome(string nome)
        {
            return _categorias.Any(c => c.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
        }
    }
}