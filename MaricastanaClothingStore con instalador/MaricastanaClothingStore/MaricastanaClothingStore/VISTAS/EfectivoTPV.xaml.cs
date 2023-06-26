using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MaricastanaClothingStore.VISTAS
{
    /// <summary>
    /// Lógica de interacción para EfectivoTPV.xaml
    /// </summary>
    public partial class EfectivoTPV : Window
    {
        public decimal Efectivo { get; private set; }

        public EfectivoTPV()
        {
            InitializeComponent();
            tbEfectivo.Focus();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (!decimal.TryParse(tbEfectivo.Text, out decimal efectivo) || efectivo <= 0)
            {
                MessageBox.Show("No se ha introducido una cantidad correcta!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Efectivo = efectivo;
            DialogResult = true;
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
