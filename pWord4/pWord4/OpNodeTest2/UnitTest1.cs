using LeftRight;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace OpNodeTest2
{
    [TestClass]
    public class UnitTest1
    {

        [TestInitialize]
        public void Init()
        {
            Castle.DynamicProxy.Generators.AttributesToAvoidReplicating.Add(typeof(System.Security.Permissions.UIPermissionAttribute));
        }

    [TestMethod]
        public void TestAddingItems()
        {
            // Arrange
            var leftRightControl = new LeftRight.LeftRight();

            // Act
            leftRightControl.Masters.Add("Item 1");
            leftRightControl.Masters.Add("Item 2");
            leftRightControl.Masters.Add("Item 3");

            // Assert
            Assert.AreEqual(3, leftRightControl.Masters.Count);
        }

        [TestMethod]
        public void TestLeftButton()
        {
            // Arrange
            var leftRightControl = new TestableLeftRight();
            leftRightControl.AddMasterItem("Item 1");
            leftRightControl.AddMasterValueItem("Value 1"); // add items to MastersValue
            leftRightControl.AddMasterItem("Item 2");
            leftRightControl.AddMasterValueItem("Value 2"); // add items to MastersValue
            leftRightControl.AddMasterItem("Item 3");
            leftRightControl.AddMasterValueItem("Value 3"); // add items to MastersValue
            leftRightControl.SetInitialIndex(2);

            // Act
            leftRightControl.SimulateLeftClick();

            // Assert
            Assert.AreEqual(1, leftRightControl.GetIndex());
        }

        [TestMethod]
        public void TestRightButton()
        {
            // Arrange
            var leftRightControl = new TestableLeftRight();
            leftRightControl.AddMasterItem("Item 1");
            leftRightControl.AddMasterValueItem("Value 1"); // add items to MastersValue
            leftRightControl.AddMasterItem("Item 2");
            leftRightControl.AddMasterValueItem("Value 2"); // add items to MastersValue
            leftRightControl.AddMasterItem("Item 3");
            leftRightControl.AddMasterValueItem("Value 3"); // add items to MastersValue
            leftRightControl.SetInitialIndex(0);

            // Act
            leftRightControl.SimulateRightClick();

            // Assert
            Assert.AreEqual(1, leftRightControl.GetIndex());
        }
    }
}
