using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectsComposition.Interfaces;

namespace ObjectsCompositionTest
{
    [TestClass]
    public class RepositoryTest
    {
        [TestMethod]
        public void GetItems_returnIEnumerable()
        {
            IRepository<object> repository;

            var result = repository.GetItems();

            Assert.IsTrue(result is IEquatable<object>);
        }
    }
}
