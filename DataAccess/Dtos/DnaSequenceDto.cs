
// ***********************************************************************
// Assembly         : DataAccess
// Author           : Julieth Gil
// Created          : 10-05-2021
//
// ***********************************************************************
// <copyright file="DnaSequenceDto.cs" company="">
//     Copyright (c) Julieth Gil. All rights reserved.
// </copyright>
// <summary></summary>

using System.Diagnostics.CodeAnalysis;

namespace DataAccess.Dtos
{
    /// <summary>
    /// Class DnaSequenceDto.
    /// </summary>
    /// <remarks>Julieth Gil</remarks>
    [ExcludeFromCodeCoverage]
    public class DnaSequenceDto
    {
        public int Id { get; set; }
        public string PersonDna { get; set; }
        public bool IsMutant { get; set; }
    }
}
