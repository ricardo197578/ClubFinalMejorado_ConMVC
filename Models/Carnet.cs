using System;

namespace ClubMinimal.Models
{
    public class Carnet
    {
        public int Id { get; set; }
        public int NroCarnet { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public Socio Socio { get; set; }

       
    }
}
