using MaricastanaClothingStore.DAL;
using MaricastanaClothingStore.MODELO;
using MaricastanaClothingStore.UTILIDADES;
using Microsoft.Win32;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Usuarios.Modelo;

namespace MaricastanaClothingStore.VISTAS
{
    public partial class CRUDArticulos : Page
    {
      UnitOfWork bd ;
        Articulo articulo = new Articulo();
        Boolean nuevo = true;
        Talla talla = new Talla();

        public CRUDArticulos(Articulo articulo, UnitOfWork bd)
        {
            InitializeComponent();
            this.bd = bd;
            this.articulo = articulo;
            gCrearArticulo.DataContext = articulo;
            gbTallas.DataContext = talla;
            dgTallas.ItemsSource = articulo.Tallas;
            //Conversor para llevar la imagen a la base de datos
            if (articulo.Imagen != null)
            {
                imagen.Source = ConversorImagen.ToImage(articulo.Imagen);
            }
            //Carga el ComboBox de Categorías
            cbCategoria.ItemsSource = bd.CategoriasRepository.GetAll();
            cbCategoria.DisplayMemberPath = "NombreCategoria";
            cbCategoria.SelectedValuePath = "CategoriaId";
        }
        #region BOTONES       
        #region VOLVER       
        private void BtnVolver_Click(object sender, RoutedEventArgs e)
        {
            Content = new Articulos();
        }
        #endregion
        #region AÑADIR TALLA
        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            String errores = Validacion.errores(talla);
            if (errores.Equals(""))
            {
                if (nuevo)
                {
                    var tallaExistente = articulo.Tallas.SingleOrDefault(x => x.NombreTalla.Equals(((ComboBoxItem)cbTalla.SelectedItem).Content.ToString()));
                    if (tallaExistente != null)
                    {
                        tallaExistente.Cantidad += talla.Cantidad;
                    }
                    else
                    {
                        articulo.Tallas.Add(talla);
                    }

                    dgTallas.Items.Refresh();
                    LimpiarTalla();
                }
            }
            else
            {
                MessageBox.Show(errores, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        #endregion
        #region ELIMINAR TALLA
        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dgTallas.SelectedItem != null)
            {
                Talla tallaSeleccionada = dgTallas.SelectedItem as Talla;

                // Verificar si la talla existe
                Talla tallaExistente = bd.TallasRepository.Single(t => t.ArticuloId == tallaSeleccionada.ArticuloId && t.NombreTalla == tallaSeleccionada.NombreTalla);
                if (tallaExistente != null)
                {
                    // Eliminar la talla 
                    bd.TallasRepository.Delete(tallaExistente);
                    bd.Save();

                    // Actualizar la vista de tallas
                    articulo.Tallas.Remove(tallaSeleccionada);
                    dgTallas.Items.Refresh();
                }
            }
        }



        #endregion
        #region CREATE
        private void BtnCrear_Click(object sender, RoutedEventArgs e)
        {
            if (dgTallas.ItemsSource == null || dgTallas.Items.Count == 0)

            {
                MessageBox.Show("Debes añadir mínimo una talla a un artículo!", "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                String errores = Validacion.errores(articulo);
                if (errores.Equals(""))
                {
                    if (bd.ArticulosRepository.Get(c => c.Codigo.Equals(tbCodigo.Text)).Count == 0)
                    {
                        if (nuevo)
                        {
                            bd.ArticulosRepository.Añadir(articulo);
                            bd.Save();
                        }
                        LimpiarArticulo();
                        dgTallas.ItemsSource = null;
                        Content = new Articulos();
                    }
                    else
                    {
                        MessageBox.Show("Ya existe un artículo con ese código!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show(errores, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        #endregion
        #region UPDATE
        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            String errores = Validacion.errores(articulo);
            if (errores.Equals(""))
            {
                if (bd.ArticulosRepository.Get(c => c.Codigo.Equals(tbCodigo.Text) && c.ArticuloId != articulo.ArticuloId).Count == 0)
                {
                    if (nuevo)
                    {
                        bd.ArticulosRepository.Update(articulo);
                        bd.Save();
                        Content = new Articulos();
                    }
                    LimpiarArticulo();
                }
                else
                {
                    MessageBox.Show("Ya existe un artículo con ese código!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                articulo.Imagen = ConversorImagen.bitmapImageToBytes(new BitmapImage(new Uri(selectedFileName)));
            }
        }
        #endregion
        #endregion
        #region LIMPIADORES
        private void LimpiarArticulo()
        {
            articulo = new Articulo();
            gCrearArticulo.DataContext = articulo;
            nuevo = true;
        }
        private void LimpiarTalla()
        {
            talla = new Talla();
            gbTallas.DataContext = talla;
            nuevo = true;
        }
        #endregion
        #region COMBOBOXES
        #region CATEGORÍAS
        private void cbCategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbCategoria.SelectedIndex != -1)
            {
                articulo.Categoria = cbCategoria.SelectedItem as Categoria;
            }
        }
        #endregion
        #region TALLAS
        private void cbTalla_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbTalla.SelectedIndex != -1)
            {
                talla.NombreTalla = ((ComboBoxItem)cbTalla.SelectedItem).Content.ToString();
            }
        }
        #endregion

        #endregion


    }
}
