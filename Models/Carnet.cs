using System;

namespace ClubMinimal.Models
{
    public class Carnet
    {
        public int Id { get; set; }
        public int SocioId { get; set; }
        public string Codigo { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public bool Activo { get; set; }
    }
}