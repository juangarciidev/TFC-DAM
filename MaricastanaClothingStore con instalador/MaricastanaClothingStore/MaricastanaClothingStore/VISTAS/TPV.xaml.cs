using iTextSharp.text.pdf;
using iTextSharp.text;
using MaricastanaClothingStore.DAL;
using MaricastanaClothingStore.MODELO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.Diagnostics;
using Image = iTextSharp.text.Image;

namespace MaricastanaClothingStore.VISTAS
{
    public partial class TPV : UserControl
    {
        UnitOfWork bd = new UnitOfWork();
        List<LineaVenta> lista = new List<LineaVenta>();
        Talla talla = new Talla();
        Usuario usuario;

        public TPV(Usuario usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
            lbTotal.Content = "Total: 0,00 €";
            lbEfectivo.Content = "Efectivo: 0,00 €";
            lbCambio.Content = "Cambio a devolver: 0,00 €";
            dgArticulos.ItemsSource = lista;
        }
        #region COMBOBOX TALLAS
        private void cbTalla_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbTalla.SelectedIndex != -1)
            {
                talla = (Talla)cbTalla.SelectedItem;
            }
        }
        #endregion
        #region BOTONES
        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbCodigo.Text))
            {
                MessageBox.Show("Por favor ingresa un código de artículo válido!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Articulo articulo = bd.ArticulosRepository.Get(c => c.Codigo == tbCodigo.Text).FirstOrDefault();

            if (articulo == null)
            {
                MessageBox.Show("No se encontró ningún artículo en la base de datos!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            tbNombre.Text = articulo.Nombre;
            tbPrecio.Text = articulo.PrecioUnidadConSimbolo.ToString();

            cbTalla.ItemsSource = bd.ArticulosRepository.ObtenerTallas(articulo.ArticuloId);
            cbTalla.DisplayMemberPath = "NombreTalla";
            cbTalla.SelectedValuePath = "TallaId";
            cbTalla.SelectedIndex = 0;
            tbCantidad.Focus();
        }


        private void BtnAgregarArticulo_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbNombre.Text))
            {
                MessageBox.Show("No se ha buscado ningún artículo!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(cbTalla.Text))
            {
                MessageBox.Show("No se ha seleccionado ninguna talla!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(tbCantidad.Text))
            {
                MessageBox.Show("No se ha introducido ninguna cantidad!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(tbCantidad.Text, out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("No se ha introducido una cantidad correcta!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Agregar el artículo al DataGrid
            try
            {
                if (talla.Cantidad < int.Parse(tbCantidad.Text))
                {
                    MessageBox.Show("Solo queda " + talla.Cantidad + " " + talla.NombreTalla + "!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                LineaVenta l = new LineaVenta
                {
                    Talla = talla,
                    PrecioUnidad = decimal.Parse(tbPrecio.Text.Replace(" €", "")),
                    Cantidad = int.Parse(tbCantidad.Text),
                };
                if (lista.Find(x => x.Talla.Articulo.Codigo.Equals(talla.Articulo.Codigo) && x.Talla.NombreTalla.Equals(talla.NombreTalla)) == null)
                {
                    {
                        lista.Add(l);
                        dgArticulos.Items.Refresh();
                    }
                    // Limpiar los campos del artículo seleccionado
                    tbCodigo.Text = "";
                    tbNombre.Text = "";
                    tbPrecio.Text = "";
                    cbTalla.ItemsSource = null;
                    tbCantidad.Text = "";


                    // Actualizar el valor del Label lbTotal
                    lbTotal.Content = "Total: " + lista.Sum(x => x.Cantidad * x.PrecioUnidad).ToString("C2");
                    dgArticulos.Items.Refresh();

                }
                else
                {
                    MessageBox.Show("Este artículo ya está agregado, si deseas modificar su Cantidad pulsa en el botón Modificar de ese artículo!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    tbCodigo.Text = "";
                    tbNombre.Text = "";
                    tbPrecio.Text = "";
                    cbTalla.ItemsSource = null;
                    tbCantidad.Text = "";
                }
            }
            catch (Exception ex) { }
        }


        private void BtnModificarCantidad_Click(object sender, RoutedEventArgs e)
        {
            // Verificar si hay algún elemento seleccionado
            if (dgArticulos.SelectedItem == null)
            {
                MessageBox.Show("No se ha seleccionado ningún artículo!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Obtener el elemento seleccionado
            LineaVenta lineaSeleccionada = (LineaVenta)dgArticulos.SelectedItem;

            // Abrir un cuadro de diálogo para ingresar la nueva cantidad
            CantidadTPV modificarCantidad = new CantidadTPV();
            modificarCantidad.ShowDialog();

            // Verificar si se ha ingresado una nueva cantidad
            if (modificarCantidad.DialogResult.HasValue && modificarCantidad.DialogResult.Value)
            {
                if (int.TryParse(modificarCantidad.tbCantidad.Text, out int nuevaCantidad) && nuevaCantidad > 0)
                {
                    // Obtener la talla correspondiente a la línea seleccionada
                    Talla talla = lineaSeleccionada.Talla;

                    if (talla == null)
                    {
                        MessageBox.Show("No se ha encontrado la talla correspondiente a la línea seleccionada!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Verificar si queda la cantidad deseada de la talla
                    if (talla.Cantidad < nuevaCantidad)
                    {
                        MessageBoxResult confirmResultado = MessageBox.Show("Solo queda " + talla.Cantidad + " " + talla.NombreTalla + "!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        if (confirmResultado == MessageBoxResult.OK)
                        {
                            return; // Salir del método sin modificar la cantidad
                        }
                    }

                    // Modificar la cantidad de la línea de venta seleccionada con la nueva cantidad ingresada
                    lineaSeleccionada.Cantidad = nuevaCantidad;

                    // Actualizar el DataGrid
                    dgArticulos.Items.Refresh();

                    // Actualizar el valor del Label lbTotal
                    if (talla.Cantidad >= nuevaCantidad)
                    {
                        lbTotal.Content = "Total: " + lista.Sum(x => x.Cantidad * x.PrecioUnidad).ToString("C2");
                    }
                }
                else
                {
                    MessageBox.Show("No se ha introducido una cantidad válida!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            // Verificar si hay algún elemento seleccionado
            if (dgArticulos.SelectedItem == null)
            {
                MessageBox.Show("No se ha seleccionado ningún artículo!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Obtener el elemento seleccionado y eliminarlo del DataGrid
            lista.Remove((LineaVenta)dgArticulos.SelectedItem);
            lbTotal.Content = "Total: " + lista.Sum(x => x.Cantidad * x.PrecioUnidad).ToString("C2");
            dgArticulos.Items.Refresh();
        }


        private void btnAnularOrden_Click(object sender, RoutedEventArgs e)
        {
            if (dgArticulos.Items.Count == 0)
            {
                MessageBox.Show("No hay artículos agregados para anular!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Establecer el ItemsSource en null para liberar el enlace de datos
            dgArticulos.ItemsSource = null;

            lista.Clear();
            dgArticulos.ItemsSource = lista;

            lbTotal.Content = "Total: 0,00 €";
            lbEfectivo.Content = "Efectivo: 0,00 €";
            lbCambio.Content = "Cambio a devolver: 0,00 €";

        }


        private void btnPagar_Click(object sender, RoutedEventArgs e)
        {
            if (dgArticulos.Items.Count == 0)
            {
                MessageBox.Show("No hay artículos agregados para cobrar!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            EfectivoTPV efectivoTPV = new EfectivoTPV();
            bool? dialogResult = efectivoTPV.ShowDialog();
            efectivoTPV.tbEfectivo.Focus();

            if (dialogResult.HasValue && dialogResult.Value)
            {
                decimal efectivo;
                if (!decimal.TryParse(efectivoTPV.tbEfectivo.Text, out efectivo) || efectivo <= 0)
                {
                    MessageBox.Show("No se ha introducido una cantidad correcta!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                // Calcular el total de la venta y cambio a devolver
                decimal totalVenta = lista.Sum(x => x.Cantidad * x.PrecioUnidad);
                decimal cambio = efectivo - totalVenta;

                lbEfectivo.Content = "Efectivo: " + efectivo.ToString("C2");
                lbCambio.Content = "Cambio a devolver: " + cambio.ToString("C2");


                // Verificar si el efectivo es suficiente
                if (efectivo < totalVenta)
                {
                    lbEfectivo.Content = "Efectivo: 0,00 €";
                    lbCambio.Content = "Cambio a devolver: 0,00 €";
                    MessageBox.Show("El efectivo ingresado es insuficiente para cubrir el total de la venta!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if (efectivo >= totalVenta)
                {
                    // Mostrar el resumen de la venta
                    MessageBoxResult confirmResult = MessageBox.Show("Efectivo introducido correctamente, se va a realizar la venta!", "Aviso", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                    if (confirmResult == MessageBoxResult.OK)
                    {
                        // Crear una nueva venta
                        Venta venta = new Venta

                        {
                            FechaVenta = DateTime.Now,
                            UsuarioId = usuario.UsuarioId, // Reemplaza con el ID del usuario correspondiente
                            NumFactura = string.Format("{0:yyyyMMddHHmmss}", DateTime.Now),
                            LineaVentas = lista,
                            PrecioTotal = lista.Sum(l => l.Cantidad * l.PrecioUnidad)
                        };

                        // eliminar lo vendido 
                        foreach (LineaVenta linea in lista)
                        {
                            Talla talla = bd.TallasRepository.ComprobarTalla(linea.Talla.Articulo.Codigo, linea.Talla.NombreTalla);
                            talla.Cantidad -= linea.Cantidad;
                            bd.TallasRepository.Actualizar(talla);
                        }


                        // Guardar la venta en la base de datos
                        bd.VentasRepository.Añadir(venta);
                        bd.Save();

                        // Generar el ticket de compra
                        GenerarTicketCompra(venta, efectivo, cambio);

                        // Limpiar los elementos del DataGrid y actualizar los Labels
                        lista.Clear();
                        dgArticulos.Items.Refresh();
                        lbTotal.Content = "Total: 0,00 €";
                        lbEfectivo.Content = "Efectivo: 0,00 €";
                        lbCambio.Content = "Cambio a devolver: 0,00 €";

                        MessageBox.Show("Venta realizada correctamente.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    if (confirmResult == MessageBoxResult.Cancel)
                    {
                        lbEfectivo.Content = "Efectivo: 0,00 €";
                        lbCambio.Content = "Cambio a devolver: 0,00 €";
                    }
                }
            }
        }
        #endregion
        #region GENERAR INFORME
        private void GenerarTicketCompra(Venta venta, decimal efectivo, decimal cambio)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivo PDF (*.pdf)|*.pdf";
            saveFileDialog.FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads",
                "TicketVenta_" + $"{DateTime.Now:dd/MM/yy_hh_mm_ss}".Replace("/", "-") + ".pdf");
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

                    Paragraph parrafoTexto = new Paragraph();
                    parrafoTexto.Alignment = Element.ALIGN_LEFT;
                    Font textoFont = new Font(Font.FontFamily.HELVETICA, 16, Font.BOLDITALIC, BaseColor.BLACK);
                    parrafoTexto.Add(new Chunk("Susana Ocampo Rodríguez", textoFont));
                    doc.Add(parrafoTexto);

                    Paragraph direccion = new Paragraph();
                    direccion.Alignment = Element.ALIGN_LEFT;
                    direccion.Add(new Chunk("Santo Domingo 76 Bajo"));
                    doc.Add(direccion);

                    Paragraph cp = new Paragraph();
                    cp.Alignment = Element.ALIGN_LEFT;
                    cp.Add(new Chunk("32003 - Ourense"));
                    doc.Add(cp);

                    Paragraph ciudad = new Paragraph();
                    ciudad.Alignment = Element.ALIGN_LEFT;
                    ciudad.Add(new Chunk("OURENSE"));
                    doc.Add(ciudad);

                    Paragraph cif = new Paragraph();
                    cif.Alignment = Element.ALIGN_LEFT;
                    cif.Add(new Chunk("C.I.F. 34986966X"));
                    doc.Add(cif);

                    Paragraph tlf = new Paragraph();
                    tlf.Alignment = Element.ALIGN_LEFT;
                    tlf.Add(new Chunk("Tlf: 988371786"));
                    doc.Add(tlf);

                    doc.Add(Chunk.NEWLINE);

                    Paragraph cliente = new Paragraph();
                    cliente.Alignment = Element.ALIGN_LEFT;
                    Font clientetextoFont = new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD, BaseColor.BLACK);
                    cliente.Add(new Chunk("CLIENTE:", clientetextoFont));
                    doc.Add(cliente);

                    Paragraph publico = new Paragraph();
                    publico.Alignment = Element.ALIGN_LEFT;
                    publico.Add(new Chunk("PÚBLICO EN GENERAL"));
                    doc.Add(publico);

                    // Información de la venta
                    Paragraph fechaVenta = new Paragraph(venta.FechaVenta.ToString());
                    doc.Add(fechaVenta);

                    // Factura y IVA INCLUIDO
                    PdfPTable facturaTable = new PdfPTable(2);
                    facturaTable.WidthPercentage = 100;
                    facturaTable.SetWidths(new float[] { 1f, 1f });

                    facturaTable.AddCell(new PdfPCell(new Phrase("Fact. Simplificada nº: " + venta.NumFactura, new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.BLACK)))
                    {
                        Border = PdfPCell.NO_BORDER
                    });

                    facturaTable.AddCell(new PdfPCell(new Phrase("- I.V.A. INCLUIDO- ", new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL, BaseColor.BLACK)))
                    {
                        Border = PdfPCell.NO_BORDER,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    });

                    doc.Add(facturaTable);

                    doc.Add(Chunk.NEWLINE);

                    // Detalles de la venta (productos, cantidades, precios, etc.)
                    PdfPTable tabla = new PdfPTable(4);
                    tabla.WidthPercentage = 100;
                    Font EncabezadoFont = new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD, BaseColor.BLACK);

                    // Encabezados de las columnas
                    PdfPCell celCant = new PdfPCell(new Phrase("CANTIDAD", EncabezadoFont));
                    celCant.Border = PdfPCell.NO_BORDER;
                    celCant.HorizontalAlignment = Element.ALIGN_CENTER;
                    celCant.VerticalAlignment = Element.ALIGN_MIDDLE;


                    PdfPCell celArt = new PdfPCell(new Phrase("ARTÍCULO", EncabezadoFont));
                    celArt.Border = PdfPCell.NO_BORDER;
                    celArt.HorizontalAlignment = Element.ALIGN_CENTER;
                    celArt.VerticalAlignment = Element.ALIGN_MIDDLE;


                    PdfPCell celPrecio = new PdfPCell(new Phrase("PRECIO", EncabezadoFont));
                    celPrecio.Border = PdfPCell.NO_BORDER;
                    celPrecio.HorizontalAlignment = Element.ALIGN_CENTER;
                    celPrecio.VerticalAlignment = Element.ALIGN_MIDDLE;


                    PdfPCell celImporte = new PdfPCell(new Phrase("IMPORTE", EncabezadoFont));
                    celImporte.Border = PdfPCell.NO_BORDER;
                    celImporte.HorizontalAlignment = Element.ALIGN_CENTER;
                    celImporte.VerticalAlignment = Element.ALIGN_MIDDLE;


                    tabla.AddCell(celCant);
                    tabla.AddCell(celArt);
                    tabla.AddCell(celPrecio);
                    tabla.AddCell(celImporte);


                    Font standarFont = new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL, BaseColor.BLACK);

                    // Contenido de la tabla con los productos vendidos
                    foreach (LineaVenta lineaVenta in venta.LineaVentas)
                    {
                        celCant = new PdfPCell(new Phrase(lineaVenta.Cantidad.ToString(), standarFont));
                        celCant.Border = PdfPCell.NO_BORDER;
                        celCant.HorizontalAlignment = Element.ALIGN_CENTER;
                        celCant.VerticalAlignment = Element.ALIGN_MIDDLE;


                        celArt = new PdfPCell(new Phrase(lineaVenta.Talla.Articulo.Codigo + " (" + lineaVenta.Talla.Articulo.Nombre + " " + lineaVenta.Talla.Articulo.Marca + ")" + " Talla " + lineaVenta.Talla.NombreTalla, standarFont));
                        celArt.Border = PdfPCell.NO_BORDER;
                        celArt.HorizontalAlignment = Element.ALIGN_CENTER;
                        celArt.VerticalAlignment = Element.ALIGN_MIDDLE;


                        celPrecio = new PdfPCell(new Phrase(lineaVenta.PrecioUnidad.ToString("C2"), standarFont));
                        celPrecio.Border = PdfPCell.NO_BORDER;
                        celPrecio.HorizontalAlignment = Element.ALIGN_CENTER;
                        celPrecio.VerticalAlignment = Element.ALIGN_MIDDLE;


                        celImporte = new PdfPCell(new Phrase((lineaVenta.Cantidad * lineaVenta.PrecioUnidad).ToString("C2"), standarFont));
                        celImporte.Border = PdfPCell.NO_BORDER;
                        celImporte.HorizontalAlignment = Element.ALIGN_CENTER;
                        celImporte.VerticalAlignment = Element.ALIGN_MIDDLE;


                        tabla.AddCell(celCant);
                        tabla.AddCell(celArt);
                        tabla.AddCell(celPrecio);
                        tabla.AddCell(celImporte);
                    }

                    doc.Add(tabla);

                    doc.Add(Chunk.NEWLINE);

                    // Total de la venta
                    Paragraph totalVenta = new Paragraph("TOTAL IMPORTE: " + venta.PrecioTotal.ToString("C2"), new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD, BaseColor.BLACK));
                    totalVenta.Alignment = Element.ALIGN_RIGHT;
                    doc.Add(totalVenta);

                    doc.Add(Chunk.NEWLINE);

                    // Agregar información de efectivo y cambio
                    Paragraph efectivoParrafo = new Paragraph();
                    efectivoParrafo.Alignment = Element.ALIGN_RIGHT;
                    efectivoParrafo.Add(new Chunk("Efectivo entregado: " + efectivo.ToString("C2"), new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD, BaseColor.BLACK)));
                    doc.Add(efectivoParrafo);

                    Paragraph cambioParrafo = new Paragraph();
                    cambioParrafo.Alignment = Element.ALIGN_RIGHT;
                    cambioParrafo.Add(new Chunk("Cambio a devolver: " + cambio.ToString("C2"), new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD, BaseColor.BLACK)));
                    doc.Add(cambioParrafo);

                    doc.Add(Chunk.NEWLINE);
                    //Iva, gracias y demás información
                    Paragraph iva = new Paragraph();
                    iva.Alignment = Element.ALIGN_CENTER;
                    iva.Add(new Chunk("IVA INCLUIDO", new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD, BaseColor.BLACK)));
                    doc.Add(iva);
                    Paragraph gracias = new Paragraph();
                    gracias.Alignment = Element.ALIGN_CENTER;
                    gracias.Add(new Chunk("*** GRACIAS POR SU VISITA ***", new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD, BaseColor.BLACK)));
                    doc.Add(gracias);

                    doc.Add(Chunk.NEWLINE);
                    Paragraph ticket = new Paragraph();
                    ticket.Alignment = Element.ALIGN_CENTER;
                    ticket.Add(new Chunk("Imprescindible guardar el ticket para cambio en un plazo máximo de 15 días.", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.BLACK)));
                    doc.Add(ticket);
                    Paragraph devol = new Paragraph();
                    devol.Alignment = Element.ALIGN_CENTER;
                    devol.Add(new Chunk("Las devoluciones se harán mediante vale.", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.BLACK)));
                    doc.Add(devol);
                    Paragraph fiesta = new Paragraph();
                    fiesta.Alignment = Element.ALIGN_CENTER;
                    fiesta.Add(new Chunk("No se admiten cambios ni devoluciones en artículos de fiesta.", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.BLACK)));
                    doc.Add(fiesta);

                    //Cerrar
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

        }
        #endregion
    }
}
