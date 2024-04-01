using Library.Entities;
using MessageAPIViewModel.Authentication;
using MessageServices.HasingService;
using Microsoft.EntityFrameworkCore;

namespace MessageServices.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        //fields const
        private readonly MessageEncryptContext _context;
        private readonly IHashingService _hashing;
        private readonly string secretKey = "hkj54hk32j5hkj3245gh#$@%#@$5lk34hjl25kjl3k4j25";
        //constructor with params
        public AuthenticationService(MessageEncryptContext context, IHashingService hashing)
        {
            _context = context;
            _hashing = hashing;
        }

        public async Task<string> CreateAccountAsync(SignUpAPIViewModel accInfo)
        {
            //Check pass and repass
            if (accInfo.Password == accInfo.RePassword)
            {
                //Check duplicate
                Account existedAcc = await _context.Accounts.Where(t => t.UserName == accInfo.Username).FirstOrDefaultAsync();
                if (existedAcc == null)
                {
                    //Id
                    string id = Guid.NewGuid().ToString();
                    //hashed password
                    string hashedPassword = _hashing.MD5(accInfo.Password + secretKey);
                    //create new account
                    Account newAccount = new Account()
                    {
                        AccountId = id,
                        Password = hashedPassword,
                        PassToConnect = null,
                        UserName = accInfo.Username
                    };
                    await _context.Accounts.AddAsync(newAccount);
                    await _context.SaveChangesAsync();
                    return "Create New account  " + newAccount.UserName + " Successful!!";
                }
                else return "Username account is already exist!!!";
            }
            else return "Password and Repassword not match";


        }
    }
}

