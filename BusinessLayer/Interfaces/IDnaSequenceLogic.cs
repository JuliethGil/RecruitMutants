
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

using DataAccess.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IDnaSequenceLogic
    {
        /// <summary> 
        /// Validate if is a mutant's chain 
        /// </summary>
        /// <param name="dna">dna list</param>
        /// <returns>Whether it is mutant or not, or generates a throw in case of error</returns>
        Task<bool> IsMutant(List<string> dna);


        /// <summary> 
        /// Get stats from humans, mutants and ratio=mutants/humans 
        /// </summary>
        /// <param name="dna">dna list</param>
        /// <returns>Stats of people and mutants</returns>
        Task<StatsDto> Stats();
    }
}
