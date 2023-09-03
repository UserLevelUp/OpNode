using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            pWordLib.dat.pNode pRoot = new pWordLib.dat.pNode();
            Assert.IsNotNull(pRoot);
        }

        // assert after set name and value to root
        [TestMethod]
        public void Set_Root_Name()
        {
            pWordLib.dat.pNode pRoot = new pWordLib.dat.pNode();
            pRoot.setName("MyName");
            Assert.AreEqual("MyName", pRoot.getName());
        }

        // when creating root set both name and value
        [TestMethod]
        public void Create_Root_Name_Value()
        {
            pWordLib.dat.pNode pRoot = new pWordLib.dat.pNode("MyName", "MyValue");
            Assert.AreEqual("MyName", pRoot.getName());
            Assert.AreEqual("MyValue", pRoot.getValue());
        }

        // when creating root set both name and object value
        [TestMethod]
        public void Create_Root_Name_Object_Value()
        {
            pWordLib.dat.pNode pRoot = new pWordLib.dat.pNode("MyName", 123);
            Assert.AreEqual("MyName", pRoot.getName());
            Assert.AreEqual(123, pRoot.getValueObject());
        }

        // assert after set value to an object not a string
        [TestMethod]
        public void Set_Root_Value()
        {
            pWordLib.dat.pNode pRoot = new pWordLib.dat.pNode();
            pRoot.setValueObject(123);
            Assert.AreEqual(123, pRoot.getValueObject());
        }

        // assert after set name and value to a child of root
        [TestMethod]
        public void Set_Child_Name_Value()
        {
            pWordLib.dat.pNode pRoot = new pWordLib.dat.pNode();
            pRoot.setName("MyName");
            pWordLib.dat.pNode pChild = new pWordLib.dat.pNode();
            pChild.setName("MyChild");
            pChild.setValue("MyValue");
            pRoot.Nodes.Add(pChild);
            Assert.AreEqual("MyChild", pRoot.getChild(0).getName());
            Assert.AreEqual("MyValue", pRoot.getChild(0).getValue());
        }

        // asert after set name and object value to a child of root
        [TestMethod]
        public void Set_Child_Name_Object_Value()
        {
            pWordLib.dat.pNode pRoot = new pWordLib.dat.pNode();
            pRoot.setName("MyName");
            pWordLib.dat.pNode pChild = new pWordLib.dat.pNode();
            pChild.setName("MyChild");
            pChild.setValueObject(123);
            pRoot.Nodes.Add(pChild);
            Assert.AreEqual("MyChild", pRoot.getChild(0).getName());
            Assert.AreEqual(123, pRoot.getChild(0).getValueObject());
        }


    }
}
