using DotNet.WPF.ApplicationFunction;
using DotNet.WPF.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DotNet.Backend.Communication.Messenger.MessengerClasses;
using System.Windows.Input;

namespace DotNet.WPF.DataContext
{
    public class SecondPageDataContext : BaseViewDataContext
    {

        public string PageName { get; set; } = "Second page";

        #region ICommand

        public ICommand ChangeCurrentPageCommand { get; set; }

        #endregion

        public SecondPageDataContext()
        {
            ChangeCurrentPageCommand = new RelayCommand(this.OnChangeCurrentPage, this.CanExecuteChangeCurrentPage);
        }

        public void OnChangeCurrentPage(object parameters)
        {
            ObserverMessenger.Observer.Instance.Notify(new NavigatePageTo((string)parameters));
        }

        public bool CanExecuteChangeCurrentPage(object parameters)
        {
            try
            {
                AppPage page = Enum.Parse<AppPage>((string)parameters);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

    }
}
