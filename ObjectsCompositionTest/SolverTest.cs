using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectsComposition.Interfaces;

namespace ObjectsCompositionTest
{
    [TestClass]
    public class SolverTest
    {
        [TestMethod]
        public void CreateOrUpdate_returnNotEmptyString()
        {
            ISolver solver;
            object o = null;
            string response = solver.CreateOrUpdate(o);

            Assert.IsTrue(response.Length > 0);
        }

        [TestMethod]
        public void Validate_Null_False()
        {
            ISolver solver;
            object o = null;
            bool valid = solver.Validate(o);

            Assert.IsFalse(valid);
        }

        [TestMethod]
        public void ObjectFromXml_NotValidXml_Null()
        {
            ISolver solver;
            string notValidXml = "not valid";
            object o = solver.ObjectFromXml(notValidXml);

            Assert.IsNull(o);
        }
    }
}
