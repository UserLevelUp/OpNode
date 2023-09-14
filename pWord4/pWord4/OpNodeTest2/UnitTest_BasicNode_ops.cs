using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using pWordLib.dat;
using System;
using System.Runtime.CompilerServices;

namespace OpNodeTest2
{
    [TestClass]
    public class UnitTest_BasicNode_ops
    {
        [TestMethod]
        public void Create_Root_OpNode()
        {
            var pRoot = new Mock<pWordLib.dat.pNode>();
            Assert.IsNotNull(pRoot);
        }

        // assert after set name and value to root
        [TestMethod]
        public void Set_Root_Name()
        {
            var pRoot = new Mock<TestablePNode>();
            pRoot.CallBase = true; // This will call the real implementation
            pRoot.Object.SetNameForTesting("MyName");
            Assert.AreEqual("MyName", pRoot.Object.GetNameForTesting());
        }

        // when creating root set both name and value
        [TestMethod]
        public void Create_Root_Name_Value()
        {
            var pRoot = new Mock<pWordLib.dat.pNode>("MyName", "MyValue");
            pRoot.CallBase = true;
            Assert.AreEqual("MyName", pRoot.Object.getName());
            Assert.AreEqual("MyValue", pRoot.Object.getValue());
        }

        // when creating root set both name and object value
        [TestMethod]
        public void Create_Root_Name_Object_Value()
        {
            var pRoot = new Mock<pWordLib.dat.pNode>("MyName", 123);
            pRoot.CallBase = true;
            Assert.AreEqual("MyName", pRoot.Object.getName());
            Assert.AreEqual(123, pRoot.Object.getValueObject());
        }

        // assert after set value to an object not a string
        [TestMethod]
        public void Set_Root_Value()
        {
            var pRoot = new Mock<pWordLib.dat.pNode>();
            pRoot.CallBase = true;
            pRoot.Object.setValueObject(123);
            Assert.AreEqual(123, pRoot.Object.getValueObject());
        }

        // assert after set name and value to a child of root
        [TestMethod]
        public void Set_Child_Name_Value()
        {
            var pRoot = new Mock<pWordLib.dat.pNode>();
            var pChild = new Mock<pWordLib.dat.pNode>();
            pRoot.CallBase = true;
            pChild.CallBase = true;
            pRoot.Object.setName("MyName");
            pChild.Object.setName("MyChild");
            pChild.Object.setValue("MyValue");
            pRoot.Object.Nodes.Add(pChild.Object);
            Assert.AreEqual("MyChild", pRoot.Object.getChild(0).getName());
            Assert.AreEqual("MyValue", pRoot.Object.getChild(0).getValue());
        }
    }
}
