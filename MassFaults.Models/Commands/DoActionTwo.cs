namespace MassFaults.Models.Commands
{
    public class DoActionTwo : IActionCommand
    {
        public Guid ConfigurationId { get; set; }
        public DemoCase DemoCase { get; set; }
    }
}
