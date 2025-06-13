using System;
using ClubMinimal.Interfaces;
using ClubMinimal.Models;

namespace ClubMinimal.Services
{
    public class CarnetService : ICarnetService
    {
        private readonly ICarnetRepository _carnetRepository;
        private readonly ISocioRepository _socioRepository;

        public CarnetService(ICarnetRepository carnetRepository, ISocioRepository socioRepository)
        {
            _carnetRepository = carnetRepository;
            _socioRepository = socioRepository;
        }

        public Carnet GenerarNuevoCarnet(int socioId)
        {
            var socio = _socioRepository.ObtenerPorId(socioId);
            if (socio == null)
                throw new Exception("Socio no encontrado");

            if (!socio.AptoFisicoAprobado)
                throw new Exception("El socio no tiene apto físico aprobado");

            // Desactivar carnets anteriores
            var carnetExistente = _carnetRepository.ObtenerCarnetPorSocioId(socioId);
            if (carnetExistente != null)
            {
                _carnetRepository.DesactivarCarnet(carnetExistente.Id);
            }

            // Generar nuevo carnet
            var nuevoCarnet = new Carnet
            {
                SocioId = socioId,
                Codigo = GenerarCodigoCarnet(socioId),
                FechaEmision = DateTime.Now,
                FechaVencimiento = DateTime.Now.AddYears(1),
                Activo = true
            };

            _carnetRepository.GenerarCarnet(nuevoCarnet);
            return nuevoCarnet;
        }

        public Carnet ObtenerCarnetVigente(int socioId)
        {
            return _carnetRepository.ObtenerCarnetPorSocioId(socioId);
        }

        public bool VerificarCarnetValido(int socioId)
        {
            var carnet = _carnetRepository.ObtenerCarnetPorSocioId(socioId);
            return carnet != null && carnet.Activo && carnet.FechaVencimiento >= DateTime.Now;
        }

        private string GenerarCodigoCarnet(int socioId)
        {
            // Generar un código único para el carnet
            return "CLB-" + socioId + "-" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }

    }
}
