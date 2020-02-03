using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Ludo._70_Converter;
using System.Globalization;

namespace Ludo_TestProject
{
    [TestClass]
    public class Converter_Test
    {
        [TestMethod]
        public void Convert_stringColorToVM()
        {
            //Arrange:
            Converter_Color converter = new Converter_Color();
            string color_string = "100,150,230";
            string expected = "#6496E6";

            ////Act:
            //var actual = converter.Convert(color_string, typeof(String), null, null);

            ////Assert:
            //string errorMsg = expected + "|" + actual;
            //Assert.AreEqual(expected, actual, errorMsg);
        }

        [TestMethod]
        public void Convert_stringColorToView()
        {
            //Arrange:
            Converter_Color converter = new Converter_Color();
            string color_string = "100,150,230";
            string expected = "#6496E6";

            //Act:
            var actual = converter.Convert(color_string, typeof(String), null, null);

            //Assert:
            string errorMsg = expected + "|" + actual;
            Assert.AreEqual(expected, actual, errorMsg);
        }
    }
}
