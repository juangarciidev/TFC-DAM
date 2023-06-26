using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MaricastanaClothingStore.MODELO
{
    public class Talla
    {
        
        public int TallaId { get; set; }
        [Required]
        public string NombreTalla { get; set; }
        [Required]
        public int Cantidad { get; set; }
        public int ArticuloId { get; set; }
        public virtual Articulo Articulo { get; set; }

        public virtual ICollection<LineaVenta> LineasVentas { get; set; }
       
        public Talla()
        {
            Cantidad = 1;
            LineasVentas = new HashSet<LineaVenta>();
        }
    }
}
