using System.Collections.Generic;
using ClubMinimal.Interfaces;
using ClubMinimal.Models;
using ClubMinimal.Repositories;

namespace ClubMinimal.Services
{
    public class SocioService : ISocioService
    {
        private readonly ISocioRepository _repository;

        // Constructor: Inyecci�n de dependencia

        public SocioService(ISocioRepository repository)
        {
            _repository = repository;
        }
        /*
         Qu� hace:

            El servicio recibe una implementaci�n de ISocioRepository (ej: SocioRepositorySQL) desde el constructor.

            Esto se llama Inversi�n de Control (IoC). El servicio no crea el repositorio, lo recibe ya construido.

            private readonly: Asegura que _repository no cambie despu�s de asignarse.

            Por qu� es importante:

            Permite cambiar f�cilmente la implementaci�n del repositorio (ej: de SQL a MongoDB) sin modificar el servicio.

            Facilita las pruebas unitarias (puedes pasar un repositorio "falso").
         */


        // M�todo para registrar un socio

		// En SocioService.cs
public void RegistrarSocio(Socio socio)
{
    _repository.Agregar(socio);
}
       
        /*
         * Qu� hace:

            Crea un nuevo objeto Socio con los datos proporcionados (nombre y apellido).

            Llama al m�todo Agregar del repositorio para guardar el socio en la base de datos (o donde sea que est� implementado).
        */
        
        
        // M�todo para obtener todos los socios
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