namespace DotNet.Data.DBModel{
    public class EmployeeDetail{
        [Key]
        public int Id {get;set;}
        public int EmployeeId {get;set;}
        public string Employee_Name{get;set;}
    }
}