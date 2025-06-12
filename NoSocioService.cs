using System.Collections.Generic;
using ClubMinimal.Interfaces;
using ClubMinimal.Models;
using ClubMinimal.Repositories;

namespace ClubMinimal.Services

{
    public class NoSocioService : INoSocioService
    {
        private readonly INoSocioRepository _repository;

        public NoSocioService(INoSocioRepository repository)
        {
            _repository = repository;
        }

        public void RegistrarNoSocio(string nombre, string apellido)
        {
            var noSocio = new NoSocio { Nombre = nombre, Apellido = apellido };
            _repository.Agregar(noSocio);
        }

        public List<NoSocio> ObtenerNoSocios()
        {
            return _repository.ObtenerTodos();
        }
    }
}