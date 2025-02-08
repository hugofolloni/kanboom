using Kanboom.Repositories.Interfaces;
using Kanboom.Services.Interfaces;
using Kanboom.Models.AuthUser.DTO;
using Kanboom.Models.Database;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Kanboom.Services {
    public class AuthService : IAuthService {
        private readonly IAuthRepository _repository;
        private readonly IConfiguration _configuration;

        public AuthService(IAuthRepository repository, IConfiguration configuration){
            _repository = repository;
            _configuration = configuration;
        }

            private string GenerateJwtToken(User user)
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
        public async Task<AuthUserResponseDTO> CheckLogin(AuthUserRequestDTO request){
            var response = new AuthUserResponseDTO();

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

    }
}