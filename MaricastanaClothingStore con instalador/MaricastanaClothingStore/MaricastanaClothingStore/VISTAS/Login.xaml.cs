using MaricastanaClothingStore.DAL;
using MaricastanaClothingStore.MODELO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MaricastanaClothingStore.VISTAS
{
    public partial class Login : Window
    {
        UnitOfWork bd = new UnitOfWork();
        Usuario usuario = new Usuario();

        public Login()
        {
            InitializeComponent();
            tbUsuario.Focus();
            tbUsuario.Text = usuario.LoginUsuario;
            pwdContra.Password = usuario.LoginContraseña;
        }
        #region BOTONES
        #region ACCEDER
        private void btAcceder_Click(object sender, RoutedEventArgs e)
        {
            if (tbUsuario.Text != "" & pwdContra.Password != "")
            {

                if (bd.UsuariosRepository.Get(c => c.LoginUsuario == tbUsuario.Text).Count > 0)
                {
                    usuario = bd.UsuariosRepository.GridDatosUsuarios().Where(c => c.LoginUsuario.ToLower() == tbUsuario.Text.ToLower()).First();
                    if (pwdContra.Password.Equals(usuario.LoginContraseña))
                    {
                           
                            MainWindow mainwindow = new MainWindow(usuario);
                            mainwindow.Show();
                            Close();
                    }
                    else
                    {
                        MessageBox.Show("La contraseña es incorrecta!", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontró ningún usuario!", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                MessageBox.Show("Completa los dos campos!", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                btAcceder_Click(sender, e);
            }
        }
        #endregion
        #region CERRAR APLICACIÓN
        private void BtnCerrarLogin_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        #endregion
        #region PASSWORDBOX
        private void MgEye_MouseEnter(object sender, MouseEventArgs e)
        {
            tbContra.Text = pwdContra.Password;
            pwdContra.Visibility = Visibility.Hidden;
            tbContra.Visibility = Visibility.Visible;

        }

        private void MgEye_MouseLeave(object sender, MouseEventArgs e)
        {
            tbContra.Visibility = Visibility.Hidden;
            pwdContra.Visibility = Visibility.Visible;
        }
        #endregion
        #endregion
    }
}
