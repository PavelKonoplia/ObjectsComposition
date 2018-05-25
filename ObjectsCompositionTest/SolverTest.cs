using System;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectsComposition.Interfaces;
using ObjectsComposition.Models;

namespace ObjectsCompositionTest
{
    [TestClass]
    public class SolverTest
    {
        [TestMethod]
        public void CreateOrUpdate_returnNotEmptyString()
        {
            ISolver solver;
            BaseModel o = null;
            string response = solver.CreateOrUpdate(o);

            Assert.IsTrue(response.Length > 0);
        }

        [TestMethod]
        public void Validate_Null_False()
        {
            ISolver solver;
            XmlDocument o = null;
            bool valid = solver.Validate(o);

            Assert.IsFalse(valid);
        }

        [TestMethod]
        public void ObjectFromXml_NotValidXml_Null()
        {
            ISolver solver;
            XmlDocument notValidXml = "not valid";
            BaseModel o = solver.ObjectFromXml(notValidXml);

            Assert.IsNull(o);
        }
    }
}
