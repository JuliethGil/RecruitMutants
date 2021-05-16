
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

namespace DataAccess.Dtos
{
    public class StatsDto
    {
        public int Count_mutant_dna { get; set; }
        public int Count_human_dna { get; set; }
        public decimal Ratio { get; set; }

        public StatsDto()
        {
            Count_mutant_dna = 0;
            Count_human_dna = 0;
            Ratio = 0;
        }
    }
}
