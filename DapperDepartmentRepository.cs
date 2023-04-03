using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLandORM
{
    public class DapperDepartmentRepository : IDepartmentRepository
    {
        //field or local variable for making queries to the DB
        private readonly IDbConnection _connection;

        public DapperDepartmentRepository(IDbConnection connection)
        {
            //constructor
            _connection = connection;

        }
        public IEnumerable<Department> GetAllDepartments()
        { 
            var depos = _connection.Query<Department>("SELECT * FROM departments");

            return depos;
        }

        public void InsertDepartment(string newDepartmentName)
        {
            _connection.Execute("INSERT INTO DEPARTMENTS (Name) VALUES (@departmentName);",
            new { departmentName = newDepartmentName });

        }
    }
}
