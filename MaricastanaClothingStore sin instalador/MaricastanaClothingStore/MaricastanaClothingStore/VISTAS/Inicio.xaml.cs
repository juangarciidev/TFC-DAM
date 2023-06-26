using LiveCharts;
using LiveCharts.Wpf;
using MaricastanaClothingStore.DAL;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace MaricastanaClothingStore.VISTAS
{
    public partial class Inicio : UserControl
    {
        UnitOfWork bd = new UnitOfWork();
        public Inicio()
        {
            InitializeComponent();
            decimal ventasTotales = bd.VentasRepository.Get().Count();
            lblVentasTotales.Content = ventasTotales.ToString();
            decimal articulosDisponibles = bd.TallasRepository.Get().Sum(t => t.Cantidad);
            lblArtDisponibles.Content = articulosDisponibles.ToString();

            // Obtener las top 10 mejores ventas
            var ventas = bd.VentasRepository.Get()
                .OrderByDescending(v => v.PrecioTotal)
                .Take(5);

            // Obtener los nombres de los productos y los totales de ventas
            var fechasVentas = ventas.Select(v => v.FechaVenta.ToShortDateString()).ToList();
            var totalesVentas = ventas.Select(v => v.PrecioTotal).ToList();

            // Crear los ChartValues para los totales de ventas
            var values = new ChartValues<decimal>(totalesVentas);

            // Configurar la gráfica
            Chart.Series.Clear();
            Chart.Series.Add(new ColumnSeries
            {
                Title = "Ventas",
                Values = values,
                DataLabels = true,
                Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#715a52"))
            });

            Chart.AxisX.Clear();
            Chart.AxisX.Add(new Axis
            {
                Title = "Fecha de Venta",
                Labels = fechasVentas
            });

            Chart.AxisY.Clear();
            Chart.AxisY.Add(new Axis
            {
                Title = "Precio Total de la Venta en €"
            });
        }
    }
}
