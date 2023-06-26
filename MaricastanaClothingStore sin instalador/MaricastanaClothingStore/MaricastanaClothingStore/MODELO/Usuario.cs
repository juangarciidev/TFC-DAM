using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MaricastanaClothingStore.MODELO
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        [Required]
        [StringLength(maximumLength: 40, ErrorMessage = "Longitud máxima admitida 40 caracteres")]
        public string Nombre { get; set; }
        [Required]
        [StringLength(maximumLength: 60, ErrorMessage = "Longitud máxima admitida 60 caracteres")]
        public string Apellidos { get; set; }
        [Required]
        [RegularExpression(@"^[A-z]?\d{8}[A-z]$", ErrorMessage = "DNI con formato erróneo")]
        public string DNI { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Formato de correo no válido")]
        public string Correo { get; set; }
        [Required]
        [Phone(ErrorMessage = "Formato de teléfono no válido")]
        public string Telefono { get; set; }
        [Required]
        public DateTime FechaNacimiento { get; set; }
        [Required]
        [StringLength(maximumLength: 100, ErrorMessage = "Longitud máxima admitida 100 caracteres")]
        public string Direccion { get; set; }
        [Required]
        public byte[] FotoPerfil { get; set; }
        [Required]
        public string LoginUsuario { get; set; }
        [Required]
        public string LoginContraseña { get; set; }
        public int PrivilegioId { get; set; }
        public virtual Privilegio Privilegio { get; set; }
        public string FechaNac
        {
            get
            {
                return FechaNacimiento.ToString("dd/MM/yyyy");

            }
        }
        public virtual ICollection<Venta> Ventas { get; set; }

        public Usuario()
        {
            FechaNacimiento = DateTime.Now;
            Ventas = new HashSet<Venta>();
        }
    }
}
