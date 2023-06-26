using MaricastanaClothingStore.DAL;
using MaricastanaClothingStore.MODELO;
using System.Collections.Generic;

namespace ExamenVentas.DAL
{
    public class UsuariosRepository : GenericRepository<Usuario>
    {
        public UsuariosRepository(MaricastanaContext context) : base(context)
        {

        }

        public List<Usuario> GridDatosUsuarios()
        {
            return Get(c => c.UsuarioId > 0, includeProperties: "Privilegio");
        }
    }
}
