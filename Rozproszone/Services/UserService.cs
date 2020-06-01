using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Rozproszone.Models;
using Rozproszone.Utilities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Rozproszone.Services
{


    public class UserService : IUserService
    {

        private readonly AppSettings _appSettings;
        protected readonly CourseContext _context;

        public UserService(IOptions<AppSettings> appSettings, CourseContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        private string generateToken(string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userId)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public User Authenticate(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == username && x.Password == password);

            if (user == null)
                return null;

            user.Token = generateToken(user.Id.ToString());
            user.Password = null;

            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.AsEnumerable();
        }
    }
}
