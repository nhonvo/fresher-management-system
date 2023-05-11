namespace Domain;

#pragma warning disable
public class AppConfiguration
{
    public string LoggingPath { get; set; }
    public ConnectionStrings ConnectionStrings { get; set; }
    public Jwt Jwt { get; set; }
    public BaseUrl BaseUrl { get; set; }
    public string JWTSecretKey { get; set; }
    public MyAllowSpecificOrigins MyAllowSpecificOrigins { get; set; }
    public Token Token { get; set; }
}
// TODO: UPDATE this class remove unnecessary properties
public class Token
{
    public string TenantId { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string AccessToken { get; set; }
}

public class MyAllowSpecificOrigins
{
    public string UserApp { get; set; }
}
public class ConnectionStrings
{
    public string DatabaseConnection { get; set; }
    public string DatabaseConnectionV1 { get; set; }
    public string DatabaseConnectionV2 { get; set; }
    public string DatabaseConnectionV3 { get; set; }
    public string HangfireConnection { get; set; }
}
public class Jwt
{
    public string Key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}
public class BaseUrl
{
    public string Outlook { get; set; }
}
