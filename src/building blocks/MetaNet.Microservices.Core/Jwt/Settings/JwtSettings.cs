namespace MetaNet.Microservices.Core.Jwt.Settings
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int TimeValidationToken { get; set; }
    }
}
