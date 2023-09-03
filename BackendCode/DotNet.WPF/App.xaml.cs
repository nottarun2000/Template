using DotNet.Backend.Startup;
using DotNet.Data.Data;
using DotNet.WPF.DataContext;
using DotNet.WPF.Windows;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DotNet.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public App()
        {
                
        }


        public void ApplicationStartup(object sender, StartupEventArgs e)
        {
            // Initialize theme

            AppMainWindow appMainWindow = new AppMainWindow();
            appMainWindow.DataContext = new AppMainWindowDataContext();
            appMainWindow.Show();

            //ApplicationDBContext applicationDBContext = new ApplicationDBContext(DBContextOptionsFactory.GetOptions());

        }

        



    }
}
