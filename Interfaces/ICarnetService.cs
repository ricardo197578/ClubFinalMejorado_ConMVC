using System;

using ClubMinimal.Models;

namespace ClubMinimal.Interfaces
{
    public interface ICarnetService
    {
        Carnet GenerarNuevoCarnet(int socioId);
        Carnet ObtenerCarnetVigente(int socioId);
        bool VerificarCarnetValido(int socioId);
    }
}