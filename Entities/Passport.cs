using SmartWay_Test_Task.Interfaces;

namespace SmartWay_Test_Task.Entities
{
    public class Passport : IEntity
    {
        public int Id { get; init; }
        public string Type { get; set; }
        public string Number { get; set; }
    }
}
