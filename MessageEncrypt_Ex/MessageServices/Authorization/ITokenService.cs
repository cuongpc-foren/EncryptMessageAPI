using MessageAPIViewModel.Authentication;

namespace MessageServices.Authorization
{
    public interface ITokenService
    {
        Task<string> GenerateTokenValue(LoginAPIViewModel loginInfo);
    }
}
