namespace Veeb_TARpv23.Models
{
    public class Device
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public DateTime NextMaintenanceTime { get; set; }
        public int RedisualValue { get; set; }
        public int AcquisitionCost { get; set; }
        public bool IsActive { get; set; }
    }
}
