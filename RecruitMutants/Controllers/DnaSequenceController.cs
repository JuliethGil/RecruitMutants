
// ***********************************************************************
// Assembly         : RecruitMutants
// Author           : Julieth Gil
// Created          : 10-05-2021
//
// ***********************************************************************
// <copyright file="Node.cs" company="">
//     Copyright (c) Julieth Gil. All rights reserved.
// </copyright>
// <summary></summary>

using AutoMapper;
using BusinessLayer.Interfaces;
using DataAccess.Dtos;
using Microsoft.AspNetCore.Mvc;
using RecruitMutants.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace RecruitMutants.Controllers
{
    [ApiController]
    [Route("api/")]
    public class MutantController : ControllerBase
    {
        /// <summary>
        /// The service
        /// </summary>
        private readonly IDnaSequenceLogic _service;

        public MutantController(IDnaSequenceLogic service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        /// <summary>
        /// Posts the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>IActionResult.</returns>
        /// <remarks>Julieth Gil</remarks>
        [Route("mutants")]
        [HttpPost]
        [DisableRequestSizeLimit]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [Produces(MediaTypeNames.Application.Json, Type = typeof(DnaSequenceModel))]
        public async Task<IActionResult> Post([FromBody] DnaSequenceModel request)
        {
            try
            {
                var objRequest = Mapper.Map<List<string>>(request.Dna);
                bool isMutant = await _service.IsMutant(objRequest);

                if (isMutant)
                    return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, null);
            }

            return StatusCode((int)HttpStatusCode.Forbidden, null);
        }


        /// <summary>
        /// Get all as an asynchronous operation.
        /// </summary>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        /// <remarks>Juliet Gil</remarks>
        [Route("stats")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(List<StatsDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                StatsDto stats = await _service.Stats();

                return Ok(stats);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.Forbidden, null);
            }
        }
    }
}
