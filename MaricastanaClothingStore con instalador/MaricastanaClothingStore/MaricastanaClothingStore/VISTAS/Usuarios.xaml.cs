using iTextSharp.text;
using iTextSharp.text.pdf;
using MaricastanaClothingStore.DAL;
using MaricastanaClothingStore.MODELO;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Image = iTextSharp.text.Image;

namespace MaricastanaClothingStore.VISTAS
{
    public partial class Usuarios : UserControl
    {
        UnitOfWork bd = new UnitOfWork();
        Usuario usuario = new Usuario();

        public Usuarios()
        {
            InitializeComponent();
            GridDatos.ItemsSource = bd.UsuariosRepository.GridDatosUsuarios();
        }
        #region CRUD
        #region CREATE
        private void BtnAgregarUsuario_Click(object sender, RoutedEventArgs e)
        {
            CRUDUsuarios ventana = new CRUDUsuarios(new Usuario());
            FrameUsuarios.Content = ventana;
            ventana.BtnCrear.Visibility = Visibility.Visible;
            ventana.Titulo.Text = "Registro de Usuario";
        }
        #endregion
        #region READ
        private void BtnConsultar_Click(object sender, RoutedEventArgs e)
        {
            CRUDUsuarios ventana = new CRUDUsuarios(usuario);
            FrameUsuarios.Content = ventana;
            ventana.BtnImagen.Visibility = Visibility.Hidden;
            ventana.Titulo.Text = "Consulta de Usuario";
            ventana.tbNombre.IsEnabled = false;
            ventana.tbApellidos.IsEnabled = false;
            ventana.tbDNI.IsEnabled = false;
            ventana.tbCorreo.IsEnabled = false;
            ventana.tbTelefono.IsEnabled = false;
            ventana.tbFechaNac.IsEnabled = false;
            ventana.tbDireccion.IsEnabled = false;
            ventana.cbPrivilegio.IsEnabled = false;
            ventana.tbUsuario.IsEnabled = false;
            ventana.tbContrasena.IsEnabled = false;
        }
        #endregion
        #region UPDATE
        private void BtnModificar_Click(object sender, RoutedEventArgs e)
        {
            CRUDUsuarios ventana = new CRUDUsuarios(usuario);
            FrameUsuarios.Content = ventana;
            ventana.BtnActualizar.Visibility = Visibility.Visible;
            ventana.Titulo.Text = "Modificación de Usuario";
        }
        #endregion
        #region DELETE
        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (GridDatos.SelectedItem != null)
            {
                usuario = GridDatos.SelectedItem as Usuario;
                bd.UsuariosRepository.Delete(usuario);
                bd.Save();
                GridDatos.ItemsSource = bd.UsuariosRepository.GetAll();
            }
        }
        #endregion
        #endregion
        #region DATAGRID USUARIOS
        private void GridDatos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GridDatos.SelectedIndex != -1)
            {
                usuario = GridDatos.SelectedItem as Usuario;
            }
        }
        #endregion
        #region TEXTBOX BÚSQUEDA
        private void tbBusqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbBusqueda.Text.Length >= 1)
            {
                GridDatos.ItemsSource = bd.UsuariosRepository.Get(c => c.Nombre.ToLower().Contains(tbBusqueda.Text.ToLower()) ||
                       c.Apellidos.ToLower().Contains(tbBusqueda.Text.ToLower()) ||
                       c.Telefono.ToLower().Contains(tbBusqueda.Text.ToLower()) ||
                       c.Correo.ToLower().Contains(tbBusqueda.Text.ToLower()));
            }
            else
            {
                if (tbBusqueda.Text.Length == 0)
                {
                    GridDatos.ItemsSource = bd.UsuariosRepository.GetAll();
                }
            }
        }
        #endregion
        #region GENERAR INFORME
        private void BtnGenerarInforme_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Deseas generar un informe sobre los usuarios de esta aplicación?", "Confirmación",
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
                            parrafoTexto.Add(new Chunk("INFORME DE USUARIOS:", textoFont));
                            doc.Add(parrafoTexto);

                            Font EncabezadoFont = new Font(Font.FontFamily.HELVETICA, 8, Font.BOLD, BaseColor.BLACK);
                            Font standarFont = new Font(Font.FontFamily.HELVETICA, 7, Font.NORMAL, BaseColor.BLACK);

                            doc.Add(Chunk.NEWLINE);

                            PdfPTable tabla = new PdfPTable(10);
                            tabla.WidthPercentage = 100;

                            PdfPCell celNombre = new PdfPCell(new Phrase("Nombre", EncabezadoFont));
                            celNombre.BorderWidth = 0.75f;
                            celNombre.HorizontalAlignment = Element.ALIGN_CENTER;
                            celNombre.VerticalAlignment = Element.ALIGN_MIDDLE;
                            celNombre.BackgroundColor = BaseColor.ORANGE;

                            PdfPCell celApellidos = new PdfPCell(new Phrase("Apellidos", EncabezadoFont));
                            celApellidos.BorderWidth = 0.75f;
                            celApellidos.HorizontalAlignment = Element.ALIGN_CENTER;
                            celApellidos.VerticalAlignment = Element.ALIGN_MIDDLE;
                            celApellidos.BackgroundColor = BaseColor.ORANGE;

                            PdfPCell celDNI = new PdfPCell(new Phrase("DNI", EncabezadoFont));
                            celDNI.BorderWidth = 0.75f;
                            celDNI.HorizontalAlignment = Element.ALIGN_CENTER;
                            celDNI.VerticalAlignment = Element.ALIGN_MIDDLE;
                            celDNI.BackgroundColor = BaseColor.ORANGE;

                            PdfPCell celCorreo = new PdfPCell(new Phrase("Correo", EncabezadoFont));
                            celCorreo.BorderWidth = 0.75f;
                            celCorreo.HorizontalAlignment = Element.ALIGN_CENTER;
                            celCorreo.VerticalAlignment = Element.ALIGN_MIDDLE;
                            celCorreo.BackgroundColor = BaseColor.ORANGE;

                            PdfPCell celTelefono = new PdfPCell(new Phrase("Teléfono", EncabezadoFont));
                            celTelefono.BorderWidth = 0.75f;
                            celTelefono.HorizontalAlignment = Element.ALIGN_CENTER;
                            celTelefono.VerticalAlignment = Element.ALIGN_MIDDLE;
                            celTelefono.BackgroundColor = BaseColor.ORANGE;

                            PdfPCell celFechaNac = new PdfPCell(new Phrase("Fecha de nacimiento", EncabezadoFont));
                            celFechaNac.BorderWidth = 0.75f;
                            celFechaNac.HorizontalAlignment = Element.ALIGN_CENTER;
                            celFechaNac.VerticalAlignment = Element.ALIGN_MIDDLE;
                            celFechaNac.BackgroundColor = BaseColor.ORANGE;

                            PdfPCell celDireccion = new PdfPCell(new Phrase("Dirección", EncabezadoFont));
                            celDireccion.BorderWidth = 0.75f;
                            celDireccion.HorizontalAlignment = Element.ALIGN_CENTER;
                            celDireccion.VerticalAlignment = Element.ALIGN_MIDDLE;
                            celDireccion.BackgroundColor = BaseColor.ORANGE;

                            PdfPCell celPrivilegio = new PdfPCell(new Phrase("Privilegio", EncabezadoFont));
                            celPrivilegio.BorderWidth = 0.75f;
                            celPrivilegio.HorizontalAlignment = Element.ALIGN_CENTER;
                            celPrivilegio.VerticalAlignment = Element.ALIGN_MIDDLE;
                            celPrivilegio.BackgroundColor = BaseColor.ORANGE;

                            PdfPCell celUsuario = new PdfPCell(new Phrase("Usuario", EncabezadoFont));
                            celUsuario.BorderWidth = 0.75f;
                            celUsuario.HorizontalAlignment = Element.ALIGN_CENTER;
                            celUsuario.VerticalAlignment = Element.ALIGN_MIDDLE;
                            celUsuario.BackgroundColor = BaseColor.ORANGE;

                            PdfPCell celContra = new PdfPCell(new Phrase("Contraseña", EncabezadoFont));
                            celContra.BorderWidth = 0.75f;
                            celContra.HorizontalAlignment = Element.ALIGN_CENTER;
                            celContra.VerticalAlignment = Element.ALIGN_MIDDLE;
                            celContra.BackgroundColor = BaseColor.ORANGE;

                            tabla.AddCell(celNombre);
                            tabla.AddCell(celApellidos);
                            tabla.AddCell(celDNI);
                            tabla.AddCell(celCorreo);
                            tabla.AddCell(celTelefono);
                            tabla.AddCell(celFechaNac);
                            tabla.AddCell(celDireccion);
                            tabla.AddCell(celPrivilegio);
                            tabla.AddCell(celUsuario);
                            tabla.AddCell(celContra);

                            foreach (Usuario user in bd.UsuariosRepository.GridDatosUsuarios())
                            {
                                celNombre = new PdfPCell(new Phrase(user.Nombre, standarFont));
                                celNombre.BorderWidth = 0.75f;
                                celNombre.HorizontalAlignment = Element.ALIGN_CENTER;
                                celNombre.VerticalAlignment = Element.ALIGN_MIDDLE;


                                celApellidos = new PdfPCell(new Phrase(user.Apellidos, standarFont));
                                celApellidos.BorderWidth = 0.75f;
                                celApellidos.HorizontalAlignment = Element.ALIGN_CENTER;
                                celApellidos.VerticalAlignment = Element.ALIGN_MIDDLE;


                                celDNI = new PdfPCell(new Phrase(user.DNI, standarFont));
                                celDNI.BorderWidth = 0.75f;
                                celDNI.HorizontalAlignment = Element.ALIGN_CENTER;
                                celDNI.VerticalAlignment = Element.ALIGN_MIDDLE;


                                celCorreo = new PdfPCell(new Phrase(user.Correo, standarFont));
                                celCorreo.BorderWidth = 0.75f;
                                celCorreo.HorizontalAlignment = Element.ALIGN_CENTER;
                                celCorreo.VerticalAlignment = Element.ALIGN_MIDDLE;


                                celTelefono = new PdfPCell(new Phrase(user.Telefono, standarFont));
                                celTelefono.BorderWidth = 0.75f;
                                celTelefono.HorizontalAlignment = Element.ALIGN_CENTER;
                                celTelefono.VerticalAlignment = Element.ALIGN_MIDDLE;


                                celFechaNac = new PdfPCell(new Phrase(user.FechaNac, standarFont));
                                celFechaNac.BorderWidth = 0.75f;
                                celFechaNac.HorizontalAlignment = Element.ALIGN_CENTER;
                                celFechaNac.VerticalAlignment = Element.ALIGN_MIDDLE;


                                celDireccion = new PdfPCell(new Phrase(user.Direccion, standarFont));
                                celDireccion.BorderWidth = 0.75f;
                                celDireccion.HorizontalAlignment = Element.ALIGN_CENTER;
                                celDireccion.VerticalAlignment = Element.ALIGN_MIDDLE;

                                celPrivilegio = new PdfPCell(new Phrase(user.Privilegio.NombrePrivilegio, standarFont));
                                celPrivilegio.BorderWidth = 0.75f;
                                celPrivilegio.HorizontalAlignment = Element.ALIGN_CENTER;
                                celPrivilegio.VerticalAlignment = Element.ALIGN_MIDDLE;

                                celUsuario = new PdfPCell(new Phrase(user.LoginUsuario, standarFont));
                                celUsuario.BorderWidth = 0.75f;
                                celUsuario.HorizontalAlignment = Element.ALIGN_CENTER;
                                celUsuario.VerticalAlignment = Element.ALIGN_MIDDLE;

                                celContra = new PdfPCell(new Phrase(user.LoginContraseña, standarFont));
                                celContra.BorderWidth = 0.75f;
                                celContra.HorizontalAlignment = Element.ALIGN_CENTER;
                                celContra.VerticalAlignment = Element.ALIGN_MIDDLE;

                                tabla.AddCell(celNombre);
                                tabla.AddCell(celApellidos);
                                tabla.AddCell(celDNI);
                                tabla.AddCell(celCorreo);
                                tabla.AddCell(celTelefono);
                                tabla.AddCell(celFechaNac);
                                tabla.AddCell(celDireccion);
                                tabla.AddCell(celPrivilegio);
                                tabla.AddCell(celUsuario);
                                tabla.AddCell(celContra);
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

        }
        #endregion
    }
}

