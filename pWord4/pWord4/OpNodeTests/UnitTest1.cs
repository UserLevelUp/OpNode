using pWordLib.dat;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.CompilerServices;

namespace OpNodeTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddOpNode()
        {
            var po = PrivateObject.Create(new OpNode());
        }


    }
}