using DataAccess.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Stubs
{
    public class DnaSequenceStub
    {
        public static readonly DnaSequenceDto DnaSequenceDto = new DnaSequenceDto()
        {
            IsMutant = true
        };

        public static readonly DnaSequenceDto DnaSequenceDto2 = new DnaSequenceDto()
        {
            IsMutant = true
        };



        public static readonly DnaSequenceDto DnaSequenceDto3 = new DnaSequenceDto()
        {
            IsMutant = false
        };


        public static readonly DnaSequenceDto DnaSequenceDto4 = new DnaSequenceDto()
        {
            IsMutant = false
        };


        public static readonly DnaSequenceDto DnaSequenceDto5 = new DnaSequenceDto()
        {
            IsMutant = false
        };

        public static readonly List<DnaSequenceDto> DnaSequencesDto = new List<DnaSequenceDto>() {
            DnaSequenceDto,
            DnaSequenceDto2
        };

        public static readonly StatsDto StatsDtoNoRatio = new StatsDto()
        {
            Count_mutant_dna = 2,
            Count_human_dna = 0,
            Ratio = 0
        };

        public static readonly List<DnaSequenceDto> DnaSequencesDto2 = new List<DnaSequenceDto>() {
            DnaSequenceDto,
            DnaSequenceDto2,
            DnaSequenceDto3,
            DnaSequenceDto4,
            DnaSequenceDto5,

        };

        public static readonly StatsDto StatsDto = new StatsDto()
        {
            Count_mutant_dna = 2,
            Count_human_dna = 3,
            Ratio = (decimal)0.7
        };

    }
}
