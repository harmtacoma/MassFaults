namespace MassFaults.Models.Commands
{
    public class StartActions
    {
        public Guid ConfigurationId { get; set; }
        public DemoCase DemoCase { get; set; }
    }
}
