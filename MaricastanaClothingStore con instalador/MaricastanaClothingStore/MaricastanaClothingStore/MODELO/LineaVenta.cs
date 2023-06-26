using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MaricastanaClothingStore.MODELO
{
    public class LineaVenta
    {
        public int LineaVentaId { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public decimal PrecioUnidad { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public string PrecioTotal { get { return PrecioTot.ToString("C2"); } }
        [NotMapped]
        public decimal PrecioTot { get { return Cantidad * PrecioUnidad; } set { value = Cantidad * PrecioUnidad; } }
        public int VentaId { get; set; }
        public int TallaId { get; set; }
        public virtual Venta Venta { get; set; }
        public virtual Talla Talla { get; set; }

    }

}
