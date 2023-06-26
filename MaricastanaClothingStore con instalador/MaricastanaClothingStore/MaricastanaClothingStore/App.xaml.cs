using System.Globalization;
using System.Windows;

namespace MaricastanaClothingStore
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            CultureInfo.CurrentCulture = new CultureInfo("es-ES");
            CultureInfo.CurrentUICulture = new CultureInfo("es-ES");
        }
    }
}
