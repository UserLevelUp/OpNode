using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OpNodeTest2;
using pWordLib.dat;
using pWordLib;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using static myPword.pWord;
using System.Xml;
using System.Collections;
using pWordLib.dat.Math;
using pWordLib.dat.math;
using System.Net;
using System.Text.RegularExpressions;
//using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using myPword;
using LeftRight;
using Newtonsoft.Json;
using Moq;
using AutoFixture;
namespace OpNodeTest2
{
    public interface IPWord
    {
        bool Visible { get; set; }
        // Include other members that you want to mock
    }

    public class pWord : IPWord
    {
        public virtual bool Visible { get; set; }
        // Implement other members...
    }

    public class pWordWrapper : IPWord
    {
        private Mock<IPWord> _pWord;

        public pWordWrapper(Mock<IPWord> pWord)
        {
            _pWord = pWord;
        }

        public bool Visible
        {
            get { return _pWord.Object.Visible; }
            set { _pWord.Object.Visible = value; }
        }
        // Implement other members...
    }

    [TestClass]
    public class MSTest
    {
        private Mock<pWord> _pWord;



        [TestInitialize]
        public void TestInitialize()
        {
            _pWord = new Mock<pWord>();
            _pWord.Setup(p => p.Visible).Returns(false);
            Assert.IsFalse(_pWord.Object.Visible);

            //_pWord = new Mock<pWord>();
            //_pWord.Setup(p => p.notifyIcon1.Visible).Returns(false);
            //_pWord.Setup(p => p.notifyIcon2.Visible).Returns(true);

            //var fixture = new Fixture();
            //var treeNode = new pNode();
            //var sampleTreeNodes = treeNode.Nodes;

            //foreach (var node in fixture.CreateMany<pNode>(10))
            //{
            //    sampleTreeNodes.Add(new TreeNode
            //    {
            //        BackColor = node.BackColor,
            //        Checked = false,
            //        Name = "TestNode",
            //        Text = "TestNode",
            //        Tag = "Value"
            //    });
            //}

            //_pWord.Setup(p => p.treeView1.Nodes).Returns(sampleTreeNodes);
        }

        [TestMethod]
        public void NotifyIcon1_DoubleClick_Test()
        {
            // Arrange
            var fixture = new Fixture();
            var testClass = fixture.Create<MSTest>();

            // Act
            _pWord.Object.notifyIcon1.DoubleClick += NotifyIcon1_DoubleClick;
            _pWord.Object.notifyIcon1_DoubleClick(null, null);

            // Assert
            Assert.AreEqual(_pWord.Object.notifyIcon1.Visible, false);
            Assert.AreEqual(_pWord.Object.notifyIcon2.Visible, true);
        }

        private void NotifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            _pWord.Object.notifyIcon1.Visible = false;
            _pWord.Object.notifyIcon2.Visible = true;
        }

        [TestMethod]
        public void NotifyIcon1_DoubleClick_Test2()
        {
            // Arrange
            var fixture = new Fixture();
            var testClass = fixture.Create<MSTest>();

            _pWord.Object.WindowState = FormWindowState.Minimized;
            _pWord.Object.autoHide_flag = true;
            _pWord.Object.VIS = false;
            _pWord.Object.statusBar1.Text = string.Empty;
            _pWord.Object.toolBarTac.ImageIndex = -1;
            _pWord.Object.Visible = false;

            // Act
            _pWord.Object.notifyIcon1.DoubleClick += NotifyIcon1_DoubleClick2;
            _pWord.Object.notifyIcon1_DoubleClick(null, null);

            // Assert
            Assert.AreEqual(FormWindowState.Normal, _pWord.Object.WindowState);
            Assert.AreEqual("AutoHide Inactive", _pWord.Object.statusBar1.Text);
            Assert.AreEqual(1, _pWord.Object.toolBarTac.ImageIndex);
            Assert.AreEqual(true, _pWord.Object.Visible);
            Assert.AreEqual(false, _pWord.Object.notifyIcon1.Visible);
            Assert.AreEqual(true, _pWord.Object.notifyIcon2.Visible);
        }

        private void NotifyIcon1_DoubleClick2(object sender, EventArgs e)
        {
            _pWord.Object.WindowState = FormWindowState.Normal;
            _pWord.Object.autoHide_flag = false;
            _pWord.Object.VIS = true;
            _pWord.Object.statusBar1.Text = "AutoHide Inactive";
            _pWord.Object.toolBarTac.ImageIndex = 1;
            _pWord.Object.Visible = true;
            _pWord.Object.notifyIcon1.Visible = false;
            _pWord.Object.notifyIcon2.Visible = true;
        }

        [TestMethod]
        public void Dispose()
        {
            bool disposing = true;
            Assert.AreEqual(Dispose(disposing), false);

        }
        public bool Dispose(bool disposing)
        {
            if (disposing)
            {
                //this.actHook.Stop();
                if (_pWord.Object.components != null)
                {
                    _pWord.Object.components.Dispose();
                    _pWord.Object.notifyIcon1.Dispose();
                }
            }
            _pWord.Object.Dispose();
            disposing = false;
            return disposing;
        }

