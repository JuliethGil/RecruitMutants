using AutoMapper;
using BusinessLayer.Dtos;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RecruitMutants.Models;
using System;
using System.Net;
using System.Net.Mime;

namespace RecruitMutants.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MutantController : ControllerBase
    {
        /// <summary>
        /// The service
        /// </summary>
        private readonly IMutantLogic _service;

        public MutantController(IMutantLogic service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        /// <summary>
        /// Posts the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>IActionResult.</returns>
        /// <remarks>Julieth Gil</remarks>
        [HttpPost]
        [DisableRequestSizeLimit]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [Produces(MediaTypeNames.Application.Json, Type = typeof(MutantModel))]
        public IActionResult Post([FromBody] MutantModel request)
        {
            var objRequest = Mapper.Map<MutantDto>(request);

            bool isMutant = _service.IsMutant(objRequest);
            
            if (isMutant)
                return Ok();
            return BadRequest();
        }
    }
}
