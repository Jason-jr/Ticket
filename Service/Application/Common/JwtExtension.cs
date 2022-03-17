using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Application.Common
{
    public static class JwtExtension
    {
        const int Admin = 4;
        public static IServiceCollection AddCustomJwtAuthentication(this IServiceCollection services)
        {
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.IncludeErrorDetails = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "ticket-tracking-system",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ticket-tracking-system")),
                        ValidateLifetime = true
                    };
                });

            services.AddAuthorization(options =>
            {
                // options.DefaultPolicy = new AuthorizationPolicyBuilder().Build();
                options.AddPolicy("admin", p => p.RequireAssertion(context => context.User.HasClaim(c => c.Type == ClaimTypes.Role && 
                    JsonSerializer.Deserialize<int[]>(c.Value).Contains(Admin))));
            });

            return services;
        }

        public static string Token(int userId, int[] roleIds = null)
        {
            var credential = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("ticket-tracking-system")),
                SecurityAlgorithms.HmacSha256Signature, SecurityAlgorithms.Sha256Digest);

            // declare identity
            var claims = new[]
            {
                new Claim(ClaimTypes.Sid, userId.ToString()),
                new Claim(ClaimTypes.Role, JsonSerializer.Serialize(roleIds))
            };

            // token content
            var token = new JwtSecurityToken(
                issuer: "ticket-tracking-system",
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credential
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}