using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MaricastanaClothingStore.MODELO
{
    public class Privilegio
    {
        
        public int PrivilegioId { get; set; }
        [Required]
        [StringLength(maximumLength: 20, ErrorMessage = "Longitud máxima admitida 20 caracteres")]
        public string NombrePrivilegio { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }

        public Privilegio()
        {
            Usuarios = new HashSet<Usuario>();
        }
    }
}
