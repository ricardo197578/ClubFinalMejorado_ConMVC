using System.Collections.Generic;
using ClubMinimal.Interfaces;
using ClubMinimal.Models;
using ClubMinimal.Repositories;

namespace ClubMinimal.Services
{
    public class SocioService : ISocioService
    {
        private readonly ISocioRepository _repository;

        public SocioService(ISocioRepository repository)
        {
            _repository = repository;
        }

        public void RegistrarSocio(string nombre, string apellido)
        {
            var socio = new Socio { Nombre = nombre, Apellido = apellido };
            _repository.Agregar(socio);
        }

        public List<Socio> ObtenerSocios()
        {
            return _repository.ObtenerTodos();
        }
    }
}