using BusinessLayer.BusinessLogic;
using BusinessLayer.Dtos;
using BusinessLayer.Interfaces;
using DataAccess.Dtos;
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
        private IDnaSequenceLogic _mutantLogit;


        [Fact]
        public void ValidateListEmpy_InvalidOperationException()
        {
            DnaDto dnaDto = new DnaDto()
            {
                Dna = new List<string>()
            };

            DnaSequenceLogic mutantLogic = new DnaSequenceLogic(_mockService);
            Action action = () => mutantLogic.IsMutant(dnaDto);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(action);
            Assert.Equal($"{nameof(DnaSequenceLogic)}: No DNA.", exception.Message);
        }

        [Fact]
        public void FormatWrong_InvalidOperationException()
        {
            DnaDto dnaDto = new DnaDto()
            {
                Dna = new List<string>()
                {
                    "ATGGGG",
                    "ATAAA",
                }
            };

            DnaSequenceLogic mutantLogic = new DnaSequenceLogic(_mockService);
            Action action = () => mutantLogic.IsMutant(dnaDto);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(action);
            Assert.Equal($"{nameof(DnaSequenceLogic)}: The DNA format is wrong.", exception.Message);
        }

        [Fact]
        public void ValidateMinimumSequence_InvalidOperationException()
        {
            DnaDto dnaDto = new DnaDto()
            {
                Dna = new List<string>()
                {
                    "ATG",
                    "ATA",
                }
            };

            DnaSequenceLogic mutantLogic = new DnaSequenceLogic(_mockService);
            Action action = () => mutantLogic.IsMutant(dnaDto);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(action);
            Assert.Equal($"{nameof(DnaSequenceLogic)}: DNA does not meet the minimum sequence to be a mutant.", exception.Message);
        }

        [Fact]
        public void ValidateNitrogenousBaseWithATCG_InvalidOperationException()
        {
            DnaDto dnaDto = new DnaDto()
            {
                Dna = new List<string>()
                {
                    "ATGTHC",
                    "ATAAAA",
                }
            };

            DnaSequenceLogic mutantLogic = new DnaSequenceLogic(_mockService);
            Action action = () => mutantLogic.IsMutant(dnaDto);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(action);
            Assert.Equal($"{nameof(DnaSequenceLogic)}: The nitrogenous base of DNA has invalid data.", exception.Message);
        }

        [Fact]
        public void NonMutantDNA_InvalidOperationException()
        {
            DnaDto dnaDto = new DnaDto()
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

            DnaSequenceLogic mutantLogic = new DnaSequenceLogic(_mockService);
            Action action = () => mutantLogic.IsMutant(dnaDto);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(action);
            Assert.Equal($"{nameof(DnaSequenceLogic)}: DNA is not from a mutant.", exception.Message);
        }

        [Fact]
        public void ValidateMutantHorizontally_True()
        {
            DnaDto dnaDto = new DnaDto()
            {
                Dna = new List<string>()
                {
                    "ATGGGG",
                    "ATAAAA",
                }
            };

            DnaSequenceLogic mutantLogic = new DnaSequenceLogic(_mockService);
            Task<bool> isMutant = mutantLogic.IsMutant(dnaDto);

            Assert.True(isMutant.Result);
        }

        [Fact]
        public void ValidateMutantDownVertical_True()
        {
            DnaDto dnaDto = new DnaDto()
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

            DnaSequenceLogic mutantLogic = new DnaSequenceLogic(_mockService);
            Task<bool> isMutant = mutantLogic.IsMutant(dnaDto);

            Assert.True(isMutant.Result);
        }

        [Fact]
        public void ValidateMutantUpVertical_True()
        {
            DnaDto dnaDto = new DnaDto()
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

            DnaSequenceLogic mutantLogic = new DnaSequenceLogic(_mockService);
            Task<bool> isMutant = mutantLogic.IsMutant(dnaDto);

            Assert.True(isMutant.Result);
        }
    }
}
