namespace MessageServices.HasingService
{
    public interface IHashingService
    {
        string MD5(string input);
        string SHA256(string input);
    }
}
