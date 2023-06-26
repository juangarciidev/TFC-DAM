using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using MaricastanaClothingStore.DAL;
using MaricastanaClothingStore.MODELO;
using MaricastanaClothingStore.UTILIDADES;
using Microsoft.Win32;
using Usuarios.Modelo;

namespace MaricastanaClothingStore.VISTAS
{
    public partial class CRUDUsuarios : Page
    {
        UnitOfWork bd = new UnitOfWork();
        Usuario usuario = new Usuario();
        Boolean nuevo = true;
        public CRUDUsuarios(Usuario usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
            gCrearUsuario.DataContext = usuario;
            //Conversor para llevar la imagen a la base de datos
            if (usuario.FotoPerfil != null)
            {
                imagen.Source = ConversorImagen.ToImage(usuario.FotoPerfil);
            }
            //Carga el ComboBox de Privilegios
            cbPrivilegio.ItemsSource = bd.PrivilegiosRepository.GetAll();
            cbPrivilegio.DisplayMemberPath = "NombrePrivilegio";
            cbPrivilegio.SelectedValuePath = "PrivilegioId";
        }
        #region BOTONES
        #region VOLVER
        private void BtnVolver_Click(object sender, RoutedEventArgs e)
        {
            Content = new Usuarios();
        }
        #endregion
        #region CREATE
        private void BtnCrear_Click(object sender, RoutedEventArgs e)
        {
            String errores = Validacion.errores(usuario);
            if (errores.Equals(""))
            {
                if (bd.UsuariosRepository.Get(c => c.DNI.Equals(tbDNI.Text)).Count == 0)
                {
                    if (bd.UsuariosRepository.Get(c => c.Correo.Equals(tbCorreo.Text)).Count == 0)
                    {
                        if (bd.UsuariosRepository.Get(c => c.LoginUsuario.Equals(tbUsuario.Text)).Count == 0)
                        {
                            if (nuevo)
                            {
                                bd.UsuariosRepository.Añadir(usuario);
                                bd.Save();
                                Content = new Usuarios();
                            }
                            Limpiar();
                        }
                        else
                        {
                            MessageBox.Show("Ya existe ese nombre de usuario!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ya existe un usuario con ese correo!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                }
                else
                {
                    MessageBox.Show("Ya existe un usuario con ese DNI!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);

                }

            }
            else
            {
                MessageBox.Show(errores, "Error",
            MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
        #region UPDATE
        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            String errores = Validacion.errores(usuario);
            if (errores.Equals(""))
            {
                // Verificar si existe otro usuario con el mismo DNI
                var usuarioExistenteDNI = bd.UsuariosRepository.Get(c => c.DNI.Equals(tbDNI.Text) && c.UsuarioId != usuario.UsuarioId).FirstOrDefault();
                if (usuarioExistenteDNI == null)
                {
                    // Verificar si existe otro usuario con el mismo correo
                    var usuarioExistenteCorreo = bd.UsuariosRepository.Get(c => c.Correo.Equals(tbCorreo.Text) && c.UsuarioId != usuario.UsuarioId).FirstOrDefault();
                    if (usuarioExistenteCorreo == null)
                    {
                        // Verificar si existe otro usuario con el mismo nombre de usuario
                        var usuarioExistenteLogin = bd.UsuariosRepository.Get(c => c.LoginUsuario.Equals(tbUsuario.Text) && c.UsuarioId != usuario.UsuarioId).FirstOrDefault();
                        if (usuarioExistenteLogin == null)
                        {
                            if (nuevo)
                            {
                                bd.UsuariosRepository.Update(usuario);
                                bd.Save();
                                Content = new Usuarios();
                            }
                            Limpiar();
                        }
                        else
                        {
                            MessageBox.Show("Ya existe ese nombre de usuario!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ya existe un usuario con ese correo!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Ya existe un usuario con ese DNI!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show(errores, "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion
        #region SUBIR IMAGEN
        string selectedFileName = "";
        private void BtnImagen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog cargarImagen = new OpenFileDialog();
            cargarImagen.InitialDirectory = "c:";
            cargarImagen.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            cargarImagen.RestoreDirectory = true;
            bool? result = cargarImagen.ShowDialog();
            if (result == true)
            {
                selectedFileName = cargarImagen.FileName;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFileName);
                bitmap.EndInit();
                imagen.Source = bitmap;
                usuario.FotoPerfil = ConversorImagen.bitmapImageToBytes(new BitmapImage(new Uri(selectedFileName)));
            }
        }
        #endregion
        #endregion
        #region RELLENAR COMBOB0X
        private void cbPrivilegio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbPrivilegio.SelectedIndex != -1)
            {
                usuario = cbPrivilegio.SelectedItem as Usuario;
            }
        }
        #endregion
        #region LIMPIAR CAMPOS
        private void Limpiar()
        {
            usuario = new Usuario();
            gCrearUsuario.DataContext = usuario;
            nuevo = true;
        }
        #endregion
    }
}
