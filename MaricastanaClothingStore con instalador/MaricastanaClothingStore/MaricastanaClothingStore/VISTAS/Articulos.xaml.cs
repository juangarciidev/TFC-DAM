using iTextSharp.text.pdf;
using iTextSharp.text;
using MaricastanaClothingStore.DAL;
using MaricastanaClothingStore.MODELO;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Image = iTextSharp.text.Image;
using System.Diagnostics;
using System.Linq;

namespace MaricastanaClothingStore.VISTAS
{
    public partial class Articulos : UserControl
    {
       public UnitOfWork bd = new UnitOfWork();
        Articulo articulo = new Articulo();

        public Articulos()
        {
            InitializeComponent();
            dgArticulos.ItemsSource = bd.ArticulosRepository.GridDatosArticulos();
            dgTallas.Visibility = Visibility.Hidden;
        }
        #region CRUD
        #region CREATE
        private void BtnAgregarArticulo_Click(object sender, RoutedEventArgs e)
        {
            dgArticulos.SelectedIndex = -1;
            CRUDArticulos ventana = new CRUDArticulos(new Articulo(),bd);
            FrameArticulos.Content = ventana;
            ventana.BtnCrear.Visibility = Visibility.Visible;
            ventana.Titulo.Text = "Registro de Artículo";
        }
        #endregion
        #region READ
        private void BtnConsultar_Click(object sender, RoutedEventArgs e)
        {
            CRUDArticulos ventana = new CRUDArticulos(articulo, bd);
            FrameArticulos.Content = ventana;
            ventana.BtnImagen.Visibility = Visibility.Hidden;
            ventana.BtnCrear.Visibility = Visibility.Hidden;
            ventana.gbTallas.Visibility = Visibility.Hidden;
            ventana.dgTallas.Visibility = Visibility.Hidden;
            ventana.Titulo.Text = "Consulta de Artículo";
            ventana.tbNombre.IsEnabled = false;
            ventana.tbCodigo.IsEnabled = false;
            ventana.tbMarca.IsEnabled = false;
            ventana.tbColor.IsEnabled = false;
            ventana.tbPrecio.IsEnabled = false;
            ventana.cbCategoria.IsEnabled = false;
        }
        #endregion
        #region UPDATE         
        private void BtnModificar_Click(object sender, RoutedEventArgs e)
        {
            CRUDArticulos ventana = new CRUDArticulos(articulo,bd);
            FrameArticulos.Content = ventana;
            ventana.BtnActualizar.Visibility = Visibility.Visible;
            ventana.BtnCrear.Visibility = Visibility.Hidden;
            ventana.Titulo.Text = "Modificación de Usuario";
        }
        #endregion
        #region DELETE
        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dgArticulos.SelectedIndex != -1)
            {
                // Obtener el artículo seleccionado
                Articulo articuloSeleccionado = dgArticulos.SelectedItem as Articulo;

                // Verificar si el artículo existe en la base de datos
                Articulo articuloExistente = bd.ArticulosRepository.Single(a => a.ArticuloId == articuloSeleccionado.ArticuloId);
                if (articuloExistente != null)
                {
                    // Eliminar las tallas asociadas al artículo
                    foreach (Talla talla in articuloExistente.Tallas.ToList())
                    {
                        bd.TallasRepository.Delete(talla);
                    }

                    // Eliminar el artículo utilizando el repositorio
                    bd.ArticulosRepository.Delete(articuloExistente);
                    bd.Save();

                    // Actualizar la vista de artículos
                    dgArticulos.ItemsSource = bd.ArticulosRepository.GridDatosArticulos();
                    dgArticulos.SelectedIndex = -1;
                    dgTallas.ItemsSource = null;
                    dgTallas.Visibility = Visibility.Hidden;
                }
            }
        }

        #endregion
        #endregion
        #region DATAGRID ARTÍCULOS
        private void dgArticulos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgArticulos.SelectedIndex != -1)
            {
                articulo = dgArticulos.SelectedItem as Articulo;
                dgTallas.Visibility = Visibility.Visible;
            }
            dgTallas.ItemsSource = bd.ArticulosRepository.ObtenerTallas(articulo.ArticuloId);
        }

        #endregion
        #region TEXTBOX BÚSQUEDA
        private void tbBusqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbBusqueda.Text.Length >= 1)
            {
                dgArticulos.ItemsSource = bd.ArticulosRepository.Get(c => c.Nombre.ToLower().Contains(tbBusqueda.Text.ToLower()) ||
                       c.Codigo.ToLower().Contains(tbBusqueda.Text.ToLower()) || c.Marca.ToLower().Contains(tbBusqueda.Text.ToLower())
                       || c.Color.ToLower().Contains(tbBusqueda.Text.ToLower()) || c.PrecioUnidad.ToString().Contains(tbBusqueda.Text.ToLower())
                       || c.Categoria.NombreCategoria.ToLower().Contains(tbBusqueda.Text.ToLower()));
            }
            else
            {
                if (tbBusqueda.Text.Length == 0)
                {
                    dgArticulos.ItemsSource = bd.ArticulosRepository.GridDatosArticulos();
                }
            }
        }
        #endregion
        #region GENERAR INFORME
        private void BtnGenerarInforme_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Deseas generar un informe sobre los artículos de esta aplicación?", "Informes",
            MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.Yes:

                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Archivo PDF (*.pdf)|*.pdf";
                    saveFileDialog.FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads",
                        "Informe_Usuarios_" + $"{DateTime.Now:dd/MM/yy_hh_mm_ss}".Replace("/", "-") + ".pdf");
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        try
                        {
                            FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create);
                            Document doc = new Document();
                            PdfWriter pw = PdfWriter.GetInstance(doc, fs);

                            doc.Open();

                            Image imagen = Image.GetInstance("BannerMaricastana.jpg");
                            imagen.ScaleToFit(350, 350);
                            imagen.Alignment = Element.ALIGN_MIDDLE;
                            Paragraph parrafo = new Paragraph();
                            parrafo.Add(imagen);
                            doc.Add(parrafo);

                            doc.Add(Chunk.NEWLINE);
                            doc.Add(Chunk.NEWLINE);

                            Paragraph parrafoTexto = new Paragraph();
                            parrafoTexto.Alignment = Element.ALIGN_LEFT;
                            Font textoFont = new Font(Font.FontFamily.HELVETICA, 16, Font.BOLDITALIC, BaseColor.BLACK);
                            parrafoTexto.Add(new Chunk("INFORME DE ARTÍCULOS:", textoFont));
                            doc.Add(parrafoTexto);

                            Font EncabezadoFont = new Font(Font.FontFamily.HELVETICA, 8, Font.BOLD, BaseColor.BLACK);
                            Font standarFont = new Font(Font.FontFamily.HELVETICA, 7, Font.NORMAL, BaseColor.BLACK);

                            doc.Add(Chunk.NEWLINE);

                            PdfPTable tabla = new PdfPTable(6);
                            tabla.WidthPercentage = 100;

                            PdfPCell celCodigo = new PdfPCell(new Phrase("Código", EncabezadoFont));
                            celCodigo.BorderWidth = 0.75f;
                            celCodigo.HorizontalAlignment = Element.ALIGN_CENTER;
                            celCodigo.VerticalAlignment = Element.ALIGN_MIDDLE;
                            celCodigo.BackgroundColor = BaseColor.ORANGE;

                            PdfPCell celNombre = new PdfPCell(new Phrase("Nombre", EncabezadoFont));
                            celNombre.BorderWidth = 0.75f;
                            celNombre.HorizontalAlignment = Element.ALIGN_CENTER;
                            celNombre.VerticalAlignment = Element.ALIGN_MIDDLE;
                            celNombre.BackgroundColor = BaseColor.ORANGE;

                            PdfPCell celMarca = new PdfPCell(new Phrase("Marca", EncabezadoFont));
                            celMarca.BorderWidth = 0.75f;
                            celMarca.HorizontalAlignment = Element.ALIGN_CENTER;
                            celMarca.VerticalAlignment = Element.ALIGN_MIDDLE;
                            celMarca.BackgroundColor = BaseColor.ORANGE;

                            PdfPCell celColor = new PdfPCell(new Phrase("Color", EncabezadoFont));
                            celColor.BorderWidth = 0.75f;
                            celColor.HorizontalAlignment = Element.ALIGN_CENTER;
                            celColor.VerticalAlignment = Element.ALIGN_MIDDLE;
                            celColor.BackgroundColor = BaseColor.ORANGE;

                            PdfPCell celPrecio = new PdfPCell(new Phrase("Precio", EncabezadoFont));
                            celPrecio.BorderWidth = 0.75f;
                            celPrecio.HorizontalAlignment = Element.ALIGN_CENTER;
                            celPrecio.VerticalAlignment = Element.ALIGN_MIDDLE;
                            celPrecio.BackgroundColor = BaseColor.ORANGE;

                            PdfPCell celCategoria = new PdfPCell(new Phrase("Categoría", EncabezadoFont));
                            celCategoria.BorderWidth = 0.75f;
                            celCategoria.HorizontalAlignment = Element.ALIGN_CENTER;
                            celCategoria.VerticalAlignment = Element.ALIGN_MIDDLE;
                            celCategoria.BackgroundColor = BaseColor.ORANGE;


                            tabla.AddCell(celCodigo);
                            tabla.AddCell(celNombre);
                            tabla.AddCell(celMarca);
                            tabla.AddCell(celColor);
                            tabla.AddCell(celPrecio);
                            tabla.AddCell(celCategoria);


                            foreach (Articulo articulo in bd.ArticulosRepository.GridDatosArticulos())
                            {
                                celCodigo = new PdfPCell(new Phrase(articulo.Codigo, standarFont));
                                celCodigo.BorderWidth = 0.75f;
                                celCodigo.HorizontalAlignment = Element.ALIGN_CENTER;
                                celCodigo.VerticalAlignment = Element.ALIGN_MIDDLE;


                                celNombre = new PdfPCell(new Phrase(articulo.Nombre, standarFont));
                                celNombre.BorderWidth = 0.75f;
                                celNombre.HorizontalAlignment = Element.ALIGN_CENTER;
                                celNombre.VerticalAlignment = Element.ALIGN_MIDDLE;


                                celMarca = new PdfPCell(new Phrase(articulo.Marca, standarFont));
                                celMarca.BorderWidth = 0.75f;
                                celMarca.HorizontalAlignment = Element.ALIGN_CENTER;
                                celMarca.VerticalAlignment = Element.ALIGN_MIDDLE;


                                celColor = new PdfPCell(new Phrase(articulo.Color, standarFont));
                                celColor.BorderWidth = 0.75f;
                                celColor.HorizontalAlignment = Element.ALIGN_CENTER;
                                celColor.VerticalAlignment = Element.ALIGN_MIDDLE;


                                celPrecio = new PdfPCell(new Phrase(articulo.PrecioUnidad.ToString() + "€", standarFont));
                                celPrecio.BorderWidth = 0.75f;
                                celPrecio.HorizontalAlignment = Element.ALIGN_CENTER;
                                celPrecio.VerticalAlignment = Element.ALIGN_MIDDLE;


                                celCategoria = new PdfPCell(new Phrase(articulo.Categoria.NombreCategoria, standarFont));
                                celCategoria.BorderWidth = 0.75f;
                                celCategoria.HorizontalAlignment = Element.ALIGN_CENTER;
                                celCategoria.VerticalAlignment = Element.ALIGN_MIDDLE;




                                tabla.AddCell(celCodigo);
                                tabla.AddCell(celNombre);
                                tabla.AddCell(celMarca);
                                tabla.AddCell(celColor);
                                tabla.AddCell(celPrecio);
                                tabla.AddCell(celCategoria);

                            }

                            doc.Add(tabla);

                            doc.Close();
                            pw.Close();
                            fs.Close();

                            Process process = Process.Start(new ProcessStartInfo(saveFileDialog.FileName) { UseShellExecute = true });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Se produjo un error al generar el informe: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }

                    break;

                case MessageBoxResult.No:

                    break;
            }
            #endregion
        }
    }
}
