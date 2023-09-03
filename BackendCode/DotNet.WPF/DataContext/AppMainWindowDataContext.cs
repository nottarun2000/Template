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
    [AddINotifyPropertyChangedInterface]
    public class AppMainWindowDataContext
    {
        public BaseViewDataContext ActiveMainWindowDC { get; set; }


        #region ICommand

        public ICommand ChangeCurrentPageCommand { get; set; }

        #endregion

        public AppMainWindowDataContext()
        {
            ActiveMainWindowDC = new MainPageDataContext();
            ChangeCurrentPageCommand = new RelayCommand(this.OnChangeCurrentPage , this.CanExecuteChangeCurrentPage);
            ObserverMessenger.Observer.Instance.Subscribe(typeof(NavigatePageTo) , this.ChangeCurrentPage);
        }


        public void OnChangeCurrentPage(object parameters)
        {
            ObserverMessenger.Observer.Instance.Notify(new NavigatePageTo((string)parameters));
        }


        public void ChangeCurrentPage(object parameters)
        {
            var temp = (NavigatePageTo)parameters;
            AppPage page = Enum.Parse<AppPage>(temp.PageName);

            switch (page)
            {
                case AppPage.MainPage:
                    ActiveMainWindowDC = new MainPageDataContext();
                    break;
                case AppPage.SecondPage:
                    ActiveMainWindowDC = new SecondPageDataContext();
                    break;
            }

        }

        public bool CanExecuteChangeCurrentPage(object parameters)
        {
            try
            {
                var temp = (NavigatePageTo)parameters;
                AppPage page = Enum.Parse<AppPage>(temp.PageName);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

    }
}
