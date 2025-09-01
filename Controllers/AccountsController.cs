using E_commerce.Core.Entities;
using E_Commerce.ApI.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.ApI.Controllers
{
    
    public class AccountsController : APIBaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountsController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        #region Register
        [HttpPost("Register")]
        public async Task <ActionResult<UserDto>> Register (RegisterDto model)
        {
            var User = new AppUser
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                PhoneNumber = model.PhoneNumber
            };
            var Result=await _userManager.CreateAsync(User,model.Password);
            if (!Result.Succeeded) return BadRequest();
            var ReturnedUser = new UserDto()
            {
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = "This Will Be Token"
            };
            return Ok(ReturnedUser);
            
        }
        #endregion
        #region Login
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto login)
        {
            var User=await _userManager.FindByEmailAsync(login.Email);
            if (User is null) return Unauthorized();
            var result=await _signInManager.CheckPasswordSignInAsync(User, login.Password,false);
            if (!result.Succeeded) return Unauthorized();
            return Ok(new UserDto()
            {
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = "This Will Be Token"

            });
               
        }
        #endregion

    }
}
