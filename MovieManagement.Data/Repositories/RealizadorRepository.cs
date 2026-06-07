using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieManagement.Data.Repositories
{
    public class RealizadorRepository : IRealizadorRepository
    {
        private List<Realizador> _realizadores = new List<Realizador>();
        private int _proximoId = 1;

        public void Adicionar(Realizador realizador)
        {
            realizador.Id = _proximoId++;
            _realizadores.Add(realizador);
        }

        public List<Realizador> ObterTodos()
        {
            return _realizadores;
        }

        public Realizador? ObterPorNome(string nome)
        {
            foreach (var r in _realizadores)
            {
                if (r.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase))
                    return r;
            }

            return null;
        }

        public bool Remover(int id)
        {
            var realizador = _realizadores.FirstOrDefault(r => r.Id == id);

            if (realizador == null)
                return false;

            _realizadores.Remove(realizador);
            return true;
        }

        public bool ExisteRealizadorComNome(string nome)
        {
            return _realizadores.Any(r => r.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
        }
    }
}