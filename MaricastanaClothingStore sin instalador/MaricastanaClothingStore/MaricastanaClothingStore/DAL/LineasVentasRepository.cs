using MaricastanaClothingStore.DAL;
using MaricastanaClothingStore.MODELO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenVentas.DAL
{
    public class LineasVentasRepository : GenericRepository<LineaVenta>
    {
        public LineasVentasRepository(MaricastanaContext context) : base(context)
        {

        }
    }
}
