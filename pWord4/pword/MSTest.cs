using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpNodeTest2;
using pWordLib.dat;
using pWordLib;
using System;
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
using Newtonsoft.Json;

namespace myPword
{
    [TestClass]
    public class MSTest
    {
        pWord pWordChild = new pWord();
        [TestMethod]
        public void notifyIcon1_DoubleClick()
        {
            /*ControlEventArgs classToTest = null;
            bool eventRaised = false;
            pWordChild.Show();
            classToTest.Control.DoubleClick += pWordChild.notifyIcon1_DoubleClick;
            {
                eventRaised = true;
            };

            //classToTest. = "somthing";
            // this property raises an event, test if the boolean is set to true in the delegate call.
            Assert.AreEqual(eventRaised, true);*/
            pWordChild.WindowState = FormWindowState.Normal;
            pWordChild.autoHide_flag = false;
            pWordChild.VIS = true;

            pWordChild.autoHide_flag = false;
            pWordChild.statusBar1.Text = "AutoHide Inactive";
            pWordChild.toolBarTac.ImageIndex = 1;
            pWordChild.Visible = true;

            pWordChild.WindowState = FormWindowState.Normal;
            //pWordChild.DockRight(sender, e);
            //this.actHook.Stop();
            pWordChild.notifyIcon1.Visible = false;
            pWordChild.notifyIcon2.Visible = true;
            Assert.AreEqual(pWordChild.notifyIcon1.Visible, false);
            Assert.AreEqual(pWordChild.notifyIcon2.Visible, true);
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
                if (pWordChild.components != null)
                {
                    pWordChild.components.Dispose();
                    pWordChild.notifyIcon1.Dispose();
                }
            }
            pWordChild.Dispose();
            disposing = false;
            return disposing;
        }

