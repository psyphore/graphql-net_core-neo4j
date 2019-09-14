namespace Models.DTOs.Configuration
{
    public class Auth0
    {
        public string ClientId { get; set; }
        public string Domain { get; set; }
        public int Leeway { get; set; }
        public string RedirectUri { get; set; }
        public string Secret { get; set; }
    }
}