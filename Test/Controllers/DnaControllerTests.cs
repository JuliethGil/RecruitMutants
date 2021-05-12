using BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using Xunit;

namespace Test.Controllers
{
    public class DnaControllerTests
    {
        [Fact]
        public void ValidateListEmpy_InvalidOperationException()
        {
            List<string> dna = new List<string>();

            MutantLogic mutantLogic = new MutantLogic();
            Action action = () => mutantLogic.IsMutant(dna);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(action);
            Assert.Equal($"{nameof(MutantLogic)}: No DNA.", exception.Message);
        }

        [Fact]
        public void FormatWrong_InvalidOperationException()
        {
            List<string> dna = new List<string>()
            {
                "ATGGGG",
                "ATAAA",
            };

            MutantLogic mutantLogic = new MutantLogic();
            Action action = () => mutantLogic.IsMutant(dna);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(action);
            Assert.Equal($"{nameof(MutantLogic)}: The DNA format is wrong.", exception.Message);
        }

        [Fact]
        public void ValidateMinimumSequence_InvalidOperationException()
        {
            List<string> dna = new List<string>()
            {
                "ATG",
                "ATA",
            };

            MutantLogic mutantLogic = new MutantLogic();
            Action action = () => mutantLogic.IsMutant(dna);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(action);
            Assert.Equal($"{nameof(MutantLogic)}: DNA does not meet the minimum sequence to be a mutant.", exception.Message);
        }

        [Fact]
        public void ValidateNitrogenousBaseWithATCG_InvalidOperationException()
        {
            List<string> dna = new List<string>()
            {
                "ATGTHC",
                "ATAAAA",
            };

            MutantLogic mutantLogic = new MutantLogic();
            Action action = () => mutantLogic.IsMutant(dna);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(action);
            Assert.Equal($"{nameof(MutantLogic)}: The nitrogenous base of DNA has invalid data.", exception.Message);
        }

        [Fact]
        public void NonMutantDNA_InvalidOperationException()
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
            Action action = () => mutantLogic.IsMutant(dna);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(action);
            Assert.Equal($"{nameof(MutantLogic)}: DNA is not from a mutant.", exception.Message);
        }

        [Fact]
        public void ValidateMutantHorizontally_True()
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
        public void ValidateMutantDownVertical_True()
        {
            List<string> dna = new List<string>
            {
                "ATGAGA",
                "CCGTGC",
                "TCGTGT",
                "ACACGG",
                "TCACTG"
            };

            MutantLogic mutantLogic = new MutantLogic();
            bool isMutant = mutantLogic.IsMutant(dna);

            Assert.True(isMutant);
        }

        [Fact]
        public void ValidateMtantUpVertical_True()
        {
            List<string> dna = new List<string>
            {
                "ATGGGA",
                "AAGTGC",
                "GGTTCT",
                "GCACGC",
                "ACCCTC",
                "TCACTG"
            };

            MutantLogic mutantLogic = new MutantLogic();
            bool isMutant = mutantLogic.IsMutant(dna);

            Assert.True(isMutant);
        }
    }
}
