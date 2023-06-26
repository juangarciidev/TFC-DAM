using MaricastanaClothingStore.MODELO;
using System.Windows;

namespace MaricastanaClothingStore.VISTAS
{
    public partial class MiCuenta : Window
    {
        public MiCuenta(Usuario usuario)
        {
            InitializeComponent();
            tbNombre.Text = usuario.Nombre;
            tbApellidos.Text = usuario.Apellidos;
            tbCorreo.Text = usuario.Correo;
            tbPrivilegio.Text = usuario.Privilegio.NombrePrivilegio;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
