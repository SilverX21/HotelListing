using AutoMapper;
using HotelListing.Data;
using HotelListing.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;

        public AccountController(UserManager<ApiUser> userManager, ILogger<AccountController> logger, IMapper mapper)
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
        }

        //FromBody -> diz que os dados têm de vir no body do pedido
        [HttpPost]
        [Route("register")] //a route para aqui vai ser account/registe
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            _logger.LogInformation($"Registration attempt for {userDTO.Email}");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var user = _mapper.Map<ApiUser>(userDTO);
                user.UserName = userDTO.Email;
                var result = await _userManager.CreateAsync(user, userDTO.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }

                    return BadRequest(ModelState);
                }

                await _userManager.AddToRolesAsync(user, userDTO.Roles);
                return Accepted(); //o accepted informa que foi aceite o pedido
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went worng in the {nameof(Register)}");
                //em baixo retornamos um problema, com o status 500
                return Problem($"Something went worng in the {nameof(Register)}", statusCode: 500);
            }
        }

        //[HttpPost]
        //[Route("login")]
        //public async Task<IActionResult> Login([FromBody] LoginUserDTO userDTO)
        //{
        //    _logger.LogInformation($"Login attempt for {userDTO.Email}");
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    try
        //    {
        //        var result = await _signInManager.PasswordSignInAsync(userDTO.Email, userDTO.Password, false, false);

        //        if (!result.Succeeded)
        //            return Unauthorized(userDTO);

        //        return Accepted();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, $"Something went worng in the {nameof(Login)}");
        //        //em baixo retornamos um problema, com o status 500
        //        return Problem($"Something went worng in the {nameof(Login)}", statusCode: 500);
        //    }
        //}
    }
}
