namespace MassFaults.Models.Commands
{
    public class DoActionThree : IActionCommand
    {
        public Guid ConfigurationId { get; set; }
        public DemoCase DemoCase { get; set; }
    }
}
