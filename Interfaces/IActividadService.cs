using System.Collections.Generic;
using ClubMinimal.Models;

namespace ClubMinimal.Interfaces
{
    public interface IActividadService
    {
        List<Actividad> ObtenerActividadesDisponibles();
        List<Actividad> ObtenerActividadesParaNoSocios();
        Actividad ObtenerActividad(int id);
    }
}