﻿
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
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IMutantLogic
    {
        /// <summary> 
        /// Validate if is a mutant's chain 
        /// </summary>
        /// <param name="dna">dna list</param>
        /// <returns>Is a mutant</returns>
        Task<bool> IsMutant(MutantDto dna);
    }
}
