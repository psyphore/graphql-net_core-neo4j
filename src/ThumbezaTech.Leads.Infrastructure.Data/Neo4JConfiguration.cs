namespace ThumbezaTech.Leads.Infrastructure.Data;

public sealed record Neo4JConfiguration
{
    public string BoltURL { get; set; } = "";
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public string DatabaseName { get; set; } = "";
}
