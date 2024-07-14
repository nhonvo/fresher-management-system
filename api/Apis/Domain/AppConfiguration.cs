namespace Domain;

#pragma warning disable
public class AppConfiguration
{
    public ConnectionStrings ConnectionStrings { get; set; }
    public Jwt Jwt { get; set; }
    public string JWTSecretKey { get; set; }
    public MyAllowSpecificOrigins MyAllowSpecificOrigins { get; set; }
    public MailConfigurations MailConfigurations { get; set; }
    public string LoggingPath { get; set; }
    public string LoggingTemplate { get; set; }
    public string HtmlTemplatePath { get; init; }
}
public class MailConfigurations
{
    public string DisplayName { get; set; }
    public string From { get; set; }
    public string Host { get; set; }
    public string Password { get; set; }
    public int Port { get; set; }
    public string UserName { get; set; }
    public bool UseSSL { get; set; }
    public bool UseStartTls { get; set; }
}

public class MyAllowSpecificOrigins
{
    public string UserApp { get; set; }
    public string UserAppDev { get; set; }
}
public class ConnectionStrings
{
    public string DatabaseConnection { get; set; }
    public string DatabaseDeploy { get; set; }
    public string HangfireConnection { get; set; }
}
public class Jwt
{
    public string Key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}

