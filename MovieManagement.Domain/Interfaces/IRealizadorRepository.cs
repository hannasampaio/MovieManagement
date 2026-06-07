using MovieManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
 
namespace MovieManagement.Domain.Interfaces
{
    public interface IRealizadorRepository
    {
        void Adicionar(Realizador realizador);
        List<Realizador> ObterTodos();
        Realizador? ObterPorNome(string nome);
        bool Remover(int id);
        bool ExisteRealizadorComNome(string nome); // adicionado para evitar duplicados
    }
}
