using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Windows.Forms;

namespace OpNodeTest2
{
    [TestClass]
    public class UnitTest1
    {
        private TestableLeftRight realUserControl;

        [TestInitialize]
        public void Init()
        {
            Castle.DynamicProxy.Generators.AttributesToAvoidReplicating.Add(typeof(System.Security.Permissions.UIPermissionAttribute));

            // Create a real instance of the UserControl.
            realUserControl = new TestableLeftRight();

            // Add the controls you want to test.
            realUserControl.Controls.Add(new Button()); // left button
            realUserControl.Controls.Add(new Button()); // right button
            realUserControl.Controls.Add(new TextBox()); // master node
        }

        [TestMethod]
        public void TestAddingItems()
        {
            // Arrange
            var leftRightControl = new Mock<TestableLeftRightWrapper> { CallBase = true };
            leftRightControl.SetupGet(x => x.Controls).Returns(realUserControl.Controls);
            // Act
            leftRightControl.Object.AddMasterItem("Item 1");
            leftRightControl.Object.AddMasterItem("Item 2");
            leftRightControl.Object.AddMasterItem("Item 3");

            // Assert
            Assert.AreEqual(3, leftRightControl.Object.MasterNames.Count);
        }

        [TestMethod]
        public void TestLeftButton()
        {
            // Arrange
            var leftRightControl = new Mock<TestableLeftRightWrapper> { CallBase = true };
            leftRightControl.SetupGet(x => x.Controls).Returns(realUserControl.Controls);
            leftRightControl.Object.AddMasterItem("Item 1");
            leftRightControl.Object.AddMasterValueItem("Value 1"); // add items to MastersValue
            leftRightControl.Object.AddMasterItem("Item 2");
            leftRightControl.Object.AddMasterValueItem("Value 2"); // add items to MastersValue
            leftRightControl.Object.AddMasterItem("Item 3");
            leftRightControl.Object.AddMasterValueItem("Value 3"); // add items to MastersValue
            leftRightControl.Object.SetInitialIndex(2);

            // Act
            leftRightControl.Object.SimulateLeftClick();

            // Assert
            Assert.AreEqual(1, leftRightControl.Object.GetIndex());
        }

        [TestMethod]
        public void TestRightButton()
        {
            // Arrange
            var mock = new Mock<EventHandler>();
            var leftRightControl = new Mock<TestableLeftRightWrapper> { CallBase = true };
            leftRightControl.SetupGet(x => x.Controls).Returns(realUserControl.Controls);
            leftRightControl.Object.AddMasterItem("Item 1");
            leftRightControl.Object.AddMasterValueItem("Value 1"); // add items to MastersValue
            leftRightControl.Object.AddMasterItem("Item 2");
            leftRightControl.Object.AddMasterValueItem("Value 2"); // add items to MastersValue
            leftRightControl.Object.AddMasterItem("Item 3");
            leftRightControl.Object.AddMasterValueItem("Value 3"); // add items to MastersValue
            leftRightControl.Object.SetInitialIndex(0);

            // Act
            leftRightControl.Object.SimulateRightClick();

            // Assert
            Assert.AreEqual(1, leftRightControl.Object.GetIndex());
        }

        [TestMethod]
        public void TestLeftButtonEventFired()
        {
            // Arrange
            var mock = new Mock<EventHandler>();
            var leftRightControl = new Mock<TestableLeftRightWrapper> { CallBase = true };
            leftRightControl.SetupGet(x => x.Controls).Returns(realUserControl.Controls);
            bool eventFired = false;
            leftRightControl.Object.LeftClicked += (sender, e) => { eventFired = true; };

            // Populate the Masters and MastersValue lists
            leftRightControl.Object.MasterNames.Add("Item 1");
            leftRightControl.Object.MasterNodes.Add(new pWordLib.dat.pNode("Item 1", "Value 1"));
            leftRightControl.Object.MasterNames.Add("Item 2");
            leftRightControl.Object.MasterNodes.Add(new pWordLib.dat.pNode("Item 2", "Value 2"));
            leftRightControl.Object.index = 1;  // Set initial index to a valid value

            // Act
            leftRightControl.Object.btnLeft_Click(null, EventArgs.Empty);

            // Assert
            Assert.IsTrue(eventFired);
            Assert.AreEqual(0, leftRightControl.Object.index);
        }

        [TestMethod]
        public void TestLeftButtonEventFiredWithMoq()
        {
            // Arrange
            var mock = new Mock<EventHandler>();
            var leftRightControl = new Mock<TestableLeftRightWrapper> { CallBase = true };
            leftRightControl.SetupGet(x => x.Controls).Returns(realUserControl.Controls);

            // Populate the Masters and MastersValue lists
            leftRightControl.Object.MasterNames.Add("Item 1");
            leftRightControl.Object.MasterNodes.Add(new pWordLib.dat.pNode("Item 1", "Value 1"));
            leftRightControl.Object.MasterNames.Add("Item 2");
            leftRightControl.Object.MasterNodes.Add(new pWordLib.dat.pNode("Item 2", "Value 2"));
            leftRightControl.Object.index = 1;  // Set initial index to a valid value

            leftRightControl.Object.LeftClicked += mock.Object;

            // Act
            leftRightControl.Object.btnLeft_Click(null, EventArgs.Empty);

            // Assert
            mock.Verify(handler => handler(It.IsAny<object>(), It.IsAny<EventArgs>()), Times.Once());
        }

    }
}
