using System;

namespace ClubMinimal.Models
{
    public class Socio : Persona
    {
        public int Id { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool AptoFisicoAprobado { get; set; }
        public DateTime? FechaVencimientoApto { get; set; }
    }
}