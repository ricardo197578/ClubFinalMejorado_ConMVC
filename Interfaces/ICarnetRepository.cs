using ClubMinimal.Models;

namespace ClubMinimal.Interfaces
{
    public interface ICarnetRepository
    {
        void GenerarCarnet(Carnet carnet);
        Carnet ObtenerCarnetPorSocioId(int socioId);
        void DesactivarCarnet(int carnetId);
    }
}