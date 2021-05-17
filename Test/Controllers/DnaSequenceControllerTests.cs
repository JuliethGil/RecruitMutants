using BusinessLayer.Interfaces;
using DataAccess.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RecruitMutants.Controllers;
using RecruitMutants.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Test.Stubs;
using Xunit;

namespace Test.Controllers
{
    public class DnaSequenceControllerTests
    {
        private readonly MockRepository _mockRepository;
        private readonly Mock<IDnaSequenceLogic> _mockDnaSequenceLogic;

        public DnaSequenceControllerTests()
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
        public async Task Post_ExpectedSetup_Ok()
        {

            _mockDnaSequenceLogic.Setup(x => x.IsMutant(It.IsAny<List<string>>())).ReturnsAsync(true);
            var mutantController = CreateMutantController();

            List<string> chains = new List<string>() { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" };
            DnaSequenceModel request = new DnaSequenceModel()
            {
                Dna = chains
            };

            IActionResult result = await mutantController.Post(request);

            Equals(StatusCodes.Status200OK, result);
            _mockDnaSequenceLogic.VerifyAll();
        }

        [Fact]
        public async Task Post_ExpectedSetup_Forbidden()
        {
            var mutantController = CreateMutantController();

            List<string> chains = new List<string>() { "ATGGA", "CAGTGC" };
            DnaSequenceModel request = new DnaSequenceModel()
            {
                Dna = chains
            };

            IActionResult result = await mutantController.Post(request);

            Equals(StatusCodes.Status403Forbidden, result);
        }

        [Fact]
        public async Task GetAllAsync_ExpectedSetup_Forbidden()
        {
            _mockDnaSequenceLogic.Setup(x => x.Stats()).ReturnsAsync(DnaSequenceStub.StatsDto);

            var mutantController = CreateMutantController();

            var result = await mutantController.GetAllAsync();

            Equals(StatusCodes.Status200OK, result);
            var okResult = result as ObjectResult;
            Assert.NotNull(okResult);
            var model = okResult.Value as StatsDto;
            Assert.NotNull(model);
            Equals(DnaSequenceStub.StatsDto, model);

            _mockDnaSequenceLogic.VerifyAll();
        }
    }
}
