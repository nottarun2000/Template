using DotNet.WPF.ApplicationFunction;
using DotNet.WPF.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DotNet.Backend.Communication.Messenger.MessengerClasses;
using System.Windows.Input;
using PropertyChanged;
using System.Diagnostics;

namespace DotNet.WPF.DataContext
{
    public class SecondPageDataContext : BaseViewDataContext
    {
        public string PageName { get; set; } = "Second page";

        #region ICommand

        public ICommand ChangeCurrentPageCommand { get; set; }
        public ICommand StartChild { get; set; }


        #endregion

        public SecondPageDataContext()
        {
            // Initialize the ChangeCurrentPageCommand with a RelayCommand.
            ChangeCurrentPageCommand = new RelayCommand(this.OnChangeCurrentPage, this.CanExecuteChangeCurrentPage);
            StartChild = new RelayCommand(this.OnStartChild, this.CanExecute);
        }


        // Method to handle changing the current page.
        public void OnChangeCurrentPage(object parameters)
        {
            // Notify the Observer about a navigation request.
            ObserverMessenger.Observer.Instance.Notify(new NavigatePageTo((string)parameters));
        }


        public void OnStartChild(object obj)
        { 
            Process process = new Process();
            process.StartInfo.FileName = Process.GetCurrentProcess().MainModule?.FileName;
            process.StartInfo.Arguments = "randomargs";
            process.Start();

        }


        // Method to check if changing the current page can be executed.
        public bool CanExecuteChangeCurrentPage(object parameters)
        {
            try
            {
                // Attempt to parse the provided parameters as an AppPage enum value.
                AppPage page = Enum.Parse<AppPage>((string)parameters);
            }
            catch (Exception ex)
            {
                // If an exception occurs, return false to indicate it cannot be executed.
                return false;
            }

            // If no exception occurs, return true to indicate it can be executed.
            return true;
        }

        public bool CanExecute(object parameters)
        {
            return true;
        }

    }
}
