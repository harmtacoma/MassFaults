﻿namespace MassFaults.Models.Events
{
  public class ConfigurationSubmitted
  {
    public Guid ConfigurationId { get; set; }
    public DemoCase DemoCase { get; set; }
  }
}
