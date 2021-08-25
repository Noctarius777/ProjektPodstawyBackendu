using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjektPodstawyBackendu.Domain;
using System.Threading.Tasks;

namespace ProjektPodstawyBackendu
{
    [Route("account/")]
    public class AccountController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager; // przypisujemy do prywatnej zmiennej
        private readonly SignInManager<ApplicationUser> _signInManager; // przypisujemy do prywatnej zmiennej
        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager) //wołamy o UserManager i SigInManager  / na niebieskim ctr . zeby zinicjalizowac zmienne, ponizej
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
         [Route("getCurrentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _userManager.GetUserAsync(User);  // funkcja zraca aktualnie zalogowanego usera, przypusjemy go do user
            if (user == null)
            {
                return Unauthorized();
            }
            return Ok (user);

        }

        [HttpPost]
        [Route("register")]
        public async Task <IActionResult> Register([FromBody] UserRegisterDto userRegisterDto) // przyjmiemy z Jasona, klasę UserRegsterDto
        {
            var newUser = new ApplicationUser
            {

                Email = userRegisterDto.Email,
                UserName = userRegisterDto.Email,
                FirstName = userRegisterDto.FirstName,
                LastName = userRegisterDto.LastName,
               
            };
           var result =  await _userManager.CreateAsync(newUser, userRegisterDto.Password);
            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

                await _userManager.ConfirmEmailAsync(newUser, token); // potwierdzamy sobie tokena
                return Ok(); // a dane register wezmie z GetCurrentUser
            }
            return NotFound();

        }
            
        [HttpPost]
        [Route("login")]
        public async Task <IActionResult> Login([FromBody] UserLoginDto userLoginDto) // przyjmiemy z Jasona, klasę UserRegsterDto
        {
            var foundUser = await _userManager.FindByEmailAsync(userLoginDto.Email);
            if (foundUser == null)
            {
                return NotFound();
            }
           var result = await  _signInManager.PasswordSignInAsync(foundUser, userLoginDto.Password, true, false);
            if (result.Succeeded)
            {
                return Ok();
            }

            return NotFound();

        }
    }
}
