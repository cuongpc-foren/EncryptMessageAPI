using MessageAPIViewModel.Authentication;
using MessageServices.HasingService;

namespace MessageServices.Authorization
{
    public class TokenService : ITokenService
    {

        private readonly IHashingService _hashing;
        private readonly string secretKey = "M@#L$ML@<$<%$@:L<M%:LM@V$V @$V%K@:$K%@%";

        public TokenService(IHashingService hash)
        {

            _hashing = hash;
        }

        public async Task<string> GenerateTokenValue(LoginAPIViewModel loginInfo)

        {
            DateTimeOffset now = DateTimeOffset.Now;
            string randomId = now.Ticks.ToString();
            string tokenString = loginInfo.Username + randomId + secretKey;
            return _hashing.MD5(tokenString);
        }

    }
}
