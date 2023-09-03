using DotNet.WPF.ApplicationFunction;
using DotNet.WPF.Command;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;
using static DotNet.Backend.Communication.Messenger.MessengerClasses;

namespace DotNet.WPF.DataContext
{
    [AddINotifyPropertyChangedInterface]
    public class MainPageDataContext : BaseViewDataContext
    {
        public string PageName { get; set; } = "Main page";

        #region ICommand

        public ICommand ChangeCurrentPageCommand { get; set; }

        #endregion

        public MainPageDataContext()
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
