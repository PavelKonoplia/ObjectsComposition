using System;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectsComposition.Interfaces;
using ObjectsComposition.Logic;
using ObjectsComposition.Models;

namespace ObjectsCompositionTest
{
    [TestClass]
    public class SolverTest
    {

        [TestMethod]
        public void Validate_Null_False()
        {
            ISolver solver = new Solver();
            BaseModel o = null;
            bool valid = solver.Validate(o);

            Assert.IsFalse(valid);
        }

        [TestMethod]
        public void ObjectFromXml_NotValidXml_Null()
        {
            ISolver solver = new Solver();
            XmlDocument notValidXml = null;
            BaseModel o = solver.ObjectFromXml(notValidXml);

            Assert.IsNull(o);
        }
    }
}
