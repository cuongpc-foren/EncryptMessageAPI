using MessageAPIViewModel.Authentication;

namespace MessageServices.Authentication
{
    public interface ILoginService
    {
        Task<LoginResponseAPIViewModel> CheckLogin(LoginAPIViewModel loginData);
    }
}
