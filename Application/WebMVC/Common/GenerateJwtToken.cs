using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebMVC.Common
{
    public class GenerateJwtToken
    {
        private readonly IConfiguration _configuration;

        public GenerateJwtToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtTokenUser(string userId, string username, string email, string role)
        {
            // Lấy thông tin từ appsettings.json
            var secretKey = _configuration["JwtSettings:SecretKey"];
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];
            var expireInDays = int.Parse(_configuration["JwtSettings:ExpireInDays"]);

            // Tạo các claims (dữ liệu bạn muốn truyền trong token)
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // JWT ID, unique ID
        };

            // Khóa bí mật (SecretKey) dùng để ký token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Tạo đối tượng JWT Token
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddDays(expireInDays), // Token hết hạn sau 1 ngày
                signingCredentials: creds
            );

            // Trả về token dưới dạng chuỗi
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
