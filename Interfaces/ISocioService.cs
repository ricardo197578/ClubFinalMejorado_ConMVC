using System;
using System.Collections.Generic;
using ClubMinimal.Models;

namespace ClubMinimal.Interfaces
{
    public interface ISocioService
    {
         void RegistrarSocio(Socio socio);  // Cambiar la firma
        //void RegistrarSocio(string nombre, string apellido);
        List<Socio> ObtenerSocios();
	Socio ObtenerPorId(int id);  
    }
}