using System.Configuration;
using System.Data;
using System.Windows;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config")]
namespace tour_planner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
    }



}
