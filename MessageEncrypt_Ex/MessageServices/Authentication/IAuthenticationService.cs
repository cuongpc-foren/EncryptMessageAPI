using MessageAPIViewModel.Authentication;

namespace MessageServices.Authentication
{
    public interface IAuthenticationService
    {
        Task<string> CreateAccountAsync(SignUpAPIViewModel accInfo);
    }
}
