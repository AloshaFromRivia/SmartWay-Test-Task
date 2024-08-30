using SmartWay_Test_Task.Interfaces;

namespace SmartWay_Test_Task.Entities
{
    public class Department : IEntity
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public string Phone { get; set; }
    }
}
