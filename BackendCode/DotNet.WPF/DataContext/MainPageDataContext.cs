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
    public class MainPageDataContext : BaseViewDataContext
    {
        private string _employee_id;
        private string _employee_name;
        public string Employee_id{
            get{
                return _employee_id;
            }
            set{
                _employee_id=value;
            }
        }
        public string Employee_Name{
            get{
                return _employee_name;
            }
            set{
                _employee_name=value;
            }
        }
        private IMainPageDataProvider _mainpagedataprovider;
        public string PageName { get; set; } = "Main page";

        #region ICommand

        public ICommand ChangeCurrentPageCommand { get; set; }

        #endregion

        public MainPageDataContext(IMainPageDataProvider mainpagedataprovider)
        {
            _mainpagedataprovider=mainpagedataprovider
            // Initialize the ChangeCurrentPageCommand with a RelayCommand.
            ChangeCurrentPageCommand = new RelayCommand(this.OnChangeCurrentPage, this.CanExecuteChangeCurrentPage);
        }


        // Method to handle changing the current page.
        public void OnChangeCurrentPage(object parameters)
        {
            EmployeeDetail employeedetail=new EmployeeDetail();
            employeedetail.Employee_Id=Int32.Parse(Employee_id);
            employeedetail.Employee_Name=Employee_Name;
            _mainpagedataprovider.InsertEmployeeData(employeedetail);
            // Notify the Observer about a navigation request.
            ObserverMessenger.Observer.Instance.Notify(new NavigatePageTo((string)parameters));
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
    }
}
