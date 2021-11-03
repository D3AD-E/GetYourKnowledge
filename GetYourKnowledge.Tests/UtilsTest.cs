using GetYourKnowledge.MVC.Core.Data;
using GetYourKnowledge.MVC.Core.Services;
using GetYourKnowledge.MVC.Core.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetYourKnowledge.Tests
{
    [TestClass]
    public class UtilsTest
    {
        [TestMethod]
        public void TestRandom()
        {
            var result = RandomHelper.GetRandomRange(0, 50, 10).ToList();
            CollectionAssert.AllItemsAreUnique(result);
            Assert.AreEqual(result.Count, 10);
        }

        [TestMethod]
        public void TestRandomNumberNotInCollection()
        {
            var data = new List<int>
            {
                1, 2, 5
            };
            var result = RandomHelper.GetRandomNumberNotInCollection(1, 6, data);
            data.Add(result);
            CollectionAssert.AllItemsAreUnique(data);
            Assert.AreEqual(data.Count, 4);
        }
    }
}