namespace MassFaults.Models.Commands
{
    public interface IActionCommand
    {
        public Guid ConfigurationId { get; set; }
        public DemoCase DemoCase { get; set; }
    }
}
