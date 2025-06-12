using System.Collections.Generic;
using ClubMinimal.Models;

namespace ClubMinimal.Interfaces
{
    public interface INoSocioRepository
    {
        void Agregar(NoSocio noSocio);
        List<NoSocio> ObtenerTodos();
    }
}