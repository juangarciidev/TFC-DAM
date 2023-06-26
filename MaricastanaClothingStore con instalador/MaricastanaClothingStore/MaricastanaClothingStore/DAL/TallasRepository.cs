using MaricastanaClothingStore.DAL;
using MaricastanaClothingStore.MODELO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenVentas.DAL
{
    public class TallasRepository : GenericRepository<Talla>
    {
        public TallasRepository(MaricastanaContext context) : base(context)
        {

        }

        public Talla ComprobarTalla(string codigoArticulo, string nombreTalla)
        {
            return Get(t => t.Articulo.Codigo == codigoArticulo && t.NombreTalla == nombreTalla).FirstOrDefault();
        }

        public void Actualizar(Talla talla)
        {
            Update(talla);
        }
    }
}
