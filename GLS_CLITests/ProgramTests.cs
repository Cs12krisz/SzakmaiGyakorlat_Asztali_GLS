using Microsoft.VisualStudio.TestTools.UnitTesting;
using GLS_CLI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLS_CLI.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        [DataRow(10, 100, 10)]
        [DataRow(16, 200, 8)]
        [DataRow(0, 0, 0)]
        public void AtlagosNapiLiterFogyasztasTest(int fogyasztottLiter, int megtettKilometer, double elvart)
        {
            Assert.AreEqual(elvart, Program.AtlagosNapiLiterFogyasztas(fogyasztottLiter, megtettKilometer));
        }

    }
}