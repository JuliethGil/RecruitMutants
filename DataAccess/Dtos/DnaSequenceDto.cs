
// ***********************************************************************
// Assembly         : DataAccess
// Author           : Julieth Gil
// Created          : 10-05-2021
//
// ***********************************************************************
// <copyright file="Node.cs" company="">
//     Copyright (c) Julieth Gil. All rights reserved.
// </copyright>
// <summary></summary>

namespace DataAccess.Dtos
{
    public class DnaSequenceDto
    {
        public int Id { get; set; }
        public string PersonDna { get; set; }
        public bool IsMutant { get; set; }
    }
}
