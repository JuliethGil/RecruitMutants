
// ***********************************************************************
// Assembly         : DataAccess
// Author           : Julieth Gil
// Created          : 15-05-2021
//
// ***********************************************************************
// <copyright file="Node.cs" company="">
//     Copyright (c) Julieth Gil. All rights reserved.
// </copyright>
// <summary></summary>

using DataAccess.Dtos;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IDnaSequenceQuery
    {
        /// <summary> 
        /// Insert the data to the DB
        /// </summary>
        /// <param name="dnaSequenceDto">DnaSequenceDto data to enter </param>
        /// <returns>ID DnaSequenceDto</returns>
        Task<int> InsertDnaSequence(DnaSequenceDto dnaSequenceDto);
    }
}
