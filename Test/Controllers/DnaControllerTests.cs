using BusinessLayer.BusinessLogic;
using BusinessLayer.Dtos;
using BusinessLayer.Interfaces;
using DataAccess.Interfaces;
using Moq;
using RecruitMutants.Controllers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Test.Controllers
{
    public class DnaControllerTests
    {

        private IDnaSequenceQuery _mockService;
        private IMutantLogic _mutantLogit;


        [Fact]
        public void ValidateListEmpy_InvalidOperationException()
        {
            MutantDto mutantDto = new MutantDto()
            {
                Dna = new List<string>()
            };

            MutantLogic mutantLogic = new MutantLogic(_mockService);
            Action action = () => mutantLogic.IsMutant(mutantDto);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(action);
            Assert.Equal($"{nameof(MutantLogic)}: No DNA.", exception.Message);
        }

        [Fact]
        public void FormatWrong_InvalidOperationException()
        {
            MutantDto mutantDto = new MutantDto()
            {
                Dna = new List<string>()
                {
                    "ATGGGG",
                    "ATAAA",
                }
            };

            MutantLogic mutantLogic = new MutantLogic(_mockService);
            Action action = () => mutantLogic.IsMutant(mutantDto);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(action);
            Assert.Equal($"{nameof(MutantLogic)}: The DNA format is wrong.", exception.Message);
        }

        [Fact]
        public void ValidateMinimumSequence_InvalidOperationException()
        {
            MutantDto mutantDto = new MutantDto()
            {
                Dna = new List<string>()
                {
                    "ATG",
                    "ATA",
                }
            };

            MutantLogic mutantLogic = new MutantLogic(_mockService);
            Action action = () => mutantLogic.IsMutant(mutantDto);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(action);
            Assert.Equal($"{nameof(MutantLogic)}: DNA does not meet the minimum sequence to be a mutant.", exception.Message);
        }

        [Fact]
        public void ValidateNitrogenousBaseWithATCG_InvalidOperationException()
        {
            MutantDto mutantDto = new MutantDto()
            {
                Dna = new List<string>()
                {
                    "ATGTHC",
                    "ATAAAA",
                }
            };

            MutantLogic mutantLogic = new MutantLogic(_mockService);
            Action action = () => mutantLogic.IsMutant(mutantDto);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(action);
            Assert.Equal($"{nameof(MutantLogic)}: The nitrogenous base of DNA has invalid data.", exception.Message);
        }

        [Fact]
        public void NonMutantDNA_InvalidOperationException()
        {
            MutantDto mutantDto = new MutantDto()
            {
                Dna = new List<string>()
                {
                    "ATGTGA",
                    "AAGCGC",
                    "TTATGA",
                    "AGATGC",
                    "CCGCTC",
                    "TCACTG"
                }
            };

            MutantLogic mutantLogic = new MutantLogic(_mockService);
            Action action = () => mutantLogic.IsMutant(mutantDto);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(action);
            Assert.Equal($"{nameof(MutantLogic)}: DNA is not from a mutant.", exception.Message);
        }

        [Fact]
        public void ValidateMutantHorizontally_True()
        {
            MutantDto mutantDto = new MutantDto()
            {
                Dna = new List<string>()
                {
                    "ATGGGG",
                    "ATAAAA",
                }
            };

            MutantLogic mutantLogic = new MutantLogic(_mockService);
            Task<bool> isMutant = mutantLogic.IsMutant(mutantDto);

            Assert.True(isMutant.Result);
        }

        [Fact]
        public void ValidateMutantDownVertical_True()
        {
            MutantDto mutantDto = new MutantDto()
            {
                Dna = new List<string>()
                {
                    "ATGAGA",
                    "CCGTGC",
                    "TCGTGT",
                    "ACACGG",
                    "TCACTG"
                }
            };

            MutantLogic mutantLogic = new MutantLogic(_mockService);
            Task<bool> isMutant = mutantLogic.IsMutant(mutantDto);

            Assert.True(isMutant.Result);
        }

        [Fact]
        public void ValidateMutantUpVertical_True()
        {
            MutantDto mutantDto = new MutantDto()
            {
                Dna = new List<string>()
                {
                    "ATGGGA",
                    "AAGTGC",
                    "GGTTCT",
                    "GCACGC",
                    "ACCCTC",
                    "TCACTG"
                }
            };

            MutantLogic mutantLogic = new MutantLogic(_mockService);
            Task<bool> isMutant = mutantLogic.IsMutant(mutantDto);

            Assert.True(isMutant.Result);
        }
    }
}
