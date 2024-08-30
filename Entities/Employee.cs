using SmartWay_Test_Task.Interfaces;

namespace SmartWay_Test_Task.Entities
{
    public class Employee : IEntity
    {
        public int Id { get; init; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Phone { get; set; }
        public int? CompanyId { get; set; }
        public int? PassportId { get; set; }
        public Passport? Passport { get; set; }  
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}
