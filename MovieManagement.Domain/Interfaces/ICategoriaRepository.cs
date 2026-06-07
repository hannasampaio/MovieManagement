using MovieManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.Domain.Interfaces
{
    public interface ICategoriaRepository
    {
        void Adicionar(Categoria categoria);
        List<Categoria> ObterTodos();
        Categoria? ObterPorNome(string nome);
        bool Remover(int id);
        bool ExisteCategoriaComNome(string nome);
    }
}
