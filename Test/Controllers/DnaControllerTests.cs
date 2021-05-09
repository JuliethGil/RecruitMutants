using RecruitMutants.Controllers;
using Xunit;

namespace Test.Controllers
{
    public class DnaControllerTests
    {
        [Fact]
        public void Post_StateUnderTest_ExpectedBehavior()
        {
            var dnaController = new DnaController();

            dnaController.Post();
            
            Assert.True(false);
        }
    }
}
