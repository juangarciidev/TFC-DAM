using MaricastanaClothingStore.DAL;
using MaricastanaClothingStore.MODELO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenVentas.DAL
{
    public class PrivilegiosRepository : GenericRepository<Privilegio>
    {
        public PrivilegiosRepository(MaricastanaContext context) : base(context)
        {

        }
    }
}
