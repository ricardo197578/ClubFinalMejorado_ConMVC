using System.Collections.Generic;
using ClubMinimal.Interfaces;
using ClubMinimal.Models;
using ClubMinimal.Repositories;

namespace ClubMinimal.Services
{
    public class SocioService : ISocioService
    {
        private readonly ISocioRepository _repository;

        // Constructor: Inyección de dependencia

        public SocioService(ISocioRepository repository)
        {
            _repository = repository;
        }
        /*
         Qué hace:

            El servicio recibe una implementación de ISocioRepository (ej: SocioRepositorySQL) desde el constructor.

            Esto se llama Inversión de Control (IoC). El servicio no crea el repositorio, lo recibe ya construido.

            private readonly: Asegura que _repository no cambie después de asignarse.

            Por qué es importante:

            Permite cambiar fácilmente la implementación del repositorio (ej: de SQL a MongoDB) sin modificar el servicio.

            Facilita las pruebas unitarias (puedes pasar un repositorio "falso").
         */


        // Método para registrar un socio

		// En SocioService.cs
public void RegistrarSocio(Socio socio)
{
    _repository.Agregar(socio);
}
       
        /*
         * Qué hace:

            Crea un nuevo objeto Socio con los datos proporcionados (nombre y apellido).

            Llama al método Agregar del repositorio para guardar el socio en la base de datos (o donde sea que esté implementado).
        */
        
        
        // Método para obtener todos los socios
        public List<Socio> ObtenerSocios()
        {
            return _repository.ObtenerTodos();
        }

	public Socio ObtenerPorId(int id)
{
    return _repository.ObtenerPorId(id);
}
    }
}