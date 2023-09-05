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
            var container=new Bootstrapper().ContainerBootstrap();
            var mainpagedataprovider=container.Resolve<IMainPageDataProvider>();
            // Initialize theme

            if (e.Args.Count() == 0)  // starting server
            {
                AppMainWindow appMainWindow = new AppMainWindow();
                var temp = new AppMainWindowDataContext(mainpagedataprovider);
                temp.StartgRPC(null);
                appMainWindow.DataContext = temp;
                appMainWindow.Show();
            }
            else
            { 
                SecondWindow secondWindow = new SecondWindow();
                secondWindow.DataContext = new SecondWindowDataContext();
                secondWindow.Show();
            }

        }

        



    }
}
