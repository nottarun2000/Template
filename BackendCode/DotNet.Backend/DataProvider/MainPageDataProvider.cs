namespace Dotnet.Backend.DataProvider{
    public class MainPageDataProvider:IMainPageDataProvider{
        private IApplicationDBContext _dbContext;
        private ISQLiteHelper _sqlliteHelper;

        public MainPageDataProvider(IApplicationDBContext applicationDBContext,ISQLiteHelper sqlliteHelper){
            _dbContext=applicationDBContext;
            _sqlliteHelper=sqlliteHelper;
        }

        public void InsertEmployeeData(EmployeeDetail EmployeeDetail){
            _sqlliteHelper.InsertEmployeeData(EmployeeDetail);
        }

    }
}