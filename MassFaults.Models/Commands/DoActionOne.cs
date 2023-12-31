namespace MassFaults.Models.Commands
{
    public class DoActionOne : IActionCommand
    {
        public Guid ConfigurationId { get; set; }
        public DemoCase DemoCase { get; set; }
    }
}
