using System.Collections.Generic;
using ClubMinimal.Models;

namespace ClubMinimal.Interfaces
{
    public interface INoSocioService
    {
        void RegistrarNoSocio(string nombre, string apellido);
        List<NoSocio> ObtenerNoSocios();
    }
}