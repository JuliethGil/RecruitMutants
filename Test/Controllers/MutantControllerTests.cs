using BusinessLayer.Interfaces;
using DataAccess.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RecruitMutants.Controllers;
using RecruitMutants.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Test.Controllers
{
    public class MutantControllerTests
    {
        private readonly MockRepository _mockRepository;
        private readonly Mock<IDnaSequenceLogic> _mockDnaSequenceLogic;

        public MutantControllerTests()
        {
            AutoMapper.Mapper.Reset(); 
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMissingTypeMaps = true;
                cfg.ValidateInlineMaps = false;
            });
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _mockDnaSequenceLogic = _mockRepository.Create<IDnaSequenceLogic>();
        }

        private MutantController CreateMutantController()
        {
            return new MutantController(_mockDnaSequenceLogic.Object);
        }

        [Fact]
        public async Task Post__ExpectedSetup_Ok()
        {
            _mockDnaSequenceLogic.Setup(x => x.IsMutant(It.IsAny<DnaDto>())).ReturnsAsync(true);

            // Arrange
            var mutantController = CreateMutantController();

            List<string> chains = new List<string>() { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" };
            DnaSequenceModel request = new DnaSequenceModel()
            {
                Dna = chains
            };

            var result = await mutantController.Post(request);


            _mockDnaSequenceLogic.VerifyAll();
        }

        [Fact]
        public async Task GetAllAsync_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var mutantController = this.CreateMutantController();

            // Act
            var result = await mutantController.GetAllAsync();

            // Assert
            Assert.True(false);
            this._mockRepository.VerifyAll();
        }
    }
}
