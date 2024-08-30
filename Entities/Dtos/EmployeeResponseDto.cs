namespace SmartWay_Test_Task.Entities.Dtos
{
    public record EmployeeResponseDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Phone { get; set; }
        public int? CompanyId { get; set; }
        public PassportDto Passport { get; set; }
        public DepartmentDto Department { get; set; }
    }
}
