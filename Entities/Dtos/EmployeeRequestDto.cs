namespace SmartWay_Test_Task.Entities.Dtos
{
    public record EmployeeRequestDto
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Phone { get; set; }
        public int? CompanyId { get; set; }
        public int? PassportId { get; set; }
        public int? DepartmentId { get; set; }
    }
}
