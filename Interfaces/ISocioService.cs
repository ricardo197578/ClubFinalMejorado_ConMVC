using System.Collections.Generic;
using ClubMinimal.Models;

namespace ClubMinimal.Interfaces
{
    public interface ISocioService
    {
        void RegistrarSocio(string nombre, string apellido);
        List<Socio> ObtenerSocios();
    }
}