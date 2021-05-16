
// ***********************************************************************
// Assembly         : BusinessLayer
// Author           : Julieth Gil
// Created          : 08-05-2021
//
// ***********************************************************************
// <copyright file="Node.cs" company="">
//     Copyright (c) Julieth Gil. All rights reserved.
// </copyright>
// <summary></summary>

using BusinessLayer.Dtos;
using DataAccess.Dtos;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IDnaSequenceLogic
    {
        /// <summary> 
        /// Validate if is a mutant's chain 
        /// </summary>
        /// <param name="dna">dna list</param>
        /// <returns>Is a mutant</returns>
        Task<bool> IsMutant(DnaDto dna);


        /// <summary> 
        /// Get information from people and mutants
        /// </summary>
        /// <param name="dna">dna list</param>
        /// <returns>Stats of people and mutants</returns>
        Task<StatsDto> Stats();
    }
}
