// ***********************************************************************
// Assembly         : Xm.Sicep.Audit.Common
// Author           : Julieth Gil
// Created          : 12-05-2021
//
// ***********************************************************************
// <copyright file="Node.cs" company="">
//     Copyright (c) Julieth Gil. All rights reserved.
// </copyright>
// <summary></summary>

namespace DataAccess.Entities
{
    public partial class DnaSequence
    {
        public int Id { get; set; }
        public string PersonDna { get; set; }
        public bool IsMutant { get; set; }
    }
}
