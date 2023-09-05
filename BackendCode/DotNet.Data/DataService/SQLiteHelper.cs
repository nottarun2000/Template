namespace DotNet.Data.DataService{
    public class SQLiteHelper:ISQLiteHelper{
        private IApplicationDBContext _applicationDBContext;
        public SQLiteHelper(IApplicationDBContext applicationDBContext){
            _applicationDBContext=applicationDBContext;
        }
        public void InsertEmployeeData(EmployeeDetail EmployeeDetails){
            try{
                IApplicationDBContext dBContext=_applicationDBContext.CreateInstance();
                dBContext.Employees.Add(EmployeeDetails);
                dBContext.SaveChanges();
            }
            catch(Exception e){

            }
        }
    }
}