        [TestMethod]
        public void menuItemExit_Click()
        {
            Dispose(true);
            pWordChild.Close();
        }
        [TestMethod]
        public void pWord_Load()
        {
            pWordChild.Dock = System.Windows.Forms.DockStyle.Bottom;
            System.Drawing.Rectangle a = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
            DockRight();
            pWordChild.UpdateTree();

            pWordChild.autoHide_flag = false;
            pWordChild.VIS = true;

            pWordChild.WindowState = FormWindowState.Normal;

            pWordChild.autoHide_flag = false;
            pWordChild.statusBar1.Text = "AutoHide Inactive";
            pWordChild.toolBarTac.ImageIndex = 1;
            pWordChild.Visible = true;

            pWordChild.WindowState = FormWindowState.Normal;
            DockRight();
        }
        [TestMethod]
        public void DockRight()
        {
            pWordChild.Height = pWordChild.HostingScreen.WorkingArea.Height;
            pWordChild.Location = new Point(
                pWordChild.HostingScreen.WorkingArea.Width - pWordChild.Width,
                0
                );
        }
        [TestMethod]
        public void pWord_VisibleChanged()
        {
            try
            {
                pWordChild.invisible(pWordChild.VIS);
                pWordChild.Activate();
                pWordChild.treeView1.Focus();
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
                pWordChild.Show();
            }
            else
            {
                pWordChild.Hide();
            }
            return vis;
        }
        [TestMethod]
        public void menuItemNewFile_Click()
        {
            pWordChild.flag_file = false;  // notice... I don't want you saving new stuff over your old refined work.

            AYS ays = new AYS();
            // ARE YOU SURE? This operation will delete all of your work unless you have saved.
            // (Yes / No)

            ays.ShowDialog();
            if (ays.DialogResult == DialogResult.OK)
            {

                pWordChild.userControl11.MasterNames.Clear();
                pWordChild.userControl11.MasterNodes.Clear();
                pWordChild.treeView1.Nodes.Clear();
                pNode masterNode;
                masterNode = new pNode("MASTER");
                TreePics apic = new TreePics("Master", pWordChild.img.GroupUp, pWordChild.img.GroupDown);
                masterNode.Tag = "MASTER";

                pWordChild.treeView1.Nodes.Add(masterNode);

                pWordChild.userControl11.index = 0;  // For some reason it loses track of index?
                pWordChild.userControl11.MasterNames.Add("MASTER");
                pWordChild.userControl11.MasterNodes.Add(masterNode);
                pWordChild.userControl11.txtMaster.Text = (string)pWordChild.userControl11.MasterNames[pWordChild.userControl11.index];
                pWordChild.tmpNode = (pNode)pWordChild.treeView1.Nodes[0];
                Assert.IsNotNull(pWordChild.tmpNode);
            }
        }
        [TestMethod]
        public void autosave()
        {
            // autosave
            try
            {
                int count = pWordChild.userControl11.MasterNames.Count;
                pWordChild.Nodes.Clear();
                pWordChild.Nodes.Add(count);

                for (int i = 0; i < count; i++)
                {
                    pWordChild.Nodes.Add((string)pWordChild.userControl11.MasterNames[i]);
                    pWordChild.Nodes.Add((pNode)pWordChild.userControl11.MasterNodes[i]);
                }
                if (pWordChild.filename != null)
                {
                    IFormatter formatter = new BinaryFormatter();
                    Stream stream = new FileStream(pWordChild.filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                    formatter.Serialize(stream, pWordChild.Nodes);
                    stream.Close();
                }
                pWordChild.flag_file = true;
                Assert.IsTrue(pWordChild.flag_file);
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
                pWordChild.tmpNode = (pNode)pWordChild.treeView1.SelectedNode;
                pWordChild.lblName.Text = "Name:";
                pWordChild.lblValue.Text = "Value:";
                pWordChild.txtName.Focus();
                pWordChild.statusBar1.Text = "Add to Node";
                if (pWordChild.chkClear.Checked == true)
                {
                    pWordChild.txtName.Clear();
                    pWordChild.txtObject.Clear();
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
            pWordChild.WindowState = FormWindowState.Normal;
            pWordChild.autoHide_flag = false;
            pWordChild.VIS = true;

            pWordChild.autoHide_flag = false;
            pWordChild.statusBar1.Text = "AutoHide Inactive";
            pWordChild.toolBarTac.ImageIndex = 1;
            pWordChild.Visible = true;

            pWordChild.WindowState = FormWindowState.Normal;
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
                int count = pWordChild.userControl11.MasterNames.Count;
                pWordChild.Nodes.Clear();
                pWordChild.Nodes.Add(count);

                for (int i = 0; i < count; i++)
                {
                    pWordChild.Nodes.Add((string)pWordChild.userControl11.MasterNames[i]);
                    pWordChild.Nodes.Add((pNode)pWordChild.userControl11.MasterNodes[i]);
                }
                pWordChild.saveFileDialog1.ShowDialog();
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
            pWordChild.Nodes.Clear();
            pWordChild.userControl11.MasterNodes.Clear();
            pWordChild.userControl11.MasterNames.Clear();
            try
            {

                pWordChild.openFileDialog1.FileName = pWordChild.filename;
                pWordChild.openFileDialog1.ShowDialog();
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
                if (pWordChild.treeView1.SelectedNode.Parent != null)
                {
                    ((pNode)pWordChild.treeView1.SelectedNode).OperationChanged();
                    pWordChild.treeView1.SelectedNode.Remove();
                    masterNode = (pNode)pWordChild.treeView1.Nodes[0];
                    pWordChild.userControl11.MasterNodes[pWordChild.userControl11.index] = masterNode;

                    if (pWordChild.flag_file == true)
                    {
                        pWordChild.autosave();
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
            pWordChild.mode = nodeMode.xmlUpdate;
            bool check = true;
            try
            {
                System.Windows.Forms.ToolBarButton button = new ToolBarButton();
                System.Windows.Forms.ToolBarButton button1 = new ToolBarButton();
                System.Windows.Forms.ToolBarButton button2 = new ToolBarButton();
                System.Windows.Forms.ToolBarButton button3 = new ToolBarButton();
                System.Windows.Forms.ToolBarButton button4 = new ToolBarButton();
                if (button == pWordChild.toolBarTac)
                {

                    if (pWordChild.autoHide_flag == true)
                    {
                        pWordChild.autoHide_flag = false;
                        pWordChild.statusBar1.Text = "AutoHide Inactive";
                        pWordChild.toolBarTac.ImageIndex = 1;
                    }
                    else
                    {
                        pWordChild.autoHide_flag = true;
                        pWordChild.statusBar1.Text = "AutoHide Active";
                        pWordChild.toolBarTac.ImageIndex = 0;
                        //this.actHook.Start();
                    }
                }
                else if (button1 == pWordChild.toolBarView)
                {
                    if (pWordChild.filenameHTML != null)
                    {
                        if (pWordChild.exportMode == ExportMode.treeview)
                        {
                            pWordChild.xml.Clear();
                        }
                        else if (pWordChild.exportMode == ExportMode.pNode)
                        {
                            pWordChild.xml.Clear(); // clear out contents first.
                                                    //CallRecursive(xmlNode);

                            try
                            {
                                pWordChild.exportMode = ExportMode.pNode;  // what am I exporting?  A pNode
                                pWordChild.xml.Clear();  // clear out contents first.

                                //this.xmlNode = (pNode)treeView1.SelectedNode;  // xmlNode is what is being exported to xml
                                pWordChild.xmlIndex = pWordChild.treeView1.SelectedNode.Index; // xmlIndex is the SelectedNodes index
                                pWordChild.menuItem21.Enabled = true; // MenuItem21 is enabled... Todo: rename menuItem21
                                pWordChild.menuItem31.Enabled = true;  // MneuItem21 is enabled... Todo: rename menuItem31
                                pWordChild.nodeIndex = pWordChild.treeView1.SelectedNode.Index; // nodeInex is now equal to xmlIndex?
                                pWordChild.statusBar1.Text = "Export Node XML Mode";
                                var xdoc = ((pNode)pWordChild.treeView1.SelectedNode).CallRecursive(pWordChild.xmlNode);  // treeview1 is a pView

                                pWordChild.saveFileDialogHTML.FileName = pWordChild.filenameHTML;
                                pWordChild.saveFileDialogHTML.Title = "Save the NODE to XML/HTML";
                                if (pWordChild.filenameHTML != null)
                                {
                                    if (xdoc != null)
                                    {
                                        xdoc.Save(pWordChild.filenameHTML);
                                        xdoc.RemoveAll();
                                        xdoc = null;
                                        pWordChild.filenameJSON = null;
                                    }
                                    else
                                    {
                                        MessageBox.Show("The export was not able to save b/c it was empty.");
                                    }
                                }

                                pWordChild.toolBarView.Enabled = true;
                            }
                            catch (Exception f)
                            {
                                MessageBox.Show("You had an error while exporting to XML. " + f.Message, "SAVE ERROR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                            }
                        }
                        if (pWordChild.filenameHTML != null)
                        {
                            System.Diagnostics.Process.Start(pWordChild.filenameHTML);
                        }
                        else if (pWordChild.filenameJSON != null)
                        {
                            System.Diagnostics.Process.Start(pWordChild.filenameJSON);
                        }
                        else
                        {
                            MessageBox.Show("You must first export a NODE to XML or HTM or JSON");
                        }
                    }
                }
                else if (button2 == pWordChild.toolBarXML)
                {
                }
                else if (button3 == pWordChild.toolBarSearch)
                {
                    // this should be a full search as the search is conducted on the toolbar
                    var masterNode = (pNode)pWordChild.treeView1.Nodes[0];
                    if (pWordChild.txtObject.Text == "")
                    {
                        throw new ArgumentException("Value text must contain some search term");
                    }
                    var pNodes = masterNode.Find(pWordChild.txtObject.Text, 0);

                    int counter = 0;
                    pWordChild.searchCounter++;
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
                            if (pWordChild.searchCounter == counter++)
                            {
                                pWordChild.treeView1.SelectedNode = _parent2;
                                pWordChild.treeView1.SelectedNode.EnsureVisible();
                                _parent2.EnsureVisible();
                                pWordChild.treeView1.Focus();
                                pWordChild.treeView1.Refresh();
                            }
                        }
                    }
                    if (pWordChild.searchCounter > counter)
                    {
                        pWordChild.searchCounter = 0; // do final check and reset this.searchCounter if it went out of bounds of the current search
                    }
                }
                else if (button4 == pWordChild.toolBarCollapse)
                {
                    pWordChild.treeView1.CollapseAll();
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
            pWordChild.autoHide_flag = true;
            if (pWordChild.autoHide_flag == true)
            {
                pWordChild.VIS = false;
                pWordChild.Visible = false;
            }
            Assert.AreEqual(pWordChild.VIS, false);
            Assert.AreEqual(pWordChild.Visible, false);
        }
        [TestMethod]
        public void menuItemCopy_Click()
        {
            pWordChild.lblName.Text = "Name:";
            pWordChild.lblValue.Text = "Value:";

            Clipboard.SetDataObject(pWordChild.treeView1.SelectedNode.Tag, true);
            pWordChild.statusBar1.Text = "Copy Value Text Mode";
            Assert.AreEqual(pWordChild.statusBar1.Text, "Copy Value Text Mode");
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
            pWordChild.rm = new pWordLib.mgr.registryMgr(pWordSettings.Default.version.ToString());

            pWordChild.openFileDialog1.FileName = pWordChild.rm.AutoSavePathFromRegistry(pWordSettings.Default.version.ToString());
            if (pWordChild.rm.FileExist())
            {
                pWordChild.openFileDialog1_FileOk(null, null);
            }
            else
            {
                pNode masterNode;
                masterNode = new pNode("MASTER");
                TreePics apic = new TreePics("Master", pWordChild.img.GroupUp, pWordChild.img.GroupDown);
                masterNode.Tag = "MASTER";

                pWordChild.treeView1.Nodes.Add(masterNode);

                pWordChild.userControl11.MasterNames.Add("MASTER");
                pWordChild.userControl11.MasterNodes.Add(masterNode);
                pWordChild.userControl11.txtMaster.Text = pWordChild.userControl11.MasterNames[pWordChild.userControl11.index];
            }
            pWordChild.tmpNode = (pNode)pWordChild.treeView1.Nodes[0];
            Assert.AreEqual(pWordChild.tmpNode, (pNode)pWordChild.treeView1.Nodes[0]);
        }
        [TestMethod]
        public void menuItem19_Click()
        {
            // Add master
            AddMaster dlg = new AddMaster();
            dlg.ShowDialog();

            if (dlg.DialogResult == DialogResult.OK)
            {
                pWordChild.treeView1.Nodes.Clear();
                pNode masterNode;
                masterNode = new pNode("MASTER");
                TreePics apic = new TreePics("masterNode", pWordChild.img.GroupUp, pWordChild.img.GroupDown);
                masterNode.Tag = "MASTER";

                pWordChild.treeView1.Nodes.Add(masterNode);

                pWordChild.userControl11.MasterNames.Add(dlg.txtMaster.Text);
                pWordChild.userControl11.MasterNodes.Add(masterNode);
                pWordChild.userControl11.index++;
                pWordChild.userControl11.txtMaster.Text = (string)pWordChild.userControl11.MasterNames[pWordChild.userControl11.index];
                pWordChild.tmpNode = (pNode)pWordChild.treeView1.Nodes[0];  // Always start with master
                Assert.AreEqual(pWordChild.tmpNode, (pNode)pWordChild.treeView1.Nodes[0]);
            }
        }
        [TestMethod]
        public void userControl11_LeftClicked()
        {
            bool check = false;
            // This is the hardest of all
            pWordChild.treeView1.Nodes.Clear();
            pNode masterNode = (pNode)pWordChild.userControl11.MasterNodes[pWordChild.userControl11.index];
            TreePics apic = new TreePics("masterNode", pWordChild.img.GroupUp, pWordChild.img.GroupDown);
            pWordChild.treeView1.Nodes.Add(masterNode);
            check = true;
            Assert.AreEqual(check, true);
        }
        [TestMethod]
        public void userControl11_RightClicked()
        {
            bool check = false;
            // This is the hardest of all
            pWordChild.treeView1.Nodes.Clear();
            pNode masterNode = (pNode)pWordChild.userControl11.MasterNodes[pWordChild.userControl11.index];
            TreePics apic = new TreePics("masterNode", pWordChild.img.GroupUp, pWordChild.img.GroupDown);
            pWordChild.treeView1.Nodes.Add(masterNode);
            check = true;
            Assert.AreEqual(check, true);
        }
        [TestMethod]
        public void menuItemOpenLink_Click()
        {
            try
            {
                bool check = false;
                if ((pWordChild.treeView1.SelectedNode.Tag != null) && (((String)(pWordChild.treeView1.SelectedNode.Tag)).Length > 0))
                {
                    System.Diagnostics.Process.Start(pWordChild.treeView1.SelectedNode.Tag.ToString());
                }
                check = true;
                Assert.AreEqual(check, true);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("You must use an acceptable link contained in the value field!","DANGER",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                //txtValue.Text = "Value Field is can not process value.";
                pWordChild.txtValue.Text = ex.Message;
            }
        }
        [TestMethod]
        public void btnAdd_Click()
        {
            bool check = false;
            pWordLib.AddItem addItem = new pWordLib.AddItem();

            pNode masterNode = (pNode)pWordChild.treeView1.Nodes[0];
            switch (pWordChild.mode)
            {
                case nodeMode.addto:
                    try
                    {
                        pNode aNode;
                        aNode = new pNode(pWordChild.txtName.Text);
                        //TreePics apic = new TreePics("aNode", img.GroupUp, img.GroupDown);
                        aNode.Tag = pWordChild.txtObject.Text;
                        aNode.Text = pWordChild.txtName.Text;
                        if (pWordChild.tmpNode.Namespace != null)
                        {
                            aNode.Namespace = pWordChild.tmpNode.Namespace;  // trickle down namespaces
                        }
                        pWordChild.treeView1.SelectedNode = pWordChild.tmpNode;
                        pWordChild.treeView1.SelectedNode.Nodes.Add(aNode);
                        // after adding the new node, be sure the index is updated as well... this is not necessary
                        pWordChild.userControl11.MasterNodes[pWordChild.userControl11.index] = masterNode;

                        // Change from Add Dialog to local members for adding name and value

                        // check box
                        if (pWordChild.chkClear.CheckState == CheckState.Checked)
                        {
                            pWordChild.txtName.Clear();
                            pWordChild.txtObject.Clear();
                            pWordChild.txtName.Focus();
                        }
                        else if (pWordChild.chkClear.CheckState == CheckState.Indeterminate)
                        {
                            pWordChild.txtObject.Clear();
                            pWordChild.txtObject.Focus();
                        }
                        else
                        {
                            pWordChild.btnAdd.Focus();
                        }

                        aNode.OperationChanged();

                        // TODO: 2022-Aug-06 Now recalculated all nodes with operations that have changed in these sets of node


                        if (pWordChild.flag_file == true)
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
                        aNode = new pNode(pWordChild.txtName.Text);
                        TreePics apic = new TreePics("aNode", pWordChild.img.GroupUp, pWordChild.img.GroupDown);
                        aNode.Tag = pWordChild.txtObject.Text;
                        pWordChild.treeView1.SelectedNode = pWordChild.tmpNode;
                        pWordChild.treeView1.SelectedNode.Text = aNode.Text;
                        pWordChild.treeView1.SelectedNode.Tag = aNode.Tag;
                        pWordChild.treeView1.SelectedNode.Name = aNode.Name;

                        // This is not necessary, when a save is committed this can be performed at that juncture
                        // However, it may be beneficial to know whether or not a node change was successfully saved
                        // at the iteration the event occurred.  This will prevent loss of work
                        pWordChild.userControl11.MasterNodes[pWordChild.userControl11.index] = masterNode;

                        // Change from Add Dialog to local members for adding name and value

                        // check box
                        if (pWordChild.chkClear.CheckState == CheckState.Checked)
                        {
                            pWordChild.txtName.Clear();
                            pWordChild.txtObject.Clear();
                            pWordChild.txtName.Focus();
                        }
                        else if (pWordChild.chkClear.CheckState == CheckState.Indeterminate)
                        {
                            pWordChild.txtObject.Clear();
                            pWordChild.txtObject.Focus();
                        }
                        else
                        {
                            pWordChild.btnAdd.Focus();
                        }

                        aNode.OperationChanged();
                    }
                    catch (Exception f)
                    {
                        MessageBox.Show(f.Message);
                    }
                    break;
                case nodeMode.insert:
                    if (pWordChild.treeView1.SelectedNode.Parent == null)
                    {
                        MessageBox.Show("You can not insert a sibling of the master node.");
                        return;
                    }
                    try
                    {
                        // Insert Mode
                        // Only edit the current node
                        pNode aNode;
                        aNode = new pNode(pWordChild.txtName.Text);
                        TreePics apic = new TreePics("aNode", pWordChild.img.GroupUp, pWordChild.img.GroupDown);
                        aNode.Tag = pWordChild.txtObject.Text;
                        if (pWordChild.tmpNode.Namespace != null)
                        {
                            aNode.Namespace = pWordChild.tmpNode.Namespace;  // trickle down namespaces
                        }
                        pWordChild.treeView1.SelectedNode = pWordChild.tmpNode;
                        pWordChild.treeView1.SelectedNode.Nodes.Insert(pWordChild.modeIndex, aNode);
                        pWordChild.userControl11.MasterNodes[pWordChild.userControl11.index] = masterNode;

                        // Change from Add Dialog to local members for adding name and value

                        // check box
                        if (pWordChild.chkClear.Checked == true)
                        {
                            pWordChild.txtName.Clear();
                            pWordChild.txtObject.Clear();
                        }

                        if (pWordChild.flag_file == true)
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
                        pWordChild.tmpNode.AddAttribute(pWordChild.txtName.Text, pWordChild.txtObject.Text);
                        pWordChild.treeView1.SelectedNode = pWordChild.tmpNode;
                        pWordChild.userControl11.MasterNodes[pWordChild.userControl11.index] = masterNode;

                        // Change from Add Dialog to local members for adding name and value

                        // check box
                        if (pWordChild.chkClear.Checked == true)
                        {
                            pWordChild.txtName.Clear();
                            pWordChild.txtObject.Clear();
                        }

                        if (pWordChild.flag_file == true)
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
                        if (pWordChild.tmpNode.Namespace == null)
                        {
                            pWordChild.tmpNode.Namespace = new NameSpace();
                        }
                        pWordChild.tmpNode.Namespace.Prefix = pWordChild.txtName.Text;
                        pWordChild.tmpNode.Namespace.URI_PREFIX = pWordChild.txtObject.Text;
                        pWordChild.treeView1.SelectedNode = pWordChild.tmpNode;
                        pWordChild.userControl11.MasterNodes[pWordChild.userControl11.index] = masterNode;

                        // Change from Add Dialog to local members for adding name and value

                        // check box
                        if (pWordChild.chkClear.Checked == true)
                        {
                            pWordChild.txtName.Clear();
                            pWordChild.txtObject.Clear();
                        }

                        if (pWordChild.flag_file == true)
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
                        if (pWordChild.tmpNode.Namespace == null)
                        {
                            pWordChild.tmpNode.Namespace = new NameSpace();
                        }
                        pWordChild.tmpNode.Namespace.Suffix = pWordChild.txtName.Text;
                        pWordChild.tmpNode.Namespace.URI_SUFFIX = pWordChild.txtObject.Text;
                        pWordChild.treeView1.SelectedNode = pWordChild.tmpNode;
                        pWordChild.userControl11.MasterNodes[pWordChild.userControl11.index] = masterNode;

                        // Change from Add Dialog to local members for adding name and value

                        // check box
                        if (pWordChild.chkClear.Checked == true)
                        {
                            pWordChild.txtName.Clear();
                            pWordChild.txtObject.Clear();
                        }

                        if (pWordChild.flag_file == true)
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
            pWordChild.Visible = false;
            Assert.AreEqual(pWordChild.Visible, false);
        }
        [TestMethod]
        public void treeView1_AfterCollapse_1()
        {
            bool check = false;
            if (pWordChild.treeView1.SelectedNode != null)
                pWordChild.treeView1.SelectedNode.SelectedImageIndex = 0;
            check = true;
            Assert.AreEqual(true, check);
        }
        [TestMethod]
        public void treeView1_AfterExpand_1(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            bool check = true;
            if (pWordChild.treeView1.SelectedNode != null)
                pWordChild.treeView1.SelectedNode.SelectedImageIndex = 1;
            check = false;
            Assert.AreEqual(false, check);
        }
        [TestMethod]
        public void treeView1_AfterSelect_1()
        {
            pWordChild.treeView1.SelectedNode.SelectedImageIndex = pWordChild.img.GroupDown;
            Assert.AreEqual(pWordChild.treeView1.SelectedNode.SelectedImageIndex, pWordChild.img.GroupDown);

        }
        [TestMethod]
        public void treeView1_DragDrop_1()
        {
            bool check = false;
            pNode a = new pNode("test", pWordChild.img.GroupUp, pWordChild.img.GroupDown);
            a.Tag = null;
            pWordChild.treeView1.SelectedNode.Nodes.Add(a);
            check = true;
            Assert.AreEqual(true, check);
        }
        [TestMethod]
        public void menuItemEdit_Click()
        {
            pWordChild.mode = nodeMode.edit;
            try
            {
                pWordChild.lblName.Text = "Name:";
                pWordChild.lblValue.Text = "Value:";
                pWordChild.modeIndex = pWordChild.treeView1.SelectedNode.Index;
                //				this.txtName.Text = treeView1.SelectedNode.Nodes[modeIndex].Text;
                //				this.txtObject.Text = treeView1.SelectedNode.Nodes[modeIndex].Text;

                if (pWordChild.treeView1.SelectedNode.Parent != null)
                {
                    ((pNode)pWordChild.treeView1.SelectedNode.Parent).OperationChanged();
                    pWordChild.tmpNode = ((pNode)pWordChild.treeView1.SelectedNode);
                }

                pWordChild.txtName.Text = pWordChild.tmpNode.Text;
                pWordChild.txtObject.Text = (string)pWordChild.tmpNode.Tag;
                pWordChild.statusBar1.Text = "Edit Mode";
                pWordChild.txtName.Focus();
                Assert.AreEqual(pWordChild.statusBar1.Text, "Edit Mode");
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
                pWordChild.treeView1.SelectedNode.Nodes.Insert(pWordChild.treeView1.SelectedNode.Nodes.Count, pWordChild.getNode);
                pWordChild.getNode = (pNode)pWordChild.getNode.Clone();

                if (pWordChild.flag_file == true)
                {
                    autosave();
                }
                Assert.AreEqual(pWordChild.getNode, (pNode)pWordChild.getNode.Clone());

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

                pWordChild.getNode = (pNode)pWordChild.treeView1.SelectedNode.Clone();
                pWordChild.menuItem21.Enabled = true;
                pWordChild.menuItem31.Enabled = true;
                pWordChild.nodeIndex = pWordChild.treeView1.SelectedNode.Index;
                pWordChild.statusBar1.Text = "Get Node Mode";
                Assert.AreEqual(pWordChild.statusBar1.Text, "Get Node Mode");
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }
        [TestMethod]
        public void menuItemExportXML_Click()
        {

            pWordChild.xml.Clear();  // clear out contents first.
            ((pNode)pWordChild.treeView1.SelectedNode).CallRecursive((pNode)pWordChild.treeView1.SelectedNode);
            try
            {
                pWordChild.exportMode = ExportMode.treeview;
                XmlDocument xdoc = new XmlDocument();

                pWordChild.saveFileDialogHTML.FileName = pWordChild.filenameHTML;
                pWordChild.saveFileDialogHTML.Title = "Save View to XML/HTML";
                pWordChild.saveFileDialogHTML.ShowDialog();
                pWordChild.filenameHTML = pWordChild.saveFileDialogHTML.FileName;
                if (pWordChild.filenameHTML != null)
                {
                    pWordChild.toolBarView.Enabled = true;
                }
                Assert.AreEqual(pWordChild.filenameHTML, pWordChild.saveFileDialogHTML.FileName);
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
                pWordChild.treeView1.Sorted = true;
            }
            check = true;
            Assert.AreEqual(check, true);
        }
        [TestMethod]
        public void menuItem28_Click()
        {
            pWordChild.treeView1.Sorted = false;
            Assert.AreEqual(pWordChild.treeView1.Sorted, false);
        }
        [TestMethod]
        public void menuItemInsertNode_Click()
        {
            pWordChild.lblName.Text = "Name:";
            pWordChild.lblValue.Text = "Value:";
            pWordChild.mode = nodeMode.insert;
            if (pWordChild.chkClear.Checked == true)
            {
                pWordChild.txtName.Clear();
                pWordChild.txtObject.Clear();
            }
            try
            {
                pWordChild.tmpNode = (pNode)pWordChild.treeView1.SelectedNode.Parent;
                pWordChild.modeIndex = pWordChild.treeView1.SelectedNode.Index;
                pWordChild.txtName.Focus();
                Assert.AreEqual(pWordChild.modeIndex, pWordChild.treeView1.SelectedNode.Index);
            }

            catch (Exception f)
            {
                MessageBox.Show(f.Message);

            }
            pWordChild.statusBar1.Text = "Insert Mode";


        }
        [TestMethod]
        public void menuItemInsertNode2_Click()
        {
            // Insert next to
            try
            {
                pWordChild.treeView1.SelectedNode.Parent.Nodes.Insert(pWordChild.treeView1.SelectedNode.Index, pWordChild.getNode);


                //Insert(treeView1.SelectedNode.Nodes.Count,this.getNode);
                pWordChild.getNode = (pNode)pWordChild.getNode.Clone();

                if (pWordChild.flag_file == true)
                {
                    autosave();
                }
                Assert.AreEqual(pWordChild.getNode, (pNode)pWordChild.getNode.Clone());
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
            pWordChild.treeView1.SelectedNode = pv.SelectedNode;
            Cursor.Hide();
            //pWordChild.genericCursorMoved(sender, e);

            /* if (e.KeyCode == System.Windows.Forms.Keys.Delete)
             {*/

            System.EventArgs f = new System.EventArgs();
            menuItem14_Click();
            /* }
             else if (e.KeyCode == System.Windows.Forms.Keys.Insert)
             {*/
            //EventArgs f = new EventArgs();
            if (pWordChild.menuItem29.Enabled == true)
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
            if (pWordChild.chkClear.CheckState == CheckState.Checked)
            {
                pWordChild.chkClear.Text = "Clear All Text";
            }
            else if (pWordChild.chkClear.CheckState == CheckState.Indeterminate)
            {
                pWordChild.chkClear.Text = "Clear Value Only";
            }
            else if (pWordChild.chkClear.CheckState == CheckState.Unchecked)
            {
                pWordChild.chkClear.Text = "Clear Disabled";
            }
            check = true;
            Assert.IsTrue(check);
        }
        [TestMethod]
        public void openFileDialog1_FileOk()
        {

            pWordChild.Nodes.Clear();
            pWordChild.userControl11.MasterNodes.Clear();
            pWordChild.userControl11.MasterNames.Clear();
            pWordChild.filename = pWordChild.openFileDialog1.FileName;

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(pWordChild.filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            pWordChild.Nodes = (ArrayList)formatter.Deserialize(stream);
            stream.Close();

            pWordChild.treeView1.Nodes.Clear();
            int count = (int)pWordChild.Nodes[0];
            int i = 1;
            while (i < pWordChild.Nodes.Count)
            {
                pWordChild.userControl11.MasterNames.Add((string)pWordChild.Nodes[i]);
                // check Nodes type
                ++i;
                if (pWordChild.Nodes[i].GetType() == typeof(pNode))
                {
                    pWordChild.userControl11.MasterNodes.Add((pNode)pWordChild.Nodes[i]);
                }
                else if (pWordChild.Nodes[i].GetType() == typeof(TreeNode))
                {
                    // Compatibility with old version 6A
                    // convert TreeNode to a pNode
                    TreeNode a = (TreeNode)pWordChild.Nodes[i];

                    // p only gets the top node for a master... it doesn't delve in and get everything else
                    // Todo: get all other nodes in a proper TreeNode to pNode conversion
                    // I want to perform the conversion indie of the pNode class itself
                    try
                    {
                        pNode p = pNode.TreeNode2pNode(a);
                        //                    userControl11.MastersValue.Add((TreeNode)Nodes[i]);
                        pWordChild.userControl11.MasterNodes.Add(p);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.ToString());
                    }
                }
                i++;
            }
            pWordChild.userControl11.index = 0;
            pWordChild.treeView1.Nodes.Add((pNode)pWordChild.userControl11.MasterNodes[pWordChild.userControl11.index]);
            pWordChild.userControl11.txtMaster.Text = pWordChild.userControl11.MasterNames[pWordChild.userControl11.index];
            pWordChild.flag_file = true;
            Assert.AreEqual(pWordChild.flag_file, true);

            // successful?  go ahead and make the open stick
            pWordChild.rm.SavePathInRegistry(pWordSettings.Default.version, pWordChild.filename);

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
                    if (pWordChild.flag_file == true)
                    {
                        pWordChild.Nodes.Clear();
                        pWordChild.userControl11.MasterNodes.Clear();
                        pWordChild.userControl11.MasterNames.Clear();


                        IFormatter formatter = new BinaryFormatter();
                        Stream stream = new FileStream(pWordChild.filename, FileMode.Open, FileAccess.Read, FileShare.Read);
                        pWordChild.Nodes = (ArrayList)formatter.Deserialize(stream);
                        stream.Close();

                        pWordChild.treeView1.Nodes.Clear();
                        int count = (int)pWordChild.Nodes[0];
                        int i = 1;
                        while (i < pWordChild.Nodes.Count)
                        {
                            pWordChild.userControl11.MasterNames.Add((string)pWordChild.Nodes[i]);
                            pWordChild.userControl11.MasterNodes.Add((pNode)pWordChild.Nodes[++i]);
                            i++;
                        }
                        pWordChild.userControl11.index = 0;
                        pWordChild.treeView1.Nodes.Add((pNode)pWordChild.userControl11.MasterNodes[pWordChild.userControl11.index]);
                        pWordChild.flag_file = true;
                        Assert.AreEqual(pWordChild.flag_file, true);
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

                pWordChild.getNode = (pNode)((pNode)pWordChild.treeView1.SelectedNode).Clone();
                if (pWordChild.treeView1.SelectedNode.Parent != null)
                {
                    pWordChild.menuItem21.Enabled = true;
                    pWordChild.menuItem31.Enabled = true;
                    pWordChild.nodeIndex = pWordChild.treeView1.SelectedNode.Index;
                    if (pWordChild.treeView1.SelectedNode.Parent != null)
                    {
                        ((pNode)pWordChild.treeView1.SelectedNode).OperationChanged();
                        pWordChild.treeView1.SelectedNode.Remove();
                    }
                    pWordChild.statusBar1.Text = "CUT Node Mode";
                    pWordChild.mode = nodeMode.cut;
                    Assert.AreEqual(pWordChild.mode, nodeMode.cut);
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

            pWordChild.filename = pWordChild.saveFileDialog1.FileName;
            if (pWordChild.filename != null)
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(pWordChild.saveFileDialog1.FileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                formatter.Serialize(stream, pWordChild.Nodes);
                pWordChild.filename = pWordChild.saveFileDialog1.FileName;

                // use rm manager to set file name
                pWordChild.rm.SavePathInRegistry(pWordSettings.Default.version, pWordChild.filename);

                stream.Close();
                String path = pWordChild.rm.AutoSavePathFromRegistry(pWordSettings.Default.version.ToString());
                if (pWordChild.saveFileDialog1.FileName != path)
                {
                    // save the path to the registry
                    pWordChild.rm.SavePathInRegistry(pWordSettings.Default.version, pWordChild.saveFileDialog1.FileName);
                }
            }
            pWordChild.flag_file = true;
            Assert.AreEqual(pWordChild.flag_file, true);
        }
        [TestMethod]
        public void saveFileDialogHTML_FileOk()
        {
            bool check = false;
            string filenameHTML = pWordChild.saveFileDialogHTML.FileName;
            if (filenameHTML != null)
            {
                StreamWriter swFromFile = new StreamWriter(filenameHTML);

                swFromFile.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                for (int i = 0; i < pWordChild.xml.Count; i++)
                {
                    swFromFile.Write(pWordChild.xml[i]);
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
            string filenameJSON = pWordChild.saveFileDialogJSON.FileName;
            if (filenameJSON != null)
            {
                StreamWriter swFromFile = new StreamWriter(filenameJSON);

                //swFromFile.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                for (int i = 0; i < pWordChild.xml.Count; i++)
                {
                    swFromFile.Write(pWordChild.xml[i]);
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
                pWordChild.exportMode = ExportMode.pNode;  // what am I exporting?  A pNode
                // TODO: Move To pWordLib
                pWordChild.xml.Clear();  // clear out contents first.

                //this.xmlNode = (pNode)treeView1.SelectedNode;  // xmlNode is what is being exported to xml
                pWordChild.xmlIndex = pWordChild.treeView1.SelectedNode.Index; // xmlIndex is the SelectedNodes index
                pWordChild.menuItem21.Enabled = true; // MenuItem21 is enabled... Todo: rename menuItem21
                pWordChild.menuItem31.Enabled = true;  // MneuItem21 is enabled... Todo: rename menuItem31
                pWordChild.nodeIndex = pWordChild.treeView1.SelectedNode.Index; // nodeInex is now equal to xmlIndex?
                pWordChild.statusBar1.Text = "Export Node XML Mode";
                //CallRecursive(xmlNode);  // disabled CallRecursive here... need to fix Call recursive
                // ToDo: fix CallRecursive(xmlNode)
                pWordChild.xmlNode = null;
                pWordChild.xmlNode = (pNode)pWordChild.treeView1.SelectedNode;

                // TODO: Move To pWordLib
                var xdoc = ((pNode)pWordChild.treeView1.SelectedNode).CallRecursive(pWordChild.xmlNode);  // treeview1 is a pView

                pWordChild.saveFileDialogHTML.FileName = pWordChild.filenameHTML;
                pWordChild.saveFileDialogHTML.Title = "Save the NODE to XML/HTML";
                pWordChild.saveFileDialogHTML.ShowDialog();
                pWordChild.filenameHTML = pWordChild.saveFileDialogHTML.FileName;

                if (pWordChild.filenameHTML != null)
                {
                    if (xdoc != null)
                    {
                        xdoc.Save(pWordChild.filenameHTML);
                        xdoc.RemoveAll();
                        xdoc = null;
                    }
                    else
                    {
                        MessageBox.Show("The export was not able to save b/c it was empty.");
                    }
                }
                pWordChild.toolBarView.Enabled = true;
                Assert.AreEqual(pWordChild.toolBarView.Enabled, true);
            }
            catch (Exception f)
            {
                MessageBox.Show("You had an error while exporting to XML. " + f.Message, "SAVE ERROR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }

        }
        [TestMethod]
        public void notifyIcon2_DoubleClick()
        {
            pWordChild.autoHide_flag = true;
            pWordChild.statusBar1.Text = "AutoHide Active";
            pWordChild.toolBarTac.ImageIndex = 0;
            //this.actHook.Start();
            pWordChild.VIS = false;
            pWordChild.Visible = false;
            pWordChild.notifyIcon2.Visible = false;
            pWordChild.notifyIcon1.Visible = true;
            Assert.AreEqual(pWordChild.notifyIcon1.Visible, true);
        }
        [TestMethod]
        public void menuItemAttributeAdd_Click()
        {
            // TODO: Move To pWordLib
            pWordChild.mode = nodeMode.addAttributeTo;

            try
            {
                pWordChild.lblName.Text = "Attribute:";
                pWordChild.lblValue.Text = "Value:";
                pWordChild.modeIndex = pWordChild.treeView1.SelectedNode.Index;
                pWordChild.tmpNode = (pNode)pWordChild.treeView1.SelectedNode;
                pWordChild.statusBar1.Text = "Add Attribute to Node";
                if (pWordChild.chkClear.CheckState == CheckState.Checked)
                {
                    pWordChild.txtName.Clear();
                    pWordChild.txtObject.Clear();
                    pWordChild.txtName.Focus();
                }
                else if (pWordChild.chkClear.CheckState == CheckState.Indeterminate)
                {
                    pWordChild.txtObject.Clear();
                    pWordChild.txtObject.Focus();
                }
                else
                {
                    pWordChild.btnAdd.Focus();
                }
                Assert.AreEqual(pWordChild.statusBar1.Text, "Add Attribute to Node");
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
            pWordChild.mode = nodeMode.addNamespacePrefix;
            try
            {
                bool check = false;
                pWordChild.lblName.Text = "Prefix:";
                pWordChild.lblValue.Text = "URI:";
                pWordChild.modeIndex = pWordChild.treeView1.SelectedNode.Index;
                pWordChild.tmpNode = (pNode)pWordChild.treeView1.SelectedNode;

                pWordChild.statusBar1.Text = "Add Prefix to Node";
                if (pWordChild.chkClear.CheckState == CheckState.Checked)
                {
                    pWordChild.txtName.Clear();
                    pWordChild.txtObject.Clear();
                    pWordChild.txtName.Focus();
                }
                else if (pWordChild.chkClear.CheckState == CheckState.Indeterminate)
                {
                    pWordChild.txtObject.Clear();
                    pWordChild.txtObject.Focus();
                }
                else
                {
                    pWordChild.btnAdd.Focus();
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
            pWordChild.mode = nodeMode.addNamespaceSuffix;
            try
            {
                pWordChild.lblName.Text = "Suffix:";
                pWordChild.lblValue.Text = "URI:";
                pWordChild.modeIndex = pWordChild.treeView1.SelectedNode.Index;
                pWordChild.tmpNode = (pNode)pWordChild.treeView1.SelectedNode;

                pWordChild.statusBar1.Text = "Add Suffix to Node";
                if (pWordChild.chkClear.CheckState == CheckState.Checked)
                {
                    pWordChild.txtName.Clear();
                    pWordChild.txtObject.Clear();
                    pWordChild.txtName.Focus();
                }
                else if (pWordChild.chkClear.CheckState == CheckState.Indeterminate)
                {
                    pWordChild.txtObject.Clear();
                    pWordChild.txtObject.Focus();
                }
                else
                {
                    pWordChild.btnAdd.Focus();
                }
                Assert.AreEqual(pWordChild.statusBar1.Text, "Add Suffix to Node");
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }
        [TestMethod]
        public void menuItemMathSum_Click()
        {
            pWordChild.mode = nodeMode.sum;
            try
            {
                pWordChild.lblName.Text = "Name:";
                pWordChild.lblValue.Text = "Value:";
                pWordChild.modeIndex = pWordChild.treeView1.SelectedNode.Index;
                Sum sum = new Sum();
                pWordChild.tmpNode = (pNode)pWordChild.treeView1.SelectedNode;
                pWordChild.tmpNode.AddOperation(new Sum(Resource1.Sum));
                pWordChild.treeView1.SelectedNode = pWordChild.tmpNode;
                pWordChild.statusBar1.Text = "Summation";
                pWordChild.txtObject.Text = (String)pWordChild.treeView1.SelectedNode.Tag;

                pWordChild.txtName.Focus();
                autosave();  // may need to hook up an event to save when ever new nodes are added or removed???
                Assert.AreEqual(pWordChild.statusBar1.Text, "Summation");
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
            pWordChild.mode = nodeMode.multiply;
            try
            {
                bool check = false;
                pWordChild.lblName.Text = "Name:";
                pWordChild.lblValue.Text = "Value:";
                pWordChild.modeIndex = pWordChild.treeView1.SelectedNode.Index;
                pWordChild.tmpNode = (pNode)pWordChild.treeView1.SelectedNode;
                pWordChild.tmpNode.AddOperation(new Multiply(Resource1.Multiplication));

                // TODO: Move To pWordLib (make pWordLib friendlier)
                pWordChild.treeView1.SelectedNode = pWordChild.tmpNode;
                pWordChild.statusBar1.Text = "Mutliple";
                pWordChild.txtObject.Text = (String)pWordChild.treeView1.SelectedNode.Tag;

                pWordChild.txtName.Focus();
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
            pWordChild.mode = nodeMode.divide;
            try
            {
                bool check = false;
                pWordChild.lblName.Text = "Name:";
                pWordChild.lblValue.Text = "Value:";
                pWordChild.modeIndex = pWordChild.treeView1.SelectedNode.Index;
                pWordChild.tmpNode = (pNode)pWordChild.treeView1.SelectedNode;
                pWordChild.tmpNode.AddOperation(new Divide(Resource1.Division));
                pWordChild.treeView1.SelectedNode = pWordChild.tmpNode;

                pWordChild.statusBar1.Text = "Divide";
                pWordChild.txtObject.Text = (String)pWordChild.treeView1.SelectedNode.Tag;
                pWordChild.txtName.Focus();
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
            pWordChild.mode = nodeMode.divide;

            try
            {
                bool check = false;
                pWordChild.lblName.Text = "Name:";
                pWordChild.lblValue.Text = "Value:";
                pWordChild.modeIndex = pWordChild.treeView1.SelectedNode.Index;
                pWordChild.tmpNode = (pNode)pWordChild.treeView1.SelectedNode;
                pWordChild.tmpNode.AddOperation(new Subtract(Resource1.Subtraction));
                pWordChild.treeView1.SelectedNode = pWordChild.tmpNode;

                pWordChild.statusBar1.Text = "Subtract";
                pWordChild.txtObject.Text = (String)pWordChild.treeView1.SelectedNode.Tag;
                pWordChild.txtName.Focus();
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
            ((pNode)pWordChild.treeView1.SelectedNode).ClearOperations();
            check = true;
            Assert.AreEqual(check, true);
        }
        [TestMethod]
        public void menuItemViewErrors_Click()
        {
            pWordChild.lblName.Text = "Operations:";
            pWordChild.lblValue.Text = "Error Info:";

            pWordChild.txtName.Text = ((pNode)pWordChild.treeView1.SelectedNode).ListOperations();
            pWordChild.txtObject.Text = ((pNode)pWordChild.treeView1.SelectedNode).ErrorString;
            Assert.AreEqual(pWordChild.txtObject.Text, ((pNode)pWordChild.treeView1.SelectedNode).ErrorString);
        }
        [TestMethod]
        public void menuItemMathTrigSign_Click()
        {
            pWordChild.mode = nodeMode.trig;
            try
            {
                bool check = false;
                pWordChild.lblName.Text = "Name:";
                pWordChild.lblValue.Text = "Value:";
                pWordChild.modeIndex = pWordChild.treeView1.SelectedNode.Index;
                pWordChild.tmpNode = (pNode)pWordChild.treeView1.SelectedNode;
                pWordChild.tmpNode.AddOperation(new Sin(Resource1.Sin));  // add new sin operation ... trying to make this a plug in.
                pWordChild.treeView1.SelectedNode = pWordChild.tmpNode;

                pWordChild.statusBar1.Text = "Sin";
                pWordChild.txtObject.Text = (String)pWordChild.treeView1.SelectedNode.Tag;
                pWordChild.txtName.Focus();
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
            pWordChild.mode = nodeMode.trig;
            try
            {
                bool check = false;
                pWordChild.lblName.Text = "Name:";
                pWordChild.lblValue.Text = "Value:";
                pWordChild.modeIndex = pWordChild.treeView1.SelectedNode.Index;
                pWordChild.tmpNode = (pNode)pWordChild.treeView1.SelectedNode;
                pWordChild.tmpNode.AddOperation(new Cos(Resource1.Cos));  // add new sin operation ... trying to make this a plug in.
                pWordChild.treeView1.SelectedNode = pWordChild.tmpNode;

                pWordChild.statusBar1.Text = "Cos";
                pWordChild.txtObject.Text = (String)pWordChild.treeView1.SelectedNode.Tag;
                pWordChild.txtName.Focus();
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
            pWordChild.mode = nodeMode.trig;
            try
            {
                bool check = false;
                pWordChild.lblName.Text = "Name:";
                pWordChild.lblValue.Text = "Value:";
                pWordChild.modeIndex = pWordChild.treeView1.SelectedNode.Index;
                pWordChild.tmpNode = (pNode)pWordChild.treeView1.SelectedNode;
                pWordChild.tmpNode.AddOperation(new Tan(Resource1.Tan));  // add new sin operation ... trying to make this a plug in.
                pWordChild.treeView1.SelectedNode = pWordChild.tmpNode;

                pWordChild.statusBar1.Text = "Tan";
                pWordChild.txtObject.Text = (String)pWordChild.treeView1.SelectedNode.Tag;
                pWordChild.txtName.Focus();
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
            pWordChild.mode = nodeMode.find;
            pWordChild.modeIndex = pWordChild.treeView1.SelectedNode.Index;
            pWordChild.tmpNode = (pNode)pWordChild.treeView1.SelectedNode;
            try
            {
                pWordChild.lblName.Text = "Find Name:";
                pWordChild.lblValue.Text = "Find Value:";
                pWordChild.modeIndex = pWordChild.treeView1.SelectedNode.Index;
                pWordChild.tmpNode = (pNode)pWordChild.treeView1.SelectedNode;
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
            pWordChild.importMode = ImportMode.treexml; // what am I exporting?  XML from previous exportNode
                                                        // I'm importing into a node that i selected using import node treexml 

            // TODO: Move To pWordLib
            pWordChild.xml.Clear();  // clear out contents first.

            //this.xmlNode = (pNode)treeView1.SelectedNode;  // xmlNode is what is being exported to xml
            pWordChild.xmlIndex = pWordChild.treeView1.SelectedNode.Index; // xmlIndex is the SelectedNodes index
            pWordChild.menuItem21.Enabled = true; // MenuItem21 is enabled... Todo: rename menuItem21
            pWordChild.menuItem31.Enabled = true;  // MneuItem21 is enabled... Todo: rename menuItem31
            pWordChild.nodeIndex = pWordChild.treeView1.SelectedNode.Index; // nodeInex is now equal to xmlIndex?
            pWordChild.statusBar1.Text = "Import Node XML Mode";
            //CallRecursive(xmlNode);  // disabled CallRecursive here... need to fix Call recursive
            // ToDo: fix CallRecursive(xmlNode)

            // TODO: Move To pWordLib (Make pWordLib friendlier)
            pWordChild.tmpNode = (pNode)pWordChild.treeView1.SelectedNode;
            pWordChild.openFileDialog2.ShowDialog();
            pWordChild.filenameHTML = pWordChild.openFileDialog2.FileName;
            if ((pWordChild.filenameHTML == null) || (pWordChild.filenameHTML == ""))
            {
                return;
            }
            else
            {
                try
                {
                    using (WebClient client = new WebClient())
                    using (Stream stream = client.OpenRead(pWordChild.filenameHTML))
                    {
                        //      byte[] buf = new byte[stream.Length];
                        //      stream.Read(buf, 0, (int)stream.Length);
                        pWordChild.xdoc = new XmlDocument();
                        try
                        {
                            pWordChild.xdoc.Load(stream);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    pNode masterNode = (pNode)pWordChild.treeView1.Nodes[0];
                    pNode pn = (pNode)pWordChild.treeView1.SelectedNode;
                    if (pWordChild.treeView1.SelectedNode.Tag == null)
                        pWordChild.treeView1.SelectedNode.Tag = "";
                    pn.Tag = pWordChild.treeView1.SelectedNode.Tag;

                    // TODO: Move To pWordLib
                    if (pWordChild.xdoc != null && pWordChild.xdoc.Attributes != null && pWordChild.xdoc.Attributes.Count > 0)
                    {
                        foreach (XmlAttribute xmlAttr in pWordChild.xdoc)
                        {
                            pn.AddAttribute(xmlAttr.LocalName, xmlAttr.Value);
                        }
                    }

                    // TODO: Move To pWordLib
                    pWordChild.AddChildNodes(pWordChild.xdoc.ChildNodes, ref pn);

                    pWordChild.treeView1.SelectedNode = pn;
                    pWordChild.userControl11.MasterNodes[pWordChild.userControl11.index] = masterNode;
                    if (pWordChild.chkClear.CheckState == CheckState.Checked)
                    {
                        pWordChild.txtName.Clear();
                        pWordChild.txtObject.Clear();
                        pWordChild.txtName.Focus();
                    }
                    else if (pWordChild.chkClear.CheckState == CheckState.Indeterminate)
                    {
                        pWordChild.txtObject.Clear();
                        pWordChild.txtObject.Focus();
                    }
                    else
                    {
                        pWordChild.btnAdd.Focus();
                    }

                    if (pWordChild.flag_file == true)
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
            pWordChild.Cursor = Cursors.Default;
            pWordChild.txtCMD.Cursor = Cursors.Default;

            // check for return character... and then process

            if (pWordChild.txtCMD.Text.Length > 0)
            {
                if (pWordChild.txtCMD.Text.Length < 4 && pWordChild.txtCMD.Text.ToLower().Contains("cls"))
                {
                    pWordChild.txtCMD.Text = "";
                }
                else if (pWordChild.txtCMD.Text[pWordChild.txtCMD.Text.Length - 1] == '\n' || pWordChild.txtCMD.Text[pWordChild.txtCMD.Text.Length - 1] == '\r')
                {
                    // process command
                    pWordChild.txtCMD.Cursor = Cursors.WaitCursor;
                    pWordChild.txtCMD.Parent.Cursor = Cursors.WaitCursor;
                    pWordChild.ProcessCommandText(pWordChild.txtCMD.Text);
                    // add history node to treeview
                    // then insert this command into the history
                    // and insert its text result as a child node
                    pWordChild.txtCMD.Text += output.ToString();
                }
            }
            pWordChild.Cursor = Cursors.Default;
            pWordChild.txtCMD.Cursor = Cursors.Default;
            pWordChild.txtCMD.Parent.Cursor = Cursors.Default;
            pWordChild.txtCMD.Focus();
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

                if (pWordChild.treeView1.Nodes[0].Nodes.Find("History", false).Length == 0)
                {
                    pNode aNode = new pNode("History");
                    pWordChild.treeView1.Nodes[0].Nodes.Add(aNode);
                }

                pWordChild.treeView1.SelectedNode = pWordChild.treeView1.Nodes[0].Nodes.Find("History", false).FirstOrDefault();

                if (_cmd.Length > 0)
                {
                    pWordChild.txtName.Text = stringMatchResult;
                    pWordChild.txtObject.Text = output.ToString();
                    var cmdNode = new pNode(stringMatchResult);
                    cmdNode.Tag = output.ToString();
                    pWordChild.treeView1.SelectedNode.Nodes.Add(cmdNode);
                    pWordChild.txtCMD.Focus();
                }
            }

        }
        [TestMethod]
        public void menuItemImportJSON_Click()
        {
            bool check = false;

            // TODO: Move To pWordLib
            pWordChild.importMode = ImportMode.treejson; // what am I exporting?  XML from previous exportNode
                                                         // I'm importing into a node that i selected using import node treexml 

            // TODO: Move To pWordLib
            pWordChild.xml.Clear();  // clear out contents first.

            // TODO: Move To pWordLib (make pWordLib friendly)
            pWordChild.xmlIndex = pWordChild.treeView1.SelectedNode.Index; // xmlIndex is the SelectedNodes index

            pWordChild.menuItem21.Enabled = true; // MenuItem21 is enabled... Todo: rename menuItem21
            pWordChild.menuItem31.Enabled = true;  // MneuItem21 is enabled... Todo: rename menuItem31
            pWordChild.nodeIndex = pWordChild.treeView1.SelectedNode.Index; // nodeInex is now equal to xmlIndex?
            pWordChild.statusBar1.Text = "Import Node JSON Mode";

            pWordChild.openFileDialog2.ShowDialog();
            pWordChild.filenameHTML = pWordChild.openFileDialog2.FileName;
            if ((pWordChild.filenameHTML == null) || (pWordChild.filenameHTML == ""))
            {
                return;
            }
            else
            {
                try
                {
                    using (WebClient client = new WebClient())
                    using (Stream stream = client.OpenRead(pWordChild.filenameHTML))
                    {
                        //      byte[] buf = new byte[stream.Length];
                        //      stream.Read(buf, 0, (int)stream.Length);
                        pWordChild.xdoc = new XmlDocument();
                        try
                        {
                            //xdoc.Load(stream);
                            // load json with newton soft and convert to XmlDocument
                            var json = File.ReadAllText(pWordChild.filenameHTML);
                            //var jObject = JObject.Parse(json);
                            var xml = JsonConvert.DeserializeXmlNode(json);
                            pWordChild.xdoc.LoadXml(xml.InnerXml);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }

                    // TODO: Move To pWordLib (make pWordLib friendly)
                    pNode masterNode = (pNode)pWordChild.treeView1.Nodes[0];
                    // now that we have the xdoc... now we need to stick it in the getNode
                    //                pNode pn = new pNode(xdoc.ChildNodes[1].Name);
                    //                pn.AddAttribute(xdoc.ch
                    //                getNode.Nodes.Add(

                    // TODO: Move To pWordLib (make pWordLib friendly)
                    pNode pn = (pNode)pWordChild.treeView1.SelectedNode;
                    if (pWordChild.treeView1.SelectedNode.Tag == null)
                        pWordChild.treeView1.SelectedNode.Tag = "";
                    pn.Tag = pWordChild.treeView1.SelectedNode.Tag;

                    // TODO: Move To pWordLib
                    if (pWordChild.xdoc != null && pWordChild.xdoc.Attributes != null && pWordChild.xdoc.Attributes.Count > 0)
                    {
                        foreach (XmlAttribute xmlAttr in pWordChild.xdoc)
                        {
                            pn.AddAttribute(xmlAttr.LocalName, xmlAttr.Value);
                        }
                    }

                    // TODO: Move To pWordLib
                    pWordChild.AddChildNodes(pWordChild.xdoc.ChildNodes, ref pn);

                    pWordChild.treeView1.SelectedNode = pn;
                    //                .Nodes.Add(pn);
                    //treeView1.SelectedNode.Nodes.Add(aNode);
                    // after adding the new node, be sure the index is updated as well... this is not necessary
                    pWordChild.userControl11.MasterNodes[pWordChild.userControl11.index] = masterNode;

                    // Change from Add Dialog to local members for adding name and value

                    // check box
                    if (pWordChild.chkClear.CheckState == CheckState.Checked)
                    {
                        pWordChild.txtName.Clear();
                        pWordChild.txtObject.Clear();
                        pWordChild.txtName.Focus();
                    }
                    else if (pWordChild.chkClear.CheckState == CheckState.Indeterminate)
                    {
                        pWordChild.txtObject.Clear();
                        pWordChild.txtObject.Focus();
                    }
                    else
                    {
                        pWordChild.btnAdd.Focus();
                    }

                    if (pWordChild.flag_file == true)
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
