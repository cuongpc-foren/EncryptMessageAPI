using MessageAPIViewModel.Authentication;
using MessageServices.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace MessageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        //fields
        private readonly IAuthenticationService _authentication;
        private readonly ILoginService _loginService;
        //constructor with params
        public AuthenticationController(IAuthenticationService auth, ILoginService login)
        {
            _authentication = auth;
            _loginService = login;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpAPIViewModel accInfo)
        {
            if (ModelState.IsValid)
            {
                string result = await _authentication.CreateAccountAsync(accInfo);
                if (result.Contains("Successful"))
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                               .Where(y => y.Count > 0)
                               .ToList();
                return BadRequest(errors);
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginAPIViewModel loginData)
        {
            if (ModelState.IsValid)
            {
                LoginResponseAPIViewModel result = await _loginService.CheckLogin(loginData);
                if (result.StatusCode == 200)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                               .Where(y => y.Count > 0)
                               .ToList();
                return BadRequest(errors);
            }
        }
    }
}