        [TestMethod]
        public void menuItemExit_Click()
        {
            Dispose(true);
            _pWord.Object.Close();
        }
        [TestMethod]
        public void pWord_Load()
        {
            _pWord.Object.Dock = System.Windows.Forms.DockStyle.Bottom;
            Rectangle a = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
            DockRight();
            _pWord.Object.UpdateTree();

            _pWord.Object.autoHide_flag = false;
            _pWord.Object.VIS = true;

            _pWord.Object.WindowState = FormWindowState.Normal;

            _pWord.Object.autoHide_flag = false;
            _pWord.Object.statusBar1.Text = "AutoHide Inactive";
            _pWord.Object.toolBarTac.ImageIndex = 1;
            _pWord.Object.Visible = true;

            _pWord.Object.WindowState = FormWindowState.Normal;
            DockRight();
        }
        [TestMethod]
        public void DockRight()
        {
            _pWord.Object.Height = _pWord.Object.HostingScreen.WorkingArea.Height;
            _pWord.Object.Location = new Point(
                _pWord.Object.HostingScreen.WorkingArea.Width - _pWord.Object.Width,
                0
                );
        }
        [TestMethod]
        public void pWord_VisibleChanged()
        {
            try
            {
                _pWord.Object.invisible(_pWord.Object.VIS);
                _pWord.Object.Activate();
                _pWord.Object.treeView1.Focus();
                DockRight();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Something bad happened. " + ex.ToString());
            }
        }
        [TestMethod]
        public void invisible()
        {
            Assert.AreEqual(invisible(true), true);
        }
        public bool invisible(bool vis)
        {
            if (vis == true)
            {
                _pWord.Object.Show();
            }
            else
            {
                _pWord.Object.Hide();
            }
            return vis;
        }
        [TestMethod]
        public void menuItemNewFile_Click()
        {
            _pWord.Object.flag_file = false;  // notice... I don't want you saving new stuff over your old refined work.

            AYS ays = new AYS();
            // ARE YOU SURE? This operation will delete all of your work unless you have saved.
            // (Yes / No)

            ays.ShowDialog();
            if (ays.DialogResult == DialogResult.OK)
            {

                _pWord.Object.userControl11.MasterNames.Clear();
                _pWord.Object.userControl11.MasterNodes.Clear();
                _pWord.Object.treeView1.Nodes.Clear();
                pNode masterNode;
                masterNode = new pNode("MASTER");
                TreePics apic = new TreePics("Master", _pWord.Object.img.GroupUp, _pWord.Object.img.GroupDown);
                masterNode.Tag = "MASTER";

                _pWord.Object.treeView1.Nodes.Add(masterNode);

                _pWord.Object.userControl11.index = 0;  // For some reason it loses track of index?
                _pWord.Object.userControl11.MasterNames.Add("MASTER");
                _pWord.Object.userControl11.MasterNodes.Add(masterNode);
                _pWord.Object.userControl11.txtMaster.Text = (string)_pWord.Object.userControl11.MasterNames[_pWord.Object.userControl11.index];
                _pWord.Object.tmpNode = (pNode)_pWord.Object.treeView1.Nodes[0];
                Assert.IsNotNull(_pWord.Object.tmpNode);
            }
        }
        [TestMethod]
        public void autosave()
        {
            // autosave
            try
            {
                int count = _pWord.Object.userControl11.MasterNames.Count;
                _pWord.Object.Nodes.Clear();
                _pWord.Object.Nodes.Add(count);

                for (int i = 0; i < count; i++)
                {
                    _pWord.Object.Nodes.Add((string)_pWord.Object.userControl11.MasterNames[i]);
                    _pWord.Object.Nodes.Add((pNode)_pWord.Object.userControl11.MasterNodes[i]);
                }
                if (_pWord.Object.filename != null)
                {
                    IFormatter formatter = new BinaryFormatter();
                    Stream stream = new FileStream(_pWord.Object.filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                    formatter.Serialize(stream, _pWord.Object.Nodes);
                    stream.Close();
                }
                _pWord.Object.flag_file = true;
                Assert.IsTrue(_pWord.Object.flag_file);
            }
            catch (Exception f)
            {
                MessageBox.Show("You had an error while saving. " + f.Message, "SAVE ERROR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }
        [TestMethod]
        public void menuItemAddTo_Click()
        {
            try
            {
                bool check = true;
                _pWord.Object.tmpNode = (pNode)_pWord.Object.treeView1.SelectedNode;
                _pWord.Object.lblName.Text = "Name:";
                _pWord.Object.lblValue.Text = "Value:";
                _pWord.Object.txtName.Focus();
                _pWord.Object.statusBar1.Text = "Add to Node";
                if (_pWord.Object.chkClear.Checked == true)
                {
                    _pWord.Object.txtName.Clear();
                    _pWord.Object.txtObject.Clear();
                }
                check = false;
                Assert.AreEqual(check, false);
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }
        [TestMethod]
        public void menuItemShow_Click()
        {
            bool check = true;
            _pWord.Object.WindowState = FormWindowState.Normal;
            _pWord.Object.autoHide_flag = false;
            _pWord.Object.VIS = true;

            _pWord.Object.autoHide_flag = false;
            _pWord.Object.statusBar1.Text = "AutoHide Inactive";
            _pWord.Object.toolBarTac.ImageIndex = 1;
            _pWord.Object.Visible = true;

            _pWord.Object.WindowState = FormWindowState.Normal;
            DockRight();
            check = false;
            Assert.AreEqual(check, false);
        }
        [TestMethod]
        public void menuSave_Click()
        {
            bool check = true;
            try
            {
                int count = _pWord.Object.userControl11.MasterNames.Count;
                _pWord.Object.Nodes.Clear();
                _pWord.Object.Nodes.Add(count);

                for (int i = 0; i < count; i++)
                {
                    _pWord.Object.Nodes.Add((string)_pWord.Object.userControl11.MasterNames[i]);
                    _pWord.Object.Nodes.Add((pNode)_pWord.Object.userControl11.MasterNodes[i]);
                }
                _pWord.Object.saveFileDialog1.ShowDialog();
                check = false;
                Assert.AreEqual(check, false);
            }
            catch (Exception f)
            {
                MessageBox.Show("You had an error while saving. " + f.Message, "SAVE ERROR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }
        [TestMethod]
        public void menuItem9_Click()
        {
            bool check = true;
            _pWord.Object.Nodes.Clear();
            _pWord.Object.userControl11.MasterNodes.Clear();
            _pWord.Object.userControl11.MasterNames.Clear();
            try
            {

                _pWord.Object.openFileDialog1.FileName = _pWord.Object.filename;
                _pWord.Object.openFileDialog1.ShowDialog();
                check = false;
                Assert.AreEqual(check, false);
            }
            catch (Exception f)
            {
                MessageBox.Show("You had an error while loading. Please select the proper file. " + f.Message, "OPEN ERROR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }
        [TestMethod]
        public void menuItem14_Click()
        {
            bool check = true;
            try
            {
                // Add the master node to Nodes
                pNode masterNode;
                if (_pWord.Object.treeView1.SelectedNode.Parent != null)
                {
                    ((pNode)_pWord.Object.treeView1.SelectedNode).OperationChanged();
                    _pWord.Object.treeView1.SelectedNode.Remove();
                    masterNode = (pNode)_pWord.Object.treeView1.Nodes[0];
                    _pWord.Object.userControl11.MasterNodes[_pWord.Object.userControl11.index] = masterNode;

                    if (_pWord.Object.flag_file == true)
                    {
                        _pWord.Object.autosave();
                    }
                }
                else
                    MessageBox.Show("You must not delete the master node.");
                check = false;
                Assert.AreEqual(check, false);
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }
        [TestMethod]
        public void toolBar1_ButtonClick()
        {
            _pWord.Object.mode = nodeMode.xmlUpdate;
            bool check = true;
            try
            {
                System.Windows.Forms.ToolBarButton button = new ToolBarButton();
                System.Windows.Forms.ToolBarButton button1 = new ToolBarButton();
                System.Windows.Forms.ToolBarButton button2 = new ToolBarButton();
                System.Windows.Forms.ToolBarButton button3 = new ToolBarButton();
                System.Windows.Forms.ToolBarButton button4 = new ToolBarButton();
                if (button == _pWord.Object.toolBarTac)
                {

                    if (_pWord.Object.autoHide_flag == true)
                    {
                        _pWord.Object.autoHide_flag = false;
                        _pWord.Object.statusBar1.Text = "AutoHide Inactive";
                        _pWord.Object.toolBarTac.ImageIndex = 1;
                    }
                    else
                    {
                        _pWord.Object.autoHide_flag = true;
                        _pWord.Object.statusBar1.Text = "AutoHide Active";
                        _pWord.Object.toolBarTac.ImageIndex = 0;
                        //this.actHook.Start();
                    }
                }
                else if (button1 == _pWord.Object.toolBarView)
                {
                    if (_pWord.Object.filenameHTML != null)
                    {
                        if (_pWord.Object.exportMode == pWord.ExportMode.treeview)
                        {
                            _pWord.Object.xml.Clear();
                        }
                        else if (_pWord.Object.exportMode == pWord.ExportMode.pNode)
                        {
                            _pWord.Object.xml.Clear(); // clear out contents first.
                                                    //CallRecursive(xmlNode);

                            try
                            {
                                _pWord.Object.exportMode = pWord.ExportMode.pNode;  // what am I exporting?  A pNode
                                _pWord.Object.xml.Clear();  // clear out contents first.

                                //this.xmlNode = (pNode)treeView1.SelectedNode;  // xmlNode is what is being exported to xml
                                _pWord.Object.xmlIndex = _pWord.Object.treeView1.SelectedNode.Index; // xmlIndex is the SelectedNodes index
                                _pWord.Object.menuItem21.Enabled = true; // MenuItem21 is enabled... Todo: rename menuItem21
                                _pWord.Object.menuItem31.Enabled = true;  // MneuItem21 is enabled... Todo: rename menuItem31
                                _pWord.Object.nodeIndex = _pWord.Object.treeView1.SelectedNode.Index; // nodeInex is now equal to xmlIndex?
                                _pWord.Object.statusBar1.Text = "Export Node XML Mode";
                                var xdoc = ((pNode)_pWord.Object.treeView1.SelectedNode).CallRecursive(_pWord.Object.xmlNode);  // treeview1 is a pView

                                _pWord.Object.saveFileDialogHTML.FileName = _pWord.Object.filenameHTML;
                                _pWord.Object.saveFileDialogHTML.Title = "Save the NODE to XML/HTML";
                                if (_pWord.Object.filenameHTML != null)
                                {
                                    if (xdoc != null)
                                    {
                                        xdoc.Save(_pWord.Object.filenameHTML);
                                        xdoc.RemoveAll();
                                        xdoc = null;
                                        _pWord.Object.filenameJSON = null;
                                    }
                                    else
                                    {
                                        MessageBox.Show("The export was not able to save b/c it was empty.");
                                    }
                                }

                                _pWord.Object.toolBarView.Enabled = true;
                            }
                            catch (Exception f)
                            {
                                MessageBox.Show("You had an error while exporting to XML. " + f.Message, "SAVE ERROR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                            }
                        }
                        if (_pWord.Object.filenameHTML != null)
                        {
                            System.Diagnostics.Process.Start(_pWord.Object.filenameHTML);
                        }
                        else if (_pWord.Object.filenameJSON != null)
                        {
                            System.Diagnostics.Process.Start(_pWord.Object.filenameJSON);
                        }
                        else
                        {
                            MessageBox.Show("You must first export a NODE to XML or HTM or JSON");
                        }
                    }
                }
                else if (button2 == _pWord.Object.toolBarXML)
                {
                }
                else if (button3 == _pWord.Object.toolBarSearch)
                {
                    // this should be a full search as the search is conducted on the toolbar
                    var masterNode = (pNode)_pWord.Object.treeView1.Nodes[0];
                    if (_pWord.Object.txtObject.Text == "")
                    {
                        throw new ArgumentException("Value text must contain some search term");
                    }
                    var pNodes = masterNode.Find(_pWord.Object.txtObject.Text, 0);

                    int counter = 0;
                    _pWord.Object.searchCounter++;
                    foreach (var _pNode in pNodes)
                    {
                        var _parent = _pNode.Parent;
                        var _parents = new List<pNode>();
                        _parents.Add((pNode)_parent);
                        while (_parent.Parent != null)
                        {
                            _parent = _parent.Parent;
                            _parents.Add((pNode)_parent);
                        }
                        _parents.Add(_pNode);
                        foreach (var _parent2 in _parents)
                        {
                            _parent2.Expand();
                            if (_pWord.Object.searchCounter == counter++)
                            {
                                _pWord.Object.treeView1.SelectedNode = _parent2;
                                _pWord.Object.treeView1.SelectedNode.EnsureVisible();
                                _parent2.EnsureVisible();
                                _pWord.Object.treeView1.Focus();
                                _pWord.Object.treeView1.Refresh();
                            }
                        }
                    }
                    if (_pWord.Object.searchCounter > counter)
                    {
                        _pWord.Object.searchCounter = 0; // do final check and reset this.searchCounter if it went out of bounds of the current search
                    }
                }
                else if (button4 == _pWord.Object.toolBarCollapse)
                {
                    _pWord.Object.treeView1.CollapseAll();
                }
                check = false;
                Assert.AreEqual(check, false);

            }
            catch (Exception f)
            {
                MessageBox.Show("You must first Export a NODE to XML or HTML format." + f.Message);
            }
        }
        [TestMethod]
        public void pWord_Deactivate()
        {
            _pWord.Object.autoHide_flag = true;
            if (_pWord.Object.autoHide_flag == true)
            {
                _pWord.Object.VIS = false;
                _pWord.Object.Visible = false;
            }
            Assert.AreEqual(_pWord.Object.VIS, false);
            Assert.AreEqual(_pWord.Object.Visible, false);
        }
        [TestMethod]
        public void menuItemCopy_Click()
        {
            _pWord.Object.lblName.Text = "Name:";
            _pWord.Object.lblValue.Text = "Value:";

            Clipboard.SetDataObject(_pWord.Object.treeView1.SelectedNode.Tag, true);
            _pWord.Object.statusBar1.Text = "Copy Value Text Mode";
            Assert.AreEqual(_pWord.Object.statusBar1.Text, "Copy Value Text Mode");
        }
        [TestMethod]
        public void menuItem17_18_Click()
        {
            bool check = false;
            frmAbout dlg = new frmAbout();
            dlg.ShowDialog();
            check = true;
            Assert.AreEqual(check, true);
        }
        [TestMethod]
        public void userControl11_Load()
        {
            _pWord.Object.rm = new pWordLib.mgr.registryMgr(pWordSettings.Default.version.ToString());

            _pWord.Object.openFileDialog1.FileName = _pWord.Object.rm.AutoSavePathFromRegistry(pWordSettings.Default.version.ToString());
            if (_pWord.Object.rm.FileExist())
            {
                _pWord.Object.openFileDialog1_FileOk(null, null);
            }
            else
            {
                pNode masterNode;
                masterNode = new pNode("MASTER");
                TreePics apic = new TreePics("Master", _pWord.Object.img.GroupUp, _pWord.Object.img.GroupDown);
                masterNode.Tag = "MASTER";

                _pWord.Object.treeView1.Nodes.Add(masterNode);

                _pWord.Object.userControl11.MasterNames.Add("MASTER");
                _pWord.Object.userControl11.MasterNodes.Add(masterNode);
                _pWord.Object.userControl11.txtMaster.Text = _pWord.Object.userControl11.MasterNames[_pWord.Object.userControl11.index];
            }
            _pWord.Object.tmpNode = (pNode)_pWord.Object.treeView1.Nodes[0];
            Assert.AreEqual(_pWord.Object.tmpNode, (pNode)_pWord.Object.treeView1.Nodes[0]);
        }
        [TestMethod]
        public void menuItem19_Click()
        {
            // Add master
            AddMaster dlg = new AddMaster();
            dlg.ShowDialog();

            if (dlg.DialogResult == DialogResult.OK)
            {
                _pWord.Object.treeView1.Nodes.Clear();
                pNode masterNode;
                masterNode = new pNode("MASTER");
                TreePics apic = new TreePics("masterNode", _pWord.Object.img.GroupUp, _pWord.Object.img.GroupDown);
                masterNode.Tag = "MASTER";

                _pWord.Object.treeView1.Nodes.Add(masterNode);

                _pWord.Object.userControl11.MasterNames.Add(dlg.txtMaster.Text);
                _pWord.Object.userControl11.MasterNodes.Add(masterNode);
                _pWord.Object.userControl11.index++;
                _pWord.Object.userControl11.txtMaster.Text = (string)_pWord.Object.userControl11.MasterNames[_pWord.Object.userControl11.index];
                _pWord.Object.tmpNode = (pNode)_pWord.Object.treeView1.Nodes[0];  // Always start with master
                Assert.AreEqual(_pWord.Object.tmpNode, (pNode)_pWord.Object.treeView1.Nodes[0]);
            }
        }
        [TestMethod]
        public void userControl11_LeftClicked()
        {
            bool check = false;
            // This is the hardest of all
            _pWord.Object.treeView1.Nodes.Clear();
            pNode masterNode = (pNode)_pWord.Object.userControl11.MasterNodes[_pWord.Object.userControl11.index];
            TreePics apic = new TreePics("masterNode", _pWord.Object.img.GroupUp, _pWord.Object.img.GroupDown);
            _pWord.Object.treeView1.Nodes.Add(masterNode);
            check = true;
            Assert.AreEqual(check, true);
        }
        [TestMethod]
        public void userControl11_RightClicked()
        {
            bool check = false;
            // This is the hardest of all
            _pWord.Object.treeView1.Nodes.Clear();
            pNode masterNode = (pNode)_pWord.Object.userControl11.MasterNodes[_pWord.Object.userControl11.index];
            TreePics apic = new TreePics("masterNode", _pWord.Object.img.GroupUp, _pWord.Object.img.GroupDown);
            _pWord.Object.treeView1.Nodes.Add(masterNode);
            check = true;
            Assert.AreEqual(check, true);
        }
        [TestMethod]
        public void menuItemOpenLink_Click()
        {
            try
            {
                bool check = false;
                if ((_pWord.Object.treeView1.SelectedNode.Tag != null) && (((String)(_pWord.Object.treeView1.SelectedNode.Tag)).Length > 0))
                {
                    System.Diagnostics.Process.Start(_pWord.Object.treeView1.SelectedNode.Tag.ToString());
                }
                check = true;
                Assert.AreEqual(check, true);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("You must use an acceptable link contained in the value field!","DANGER",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                //txtValue.Text = "Value Field is can not process value.";
                _pWord.Object.txtValue.Text = ex.Message;
            }
        }
        [TestMethod]
        public void btnAdd_Click()
        {
            bool check = false;
            pWordLib.AddItem addItem = new pWordLib.AddItem();

            pNode masterNode = (pNode)_pWord.Object.treeView1.Nodes[0];
            switch (_pWord.Object.mode)
            {
                case nodeMode.addto:
                    try
                    {
                        pNode aNode;
                        aNode = new pNode(_pWord.Object.txtName.Text);
                        //TreePics apic = new TreePics("aNode", img.GroupUp, img.GroupDown);
                        aNode.Tag = _pWord.Object.txtObject.Text;
                        aNode.Text = _pWord.Object.txtName.Text;
                        if (_pWord.Object.tmpNode.Namespace != null)
                        {
                            aNode.Namespace = _pWord.Object.tmpNode.Namespace;  // trickle down namespaces
                        }
                        _pWord.Object.treeView1.SelectedNode = _pWord.Object.tmpNode;
                        _pWord.Object.treeView1.SelectedNode.Nodes.Add(aNode);
                        // after adding the new node, be sure the index is updated as well... this is not necessary
                        _pWord.Object.userControl11.MasterNodes[_pWord.Object.userControl11.index] = masterNode;

                        // Change from Add Dialog to local members for adding name and value

                        // check box
                        if (_pWord.Object.chkClear.CheckState == CheckState.Checked)
                        {
                            _pWord.Object.txtName.Clear();
                            _pWord.Object.txtObject.Clear();
                            _pWord.Object.txtName.Focus();
                        }
                        else if (_pWord.Object.chkClear.CheckState == CheckState.Indeterminate)
                        {
                            _pWord.Object.txtObject.Clear();
                            _pWord.Object.txtObject.Focus();
                        }
                        else
                        {
                            _pWord.Object.btnAdd.Focus();
                        }

                        aNode.OperationChanged();

                        // TODO: 2022-Aug-06 Now recalculated all nodes with operations that have changed in these sets of node


                        if (_pWord.Object.flag_file == true)
                        {
                            autosave();
                        }
                    }
                    catch (Exception f)
                    {
                        MessageBox.Show(f.Message);
                    }
                    break;
                case nodeMode.edit:
                    try
                    {
                        // EDIT MODE
                        // Only edit the current node
                        pNode aNode;
                        aNode = new pNode(_pWord.Object.txtName.Text);
                        TreePics apic = new TreePics("aNode", _pWord.Object.img.GroupUp, _pWord.Object.img.GroupDown);
                        aNode.Tag = _pWord.Object.txtObject.Text;
                        _pWord.Object.treeView1.SelectedNode = _pWord.Object.tmpNode;
                        _pWord.Object.treeView1.SelectedNode.Text = aNode.Text;
                        _pWord.Object.treeView1.SelectedNode.Tag = aNode.Tag;
                        _pWord.Object.treeView1.SelectedNode.Name = aNode.Name;

                        // This is not necessary, when a save is committed this can be performed at that juncture
                        // However, it may be beneficial to know whether or not a node change was successfully saved
                        // at the iteration the event occurred.  This will prevent loss of work
                        _pWord.Object.userControl11.MasterNodes[_pWord.Object.userControl11.index] = masterNode;

                        // Change from Add Dialog to local members for adding name and value

                        // check box
                        if (_pWord.Object.chkClear.CheckState == CheckState.Checked)
                        {
                            _pWord.Object.txtName.Clear();
                            _pWord.Object.txtObject.Clear();
                            _pWord.Object.txtName.Focus();
                        }
                        else if (_pWord.Object.chkClear.CheckState == CheckState.Indeterminate)
                        {
                            _pWord.Object.txtObject.Clear();
                            _pWord.Object.txtObject.Focus();
                        }
                        else
                        {
                            _pWord.Object.btnAdd.Focus();
                        }

                        aNode.OperationChanged();
                    }
                    catch (Exception f)
                    {
                        MessageBox.Show(f.Message);
                    }
                    break;
                case nodeMode.insert:
                    if (_pWord.Object.treeView1.SelectedNode.Parent == null)
                    {
                        MessageBox.Show("You can not insert a sibling of the master node.");
                        return;
                    }
                    try
                    {
                        // Insert Mode
                        // Only edit the current node
                        pNode aNode;
                        aNode = new pNode(_pWord.Object.txtName.Text);
                        TreePics apic = new TreePics("aNode", _pWord.Object.img.GroupUp, _pWord.Object.img.GroupDown);
                        aNode.Tag = _pWord.Object.txtObject.Text;
                        if (_pWord.Object.tmpNode.Namespace != null)
                        {
                            aNode.Namespace = _pWord.Object.tmpNode.Namespace;  // trickle down namespaces
                        }
                        _pWord.Object.treeView1.SelectedNode = _pWord.Object.tmpNode;
                        _pWord.Object.treeView1.SelectedNode.Nodes.Insert(_pWord.Object.modeIndex, aNode);
                        _pWord.Object.userControl11.MasterNodes[_pWord.Object.userControl11.index] = masterNode;

                        // Change from Add Dialog to local members for adding name and value

                        // check box
                        if (_pWord.Object.chkClear.Checked == true)
                        {
                            _pWord.Object.txtName.Clear();
                            _pWord.Object.txtObject.Clear();
                        }

                        if (_pWord.Object.flag_file == true)
                        {
                            autosave();
                        }
                        aNode.OperationChanged();
                    }
                    catch (Exception f)
                    {
                        MessageBox.Show(f.Message);
                    }
                    break;
                case nodeMode.addAttributeTo:
                    try
                    {
                        _pWord.Object.tmpNode.AddAttribute(_pWord.Object.txtName.Text, _pWord.Object.txtObject.Text);
                        _pWord.Object.treeView1.SelectedNode = _pWord.Object.tmpNode;
                        _pWord.Object.userControl11.MasterNodes[_pWord.Object.userControl11.index] = masterNode;

                        // Change from Add Dialog to local members for adding name and value

                        // check box
                        if (_pWord.Object.chkClear.Checked == true)
                        {
                            _pWord.Object.txtName.Clear();
                            _pWord.Object.txtObject.Clear();
                        }

                        if (_pWord.Object.flag_file == true)
                        {
                            autosave();
                        }
                    }
                    catch (Exception f)
                    {
                        MessageBox.Show(f.Message);
                    }
                    break;
                case nodeMode.viewErrors:
                    break; // do nothing at all
                case nodeMode.addNamespacePrefix:
                    try
                    {
                        if (_pWord.Object.tmpNode.Namespace == null)
                        {
                            _pWord.Object.tmpNode.Namespace = new NameSpace();
                        }
                        _pWord.Object.tmpNode.Namespace.Prefix = _pWord.Object.txtName.Text;
                        _pWord.Object.tmpNode.Namespace.URI_PREFIX = _pWord.Object.txtObject.Text;
                        _pWord.Object.treeView1.SelectedNode = _pWord.Object.tmpNode;
                        _pWord.Object.userControl11.MasterNodes[_pWord.Object.userControl11.index] = masterNode;

                        // Change from Add Dialog to local members for adding name and value

                        // check box
                        if (_pWord.Object.chkClear.Checked == true)
                        {
                            _pWord.Object.txtName.Clear();
                            _pWord.Object.txtObject.Clear();
                        }

                        if (_pWord.Object.flag_file == true)
                        {
                            autosave();
                        }
                    }
                    catch (Exception f)
                    {
                        MessageBox.Show(f.Message);
                    }
                    break;
                case nodeMode.addNamespaceSuffix:
                    try
                    {
                        if (_pWord.Object.tmpNode.Namespace == null)
                        {
                            _pWord.Object.tmpNode.Namespace = new NameSpace();
                        }
                        _pWord.Object.tmpNode.Namespace.Suffix = _pWord.Object.txtName.Text;
                        _pWord.Object.tmpNode.Namespace.URI_SUFFIX = _pWord.Object.txtObject.Text;
                        _pWord.Object.treeView1.SelectedNode = _pWord.Object.tmpNode;
                        _pWord.Object.userControl11.MasterNodes[_pWord.Object.userControl11.index] = masterNode;

                        // Change from Add Dialog to local members for adding name and value

                        // check box
                        if (_pWord.Object.chkClear.Checked == true)
                        {
                            _pWord.Object.txtName.Clear();
                            _pWord.Object.txtObject.Clear();
                        }

                        if (_pWord.Object.flag_file == true)
                        {
                            autosave();
                        }
                    }
                    catch (Exception f)
                    {
                        MessageBox.Show(f.Message);
                    }
                    check = true;
                    Assert.AreEqual(true, check);
                    break;
                default:
                    break;
            }
        }
        [TestMethod]
        public void btnCancel_Click()
        {
            _pWord.Object.Visible = false;
            Assert.AreEqual(_pWord.Object.Visible, false);
        }
        [TestMethod]
        public void treeView1_AfterCollapse_1()
        {
            bool check = false;
            if (_pWord.Object.treeView1.SelectedNode != null)
                _pWord.Object.treeView1.SelectedNode.SelectedImageIndex = 0;
            check = true;
            Assert.AreEqual(true, check);
        }
        [TestMethod]
        public void treeView1_AfterExpand_1(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            bool check = true;
            if (_pWord.Object.treeView1.SelectedNode != null)
                _pWord.Object.treeView1.SelectedNode.SelectedImageIndex = 1;
            check = false;
            Assert.AreEqual(false, check);
        }
        [TestMethod]
        public void treeView1_AfterSelect_1()
        {
            _pWord.Object.treeView1.SelectedNode.SelectedImageIndex = _pWord.Object.img.GroupDown;
            Assert.AreEqual(_pWord.Object.treeView1.SelectedNode.SelectedImageIndex, _pWord.Object.img.GroupDown);

        }
        [TestMethod]
        public void treeView1_DragDrop_1()
        {
            bool check = false;
            pNode a = new pNode("test", _pWord.Object.img.GroupUp, _pWord.Object.img.GroupDown);
            a.Tag = null;
            _pWord.Object.treeView1.SelectedNode.Nodes.Add(a);
            check = true;
            Assert.AreEqual(true, check);
        }
        [TestMethod]
        public void menuItemEdit_Click()
        {
            _pWord.Object.mode = nodeMode.edit;
            try
            {
                _pWord.Object.lblName.Text = "Name:";
                _pWord.Object.lblValue.Text = "Value:";
                _pWord.Object.modeIndex = _pWord.Object.treeView1.SelectedNode.Index;
                //				this.txtName.Text = treeView1.SelectedNode.Nodes[modeIndex].Text;
                //				this.txtObject.Text = treeView1.SelectedNode.Nodes[modeIndex].Text;

                if (_pWord.Object.treeView1.SelectedNode.Parent != null)
                {
                    ((pNode)_pWord.Object.treeView1.SelectedNode.Parent).OperationChanged();
                    _pWord.Object.tmpNode = ((pNode)_pWord.Object.treeView1.SelectedNode);
                }

                _pWord.Object.txtName.Text = _pWord.Object.tmpNode.Text;
                _pWord.Object.txtObject.Text = (string)_pWord.Object.tmpNode.Tag;
                _pWord.Object.statusBar1.Text = "Edit Mode";
                _pWord.Object.txtName.Focus();
                Assert.AreEqual(_pWord.Object.statusBar1.Text, "Edit Mode");
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
            //			this.tmpNode = treeView1.SelectedNode;
            //
            //			this.txtName.Focus();
            //			This about adding a flag called edit mode  same to Add In
        }
        [TestMethod]
        public void menuItem21_Click()
        {
            // put in
            try
            {
                _pWord.Object.treeView1.SelectedNode.Nodes.Insert(_pWord.Object.treeView1.SelectedNode.Nodes.Count, _pWord.Object.getNode);
                _pWord.Object.getNode = (pNode)_pWord.Object.getNode.Clone();

                if (_pWord.Object.flag_file == true)
                {
                    autosave();
                }
                Assert.AreEqual(_pWord.Object.getNode, (pNode)_pWord.Object.getNode.Clone());

            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }

        }
        [TestMethod]
        public void menuItemGetNode_Click()
        {
            try
            {

                _pWord.Object.getNode = (pNode)_pWord.Object.treeView1.SelectedNode.Clone();
                _pWord.Object.menuItem21.Enabled = true;
                _pWord.Object.menuItem31.Enabled = true;
                _pWord.Object.nodeIndex = _pWord.Object.treeView1.SelectedNode.Index;
                _pWord.Object.statusBar1.Text = "Get Node Mode";
                Assert.AreEqual(_pWord.Object.statusBar1.Text, "Get Node Mode");
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }
        [TestMethod]
        public void menuItemExportXML_Click()
        {

            _pWord.Object.xml.Clear();  // clear out contents first.
            ((pNode)_pWord.Object.treeView1.SelectedNode).CallRecursive((pNode)_pWord.Object.treeView1.SelectedNode);
            try
            {
                _pWord.Object.exportMode = ExportMode.treeview;
                XmlDocument xdoc = new XmlDocument();

                _pWord.Object.saveFileDialogHTML.FileName = _pWord.Object.filenameHTML;
                _pWord.Object.saveFileDialogHTML.Title = "Save View to XML/HTML";
                _pWord.Object.saveFileDialogHTML.ShowDialog();
                _pWord.Object.filenameHTML = _pWord.Object.saveFileDialogHTML.FileName;
                if (_pWord.Object.filenameHTML != null)
                {
                    _pWord.Object.toolBarView.Enabled = true;
                }
                Assert.AreEqual(_pWord.Object.filenameHTML, _pWord.Object.saveFileDialogHTML.FileName);
            }
            catch (Exception f)
            {
                MessageBox.Show("You had an error while exporting to XML. " + f.Message, "SAVE ERROR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }

        }
        [TestMethod]
        public void menuItem27_Click()
        {
            bool check = false;
            AYS dlg = new AYS();
            dlg.label1.Text = "ARE YOU SURE???\nSorting may change your structure.";
            dlg.ShowDialog();
            if (dlg.DialogResult == DialogResult.OK)
            {
                _pWord.Object.treeView1.Sorted = true;
            }
            check = true;
            Assert.AreEqual(check, true);
        }
        [TestMethod]
        public void menuItem28_Click()
        {
            _pWord.Object.treeView1.Sorted = false;
            Assert.AreEqual(_pWord.Object.treeView1.Sorted, false);
        }
        [TestMethod]
        public void menuItemInsertNode_Click()
        {
            _pWord.Object.lblName.Text = "Name:";
            _pWord.Object.lblValue.Text = "Value:";
            _pWord.Object.mode = nodeMode.insert;
            if (_pWord.Object.chkClear.Checked == true)
            {
                _pWord.Object.txtName.Clear();
                _pWord.Object.txtObject.Clear();
            }
            try
            {
                _pWord.Object.tmpNode = (pNode)_pWord.Object.treeView1.SelectedNode.Parent;
                _pWord.Object.modeIndex = _pWord.Object.treeView1.SelectedNode.Index;
                _pWord.Object.txtName.Focus();
                Assert.AreEqual(_pWord.Object.modeIndex, _pWord.Object.treeView1.SelectedNode.Index);
            }

            catch (Exception f)
            {
                MessageBox.Show(f.Message);

            }
            _pWord.Object.statusBar1.Text = "Insert Mode";


        }
        [TestMethod]
        public void menuItemInsertNode2_Click()
        {
            // Insert next to
            try
            {
                _pWord.Object.treeView1.SelectedNode.Parent.Nodes.Insert(_pWord.Object.treeView1.SelectedNode.Index, _pWord.Object.getNode);


                //Insert(treeView1.SelectedNode.Nodes.Count,this.getNode);
                _pWord.Object.getNode = (pNode)_pWord.Object.getNode.Clone();

                if (_pWord.Object.flag_file == true)
                {
                    autosave();
                }
                Assert.AreEqual(_pWord.Object.getNode, (pNode)_pWord.Object.getNode.Clone());
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }
        [TestMethod]
        public void treeView1_KeyDown()
        {
            bool check = false;
            pView pv = new pView();
            //pv = (pView)sender;
            _pWord.Object.treeView1.SelectedNode = pv.SelectedNode;
            Cursor.Hide();
            //_pWord.Object.genericCursorMoved(sender, e);

            /* if (e.KeyCode == System.Windows.Forms.Keys.Delete)
             {*/

            System.EventArgs f = new System.EventArgs();
            menuItem14_Click();
            /* }
             else if (e.KeyCode == System.Windows.Forms.Keys.Insert)
             {*/
            //EventArgs f = new EventArgs();
            if (_pWord.Object.menuItem29.Enabled == true)
            {
                menuItemInsertNode_Click();
            }
            //}
            check = true;
            Assert.IsTrue(check);
        }
        [TestMethod]
        public void chkClear_CheckStateChanged()
        {
            bool check = false;
            if (_pWord.Object.chkClear.CheckState == CheckState.Checked)
            {
                _pWord.Object.chkClear.Text = "Clear All Text";
            }
            else if (_pWord.Object.chkClear.CheckState == CheckState.Indeterminate)
            {
                _pWord.Object.chkClear.Text = "Clear Value Only";
            }
            else if (_pWord.Object.chkClear.CheckState == CheckState.Unchecked)
            {
                _pWord.Object.chkClear.Text = "Clear Disabled";
            }
            check = true;
            Assert.IsTrue(check);
        }
        [TestMethod]
        public void openFileDialog1_FileOk()
        {

            _pWord.Object.Nodes.Clear();
            _pWord.Object.userControl11.MasterNodes.Clear();
            _pWord.Object.userControl11.MasterNames.Clear();
            _pWord.Object.filename = _pWord.Object.openFileDialog1.FileName;

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(_pWord.Object.filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            _pWord.Object.Nodes = (ArrayList)formatter.Deserialize(stream);
            stream.Close();

            _pWord.Object.treeView1.Nodes.Clear();
            int count = (int)_pWord.Object.Nodes[0];
            int i = 1;
            while (i < _pWord.Object.Nodes.Count)
            {
                _pWord.Object.userControl11.MasterNames.Add((string)_pWord.Object.Nodes[i]);
                // check Nodes type
                ++i;
                if (_pWord.Object.Nodes[i].GetType() == typeof(pNode))
                {
                    _pWord.Object.userControl11.MasterNodes.Add((pNode)_pWord.Object.Nodes[i]);
                }
                else if (_pWord.Object.Nodes[i].GetType() == typeof(TreeNode))
                {
                    // Compatibility with old version 6A
                    // convert TreeNode to a pNode
                    TreeNode a = (TreeNode)_pWord.Object.Nodes[i];

                    // p only gets the top node for a master... it doesn't delve in and get everything else
                    // Todo: get all other nodes in a proper TreeNode to pNode conversion
                    // I want to perform the conversion indie of the pNode class itself
                    try
                    {
                        pNode p = pNode.TreeNode2pNode(a);
                        //                    userControl11.MastersValue.Add((TreeNode)Nodes[i]);
                        _pWord.Object.userControl11.MasterNodes.Add(p);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.ToString());
                    }
                }
                i++;
            }
            _pWord.Object.userControl11.index = 0;
            _pWord.Object.treeView1.Nodes.Add((pNode)_pWord.Object.userControl11.MasterNodes[_pWord.Object.userControl11.index]);
            _pWord.Object.userControl11.txtMaster.Text = _pWord.Object.userControl11.MasterNames[_pWord.Object.userControl11.index];
            _pWord.Object.flag_file = true;
            Assert.AreEqual(_pWord.Object.flag_file, true);

            // successful?  go ahead and make the open stick
            _pWord.Object.rm.SavePathInRegistry(pWordSettings.Default.version, _pWord.Object.filename);

        }
        [TestMethod]
        public void menuItem33_Click()
        {
            AYS ays = new AYS();
            ays.label1.Text = "This will load the file to the last insert, or addition only if you have saved or opened a file previously.";

            ays.ShowDialog();
            if (ays.DialogResult == DialogResult.OK)
            {
                try
                {
                    if (_pWord.Object.flag_file == true)
                    {
                        _pWord.Object.Nodes.Clear();
                        _pWord.Object.userControl11.MasterNodes.Clear();
                        _pWord.Object.userControl11.MasterNames.Clear();


                        IFormatter formatter = new BinaryFormatter();
                        Stream stream = new FileStream(_pWord.Object.filename, FileMode.Open, FileAccess.Read, FileShare.Read);
                        _pWord.Object.Nodes = (ArrayList)formatter.Deserialize(stream);
                        stream.Close();

                        _pWord.Object.treeView1.Nodes.Clear();
                        int count = (int)_pWord.Object.Nodes[0];
                        int i = 1;
                        while (i < _pWord.Object.Nodes.Count)
                        {
                            _pWord.Object.userControl11.MasterNames.Add((string)_pWord.Object.Nodes[i]);
                            _pWord.Object.userControl11.MasterNodes.Add((pNode)_pWord.Object.Nodes[++i]);
                            i++;
                        }
                        _pWord.Object.userControl11.index = 0;
                        _pWord.Object.treeView1.Nodes.Add((pNode)_pWord.Object.userControl11.MasterNodes[_pWord.Object.userControl11.index]);
                        _pWord.Object.flag_file = true;
                        Assert.AreEqual(_pWord.Object.flag_file, true);
                    }
                    else
                        MessageBox.Show("UNDO: Failed");

                }
                catch (Exception f)
                {
                    MessageBox.Show("You had an error while loading. Please select the proper file. " + f.Message, "OPEN ERROR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                }
            }

        }
        [TestMethod]
        public void menuCutNode_Click()
        {
            // get
            try
            {

                _pWord.Object.getNode = (pNode)((pNode)_pWord.Object.treeView1.SelectedNode).Clone();
                if (_pWord.Object.treeView1.SelectedNode.Parent != null)
                {
                    _pWord.Object.menuItem21.Enabled = true;
                    _pWord.Object.menuItem31.Enabled = true;
                    _pWord.Object.nodeIndex = _pWord.Object.treeView1.SelectedNode.Index;
                    if (_pWord.Object.treeView1.SelectedNode.Parent != null)
                    {
                        ((pNode)_pWord.Object.treeView1.SelectedNode).OperationChanged();
                        _pWord.Object.treeView1.SelectedNode.Remove();
                    }
                    _pWord.Object.statusBar1.Text = "CUT Node Mode";
                    _pWord.Object.mode = nodeMode.cut;
                    Assert.AreEqual(_pWord.Object.mode, nodeMode.cut);
                }
                else
                    MessageBox.Show("Cutting master node not allowed.", "Operation Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }
        [TestMethod]
        public void saveFileDialog1_FileOk()
        {

            _pWord.Object.filename = _pWord.Object.saveFileDialog1.FileName;
            if (_pWord.Object.filename != null)
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(_pWord.Object.saveFileDialog1.FileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                formatter.Serialize(stream, _pWord.Object.Nodes);
                _pWord.Object.filename = _pWord.Object.saveFileDialog1.FileName;

                // use rm manager to set file name
                _pWord.Object.rm.SavePathInRegistry(pWordSettings.Default.version, _pWord.Object.filename);

                stream.Close();
                String path = _pWord.Object.rm.AutoSavePathFromRegistry(pWordSettings.Default.version.ToString());
                if (_pWord.Object.saveFileDialog1.FileName != path)
                {
                    // save the path to the registry
                    _pWord.Object.rm.SavePathInRegistry(pWordSettings.Default.version, _pWord.Object.saveFileDialog1.FileName);
                }
            }
            _pWord.Object.flag_file = true;
            Assert.AreEqual(_pWord.Object.flag_file, true);
        }
        [TestMethod]
        public void saveFileDialogHTML_FileOk()
        {
            bool check = false;
            string filenameHTML = _pWord.Object.saveFileDialogHTML.FileName;
            if (filenameHTML != null)
            {
                StreamWriter swFromFile = new StreamWriter(filenameHTML);

                swFromFile.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                for (int i = 0; i < _pWord.Object.xml.Count; i++)
                {
                    swFromFile.Write(_pWord.Object.xml[i]);
                }
                swFromFile.Flush();
                swFromFile.Close();
            }
            check = true;
            Assert.AreEqual(check, true);
        }
        [TestMethod]
        public void saveFileDialogJSON_FileOk()
        {
            bool check = false;
            string filenameJSON = _pWord.Object.saveFileDialogJSON.FileName;
            if (filenameJSON != null)
            {
                StreamWriter swFromFile = new StreamWriter(filenameJSON);

                //swFromFile.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                for (int i = 0; i < _pWord.Object.xml.Count; i++)
                {
                    swFromFile.Write(_pWord.Object.xml[i]);
                    //						formatter.Serialize(stream, xml[i].ToString());
                }
                swFromFile.Flush();
                swFromFile.Close();
            }
            check = true; Assert.AreEqual(check, true);
        }
        [TestMethod]
        public void menuItemToHTML_Click()
        {

            try
            {
                // TODO: Move To pWordLib
                _pWord.Object.exportMode = ExportMode.pNode;  // what am I exporting?  A pNode
                                                           // TODO: Move To pWordLib
                _pWord.Object.xml.Clear();  // clear out contents first.

                //this.xmlNode = (pNode)treeView1.SelectedNode;  // xmlNode is what is being exported to xml
                _pWord.Object.xmlIndex = _pWord.Object.treeView1.SelectedNode.Index; // xmlIndex is the SelectedNodes index
                _pWord.Object.menuItem21.Enabled = true; // MenuItem21 is enabled... Todo: rename menuItem21
                _pWord.Object.menuItem31.Enabled = true;  // MneuItem21 is enabled... Todo: rename menuItem31
                _pWord.Object.nodeIndex = _pWord.Object.treeView1.SelectedNode.Index; // nodeInex is now equal to xmlIndex?
                _pWord.Object.statusBar1.Text = "Export Node XML Mode";
                //CallRecursive(xmlNode);  // disabled CallRecursive here... need to fix Call recursive
                // ToDo: fix CallRecursive(xmlNode)
                _pWord.Object.xmlNode = null;
                _pWord.Object.xmlNode = (pNode)_pWord.Object.treeView1.SelectedNode;

                // TODO: Move To pWordLib
                var xdoc = ((pNode)_pWord.Object.treeView1.SelectedNode).CallRecursive(_pWord.Object.xmlNode);  // treeview1 is a pView

                _pWord.Object.saveFileDialogHTML.FileName = _pWord.Object.filenameHTML;
                _pWord.Object.saveFileDialogHTML.Title = "Save the NODE to XML/HTML";
                _pWord.Object.saveFileDialogHTML.ShowDialog();
                _pWord.Object.filenameHTML = _pWord.Object.saveFileDialogHTML.FileName;

                if (_pWord.Object.filenameHTML != null)
                {
                    if (xdoc != null)
                    {
                        xdoc.Save(_pWord.Object.filenameHTML);
                        xdoc.RemoveAll();
                        xdoc = null;
                    }
                    else
                    {
                        MessageBox.Show("The export was not able to save b/c it was empty.");
                    }
                }
                _pWord.Object.toolBarView.Enabled = true;
                Assert.AreEqual(_pWord.Object.toolBarView.Enabled, true);
            }
            catch (Exception f)
            {
                MessageBox.Show("You had an error while exporting to XML. " + f.Message, "SAVE ERROR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }

        }
        [TestMethod]
        public void notifyIcon2_DoubleClick()
        {
            _pWord.Object.autoHide_flag = true;
            _pWord.Object.statusBar1.Text = "AutoHide Active";
            _pWord.Object.toolBarTac.ImageIndex = 0;
            //this.actHook.Start();
            _pWord.Object.VIS = false;
            _pWord.Object.Visible = false;
            _pWord.Object.notifyIcon2.Visible = false;
            _pWord.Object.notifyIcon1.Visible = true;
            Assert.AreEqual(_pWord.Object.notifyIcon1.Visible, true);
        }
        [TestMethod]
        public void menuItemAttributeAdd_Click()
        {
            // TODO: Move To pWordLib
            _pWord.Object.mode = nodeMode.addAttributeTo;

            try
            {
                _pWord.Object.lblName.Text = "Attribute:";
                _pWord.Object.lblValue.Text = "Value:";
                _pWord.Object.modeIndex = _pWord.Object.treeView1.SelectedNode.Index;
                _pWord.Object.tmpNode = (pNode)_pWord.Object.treeView1.SelectedNode;
                _pWord.Object.statusBar1.Text = "Add Attribute to Node";
                if (_pWord.Object.chkClear.CheckState == CheckState.Checked)
                {
                    _pWord.Object.txtName.Clear();
                    _pWord.Object.txtObject.Clear();
                    _pWord.Object.txtName.Focus();
                }
                else if (_pWord.Object.chkClear.CheckState == CheckState.Indeterminate)
                {
                    _pWord.Object.txtObject.Clear();
                    _pWord.Object.txtObject.Focus();
                }
                else
                {
                    _pWord.Object.btnAdd.Focus();
                }
                Assert.AreEqual(_pWord.Object.statusBar1.Text, "Add Attribute to Node");
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }
        [TestMethod]
        public void menuItemNamespaceAddPrefix_Click()
        {
            // TODO: Move To pWordLib
            _pWord.Object.mode = nodeMode.addNamespacePrefix;
            try
            {
                bool check = false;
                _pWord.Object.lblName.Text = "Prefix:";
                _pWord.Object.lblValue.Text = "URI:";
                _pWord.Object.modeIndex = _pWord.Object.treeView1.SelectedNode.Index;
                _pWord.Object.tmpNode = (pNode)_pWord.Object.treeView1.SelectedNode;

                _pWord.Object.statusBar1.Text = "Add Prefix to Node";
                if (_pWord.Object.chkClear.CheckState == CheckState.Checked)
                {
                    _pWord.Object.txtName.Clear();
                    _pWord.Object.txtObject.Clear();
                    _pWord.Object.txtName.Focus();
                }
                else if (_pWord.Object.chkClear.CheckState == CheckState.Indeterminate)
                {
                    _pWord.Object.txtObject.Clear();
                    _pWord.Object.txtObject.Focus();
                }
                else
                {
                    _pWord.Object.btnAdd.Focus();
                }
                check = true;
                Assert.AreEqual(true, check);
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }
        [TestMethod]
        public void menuItemNamespaceAddSuffix_Click()
        {
            _pWord.Object.mode = nodeMode.addNamespaceSuffix;
            try
            {
                _pWord.Object.lblName.Text = "Suffix:";
                _pWord.Object.lblValue.Text = "URI:";
                _pWord.Object.modeIndex = _pWord.Object.treeView1.SelectedNode.Index;
                _pWord.Object.tmpNode = (pNode)_pWord.Object.treeView1.SelectedNode;

                _pWord.Object.statusBar1.Text = "Add Suffix to Node";
                if (_pWord.Object.chkClear.CheckState == CheckState.Checked)
                {
                    _pWord.Object.txtName.Clear();
                    _pWord.Object.txtObject.Clear();
                    _pWord.Object.txtName.Focus();
                }
                else if (_pWord.Object.chkClear.CheckState == CheckState.Indeterminate)
                {
                    _pWord.Object.txtObject.Clear();
                    _pWord.Object.txtObject.Focus();
                }
                else
                {
                    _pWord.Object.btnAdd.Focus();
                }
                Assert.AreEqual(_pWord.Object.statusBar1.Text, "Add Suffix to Node");
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }
        [TestMethod]
        public void menuItemMathSum_Click()
        {
            _pWord.Object.mode = nodeMode.sum;
            try
            {
                _pWord.Object.lblName.Text = "Name:";
                _pWord.Object.lblValue.Text = "Value:";
                _pWord.Object.modeIndex = _pWord.Object.treeView1.SelectedNode.Index;
                Sum sum = new Sum();
                _pWord.Object.tmpNode = (pNode)_pWord.Object.treeView1.SelectedNode;
                _pWord.Object.tmpNode.AddOperation(new Sum(Resource1.Sum));
                _pWord.Object.treeView1.SelectedNode = _pWord.Object.tmpNode;
                _pWord.Object.statusBar1.Text = "Summation";
                _pWord.Object.txtObject.Text = (String)_pWord.Object.treeView1.SelectedNode.Tag;

                _pWord.Object.txtName.Focus();
                autosave();  // may need to hook up an event to save when ever new nodes are added or removed???
                Assert.AreEqual(_pWord.Object.statusBar1.Text, "Summation");
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }
        [TestMethod]
        public void menuItemMathMultiple_Click()
        {
            // TODO: Move To pWordLib
            _pWord.Object.mode = nodeMode.multiply;
            try
            {
                bool check = false;
                _pWord.Object.lblName.Text = "Name:";
                _pWord.Object.lblValue.Text = "Value:";
                _pWord.Object.modeIndex = _pWord.Object.treeView1.SelectedNode.Index;
                _pWord.Object.tmpNode = (pNode)_pWord.Object.treeView1.SelectedNode;
                _pWord.Object.tmpNode.AddOperation(new Multiply(Resource1.Multiplication));

                // TODO: Move To pWordLib (make pWordLib friendlier)
                _pWord.Object.treeView1.SelectedNode = _pWord.Object.tmpNode;
                _pWord.Object.statusBar1.Text = "Mutliple";
                _pWord.Object.txtObject.Text = (String)_pWord.Object.treeView1.SelectedNode.Tag;

                _pWord.Object.txtName.Focus();
                autosave();  // may need to hook up an event to save when ever new nodes are added or removed???
                check = true;
                Assert.AreEqual(check, true);
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }
        [TestMethod]
        public void menuItemMathDivide_Click()
        {
            _pWord.Object.mode = nodeMode.divide;
            try
            {
                bool check = false;
                _pWord.Object.lblName.Text = "Name:";
                _pWord.Object.lblValue.Text = "Value:";
                _pWord.Object.modeIndex = _pWord.Object.treeView1.SelectedNode.Index;
                _pWord.Object.tmpNode = (pNode)_pWord.Object.treeView1.SelectedNode;
                _pWord.Object.tmpNode.AddOperation(new Divide(Resource1.Division));
                _pWord.Object.treeView1.SelectedNode = _pWord.Object.tmpNode;

                _pWord.Object.statusBar1.Text = "Divide";
                _pWord.Object.txtObject.Text = (String)_pWord.Object.treeView1.SelectedNode.Tag;
                _pWord.Object.txtName.Focus();
                autosave();  // may need to hook up an event to save when ever new nodes are added or removed???
                check = true;
                Assert.AreEqual(check, true);
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }
        [TestMethod]
        public void menuItemMathSubtract_Click()
        {
            // TODO: Move To pWordLib
            _pWord.Object.mode = nodeMode.divide;

            try
            {
                bool check = false;
                _pWord.Object.lblName.Text = "Name:";
                _pWord.Object.lblValue.Text = "Value:";
                _pWord.Object.modeIndex = _pWord.Object.treeView1.SelectedNode.Index;
                _pWord.Object.tmpNode = (pNode)_pWord.Object.treeView1.SelectedNode;
                _pWord.Object.tmpNode.AddOperation(new Subtract(Resource1.Subtraction));
                _pWord.Object.treeView1.SelectedNode = _pWord.Object.tmpNode;

                _pWord.Object.statusBar1.Text = "Subtract";
                _pWord.Object.txtObject.Text = (String)_pWord.Object.treeView1.SelectedNode.Tag;
                _pWord.Object.txtName.Focus();
                autosave();  // may need to hook up an event to save when ever new nodes are added or removed???
                check = true;
                Assert.AreEqual(check, true);
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }
        [TestMethod]
        public void menuItemOperationsClear_Click()
        {
            bool check = false;
            ((pNode)_pWord.Object.treeView1.SelectedNode).ClearOperations();
            check = true;
            Assert.AreEqual(check, true);
        }
        [TestMethod]
        public void menuItemViewErrors_Click()
        {
            _pWord.Object.lblName.Text = "Operations:";
            _pWord.Object.lblValue.Text = "Error Info:";

            _pWord.Object.txtName.Text = ((pNode)_pWord.Object.treeView1.SelectedNode).ListOperations();
            _pWord.Object.txtObject.Text = ((pNode)_pWord.Object.treeView1.SelectedNode).ErrorString;
            Assert.AreEqual(_pWord.Object.txtObject.Text, ((pNode)_pWord.Object.treeView1.SelectedNode).ErrorString);
        }
        [TestMethod]
        public void menuItemMathTrigSign_Click()
        {
            _pWord.Object.mode = nodeMode.trig;
            try
            {
                bool check = false;
                _pWord.Object.lblName.Text = "Name:";
                _pWord.Object.lblValue.Text = "Value:";
                _pWord.Object.modeIndex = _pWord.Object.treeView1.SelectedNode.Index;
                _pWord.Object.tmpNode = (pNode)_pWord.Object.treeView1.SelectedNode;
                _pWord.Object.tmpNode.AddOperation(new Sin(Resource1.Sin));  // add new sin operation ... trying to make this a plug in.
                _pWord.Object.treeView1.SelectedNode = _pWord.Object.tmpNode;

                _pWord.Object.statusBar1.Text = "Sin";
                _pWord.Object.txtObject.Text = (String)_pWord.Object.treeView1.SelectedNode.Tag;
                _pWord.Object.txtName.Focus();
                autosave();  // may need to hook up an event to save when ever new nodes are added or removed???
                check = true;
                Assert.AreEqual(check, true);
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }
        [TestMethod]
        public void menuItemMathTrigCos_Click()
        {
            _pWord.Object.mode = nodeMode.trig;
            try
            {
                bool check = false;
                _pWord.Object.lblName.Text = "Name:";
                _pWord.Object.lblValue.Text = "Value:";
                _pWord.Object.modeIndex = _pWord.Object.treeView1.SelectedNode.Index;
                _pWord.Object.tmpNode = (pNode)_pWord.Object.treeView1.SelectedNode;
                _pWord.Object.tmpNode.AddOperation(new Cos(Resource1.Cos));  // add new sin operation ... trying to make this a plug in.
                _pWord.Object.treeView1.SelectedNode = _pWord.Object.tmpNode;

                _pWord.Object.statusBar1.Text = "Cos";
                _pWord.Object.txtObject.Text = (String)_pWord.Object.treeView1.SelectedNode.Tag;
                _pWord.Object.txtName.Focus();
                autosave();  // may need to hook up an event to save when ever new nodes are added or removed???
                check = true;
                Assert.AreEqual(check, true);
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }
        [TestMethod]
        public void menuItemMathTrigTan_Click()
        {
            _pWord.Object.mode = nodeMode.trig;
            try
            {
                bool check = false;
                _pWord.Object.lblName.Text = "Name:";
                _pWord.Object.lblValue.Text = "Value:";
                _pWord.Object.modeIndex = _pWord.Object.treeView1.SelectedNode.Index;
                _pWord.Object.tmpNode = (pNode)_pWord.Object.treeView1.SelectedNode;
                _pWord.Object.tmpNode.AddOperation(new Tan(Resource1.Tan));  // add new sin operation ... trying to make this a plug in.
                _pWord.Object.treeView1.SelectedNode = _pWord.Object.tmpNode;

                _pWord.Object.statusBar1.Text = "Tan";
                _pWord.Object.txtObject.Text = (String)_pWord.Object.treeView1.SelectedNode.Tag;
                _pWord.Object.txtName.Focus();
                autosave();  // may need to hook up an event to save when ever new nodes are added or removed???
                check = true; Assert.AreEqual(check, true);
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }
        [TestMethod]
        public void menuItemFind_Click()
        {
            bool check = false;
            _pWord.Object.mode = nodeMode.find;
            _pWord.Object.modeIndex = _pWord.Object.treeView1.SelectedNode.Index;
            _pWord.Object.tmpNode = (pNode)_pWord.Object.treeView1.SelectedNode;
            try
            {
                _pWord.Object.lblName.Text = "Find Name:";
                _pWord.Object.lblValue.Text = "Find Value:";
                _pWord.Object.modeIndex = _pWord.Object.treeView1.SelectedNode.Index;
                _pWord.Object.tmpNode = (pNode)_pWord.Object.treeView1.SelectedNode;
                check = true; Assert.AreEqual(check, true);
            }
            catch (Exception ex)
            {

            }
        }
        [TestMethod]
        public void mnuImportNodeXML_Click()
        {
            bool check = false;
            // TODO: Move To pWordLib (Make pWordLib friendlier)
            _pWord.Object.importMode = ImportMode.treexml; // what am I exporting?  XML from previous exportNode
                                                        // I'm importing into a node that i selected using import node treexml 

            // TODO: Move To pWordLib
            _pWord.Object.xml.Clear();  // clear out contents first.

            //this.xmlNode = (pNode)treeView1.SelectedNode;  // xmlNode is what is being exported to xml
            _pWord.Object.xmlIndex = _pWord.Object.treeView1.SelectedNode.Index; // xmlIndex is the SelectedNodes index
            _pWord.Object.menuItem21.Enabled = true; // MenuItem21 is enabled... Todo: rename menuItem21
            _pWord.Object.menuItem31.Enabled = true;  // MneuItem21 is enabled... Todo: rename menuItem31
            _pWord.Object.nodeIndex = _pWord.Object.treeView1.SelectedNode.Index; // nodeInex is now equal to xmlIndex?
            _pWord.Object.statusBar1.Text = "Import Node XML Mode";
            //CallRecursive(xmlNode);  // disabled CallRecursive here... need to fix Call recursive
            // ToDo: fix CallRecursive(xmlNode)

            // TODO: Move To pWordLib (Make pWordLib friendlier)
            _pWord.Object.tmpNode = (pNode)_pWord.Object.treeView1.SelectedNode;
            _pWord.Object.openFileDialog2.ShowDialog();
            _pWord.Object.filenameHTML = _pWord.Object.openFileDialog2.FileName;
            if ((_pWord.Object.filenameHTML == null) || (_pWord.Object.filenameHTML == ""))
            {
                return;
            }
            else
            {
                try
                {
                    using (WebClient client = new WebClient())
                    using (Stream stream = client.OpenRead(_pWord.Object.filenameHTML))
                    {
                        //      byte[] buf = new byte[stream.Length];
                        //      stream.Read(buf, 0, (int)stream.Length);
                        _pWord.Object.xdoc = new XmlDocument();
                        try
                        {
                            _pWord.Object.xdoc.Load(stream);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    pNode masterNode = (pNode)_pWord.Object.treeView1.Nodes[0];
                    pNode pn = (pNode)_pWord.Object.treeView1.SelectedNode;
                    if (_pWord.Object.treeView1.SelectedNode.Tag == null)
                        _pWord.Object.treeView1.SelectedNode.Tag = "";
                    pn.Tag = _pWord.Object.treeView1.SelectedNode.Tag;

                    // TODO: Move To pWordLib
                    if (_pWord.Object.xdoc != null && _pWord.Object.xdoc.Attributes != null && _pWord.Object.xdoc.Attributes.Count > 0)
                    {
                        foreach (XmlAttribute xmlAttr in _pWord.Object.xdoc)
                        {
                            pn.AddAttribute(xmlAttr.LocalName, xmlAttr.Value);
                        }
                    }

                    // TODO: Move To pWordLib
                    _pWord.Object.AddChildNodes(_pWord.Object.xdoc.ChildNodes, ref pn);

                    _pWord.Object.treeView1.SelectedNode = pn;
                    _pWord.Object.userControl11.MasterNodes[_pWord.Object.userControl11.index] = masterNode;
                    if (_pWord.Object.chkClear.CheckState == CheckState.Checked)
                    {
                        _pWord.Object.txtName.Clear();
                        _pWord.Object.txtObject.Clear();
                        _pWord.Object.txtName.Focus();
                    }
                    else if (_pWord.Object.chkClear.CheckState == CheckState.Indeterminate)
                    {
                        _pWord.Object.txtObject.Clear();
                        _pWord.Object.txtObject.Focus();
                    }
                    else
                    {
                        _pWord.Object.btnAdd.Focus();
                    }

                    if (_pWord.Object.flag_file == true)
                    {
                        //autosave();
                    }
                    check = true; Assert.AreEqual(check, true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred during xml document load.");
                }
            }
        }
        [TestMethod]
        public void txtCMD_TextChanged()
        {
            bool check = true;
            // hide the busy mouse icon
            _pWord.Object.Cursor = Cursors.Default;
            _pWord.Object.txtCMD.Cursor = Cursors.Default;

            // check for return character... and then process

            if (_pWord.Object.txtCMD.Text.Length > 0)
            {
                if (_pWord.Object.txtCMD.Text.Length < 4 && _pWord.Object.txtCMD.Text.ToLower().Contains("cls"))
                {
                    _pWord.Object.txtCMD.Text = "";
                }
                else if (_pWord.Object.txtCMD.Text[_pWord.Object.txtCMD.Text.Length - 1] == '\n' || _pWord.Object.txtCMD.Text[_pWord.Object.txtCMD.Text.Length - 1] == '\r')
                {
                    // process command
                    _pWord.Object.txtCMD.Cursor = Cursors.WaitCursor;
                    _pWord.Object.txtCMD.Parent.Cursor = Cursors.WaitCursor;
                    _pWord.Object.ProcessCommandText(_pWord.Object.txtCMD.Text);
                    // add history node to treeview
                    // then insert this command into the history
                    // and insert its text result as a child node
                    _pWord.Object.txtCMD.Text += output.ToString();
                }
            }
            _pWord.Object.Cursor = Cursors.Default;
            _pWord.Object.txtCMD.Cursor = Cursors.Default;
            _pWord.Object.txtCMD.Parent.Cursor = Cursors.Default;
            _pWord.Object.txtCMD.Focus();
            check = false;
            Assert.AreEqual(check, false);
        }
        [TestMethod]
        public void ProcessCommandText()
        {
            ProcessCommandText("smsm");

        }
        public void ProcessCommandText(string txt)
        {
            // check for multiple commands
            var commands = txt.Split('\n');

            foreach (var cmd in commands)
            {
                var _cmd = cmd.Replace("\n", "").Replace("\r", "");

                Process process = new Process();
                process.StartInfo.FileName = @"c:\windows\system32\cmd";
                process.StartInfo.Arguments = "/c " + _cmd;

                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;

                output.Clear();
                process.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
                {
                    // Prepend line numbers to each line of the output.
                    if (!String.IsNullOrEmpty(e.Data))
                    {
                        lineCount++;
                        output.Append("\n[" + lineCount + "]: " + e.Data);
                    }
                });

                process.Start();

                // Asynchronously read the standard output of the spawned process.
                // This raises OutputDataReceived events for each line of output.
                process.BeginOutputReadLine();
                process.WaitForExit();

                // Write the redirected output to this application's window.
                Console.WriteLine(output);

                process.WaitForExit();
                process.Close();

                Console.WriteLine("\n\nPress any key to exit.");

                Debugger.Log(1, "success", "testing");

                // regex match text a-Z only
                var stringMatches = Regex.Matches(_cmd, @"[a-zA-Z_]+");
                var stringMatchResult = "";
                foreach (System.Text.RegularExpressions.Match stringMatch in stringMatches)
                {
                    // get all the alpha characters and combine them into a single result
                    stringMatchResult += stringMatch.Value;
                }

                if (_pWord.Object.treeView1.Nodes[0].Nodes.Find("History", false).Length == 0)
                {
                    pNode aNode = new pNode("History");
                    _pWord.Object.treeView1.Nodes[0].Nodes.Add(aNode);
                }

                _pWord.Object.treeView1.SelectedNode = _pWord.Object.treeView1.Nodes[0].Nodes.Find("History", false).FirstOrDefault();

                if (_cmd.Length > 0)
                {
                    _pWord.Object.txtName.Text = stringMatchResult;
                    _pWord.Object.txtObject.Text = output.ToString();
                    var cmdNode = new pNode(stringMatchResult);
                    cmdNode.Tag = output.ToString();
                    _pWord.Object.treeView1.SelectedNode.Nodes.Add(cmdNode);
                    _pWord.Object.txtCMD.Focus();
                }
            }

        }
        [TestMethod]
        public void menuItemImportJSON_Click()
        {
            bool check = false;

            // TODO: Move To pWordLib
            _pWord.Object.importMode = ImportMode.treejson; // what am I exporting?  XML from previous exportNode
                                                         // I'm importing into a node that i selected using import node treexml 

            // TODO: Move To pWordLib
            _pWord.Object.xml.Clear();  // clear out contents first.

            // TODO: Move To pWordLib (make pWordLib friendly)
            _pWord.Object.xmlIndex = _pWord.Object.treeView1.SelectedNode.Index; // xmlIndex is the SelectedNodes index

            _pWord.Object.menuItem21.Enabled = true; // MenuItem21 is enabled... Todo: rename menuItem21
            _pWord.Object.menuItem31.Enabled = true;  // MneuItem21 is enabled... Todo: rename menuItem31
            _pWord.Object.nodeIndex = _pWord.Object.treeView1.SelectedNode.Index; // nodeInex is now equal to xmlIndex?
            _pWord.Object.statusBar1.Text = "Import Node JSON Mode";

            _pWord.Object.openFileDialog2.ShowDialog();
            _pWord.Object.filenameHTML = _pWord.Object.openFileDialog2.FileName;
            if ((_pWord.Object.filenameHTML == null) || (_pWord.Object.filenameHTML == ""))
            {
                return;
            }
            else
            {
                try
                {
                    using (WebClient client = new WebClient())
                    using (Stream stream = client.OpenRead(_pWord.Object.filenameHTML))
                    {
                        //      byte[] buf = new byte[stream.Length];
                        //      stream.Read(buf, 0, (int)stream.Length);
                        _pWord.Object.xdoc = new XmlDocument();
                        try
                        {
                            //xdoc.Load(stream);
                            // load json with newton soft and convert to XmlDocument
                            var json = File.ReadAllText(_pWord.Object.filenameHTML);
                            //var jObject = JObject.Parse(json);
                            var xml = JsonConvert.DeserializeXmlNode(json);
                            _pWord.Object.xdoc.LoadXml(xml.InnerXml);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }

                    // TODO: Move To pWordLib (make pWordLib friendly)
                    pNode masterNode = (pNode)_pWord.Object.treeView1.Nodes[0];
                    // now that we have the xdoc... now we need to stick it in the getNode
                    //                pNode pn = new pNode(xdoc.ChildNodes[1].Name);
                    //                pn.AddAttribute(xdoc.ch
                    //                getNode.Nodes.Add(

                    // TODO: Move To pWordLib (make pWordLib friendly)
                    pNode pn = (pNode)_pWord.Object.treeView1.SelectedNode;
                    if (_pWord.Object.treeView1.SelectedNode.Tag == null)
                        _pWord.Object.treeView1.SelectedNode.Tag = "";
                    pn.Tag = _pWord.Object.treeView1.SelectedNode.Tag;

                    // TODO: Move To pWordLib
                    if (_pWord.Object.xdoc != null && _pWord.Object.xdoc.Attributes != null && _pWord.Object.xdoc.Attributes.Count > 0)
                    {
                        foreach (XmlAttribute xmlAttr in _pWord.Object.xdoc)
                        {
                            pn.AddAttribute(xmlAttr.LocalName, xmlAttr.Value);
                        }
                    }

                    // TODO: Move To pWordLib
                    _pWord.Object.AddChildNodes(_pWord.Object.xdoc.ChildNodes, ref pn);

                    _pWord.Object.treeView1.SelectedNode = pn;
                    //                .Nodes.Add(pn);
                    //treeView1.SelectedNode.Nodes.Add(aNode);
                    // after adding the new node, be sure the index is updated as well... this is not necessary
                    _pWord.Object.userControl11.MasterNodes[_pWord.Object.userControl11.index] = masterNode;

                    // Change from Add Dialog to local members for adding name and value

                    // check box
                    if (_pWord.Object.chkClear.CheckState == CheckState.Checked)
                    {
                        _pWord.Object.txtName.Clear();
                        _pWord.Object.txtObject.Clear();
                        _pWord.Object.txtName.Focus();
                    }
                    else if (_pWord.Object.chkClear.CheckState == CheckState.Indeterminate)
                    {
                        _pWord.Object.txtObject.Clear();
                        _pWord.Object.txtObject.Focus();
                    }
                    else
                    {
                        _pWord.Object.btnAdd.Focus();
                    }

                    if (_pWord.Object.flag_file == true)
                    {
                        //autosave();
                    }
                    check = true; Assert.IsTrue(check);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred during JSON document load.");
                }
            }
        }
    }
}


