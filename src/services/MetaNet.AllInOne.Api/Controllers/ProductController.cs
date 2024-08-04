using Esterdigi.Api.Core.Controller;
using Esterdigi.Api.Core.Response;
using MetaNet.Microservices.Domain.Http.Request;
using MetaNet.Microservices.Domain.Http.Response;
using MetaNet.Microservices.Service;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MetaNet.AllInOne.Api.Controllers
{
    [Route("product")]
    public class ProductController : BaseController
    {
        private readonly ProductService _service;

        public ProductController(ProductService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retorna a lista dos registros da tabela de produtos
        /// </summary>
        /// <response code="200">Registros que foram retornado com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condição ou um algum erro interno.</response>
        [HttpGet, Route("get-all")]
        [ProducesResponseType(typeof(BaseResponse<IEnumerable<ProductResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.Handle();
            return await Response(data, _service.Notifications);

        }

        /// <summary>
        /// Retorna a lista dos registros da tabela de produtos
        /// </summary>
        /// <response code="200">Registros que foram retornado com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condição ou um algum erro interno.</response>
        [HttpGet, Route("get-all-dapper")]
        [ProducesResponseType(typeof(BaseResponse<IEnumerable<ProductResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> GetAllFromDapper()
        {
            var data = await _service.HandleDapper();
            return await Response(data, _service.Notifications);

        }

        /// <summary>
        /// Retorna o registro da tabela produto filtrado pelo id
        /// </summary>
        /// <response code="200">Registro que foi retornado com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condição ou um algum erro interno.</response>
        [HttpGet, Route("get")]
        [ProducesResponseType(typeof(BaseResponse<ProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> Get([Required] Guid id)
        {
            var data = await _service.Handle(id);
            return await Response(data, _service.Notifications);
        }

        /// <summary>
        /// Retorna o registro da tabela produto filtrado pelo código de barras
        /// </summary>
        /// <response code="200">Registro que foi retornado com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condição ou um algum erro interno.</response>
        [HttpGet, Route("get-by-barcode")]
        [ProducesResponseType(typeof(BaseResponse<ProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> GetBarcode([Required] string barcode)
        {
            var data = await _service.Handle(barcode);
            return await Response(data, _service.Notifications);
        }

        /// <summary>
        /// Insere um registro na tabela produtos
        /// </summary>
        /// <response code="200">Registro que foi inserido com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condição ou um algum erro interno.</response>
        [HttpPost, Route("add")]
        [ProducesResponseType(typeof(BaseResponse<ProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> Post([FromBody] ProductRegisterRequest request)
        {
            var data = await _service.Handle(request);
            return await Response(data, _service.Notifications);
        }

        /// <summary>
        /// Altera um registro da tabela produto
        /// </summary>
        /// <response code="200">Registro que foi alterado com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condição ou um algum erro interno.</response>
        [HttpPut, Route("update")]
        [ProducesResponseType(typeof(BaseResponse<ProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> Put([FromBody] ProductUpdateRequest request)
        {
            var data = await _service.Handle(request);
            return await Response(data, _service.Notifications);
        }

        /// <summary>
        /// Deleta um registro da tabela produto
        /// </summary>
        /// <response code="200">Registro que foi deletado com sucesso.</response>
        /// <response code="412">Ocorreu uma falha de pre-condição ou um algum erro interno.</response>
        [HttpDelete, Route("delete")]
        [ProducesResponseType(typeof(BaseResponse<ProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status412PreconditionFailed)]
        public async Task<IActionResult> Delete([FromQuery, Required] Guid id)
        {
            var data = await _service.Delete(id);
            return await Response(data, _service.Notifications);
        }

    }
}