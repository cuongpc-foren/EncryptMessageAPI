using Library.Entities;
using MessageAPIViewModel.Authentication;
using MessageServices.Authorization;
using MessageServices.HasingService;
using Microsoft.EntityFrameworkCore;


namespace MessageServices.Authentication
{
    public class LoginService : ILoginService
    {
        private readonly MessageEncryptContext _context;
        private readonly IHashingService _hashing;
        private readonly ITokenService _tokenService;
        private readonly string secretKey = "hkj54hk32j5hkj3245gh#$@%#@$5lk34hjl25kjl3k4j25";

        public LoginService(MessageEncryptContext context, IHashingService hashing, ITokenService token)
        {
            _context = context;
            _hashing = hashing;
            _tokenService = token;
        }

        public async Task<LoginResponseAPIViewModel> CheckLogin(LoginAPIViewModel loginData)
        {
            try
            {
                var user = await _context.Accounts.FirstOrDefaultAsync(q => q.UserName == loginData.Username);

                if (user != null)
                {
                    var hashedPassword = _hashing.MD5(loginData.Password + secretKey);
                    var isValidPassword = await _context.Accounts.AnyAsync(q => q.UserName == loginData.Username && q.Password == hashedPassword);
                    if (isValidPassword)
                    {
                        string savedToken = await _tokenService.GenerateTokenValue(loginData);
                        var existedToken = await _context.Tokens.Where(q => q.AccountId == user.AccountId).FirstOrDefaultAsync();
                        if (existedToken == null)
                        {

                            Token newToken = new Token()
                            {
                                TokenValue = savedToken,
                                AccountId = user.AccountId,
                                CreateTime = DateTime.UtcNow
                            };
                            await _context.Tokens.AddAsync(newToken);
                            await _context.SaveChangesAsync();
                            return new LoginResponseAPIViewModel()
                            {
                                StatusCode = 200,
                                Value = newToken.TokenValue
                            };
                        }
                        else
                        {
                            existedToken.TokenValue = savedToken;
                            existedToken.CreateTime = DateTime.UtcNow;
                            _context.Tokens.Update(existedToken);
                            await _context.SaveChangesAsync();
                            return new LoginResponseAPIViewModel()
                            {
                                StatusCode = 200,
                                Value = existedToken.TokenValue
                            };
                        }


                    }
                    else
                    {
                        return new LoginResponseAPIViewModel()
                        {
                            StatusCode = 404,
                            Value = "Invalid username or password!"
                        };
                    }
                }
                else
                {
                    return new LoginResponseAPIViewModel()
                    {
                        StatusCode = 404,
                        Value = "Invalid username or password!"
                    };
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return new LoginResponseAPIViewModel()
                {
                    StatusCode = 404,
                    Value = "Invalid username or password!"
                };
            }
        }
    }
}
