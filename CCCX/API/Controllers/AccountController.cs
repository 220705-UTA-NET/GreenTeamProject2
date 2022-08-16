using System.Security.Claims;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController // Extends from BaseApiController
    {
        private readonly UserManager<AppUser> _userManager; // UserManager is a class that manages users in the system
        private readonly SignInManager<AppUser> _signInManager; // SignInManager is a class that manages sign in
        private readonly ITokenService _tokenService; // TokenService is a class that manages tokens
        private readonly IMapper _mapper; // Mapper is a class that maps one object to another object
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, // Constructor
            ITokenService tokenService, IMapper mapper) 
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [Authorize] // Authorize is a class that authorizes a user
        [HttpGet] // Get is a method that gets data from the server
        public async Task<ActionResult<UserDto>> GetCurrentUser() // UserDto gets several pieces of information from User.
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(User); // FindByEmailFromClaimsPrinciple is a method that finds a user by email

            return new UserDto 
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user), // CreateToken is a method that creates a token
                DisplayName = user.DisplayName
            };
        }

        [HttpGet("emailexists")] 
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email) //Bool to check if email exist
        {
            return await _userManager.FindByEmailAsync(email) != null; //if email exist returns user info
        }

        [Authorize] 
        [HttpGet("address")] 
        public async Task<ActionResult<AddressDto>> GetUserAddress() //gets user address from database
        {
            var user = await _userManager.FindByEmailWithAddressAsync(User); //waits for database to return user info

            return _mapper.Map<AddressDto>(user.Address); //returns user address
        }

        [Authorize]
        [HttpPut("address")] //PUT is a method that updates data on the server
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address) //
        {
            var user = await _userManager.FindByEmailWithAddressAsync(User); //FindByEmailWithAddressAsync is a method that finds a user by email
            user.Address = _mapper.Map<Address>(address); //Map is a method that maps one object to another object
            var result = await _userManager.UpdateAsync(user); //UpdateAsync is a method that updates a user
            if (result.Succeeded) //if result is successful
            {
                return _mapper.Map<AddressDto>(user.Address); //returns user address
            }
            return BadRequest("Problem updating the user"); //if result is not successful returns bad request
          
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto) // task that uses user data to be able to login to account.
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email); //FindByEmailAsync is a method that finds a user by email

            if (user == null) return Unauthorized(new ApiResponse(401));//if user is not found return unauthorized

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false); //CheckPasswordSignInAsync checks to see if the user is able to login with the password

            if (!result.Succeeded) return Unauthorized(new ApiResponse(401)); //if unable to log in with email and password return unauthorized

            return new UserDto //if able to log in return user data
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user), //CreateToken is a method that creates a token
                DisplayName = user.DisplayName
            };
           
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto) // task that takes new user data to be able to create an account
        {
            if (CheckEmailExistsAsync(registerDto.Email).Result.Value) //checks to see if email already exists
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse{Errors = new[] {"Email address is in use"}}); //if email already exists return bad request
            }

            var user = new AppUser //creates new user using registerDto
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email
            };
           
            var result = await _userManager.CreateAsync(user, registerDto.Password); // creates password for user ????

            if (!result.Succeeded) return BadRequest(new ApiResponse(400)); //if unable to create user return bad request

            return new UserDto // return user data if successful
            {
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user),
                Email = user.Email
            };
        }
    }
}
