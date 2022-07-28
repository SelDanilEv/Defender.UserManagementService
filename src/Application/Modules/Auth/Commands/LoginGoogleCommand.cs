using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Defender.UserManagement.Application.Common.Interfaces;
using Defender.UserManagement.Application.DTOs;
using Defender.UserManagement.Application.Enums;
using Defender.UserManagement.Application.Helpers;
using Defender.UserManagement.Application.Models.LoginResponse;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Defender.UserManagement.Application.Modules.Auth.Commands;

public record LoginGoogleCommand : IRequest<LoginResponse>
{
    public string? Token { get; set; }
};

public sealed class LoginGoogleCommandValidator : AbstractValidator<LoginGoogleCommand>
{
    public LoginGoogleCommandValidator()
    {
        RuleFor(x => x.Token).NotEmpty().WithMessage("No token!");
    }
}

public sealed class LoginGoogleCommandHandler : IRequestHandler<LoginGoogleCommand, LoginResponse>
{
    private readonly IAuthService _authService;
    private readonly IGoogleTokenValidationService _googleTokenValidationService;
    private readonly IConfiguration _configuration;
    protected readonly IMapper _mapper;

    public LoginGoogleCommandHandler(
        IAuthService authService,
        IGoogleTokenValidationService googleTokenValidationService,
        IConfiguration configuration,
        IMapper mapper
        )
    {
        _authService = authService;
        _googleTokenValidationService = googleTokenValidationService;
        _configuration = configuration;
        _mapper = mapper;
    }

    public async Task<LoginResponse> Handle(LoginGoogleCommand request, CancellationToken cancellationToken)
    {
        var response = new LoginResponse();

        try
        {
            var googleUser = await _googleTokenValidationService.GetTokenInfoAsync(request.Token);
            var user = await _authService.Authenticate(googleUser);

            response.UserDetails = _mapper.Map<UserDto>(user);

            var claims = new List<Claim>
            {
                    new Claim(
                        ClaimTypes.DateOfBirth,
                        DateTime.Now.ToString()),

                    new Claim(
                        ClaimTypes.NameIdentifier,
                        user.Id.ToString()),

                    new Claim(
                        ClaimTypes.Email,
                        user.Email),

                    new Claim(
                        ClaimTypes.Name,
                        user.Name)
                };

            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(EnvVariableResolver.GetEnvironmentVariable(EnvVariable.JwtSecret)));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              _configuration["JwtTokenIssuer"],
              null,
              claims,
              expires: DateTime.Now.AddSeconds(60 * 60),
              signingCredentials: creds);

            response.Token = new JwtSecurityTokenHandler().WriteToken(token);
            response.Authorized = true;
        }
        catch (Exception ex)
        {
            response.Authorized = false;
            response.UserDetails = null;
            response.Token = String.Empty;

            SimpleLogger.Log(ex);
        }

        return response;
    }
}
