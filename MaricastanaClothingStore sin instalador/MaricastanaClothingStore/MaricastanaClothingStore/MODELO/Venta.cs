using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MaricastanaClothingStore.MODELO
{
    public class Venta
    {
        public int VentaId { get; set; }
        [Required]
        public string NumFactura { get; set; }
        [Required]
        public DateTime FechaVenta { get; set; }
        [Required]
        public decimal PrecioTotal { get; set; }
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<LineaVenta> LineaVentas { get; set; }

        public Venta()
        {
            LineaVentas = new HashSet<LineaVenta>();
        }

    }
}
