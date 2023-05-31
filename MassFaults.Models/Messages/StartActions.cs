namespace MassFaults.Models.Messages
{
  public class StartActions
  {
    public Guid ConfigurationId { get; set; }
    public DemoCase DemoCase { get; set; }
  }
}
