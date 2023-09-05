using DotNet.gRPC.Wrapper;
using DotNet.WPF.ApplicationFunction;
using DotNet.WPF.Command;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static DotNet.Backend.Communication.Messenger.MessengerClasses;

namespace DotNet.WPF.DataContext
{
    
    public class AppMainWindowDataContext : BaseViewDataContext
    {
        public BaseViewDataContext ActiveMainWindowDC { get; set; }
        public string CurrentPage { get; set; }
        private IMainPageDataProvider _mainpagedataprovider;
        #region ICommand

        public ICommand ChangeCurrentPageCommand { get; set; }

        #endregion

        public AppMainWindowDataContext(IMainPageDataProvider mainpagedataprovider)
        {
            _mainpagedataprovider=mainpagedataprovider;
            // Initialize the ActiveMainWindowDC with the MainPageDataContext.
            ActiveMainWindowDC = new MainPageDataContext();
            CurrentPage = "MainPage";

            // Create a RelayCommand for changing the current page and checking if it can be executed.
            ChangeCurrentPageCommand = new RelayCommand(this.OnChangeCurrentPage, this.CanExecuteChangeCurrentPage);

            // Subscribe to the Observer to handle page navigation requests.
            ObserverMessenger.Observer.Instance.Subscribe(typeof(NavigatePageTo), this.ChangeCurrentPage);
        }

        // Method to handle changing the current page.
        public void OnChangeCurrentPage(object parameters)
        {
            // Notify the Observer about the navigation request.
            ObserverMessenger.Observer.Instance.Notify(new NavigatePageTo((string)parameters));
        }

        // Method to change the current page based on the navigation request.
        public void ChangeCurrentPage(object parameters)
        {
            var temp = (NavigatePageTo)parameters;
            AppPage page = Enum.Parse<AppPage>(temp.PageName);

            // Update ActiveMainWindowDC based on the selected AppPage.
            switch (page)
            {
                case AppPage.MainPage:
                    ActiveMainWindowDC = new MainPageDataContext();
                    CurrentPage = "MainPage";

                    break;
                case AppPage.SecondPage:
                    ActiveMainWindowDC = new SecondPageDataContext();
                    CurrentPage = "SecondPage";

                    break;
            }
        }

        // Method to check if changing the current page can be executed.
        public bool CanExecuteChangeCurrentPage(object parameters)
        {
            try
            {
                var temp = (NavigatePageTo)parameters;
                AppPage page = Enum.Parse<AppPage>(temp.PageName);
            }
            catch (Exception ex)
            {
                // If an exception occurs, return false to indicate it cannot be executed.
                return false;
            }

            // If no exception occurs, return true to indicate it can be executed.
            return true;
        }



        public void StartgRPC(object obj)
        {
            try
            {
                var server = new gRPCServer("localhost", 5000);

                server.ClientConnected += (sender, e) => {

                };

                server.ClientDisconnected += (sender, e) => {

                };

                server.MessageReceived += (sender, e) => {

                    if (CurrentPage == "MainPage")
                    {
                        ObserverMessenger.Observer.Instance.Notify(new NavigatePageTo("SecondPage"));

                    }
                    else
                    {
                        ObserverMessenger.Observer.Instance.Notify(new NavigatePageTo("MainPage"));

                    }

                };

                server.ExceptionOccurred += (sender, e) => {

                };

                server.StartServer();

            }
            catch (Exception ex)
            {

            }

        }

    }
}
