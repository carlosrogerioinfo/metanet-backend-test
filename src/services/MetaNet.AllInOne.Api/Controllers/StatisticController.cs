using Esterdigi.Api.Core.Controller;
using Esterdigi.Api.Core.Response;
using MetaNet.Microservices.Domain.Http.Response;
using MetaNet.Microservices.Service;
using Microsoft.AspNetCore.Mvc;

namespace MetaNet.AllInOne.Api.Controllers
{
    [Route("statistic")]
    public class StatisticController : BaseController
    {
        private readonly StatisticService _service;

        public StatisticController(StatisticService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retorna estatísticas da aplicação
        /// </summary>
        /// <response code="200">Registro que foi retornado com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condição ou um algum erro interno.</response>
        [HttpGet, Route("get")]
        [ProducesResponseType(typeof(BaseResponse<StatisticResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> Get()
        {
            var data = await _service.Handle();
            return await Response(data, _service.Notifications);
        }

        

    }
}