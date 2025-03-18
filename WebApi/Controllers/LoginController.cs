using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.Application.CustomerOperations.Commands.CreateToken;
using WebApi.Application.CustomerOperations.Commands.RefreshToken;
using WebApi.Applications.CustomerOperations.Commands.CreateCustomer;
using WebApi.DBOperations;
using WebApi.TokenOperations.Model;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[Controller]s")]
    public class LoginController: ControllerBase
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public LoginController(IMapper mapper, IMovieStoreDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }    

        [HttpPost("connect/token")]
        public ActionResult<Token> CreateToken([FromBody] CreateTokenModel login)
        {
            CreateTokenCommand command = new CreateTokenCommand(_context, _configuration);
            command.Model = login;

            CreateTokenCommandValidator validator = new CreateTokenCommandValidator();
            validator.ValidateAndThrow(command);

            var token = command.Handle();
            return token;
        }
        
        [HttpGet("refreshToken")]
        public ActionResult<Token> RefreshToken([FromQuery] string token)
        {
            RefreshTokenCommand command = new RefreshTokenCommand(_context, _configuration);
            command.refreshToken = token;

            RefreshTokenCommandValidator validator = new RefreshTokenCommandValidator();
            validator.ValidateAndThrow(command);

            var resultToken = command.Handle();
            return resultToken;
        }

        [HttpPost("sign-up")]
        public IActionResult SignUp([FromBody] CreateCustomerModel newCustomer)
        {
            CreateCustomerCommand command = new CreateCustomerCommand(_context, _mapper);
            command.Model = newCustomer;

            CreateCustomerCommandValidator validator = new CreateCustomerCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        } 
    }
}