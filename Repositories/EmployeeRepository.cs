using Dapper;
using Microsoft.AspNetCore.Connections;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using SmartWay_Test_Task.Entities;
using SmartWay_Test_Task.Entities.Dtos;
using SmartWay_Test_Task.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;

namespace SmartWay_Test_Task.Repositories
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private readonly IConfiguration _configuration;

        public EmployeeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> Add(Employee entity)
        {
            var insertQuery = "INSERT INTO Employees (name,surname,phone,companyId,passportid, departmentid) VALUES(@Name,@Surname,@Phone,@CompanyId,@PassportId,@DepartmentId); SELECT CAST(SCOPE_IDENTITY() as int)";
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                db.Open();
                return (await db.QueryAsync<int>(insertQuery,entity)).FirstOrDefault();
            }
        }
        public async Task Delete(int id)
        {
            var deleteQuery = "DELETE Employees WHERE id= @id";
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                db.Open();
                await db.QueryAsync(deleteQuery, new { id });
            }
        }

        public async Task<Employee> Get(int id)
        {
            var selectQuery = @"SELECT * FROM Employees
                                        LEFT OUTER JOIN Passports ON Employees.passportid = Passports.passid
                                        LEFT OUTER JOIN Departments ON Employees.departmentid = Departments.depid
                                        WHERE id = @id";
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                db.Open();
                return (await db.QueryAsync<Employee, Passport, Department, Employee>(selectQuery, (e, p, d) =>
                {
                    e.Passport = p;
                    e.Department = d;
                    return e;
                }, param: new {id}, splitOn: "passid, depid")).FirstOrDefault();
            }
        }

        public async Task<IQueryable<Employee>> GetAll()
        {
            var selectQuery = @"SELECT * FROM Employees
                                        LEFT OUTER JOIN Passports ON Employees.passportid = Passports.passid
                                        LEFT OUTER JOIN Departments ON Employees.departmentid = Departments.depid";
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                db.Open();
                return (await db.QueryAsync<Employee,Passport,Department,Employee>(selectQuery, (e,p,d) =>
                {
                    e.Passport= p;
                    e.Department= d;
                    return e;
                }, splitOn: "passid, depid")).AsQueryable();
            }
        }

        public async Task<IQueryable<Employee>> GetAll(Func<Employee,bool> predicate)
        {
            var selectQuery = @"SELECT * FROM Employees
                                        LEFT OUTER JOIN Passports ON Employees.passportid = Passports.passid
                                        LEFT OUTER JOIN Departments ON Employees.departmentid = Departments.depid";
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                db.Open();
                return (await db.QueryAsync<Employee, Passport, Department, Employee>(selectQuery, (e, p, d) =>
                {
                    e.Passport = p;
                    e.Department = d;
                    return e;
                }, splitOn: "passid, depid")).Where(predicate).AsQueryable();
            }
        }

        public async Task Update(int id, Employee entity)
        {
            var selectQuery = $"UPDATE Employees SET {GetChangedParams(entity)} WHERE id = @id";
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                db.Open();
                await db.QueryAsync<Employee>(selectQuery, new { id });
            }
        }

        private string GetChangedParams(Employee entity)
        {
            List<string> sb = new List<string>();

            if(entity.Name != null) sb.Add($"name = '{entity.Name}' ");
            if(entity.Surname != null) sb.Add($"surname = '{entity.Surname}' ");
            if(entity.Phone != null) sb.Add($"phone = '{entity.Phone}'");
            if(entity.CompanyId != null) sb.Add($"companyid = {entity.CompanyId} ");
            if(entity.PassportId != null) sb.Add($"passportid = {entity.PassportId} ");
            if(entity.DepartmentId != null) sb.Add($"departmentid = {entity.DepartmentId} ");

            return String.Join(",", sb.ToArray());
        }
    }
}

    



