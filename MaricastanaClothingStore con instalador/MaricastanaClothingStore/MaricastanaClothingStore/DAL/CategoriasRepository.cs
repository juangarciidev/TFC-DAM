using MaricastanaClothingStore.DAL;
using MaricastanaClothingStore.MODELO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenVentas.DAL
{
    public class CategoriasRepository : GenericRepository<Categoria>
    {
        public CategoriasRepository(MaricastanaContext context) : base(context)
        {

        }
    }
}
