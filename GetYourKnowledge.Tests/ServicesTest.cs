using GetYourKnowledge.MVC.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace GetYourKnowledge.Tests
{
    [TestClass]
    public class ServicesTest
    {
        [TestMethod]
        public async Task AdviceSlipSingleAdviceAsync()
        {
            AdviceSlipService service = new(new System.Net.Http.HttpClient());
            var result = await service.GetAdvice();
            Assert.IsTrue(result.Id > 0);
            Assert.IsNotNull(result.Advice);
        }
    }
}
