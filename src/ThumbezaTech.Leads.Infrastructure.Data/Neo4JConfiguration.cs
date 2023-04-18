namespace ThumbezaTech.Leads.Infrastructure.Data;

internal sealed record Neo4JConfiguration
{
    public string BoltURL { get; set; } = "";
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public string DatabaseName { get; set; } = "";
}
