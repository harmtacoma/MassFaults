namespace MassFaults.Models.Commands
{
    public class ImportConfiguration
    {
        public Guid ConfigurationId { get; set; }
        public DemoCase DemoCase { get; set; }
    }
}
