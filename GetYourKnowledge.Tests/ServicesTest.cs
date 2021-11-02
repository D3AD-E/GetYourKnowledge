using GetYourKnowledge.MVC.Core.Data;
using GetYourKnowledge.MVC.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
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

        [TestMethod]
        public async Task AdviceSlipMultipleAdviceAsync()
        {
            AdviceSlipService service = new(new System.Net.Http.HttpClient());
            var result = await service.GetAdvices(20);
            var collectionAssertResult = result.ToList(); //for simplicity of testing ToList is used
            CollectionAssert.AllItemsAreNotNull(collectionAssertResult);
            CollectionAssert.AllItemsAreUnique(collectionAssertResult);
            Assert.AreEqual(result.Count(), 20);
        }

        [TestMethod]
        public async Task LibreTranslateTranslationAsync()
        {
            LibreTranslateService service = new(new System.Net.Http.HttpClient());
            var expected = "Polski jest niesamowity";
            var actual = await service.TranslateAsync("Polish is amazing", LanguageType.English, LanguageType.Polish);
            Assert.AreEqual(actual, expected);
        }
    }
}
