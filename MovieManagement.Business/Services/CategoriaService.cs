using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.Business.Services
{
    public class CategoriaService
    {
        private ICategoriaRepository _repositorio;

        public CategoriaService(ICategoriaRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public void AdicionarCategoria(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome da categoria é obrigatório.");

            if (_repositorio.ExisteCategoriaComNome(nome))
                throw new Exception("Já existe uma categoria com esse nome.");

            Categoria categoria = new Categoria
            {
                Nome = nome
            };

            _repositorio.Adicionar(categoria);
        }

        public List<Categoria> ListarTodos()
        {
            return _repositorio.ObterTodos();
        }

        public Categoria? ProcurarCategoria(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome da categoria é obrigatório.");

            return _repositorio.ObterPorNome(nome);
        }

        public void RemoverCategoria(int id)
        {
            bool removido = _repositorio.Remover(id);

            if (!removido) throw new Exception("Categoria não encontrada.");
        }
    }
}
