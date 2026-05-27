using MovieManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.Domain.Interfaces
{
    public interface IFilmeRepository
    {
        public void Adicionar(Filme filme);

        public List<Filme> ObterTodos();

        public Filme? ObterPorTitulo(string titulo);

        public bool Remover(int id);

        bool ExisteFilmeComTitulo(string titulo);
    }
}
