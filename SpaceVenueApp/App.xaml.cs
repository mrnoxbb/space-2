using System.Globalization;
using System.Threading;
using System.Windows;

namespace SpaceVenueApp;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        var culture = new CultureInfo("ar-EG");
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;
    }
}
