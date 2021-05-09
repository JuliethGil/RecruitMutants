using BusinessLayer.BusinessLogic;
using System.Collections.Generic;
using Xunit;

namespace Test.Controllers
{
    public class DnaControllerTests
    {

        [Fact]
        public void ValidateArrayFormatTwoChain_True()
        {
            List<string> dna = new List<string>
            {
                "ATGGGG",
                "ATAAAA",
            };

            MutantLogic mutantLogic = new MutantLogic();
            bool isMutant = mutantLogic.IsMutant(dna);

            Assert.True(isMutant);
        }

        [Fact]
        public void ValidateArrayFormatTwoChain_False()
        {
            List<string> dna = new List<string>
            {
                "ATMCGA",
                "ATMCGA",
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
                "ATGAGA",
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

        [Fact]
        public void ValidateMutanChainVertical_True()
        {
            List<string> dna = new List<string>
            {
                "ATGTGA",
                "AAGTGC",
                "ATATGC",
                "AGATGC",
                "CCACTC",
                "TCACTG"
            };

            MutantLogic mutantLogic = new MutantLogic();
            bool isMutant = mutantLogic.IsMutant(dna);

            Assert.True(isMutant);
        }

        [Fact]
        public void ValidateMutanChainVertical_False()
        {
            List<string> dna = new List<string>
            {
                "ATGTGA",
                "AAGTGC",
                "TTAAGA",
                "AGCTTC",
                "CCACTC",
                "TCACTG"
            };

            MutantLogic mutantLogic = new MutantLogic();
            bool isMutant = mutantLogic.IsMutant(dna);

            Assert.False(isMutant);
        }

        [Fact]
        public void ValidateMutanChainHorizontal_True()
        {
            List<string> dna = new List<string>
            {
                "ATGTGA",
                "AAGCGC",
                "TTATGA",
                "AGATGC",
                "CCCCTC",
                "TCACTG"
            };

            MutantLogic mutantLogic = new MutantLogic();
            bool isMutant = mutantLogic.IsMutant(dna);

            Assert.True(isMutant);
        }

        [Fact]
        public void ValidateMutanChainHorizontal_False()
        {
            List<string> dna = new List<string>
            {
                "ATGTGA",
                "AAGCGC",
                "TTATGA",
                "AGATGC",
                "CCGCTC",
                "TCACTG"
            };

            MutantLogic mutantLogic = new MutantLogic();
            bool isMutant = mutantLogic.IsMutant(dna);

            Assert.False(isMutant);
        }

        [Fact]
        public void ValidateMutanChainBottomRight_True()
        {
            List<string> dna = new List<string>
            {
                "ATGTGT",
                "AATCTC",
                "TTATTA",
                "AGAATC",
            };

            MutantLogic mutantLogic = new MutantLogic();
            bool isMutant = mutantLogic.IsMutant(dna);

            Assert.True(isMutant);
        }

        [Fact]
        public void ValidateMutanChainBottomLeft_True()
        {
            List<string> dna = new List<string>
            {
                "ATGTGT",
                "AACGTC",
                "TTGTTA",
                "AGTATC",
            };

            MutantLogic mutantLogic = new MutantLogic();
            bool isMutant = mutantLogic.IsMutant(dna);

            Assert.True(isMutant);
        }
    }
}
