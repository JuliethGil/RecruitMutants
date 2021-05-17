// ***********************************************************************
// Assembly         : DataAccess
// Author           : Julieth Gil
// Created          : 12-05-2021
//
// ***********************************************************************
// <copyright file="DnaSequence.cs" company="">
//     Copyright (c) Julieth Gil. All rights reserved.
// </copyright>
// <summary></summary>

using System.Diagnostics.CodeAnalysis;

namespace DataAccess.Entities
{
    /// <summary>
    /// Class DnaSequenceDto.
    /// </summary>
    /// <remarks>Julieth Gil</remarks>
    [ExcludeFromCodeCoverage]
    public partial class DnaSequence
    {
        public int Id { get; set; }
        public string PersonDna { get; set; }
        public bool IsMutant { get; set; }
    }
}
