using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Windows;

namespace MaricastanaClothingStore.MODELO
{
    public class Articulo
    {
        public int ArticuloId { get; set; }
        [Required]
        [StringLength(maximumLength: 40, ErrorMessage = "Longitud máxima admitida 40 caracteres")]
        public string Nombre { get; set; }
        [Required]
        [StringLength(maximumLength: 20, ErrorMessage = "Longitud máxima admitida 20 caracteres")]
        public string Marca { get; set; }
        [Required]
        public string Codigo { get; set; }
        [Required]
        [StringLength(maximumLength: 20, ErrorMessage = "Longitud máxima admitida 20 caracteres")]
        public string Color { get; set; }
        [Required]
        public decimal PrecioUnidad { get; set; }
        public string PrecioUnidadConSimbolo
        {
            get { return $"{PrecioUnidad} €"; }
            set
            {
                // Eliminar el símbolo del euro de la cadena y convertir el precio a decimal
                if (decimal.TryParse(value.Replace(" €", ""), out decimal precio))
                {
                    PrecioUnidad = precio;
                }
                else
                {
                    MessageBox.Show("El formato del precio no es válido!", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        public byte[]? Imagen { get; set; }
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual ICollection<Talla> Tallas { get; set; }

        public Articulo()
        {
            PrecioUnidad = 0.00m;
            Tallas = new HashSet<Talla>();
        }
    }
}
