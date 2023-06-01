namespace MassFaults.Models.Commands
{
    public class ReleaseConfiguration
    {
        public Guid ConfigurationId { get; set; }
        public DemoCase DemoCase { get; set; }
    }
}
