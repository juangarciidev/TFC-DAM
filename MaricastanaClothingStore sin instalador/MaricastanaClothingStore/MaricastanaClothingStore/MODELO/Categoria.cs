using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MaricastanaClothingStore.MODELO
{
    public class Categoria
    {
        public int CategoriaId { get; set; }
        [Required]
        [StringLength(maximumLength: 20, ErrorMessage = "Longitud máxima admitida 20 caracteres")]
        public string NombreCategoria { get; set; }
        public virtual ICollection<Articulo> Articulos { get; set; }
        
        public Categoria()
        {
            Articulos = new HashSet<Articulo>();
        }
    }
}
