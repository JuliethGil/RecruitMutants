
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
using Xunit;

namespace Test.Controllers
{
    public class DnaSequenceLogicTests2
    {
        private MockRepository _mockRepository;
        private Mock<IDnaSequenceQuery> _mockDnaSequenceQuery;

        public DnaSequenceLogicTests2()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _mockDnaSequenceQuery = _mockRepository.Create<IDnaSequenceQuery>();
        }

        private DnaSequenceLogic CreateDnaSequenceLogic()
        {
            return new DnaSequenceLogic(_mockDnaSequenceQuery.Object);
        }

        [Fact]
        public async Task ValidateListEmpy_InvalidOperationException()
        {
            var dnaSequenceLogic = CreateDnaSequenceLogic();

            DnaDto dnaDto = new DnaDto()
            {
                Dna = new List<string>()
            };

            try
            {
                await dnaSequenceLogic.IsMutant(dnaDto);
            }
            catch (Exception ex)
            {
                var expected = $"{ nameof(DnaSequenceLogic)}: No DNA.";
                Assert.Contains(expected, ex.Message);
            }

            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task FormatWrong_InvalidOperationException()
        {
            var dnaSequenceLogic = CreateDnaSequenceLogic();

            DnaDto dnaDto = new DnaDto()
            {
                Dna = new List<string>()
                {
                    "ATGGGG",
                    "ATAAA",
                }
            };

            try
            {
                await dnaSequenceLogic.IsMutant(dnaDto);
            }
            catch (Exception ex)
            {
                var expected = $"{ nameof(DnaSequenceLogic)}: The DNA format is wrong.";
                Assert.Contains(expected, ex.Message);
            }
            _mockRepository.VerifyAll();

        }

        [Fact]
        public async Task ValidateNitrogenousBaseWithATCG_InvalidOperationException()
        {
            var dnaSequenceLogic = CreateDnaSequenceLogic();

            DnaDto dnaDto = new DnaDto()
            {
                Dna = new List<string>()
                {
                    "ATGTHC",
                    "ATAAAA",
                }
            };

            try
            {
                await dnaSequenceLogic.IsMutant(dnaDto);
            }
            catch (Exception ex)
            {
                var expected = $"{ nameof(DnaSequenceLogic)}: The nitrogenous base of DNA has invalid data.";
                Assert.Contains(expected, ex.Message);
            }
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task ValidateMinimumSequence_InvalidOperationException()
        {
            _mockDnaSequenceQuery.Setup(x => x.InsertDnaSequence(It.IsAny<DnaSequenceDto>())
                                       ).ReturnsAsync(1);

            var dnaSequenceLogic = CreateDnaSequenceLogic();

            DnaDto dnaDto = new DnaDto()
            {
                Dna = new List<string>()
                {
                    "ATG",
                    "ATA",
                }
            };

            bool result = await dnaSequenceLogic.IsMutant(dnaDto);

            Assert.False(result);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task NonMutantDNA_InvalidOperationException()
        {
            _mockDnaSequenceQuery.Setup(x => x.InsertDnaSequence(It.IsAny<DnaSequenceDto>())
                                       ).ReturnsAsync(1);
            var dnaSequenceLogic = CreateDnaSequenceLogic();

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

            bool result = await dnaSequenceLogic.IsMutant(dnaDto);

            Assert.False(result);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task ValidateMutantHorizontally_True()
        {
            _mockDnaSequenceQuery.Setup(x => x.InsertDnaSequence(It.IsAny<DnaSequenceDto>())
                                       ).ReturnsAsync(1);
            var dnaSequenceLogic = CreateDnaSequenceLogic();

            DnaDto dnaDto = new DnaDto()
            {
                Dna = new List<string>()
                {
                    "ATGGGG",
                    "ATAAAA",
                }
            };

            bool result = await dnaSequenceLogic.IsMutant(dnaDto);

            Assert.True(result);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task ValidateMutantDownVertical_True()
        {
            _mockDnaSequenceQuery.Setup(x => x.InsertDnaSequence(It.IsAny<DnaSequenceDto>())
                                       ).ReturnsAsync(1);
            var dnaSequenceLogic = CreateDnaSequenceLogic();

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

            bool result = await dnaSequenceLogic.IsMutant(dnaDto);

            Assert.True(result);
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task ValidateMutantUpVertical_True()
        {
            _mockDnaSequenceQuery.Setup(x => x.InsertDnaSequence(It.IsAny<DnaSequenceDto>())
                                       ).ReturnsAsync(1);
            var dnaSequenceLogic = CreateDnaSequenceLogic();

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

            bool result = await dnaSequenceLogic.IsMutant(dnaDto);

            Assert.True(result);
            _mockRepository.VerifyAll();
        }
    }
}
