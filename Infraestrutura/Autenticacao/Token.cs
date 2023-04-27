using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using SistemaCadastroEleitoral.Models.Admin;

namespace SistemaCadastroEleitoral.Infraestrutura.Autenticacao
{
    public class Token
    {public static string GerarToken(AdminModel adm)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			JToken jAppSettings = JToken.Parse(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "appsettings.json")));
			var key = Encoding.ASCII.GetBytes(jAppSettings["ConnectionStrings"]["JwtToken"].ToString());
			var expirationTime = Convert.ToInt32(jAppSettings["ConnectionStrings"]["ExpirationTime"]);
            var tokenDescriptor = new SecurityTokenDescriptor()
			{
				Subject = new ClaimsIdentity(new Claim[]{
						new Claim(ClaimTypes.Name, adm.NomeAdmin),
						new Claim(ClaimTypes.Role, adm.Acesso),
					}),
				Expires = DateTime.UtcNow.AddHours(expirationTime),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
    }
}