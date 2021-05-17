
// ***********************************************************************
// Assembly         : DataAccess
// Author           : Julieth Gil
// Created          : 07-05-2021
//
// ***********************************************************************
// <copyright file="Node.cs" company="">
//     Copyright (c) Julieth Gil. All rights reserved.
// </copyright>
// <summary>Unit test</summary>

using BusinessLayer.BusinessLogic;
using DataAccess.Dtos;
using DataAccess.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Stubs;
using Xunit;

namespace Test.BusinessLogic
{
    public class DnaSequenceLogicTests
    {
        private readonly MockRepository _mockRepository;
        private readonly Mock<IDnaSequenceQuery> _mockDnaSequenceQuery;

        public DnaSequenceLogicTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _mockDnaSequenceQuery = _mockRepository.Create<IDnaSequenceQuery>();
        }

        private DnaSequenceLogic CreateDnaSequenceLogic()
        {
            return new DnaSequenceLogic(_mockDnaSequenceQuery.Object);
        }

        [Fact]
        public async Task IsMutant_ValidateListEmpy_InvalidOperationException()
        {
            var dnaSequenceLogic = CreateDnaSequenceLogic();
            List<string> dna = new List<string>();

            try
            {
                await dnaSequenceLogic.IsMutant(dna);
            }
            catch (Exception ex)
            {
                var expected = $"{ nameof(DnaSequenceLogic)}: No DNA.";
                Assert.Contains(expected, ex.Message);
            }
        }

        [Fact]
        public async Task IsMutant_FormatWrong_InvalidOperationException()
        {
            var dnaSequenceLogic = CreateDnaSequenceLogic();

            List<string> dna = new List<string>()
            {
                "ATGGGG",
                "ATAAA",
            };

            try
            {
                await dnaSequenceLogic.IsMutant(dna);
            }
            catch (Exception ex)
            {
                var expected = $"{ nameof(DnaSequenceLogic)}: The DNA format is wrong.";
                Assert.Contains(expected, ex.Message);
            }
        }

        [Fact]
        public async Task IsMutant_ValidateNitrogenousBaseWithATCG_InvalidOperationException()
        {
            var dnaSequenceLogic = CreateDnaSequenceLogic();

            List<string> dna = new List<string>()
            {
                "ATGTHC",
                "ATAAAA",
            };

            try
            {
                await dnaSequenceLogic.IsMutant(dna);
            }
            catch (Exception ex)
            {
                var expected = $"{ nameof(DnaSequenceLogic)}: The nitrogenous base of DNA has invalid data.";
                Assert.Contains(expected, ex.Message);
            }
        }

        [Fact]
        public async Task IsMutant_ValidateMinimumSequence_InvalidOperationException()
        {
            _mockDnaSequenceQuery.Setup(x => x.InsertDnaSequence(It.IsAny<DnaSequenceDto>())
                                       ).ReturnsAsync(1);

            var dnaSequenceLogic = CreateDnaSequenceLogic();

            List<string> dna = new List<string>()
            {
                "ATG",
                "ATA",
            };


            bool result = await dnaSequenceLogic.IsMutant(dna);

            Assert.False(result);
            _mockDnaSequenceQuery.VerifyAll();
        }

        [Fact]
        public async Task IsMutant_NonMutantDNA_InvalidOperationException()
        {
            _mockDnaSequenceQuery.Setup(x => x.InsertDnaSequence(It.IsAny<DnaSequenceDto>())
                                       ).ReturnsAsync(1);
            var dnaSequenceLogic = CreateDnaSequenceLogic();

            List<string> dna = new List<string>()
            {
                "ATGTGA",
                "AAGCGC",
                "TTATGA",
                "AGATGC",
                "CCGCTC",
                "TCACTG"
            };

            bool result = await dnaSequenceLogic.IsMutant(dna);

            Assert.False(result);
            _mockDnaSequenceQuery.VerifyAll();
        }

        [Fact]
        public async Task IsMutant_ValidateMutantHorizontally_True()
        {
            _mockDnaSequenceQuery.Setup(x => x.InsertDnaSequence(It.IsAny<DnaSequenceDto>())
                                       ).ReturnsAsync(1);
            var dnaSequenceLogic = CreateDnaSequenceLogic();

            List<string> dna = new List<string>()
            {
                "ATGGGG",
                "ATAAAA"
            };

            bool result = await dnaSequenceLogic.IsMutant(dna);

            Assert.True(result);
            _mockDnaSequenceQuery.VerifyAll();
        }

        [Fact]
        public async Task IsMutant_ValidateMutantDownVertical_True()
        {
            _mockDnaSequenceQuery.Setup(x => x.InsertDnaSequence(It.IsAny<DnaSequenceDto>())
                                       ).ReturnsAsync(1);
            var dnaSequenceLogic = CreateDnaSequenceLogic();

            List<string> dna = new List<string>()
            {
                "ATGAGA",
                "CCGTGC",
                "TCGTGT",
                "ACACGG",
                "TCACTG"
            };

            bool result = await dnaSequenceLogic.IsMutant(dna);

            Assert.True(result);
            _mockDnaSequenceQuery.VerifyAll();
        }

        [Fact]
        public async Task IsMutant_ValidateMutantUpVertical_True()
        {
            _mockDnaSequenceQuery.Setup(x => x.InsertDnaSequence(It.IsAny<DnaSequenceDto>())
                                       ).ReturnsAsync(1);
            var dnaSequenceLogic = CreateDnaSequenceLogic();

            List<string> dna = new List<string>()
            {
                "ATGGGA",
                "AAGTGC",
                "GGTTCT",
                "GCACGC",
                "ACCCTC",
                "TCACTG"
            };

            bool result = await dnaSequenceLogic.IsMutant(dna);

            Assert.True(result);
            _mockDnaSequenceQuery.VerifyAll();
        }

        [Fact]
        public async Task Stats_NoPersonButMutants_NoRatio()
        {
            _mockDnaSequenceQuery.Setup(x => x.PutDnaSequence()
                                       ).ReturnsAsync(DnaSequenceStub.DnaSequencesDto);

            var dnaSequenceLogic = CreateDnaSequenceLogic();

            var result = await dnaSequenceLogic.Stats();

            Equals(DnaSequenceStub.StatsDtoNoRatio, result);
            Assert.Equal(0, result.Ratio);
            _mockDnaSequenceQuery.VerifyAll();
        }


        [Fact]
        public async Task Stats_NoPersonButMutants_Ratio()
        {
            _mockDnaSequenceQuery.Setup(x => x.PutDnaSequence()
                                       ).ReturnsAsync(DnaSequenceStub.DnaSequencesDto2);

            var dnaSequenceLogic = CreateDnaSequenceLogic();

            var result = await dnaSequenceLogic.Stats();

            Equals(DnaSequenceStub.StatsDto, result);
            Assert.Equal(DnaSequenceStub.StatsDto.Ratio, result.Ratio);
            _mockDnaSequenceQuery.VerifyAll();
        }
    }
}
