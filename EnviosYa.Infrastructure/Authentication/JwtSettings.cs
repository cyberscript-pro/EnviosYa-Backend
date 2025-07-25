namespace EnviosYa.Infrastructure.Authentication;

public class JwtSettings
{
    public required string SecretKey { get; set; }
    public string Issuer { get; set; } = "EnviosYa";
    public string Audience { get; set; } = "EnviosYaBackend";
    public int ExpirationHours { get; set; } = 1;
}