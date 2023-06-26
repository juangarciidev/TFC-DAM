using MaricastanaClothingStore.MODELO;
using MaricastanaClothingStore.VISTAS;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;


namespace MaricastanaClothingStore
{
    public partial class MainWindow : Window
    {
        //public static int privilegioId = 0;
        public  Usuario usuario;
        public static Articulo articulo;
        public MainWindow(Usuario usuario)
        {
            InitializeComponent();
            this.usuario= usuario;  
            DataContext = new Inicio();
            if (usuario.PrivilegioId == 2)
            {
                DataContext = new TPV(usuario);
                lvMenu.Items.RemoveAt(3);
                lvMenu.Items.RemoveAt(0);
            }
        }
        #region BARRA LATERAL
        private void TBMostrar(object sender, RoutedEventArgs e)
        {
            GridContenido.Opacity = 0.5;
        }

        private void TBOcultar(object sender, RoutedEventArgs e)
        {
            GridContenido.Opacity = 1;
        }

        private void PreviewMouseLeftButtonDownBG(object sender, MouseButtonEventArgs e)
        {
            BtnMostrarOcultar.IsChecked = false;
        }
        #endregion
        #region MENÚ
        private void BtnInicio_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new VISTAS.Inicio();
        }

        private void BtnPuntoDeVentas_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new VISTAS.TPV(usuario);
        }

        private void BtnArticulos_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new VISTAS.Articulos();
        }

        private void BtnUsuarios_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new VISTAS.Usuarios();
        }
        #endregion
        #region ENCABEZADO
        private void BtnCuenta_Click(object sender, RoutedEventArgs e)
        {
            MiCuenta miCuenta = new MiCuenta(usuario);
            miCuenta.ShowDialog();
        }

        private void BtnAyuda_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Deseas visualizar el Manual de Usuario de esta aplicación?", "Confirmación",
            MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Process process = Process.Start(new ProcessStartInfo(@"Manual de Usuario Maricastaña.pdf") { UseShellExecute = true });
                    break;
                case MessageBoxResult.No:

                    break;
            }

        }

        #region AJUSTES VENTANA
        private void BtnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void BtnMaximizar_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal) this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        #endregion

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Deseas salir a la ventana de Login?", "Confirmación",
            MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Login login = new Login();
                    login.Show();
                    this.Close();
                    break;
                case MessageBoxResult.No:

                    break;
            }
        }
        #endregion


    }

}
