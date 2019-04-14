namespace TestNinja.Mocking
{
    public interface IEmployeeRepository
    {
        void DeleteEmployee(int id);
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        private EmployeeContext _db;

        public EmployeeRepository()
        {
            _db = new EmployeeContext();
        }

        public void DeleteEmployee(int id)
        {
            var employee = GetEmployee(_db, id);

            if (employee != null)
            {
                _db.Employees.Remove(employee);
                _db.SaveChanges();
            }
        }

        private Employee GetEmployee(EmployeeContext _db, int id)
        {
            return _db.Employees.Find(id);
        }
    }
}
