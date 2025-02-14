using Kanboom.Repositories.Interfaces;
using Kanboom.Services.Interfaces;
using Kanboom.Models.AuthLogin.DTO;
using Kanboom.Models.Database;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Kanboom.Models.PersistUser.DTO;
namespace Kanboom.Services;
 
public class AuthService : IAuthService {
    private readonly IAuthRepository _repository;
    private readonly IConfiguration _configuration;

    public AuthService(IAuthRepository repository, IConfiguration configuration){
        _repository = repository;
        _configuration = configuration;
    }

    public string GenerateJwtToken(User user)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(3),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<AuthLoginResponseDTO> CheckLogin(AuthLoginRequestDTO request){
        var response = new AuthLoginResponseDTO();
        try {
            var data = await _repository.GetUser(request);

            if (data == null || !request.Password.Equals(data.Password)){
                response.IsSuccessful = false;
                response.Message = "NO_MATCH";
                return response;
            }

            var token = GenerateJwtToken(data);

            response.IsSuccessful = true;
            response.Token = token;
            return response;
        }
        catch(Exception ex){
            response.IsSuccessful = false;
            response.Message = ex.Message;
            return response;
        }
    }

    public PersistUserResponseDTO ValidateToken(PersistUserRequestDTO token)
    {
        var response = new  PersistUserResponseDTO();
        var jwtSettings = _configuration.GetSection("Jwt");
        var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);        
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            var principal = tokenHandler.ValidateToken(token.Token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidateAudience = true,
                ValidAudience = jwtSettings["Audience"],
                ValidateLifetime = true
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var username = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            
            response.IsSuccessful = true;
            response.Message = "VALIDATE_TOKEN";
            response.Username = username;
            return response;
        }
        catch(Exception ex)
        {
            response.IsSuccessful = false;
            response.Message = ex.Message;
            response.Username = null;
            return response;
        }
    }

}