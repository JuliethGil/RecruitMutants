using BusinessLayer.BusinessLogic;
using System.Collections.Generic;
using Xunit;

namespace Test.Controllers
{
    public class DnaControllerTests
    {
        [Fact]
        public void MutantsData_True()
        {
            List<string> dna = new List<string>();
            MutantLogic mutantLogic = new MutantLogic();
            bool isMutant = mutantLogic.IsMutant(dna);

            Assert.True(isMutant);
        }

        [Fact]
        public void ValidateArrayFormatOneChain_True()
        {
            List<string> dna = new List<string>
            {
                "ATGCGA"
            };

            MutantLogic mutantLogic = new MutantLogic();
            bool isMutant = mutantLogic.IsMutant(dna);

            Assert.True(isMutant);
        }

        [Fact]
        public void ValidateArrayFormatOneChain_False()
        {
            List<string> dna = new List<string>
            {
                "ATMCGA"
            };

            MutantLogic mutantLogic = new MutantLogic();
            bool isMutant = mutantLogic.IsMutant(dna);

            Assert.False(isMutant);
        }

        [Fact]
        public void ValidateArrayFormatMultiChain_True()
        {
            List<string> dna = new List<string>
            {
                "ATGCGA",
                "CAGTGC",
                "TTATGT",
                "AGAAGG",
                "CCCCTA",
                "TCACTG"
            };

            MutantLogic mutantLogic = new MutantLogic();
            bool isMutant = mutantLogic.IsMutant(dna);

            Assert.True(isMutant);
        }

        [Fact]
        public void ValidateArrayFormatMultiChain_False()
        {
            List<string> dna = new List<string>
            {
                "ATMCGA",
                "TCACTG",
                "TCACKG",
            };

            MutantLogic mutantLogic = new MutantLogic();
            bool isMutant = mutantLogic.IsMutant(dna);

            Assert.False(isMutant);
        }
    }
}
