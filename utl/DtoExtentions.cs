using SmartWay_Test_Task.Entities;
using SmartWay_Test_Task.Entities.Dtos;

namespace SmartWay_Test_Task.utl
{
    public static class DtoExtentions
    {
        public static EmployeeResponseDto ToResponseDto(this Employee employee)
        {
            return new EmployeeResponseDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Surname = employee.Surname,
                Phone = employee.Phone,
                CompanyId = employee.CompanyId,
                Department = new DepartmentDto
                {
                    Name = employee.Department.Name,
                    Phone = employee.Department.Phone,
                },
                Passport = new PassportDto
                {
                    Number = employee.Passport.Number,
                    Type = employee.Passport.Type,
                }
            };
        }

        public static Employee ToEmployee(this EmployeeRequestDto dto)
        {
            return new Employee
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Phone = dto.Phone,
                CompanyId = dto.CompanyId,
                PassportId = dto.PassportId,
                DepartmentId = dto.DepartmentId,
            };
        }
    }
}
