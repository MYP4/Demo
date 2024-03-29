namespace EventPad.Settings;

public class EmailSettings
{
    public string FromName {get; private set;}
    public string FromEmail { get; private set;}
    public string Server { get; private set;}
    public int Port { get; private set;}
    public string Login {  get; private set;}
    public string Password { get; private set;}
    public bool Ssl { get; private set;}
}
