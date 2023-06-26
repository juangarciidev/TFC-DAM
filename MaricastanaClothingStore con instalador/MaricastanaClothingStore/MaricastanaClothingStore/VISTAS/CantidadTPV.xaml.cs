using System.Windows;

namespace MaricastanaClothingStore.VISTAS
{

    public partial class CantidadTPV : Window
    {
        public CantidadTPV()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            // Verificar si se ha ingresado una cantidad válida
            if (string.IsNullOrWhiteSpace(tbCantidad.Text))
            {
                MessageBox.Show("Por favor ingresa una cantidad válida!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Verificar si la cantidad es un número válido
            if (!int.TryParse(tbCantidad.Text, out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("No se ha introducido una cantidad correcta!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            // Cerrar el cuadro de diálogo con DialogResult true
            DialogResult = true;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            // Cerrar el cuadro de diálogo con DialogResult false
            DialogResult = false;
        }
    }
}
