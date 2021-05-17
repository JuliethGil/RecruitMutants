
// ***********************************************************************
// Assembly         : DataAccess
// Author           : Julieth Gil
// Created          : 13-05-2021
//
// ***********************************************************************
// <copyright file="DnaSequenceQuery.cs" company="">
//     Copyright (c) Julieth Gil. All rights reserved.
// </copyright>
// <summary></summary>

using AutoMapper;
using DataAccess.Dtos;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    /// <summary>
    /// Class DnaSequenceDto.
    /// </summary>
    /// <remarks>Julieth Gil</remarks>
    [ExcludeFromCodeCoverage]
    public class DnaSequenceQuery: IDnaSequenceQuery
    {
        private readonly CoreContext CoreContext;

        public DnaSequenceQuery(CoreContext coreContext)
        {
            CoreContext = coreContext;
        }

        public async Task<int> InsertDnaSequence(DnaSequenceDto dnaSequenceDto)
        {
            CoreContext.DnaSequences.Add(Mapper.Map<DnaSequence>(dnaSequenceDto));
            return await CoreContext.SaveChangesAsync();
        }

        public async Task<List<DnaSequenceDto>> PutDnaSequence()
        {
            var result =   await CoreContext.DnaSequences.Select(
                s => new DnaSequence
                {
                    IsMutant = s.IsMutant
                }).ToListAsync();

            return Mapper.Map<List<DnaSequenceDto>>(result);
        }
    }
}
