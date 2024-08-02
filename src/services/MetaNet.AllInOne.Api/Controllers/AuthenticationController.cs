using Esterdigi.Api.Core.Controller;
using Esterdigi.Api.Core.Response;
using MetaNet.Microservices.Core.Jwt;
using MetaNet.Microservices.Core.Jwt.Settings;
using MetaNet.Microservices.Domain.Http.Request;
using MetaNet.Microservices.Domain.Http.Response;
using MetaNet.Microservices.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MetaNet.AllInOne.Api.Controllers
{
    [Route("authentication")]
    public class AuthenticationController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IJwtService _jwt;
        private readonly UserService _service;
        private readonly JwtSettings _jwtSettings;

        public AuthenticationController(UserService service, IJwtService jwt, IConfiguration configuration)
        {
            _service = service;
            _jwt = jwt;
            _configuration = configuration;
            _jwtSettings = _configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
        }

        /// <summary>
        /// Autenticação do usuário
        /// </summary>
        /// <response code="200">Registros que foram retornado com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condição ou um algum erro interno.</response>
        [HttpPost, Route("login"), AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<AuthenticationResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> Authentication([FromBody] AuthenticationRequest request)
        {
            var data = await _service.Handle(request);

            if (data is not null) return await Response(new AuthenticationResponse { Token = _jwt.GenerateToken(data, _jwtSettings) }, _service.Notifications);

            return await Response(data, _service.Notifications);

        }


    }
}