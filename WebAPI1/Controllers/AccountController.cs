using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI1.DTO;
using WebAPI1.Models;

namespace WebAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration config;
        public AccountController(UserManager <ApplicationUser> userManager,IConfiguration config) 
        {
            this.userManager= userManager;
            this.config= config;
        }
        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterUserDto userDto)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = userDto.UserName;
                user.Email = userDto.Email;
               IdentityResult result= await userManager.CreateAsync(user, userDto.Password);
                if (result.Succeeded) 
                {
                    return Ok("account add success");
                }
                else
                {
                    return BadRequest(result.Errors.FirstOrDefault());
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("Login")]
        public async  Task <ActionResult> Login(LoginUserDto userDto)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByNameAsync(userDto.Name);
                if (user != null)
                {
                    bool Found= await userManager.CheckPasswordAsync(user,userDto.Password);
                    if (Found) 
                    {
                        //creat claims
                        var claims = new List<Claim>();
                        claims.Add(new Claim (ClaimTypes.Name,user.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()));

                        //get role
                        var roles = await userManager.GetRolesAsync(user);
                        foreach (var item in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, item));
                        }

                        //credentials 
                        SecurityKey securityKey =
                             new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Secret"]));

                        SigningCredentials signincred =
                            new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                        //creat token
                        JwtSecurityToken mytoken = new JwtSecurityToken(
                            issuer: config["JWT:ValidIssuer"],
                            audience: config["JWT:ValidAudiance"],
                            claims:claims,
                            expires: DateTime.Now.AddHours(1),
                            signingCredentials:signincred
                            );
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                            expiration =mytoken.ValidTo
                        });
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
            }
            
                return Unauthorized();

        }
    }
}
