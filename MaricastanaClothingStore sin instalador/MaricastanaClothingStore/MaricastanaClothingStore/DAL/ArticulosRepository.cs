using MaricastanaClothingStore.DAL;
using MaricastanaClothingStore.MODELO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenVentas.DAL
{
    public class ArticulosRepository : GenericRepository<Articulo>
    {
        public ArticulosRepository(MaricastanaContext context) : base(context)
        {

        }

        public List<Articulo> GridDatosArticulos()
        {
            return Get(c => c.ArticuloId > 0, includeProperties: "Categoria");
        }

        public List<Talla> ObtenerTallas(int articuloid)
        {
            return Get(c => c.ArticuloId == articuloid, includeProperties: "Tallas")
                .FirstOrDefault()?.Tallas.ToList();
        }
    }
}
