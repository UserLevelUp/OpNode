using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using LL;
using System.IO;
using System.Runtime.InteropServices;
using Win32;
using UserActivityHook;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using LeftRight;
using System.Xml.Serialization;
using System.Xml;
using System.Diagnostics;
using pWordLib;
using pWordLib.dat;
using pWordLib.dat.math;
using pWordLib.dat.Math;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace myPword
{
    /// <summary> 
    /// pWord main form
    /// </summary>
    /// 
    public class pWord : System.Windows.Forms.Form
    {
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ContextMenu contextMenuNotify;
        private System.Windows.Forms.MenuItem menuItemExit;
        private System.Windows.Forms.MenuItem menuItemShow;
        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.ToolBar toolBar1;
        private System.Windows.Forms.ImageList imgToolbar1;
        private System.Windows.Forms.ToolBarButton toolBarTac;
        private System.Windows.Forms.ContextMenu cmTree;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem7;
        private System.Windows.Forms.MenuItem menuItem8;
        private System.Windows.Forms.MenuItem menuItem9;
        private System.Windows.Forms.MenuItem menuItem10;
        private System.Windows.Forms.MenuItem menuItem14;
        private System.Windows.Forms.ImageList imageTree1;
        private System.Windows.Forms.MenuItem menuItem15;
        private System.Windows.Forms.MenuItem menuItem16;
        private System.Windows.Forms.MenuItem menuItemBlank;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolBarButton toolBarView;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem17;
        private System.Windows.Forms.MenuItem menuItem18;
        private System.Windows.Forms.NotifyIcon notifyIcon1;

        pNode p = new pNode();

        /// <summary>
        ///   Add my VARIABLES HERE
        /// </summary>
        //
        bool VIS = true;

        // The master list for all views
        LL.List masterList = new LL.List();

        // flag_file is false if it's a new file, or upon opening program.
        // If flag_file is true... it's because the current tree was saved or opened.
        bool flag_file = false;
        string filename = "";
        string filenameHTML = "";
        pNode tmpNode = new pNode();  // used to store a tree node temporarily
        pNode getNode = new pNode();  // used for put op
        pNode moveNode = new pNode(); // moved pNode
        pNode xmlNode = new pNode();  // used for xml ... especially the EYE icon next to ( to the right of when active) the Pin Icon
        ArrayList xml = new ArrayList();
        int nodeIndex = 0;
        int xmlIndex = 0;

        public enum nodeMode
        {
            addto = 1,
            insert = 2,
            edit = 3,
            addAttributeTo = 4,
            addNamespacePrefix = 5,
            addNamespaceSuffix = 6,
            sum = 7,
            multiply = 8,
            divide = 9,
            viewErrors = 10,
            cut = 11,
            trig,
            find,
            xmlUpdate
        }

        nodeMode mode = nodeMode.addto;  // see above
        int modeIndex = 0;  // this represents the index of interest depending on the mode

        public enum ExportMode
        {
            treeview = 1,
            pNode = 2,
            treexml = 3
        }

        public enum ImportMode
        {
            treeview = 1,
            pNode = 2,
            treexml = 3
        }


        ExportMode exportMode = ExportMode.treeview;
        ImportMode importMode = ImportMode.treeview;

        System.Drawing.Point StartPt; // Used for selecting multi nodes
        System.Drawing.Point StopPt;

        // TODO: Add TREE Stuff here
        //			pNode rootNode = new pNode("Master"

        private pWordLib.Image img;
        private ArrayList Nodes = new ArrayList();

        // TODO: decide if drag_flag needs to be used or not.
        private bool drag_flag = false;
        private bool autoHide_flag = false;
        private Point mousePT;
        private System.Windows.Forms.TextBox txtValue;
        private LeftRight.LeftRight userControl11;
        private System.Windows.Forms.ContextMenu cmMasters;
        private System.Windows.Forms.MenuItem menuItem19;
        private System.Windows.Forms.MenuItem menuItem20;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.MenuItem menuItem11;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuItem menuItem12;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox txtObject;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.CheckBox chkClear;
        private pView treeView1;
        private System.Windows.Forms.MenuItem menuItem13;
        private System.Windows.Forms.MenuItem menuItem21;
        private System.Windows.Forms.MenuItem menuItem22;
        private System.Windows.Forms.MenuItem menuItem24;
        private System.Windows.Forms.MenuItem menuItem23;
        private System.Windows.Forms.MenuItem menuItem25;
        private System.Windows.Forms.MenuItem menuItem26;
        private System.Windows.Forms.MenuItem menuItem27;
        private System.Windows.Forms.MenuItem menuItem28;
        private System.Windows.Forms.MenuItem menuItem29;
        private System.Windows.Forms.MenuItem menuItem30;
        private System.Windows.Forms.MenuItem menuItem31;
        private System.Windows.Forms.MenuItem menuItem32;
        private System.Windows.Forms.SaveFileDialog saveFileDialogHTML;
        private System.Windows.Forms.MenuItem menuItem33;
        private System.Windows.Forms.MenuItem menuItem34;
        private System.Windows.Forms.MenuItem menuItem35;
        private ToolBarButton toolBarXML;
        private NotifyIcon notifyIcon2;
        private MenuItem menuItem2;

        // Import interop services

        pWordLib.UserActivityHook.UserActivityHook actHook;
        private MenuItem menuItem5;
        private MenuItem menuItem6;
        private MenuItem menuItem36;
        private MenuItem menuItem37;
        private MenuItem menuItem40;
        private MenuItem menuItem39;
        private MenuItem menuItem41;
        private MenuItem menuItem42;
        private MenuItem menuItem43;
        private MenuItem menuItem44;
        private MenuItem menuItem45;
        private MenuItem menuItem46;
        private MenuItem menuItemViewErrors;
        private MenuItem menuItem50;
        private MenuItem menuItem47;
        private MenuItem menuItem48;
        private MenuItem menuItem49;
        private MenuItem menuItem51;
        private MenuItem menuItem52;
        private MenuItem menuItem53;
        private MenuItem menuItem54;
        private MenuItem menuItem55;
        private MenuItem menuItem56;
        private MenuItem menuItem57;
        private MenuItem menuItem58;
        private MenuItem menuItem59;
        private MenuItem menuItem60;
        private MenuItem menuItem61;
        private MenuItem menuItem62;
        private MenuItem menuItem63;
        private MenuItem menuItem64;
        private MenuItem menuItem65;
        private MenuItem menuItem66;
        private MenuItem menuItem67;
        private MenuItem menuItem68;
        private MenuItem menuItem69;
        private MenuItem menuItem70;
        private MenuItem menuItem71;
        private MenuItem menuItem72;
        private MenuItem menuItem73;
        private MenuItem menuItem74;
        private MenuItem menuItem75;
        private MenuItem menuItem38;
        private MenuItem menuItem76;
        private MenuItem menuItem77;
        private MenuItem menuItem78;
        private MenuItem menuItem79;
        private MenuItem menuItem81;
        private MenuItem menuItem80;
        private MenuItem menuItem82;
        private MenuItem menuItem83;
        private MenuItem menuItem84;
        private MenuItem mnuNamespaces;
        private MenuItem mnuAttributes;
        private TabControl tabs;
        private TabPage tabValue;
        private TabPage tabNamespaces;
        private TabPage tabAttributes;
        private ListView lstNamespaces;
        private ListView lstAttributes;
        private ColumnHeader columnPrefix;
        private ColumnHeader columnPrefixURI;
        private ColumnHeader columnAttributeName;
        private ColumnHeader columnAttributeValue;
        private ColumnHeader columnSuffix;
        private MenuItem mnuImportXML;
        private MenuItem mnuImportNodeXML;
        private OpenFileDialog openFileDialog2;
        private MenuItem menuItem85;
        private MenuItem menuItem86;
        private MenuItem menuItem87;
        private MenuItem menuItem88;
        private MenuItem menuItem89;
        private MenuItem menuItem90;
        pWordLib.mgr.registryMgr rm = null;
        public pWord()
        {

            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            //
            // DONE: Add any constructor code after InitializeComponent call
            //
            this.components = new System.ComponentModel.Container();

            // Create the NotifyIcon
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);

            // the icon property sets the icon that will appear
            // in the systray for this application.
            notifyIcon1.Icon = new Icon(@".\ICONS\PW.ico");

            // The context menu property sets the menu that will 
            // Appear when the systray icon is right clicked.
            this.notifyIcon1.ContextMenu = this.contextMenuNotify;

            // the text property sets the text that will be displayed 
            // in a tooltip, when the mouse hovers over the systray icon
            notifyIcon1.Text = "pWord (Notify Icon)";
            notifyIcon1.Visible = false;  // the notify icon should only be visible when it is minimized

            // Handle the double click event to activate the form.
            //notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            notifyIcon1.DoubleClick += new EventHandler(notifyIcon1_DoubleClick);

            actHook = new pWordLib.UserActivityHook.UserActivityHook();  // create an instance
            // hang on events
            actHook.OnMouseActivity += new MouseEventHandler(MouseMoved);



        }

        void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            // 
            this.WindowState = FormWindowState.Normal;
            this.autoHide_flag = false;
            VIS = true;

            this.autoHide_flag = false;
            this.statusBar1.Text = "AutoHide Inactive";
            this.toolBarTac.ImageIndex = 1;
            this.Visible = true;

            this.WindowState = FormWindowState.Normal;
            this.DockRight(sender, e);
            this.actHook.Stop();
            notifyIcon1.Visible = false;
            notifyIcon2.Visible = true;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.actHook.Stop();
                if (components != null)
                {
                    components.Dispose();
                    notifyIcon1.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.Configuration.AppSettingsReader configurationAppSettings = new System.Configuration.AppSettingsReader();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pWord));
			this.contextMenuNotify = new System.Windows.Forms.ContextMenu();
			this.menuItemShow = new System.Windows.Forms.MenuItem();
			this.menuItemBlank = new System.Windows.Forms.MenuItem();
			this.menuItemExit = new System.Windows.Forms.MenuItem();
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.toolBar1 = new System.Windows.Forms.ToolBar();
			this.toolBarTac = new System.Windows.Forms.ToolBarButton();
			this.toolBarView = new System.Windows.Forms.ToolBarButton();
			this.toolBarXML = new System.Windows.Forms.ToolBarButton();
			this.imgToolbar1 = new System.Windows.Forms.ImageList(this.components);
			this.cmTree = new System.Windows.Forms.ContextMenu();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem13 = new System.Windows.Forms.MenuItem();
			this.menuItem29 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem36 = new System.Windows.Forms.MenuItem();
			this.menuItem37 = new System.Windows.Forms.MenuItem();
			this.menuItem83 = new System.Windows.Forms.MenuItem();
			this.menuItem30 = new System.Windows.Forms.MenuItem();
			this.menuItem22 = new System.Windows.Forms.MenuItem();
			this.menuItem32 = new System.Windows.Forms.MenuItem();
			this.menuItem21 = new System.Windows.Forms.MenuItem();
			this.menuItem31 = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem14 = new System.Windows.Forms.MenuItem();
			this.menuItem34 = new System.Windows.Forms.MenuItem();
			this.menuItem35 = new System.Windows.Forms.MenuItem();
			this.menuItem85 = new System.Windows.Forms.MenuItem();
			this.mnuImportXML = new System.Windows.Forms.MenuItem();
			this.mnuImportNodeXML = new System.Windows.Forms.MenuItem();
			this.menuItem40 = new System.Windows.Forms.MenuItem();
			this.menuItem39 = new System.Windows.Forms.MenuItem();
			this.menuItem46 = new System.Windows.Forms.MenuItem();
			this.menuItem74 = new System.Windows.Forms.MenuItem();
			this.menuItem75 = new System.Windows.Forms.MenuItem();
			this.menuItem42 = new System.Windows.Forms.MenuItem();
			this.menuItem44 = new System.Windows.Forms.MenuItem();
			this.menuItem43 = new System.Windows.Forms.MenuItem();
			this.menuItem45 = new System.Windows.Forms.MenuItem();
			this.menuItem41 = new System.Windows.Forms.MenuItem();
			this.menuItem60 = new System.Windows.Forms.MenuItem();
			this.menuItem61 = new System.Windows.Forms.MenuItem();
			this.menuItem62 = new System.Windows.Forms.MenuItem();
			this.menuItem57 = new System.Windows.Forms.MenuItem();
			this.menuItem58 = new System.Windows.Forms.MenuItem();
			this.menuItem59 = new System.Windows.Forms.MenuItem();
			this.menuItem54 = new System.Windows.Forms.MenuItem();
			this.menuItem55 = new System.Windows.Forms.MenuItem();
			this.menuItem56 = new System.Windows.Forms.MenuItem();
			this.menuItem69 = new System.Windows.Forms.MenuItem();
			this.menuItem70 = new System.Windows.Forms.MenuItem();
			this.menuItem71 = new System.Windows.Forms.MenuItem();
			this.menuItem72 = new System.Windows.Forms.MenuItem();
			this.menuItem73 = new System.Windows.Forms.MenuItem();
			this.menuItem63 = new System.Windows.Forms.MenuItem();
			this.menuItem64 = new System.Windows.Forms.MenuItem();
			this.menuItem65 = new System.Windows.Forms.MenuItem();
			this.menuItem66 = new System.Windows.Forms.MenuItem();
			this.menuItem67 = new System.Windows.Forms.MenuItem();
			this.menuItem68 = new System.Windows.Forms.MenuItem();
			this.menuItem50 = new System.Windows.Forms.MenuItem();
			this.menuItem47 = new System.Windows.Forms.MenuItem();
			this.menuItem48 = new System.Windows.Forms.MenuItem();
			this.menuItem49 = new System.Windows.Forms.MenuItem();
			this.menuItem51 = new System.Windows.Forms.MenuItem();
			this.menuItem52 = new System.Windows.Forms.MenuItem();
			this.menuItem53 = new System.Windows.Forms.MenuItem();
			this.menuItem38 = new System.Windows.Forms.MenuItem();
			this.menuItem76 = new System.Windows.Forms.MenuItem();
			this.menuItem77 = new System.Windows.Forms.MenuItem();
			this.menuItem80 = new System.Windows.Forms.MenuItem();
			this.menuItem78 = new System.Windows.Forms.MenuItem();
			this.menuItem81 = new System.Windows.Forms.MenuItem();
			this.menuItem82 = new System.Windows.Forms.MenuItem();
			this.menuItem79 = new System.Windows.Forms.MenuItem();
			this.menuItemViewErrors = new System.Windows.Forms.MenuItem();
			this.menuItem86 = new System.Windows.Forms.MenuItem();
			this.menuItem87 = new System.Windows.Forms.MenuItem();
			this.menuItem88 = new System.Windows.Forms.MenuItem();
			this.menuItem89 = new System.Windows.Forms.MenuItem();
			this.menuItem90 = new System.Windows.Forms.MenuItem();
			this.menuItem24 = new System.Windows.Forms.MenuItem();
			this.menuItem11 = new System.Windows.Forms.MenuItem();
			this.imageTree1 = new System.Windows.Forms.ImageList(this.components);
			this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.menuItem15 = new System.Windows.Forms.MenuItem();
			this.menuItem23 = new System.Windows.Forms.MenuItem();
			this.menuItem25 = new System.Windows.Forms.MenuItem();
			this.menuItem16 = new System.Windows.Forms.MenuItem();
			this.menuItem26 = new System.Windows.Forms.MenuItem();
			this.menuItem27 = new System.Windows.Forms.MenuItem();
			this.menuItem28 = new System.Windows.Forms.MenuItem();
			this.menuItem84 = new System.Windows.Forms.MenuItem();
			this.mnuNamespaces = new System.Windows.Forms.MenuItem();
			this.mnuAttributes = new System.Windows.Forms.MenuItem();
			this.menuItem33 = new System.Windows.Forms.MenuItem();
			this.menuItem17 = new System.Windows.Forms.MenuItem();
			this.menuItem12 = new System.Windows.Forms.MenuItem();
			this.menuItem18 = new System.Windows.Forms.MenuItem();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.txtValue = new System.Windows.Forms.TextBox();
			this.cmMasters = new System.Windows.Forms.ContextMenu();
			this.menuItem19 = new System.Windows.Forms.MenuItem();
			this.menuItem20 = new System.Windows.Forms.MenuItem();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel4 = new System.Windows.Forms.Panel();
			this.txtObject = new System.Windows.Forms.TextBox();
			this.lblValue = new System.Windows.Forms.Label();
			this.panel3 = new System.Windows.Forms.Panel();
			this.txtName = new System.Windows.Forms.TextBox();
			this.lblName = new System.Windows.Forms.Label();
			this.panel5 = new System.Windows.Forms.Panel();
			this.chkClear = new System.Windows.Forms.CheckBox();
			this.btnAdd = new System.Windows.Forms.Button();
			this.panel6 = new System.Windows.Forms.Panel();
			this.treeView1 = new myPword.pView();
			this.tabs = new System.Windows.Forms.TabControl();
			this.tabValue = new System.Windows.Forms.TabPage();
			this.tabNamespaces = new System.Windows.Forms.TabPage();
			this.lstNamespaces = new System.Windows.Forms.ListView();
			this.columnPrefix = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnPrefixURI = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnSuffix = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tabAttributes = new System.Windows.Forms.TabPage();
			this.lstAttributes = new System.Windows.Forms.ListView();
			this.columnAttributeName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnAttributeValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.btnCancel = new System.Windows.Forms.Button();
			this.saveFileDialogHTML = new System.Windows.Forms.SaveFileDialog();
			this.notifyIcon2 = new System.Windows.Forms.NotifyIcon(this.components);
			this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
			this.userControl11 = new LeftRight.LeftRight();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel5.SuspendLayout();
			this.panel6.SuspendLayout();
			this.tabs.SuspendLayout();
			this.tabValue.SuspendLayout();
			this.tabNamespaces.SuspendLayout();
			this.tabAttributes.SuspendLayout();
			this.SuspendLayout();
			// 
			// contextMenuNotify
			// 
			this.contextMenuNotify.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemShow,
            this.menuItemBlank,
            this.menuItemExit});
			this.contextMenuNotify.Popup += new System.EventHandler(this.contextMenu1_Popup);
			// 
			// menuItemShow
			// 
			this.menuItemShow.Index = 0;
			this.menuItemShow.Text = "&Show";
			this.menuItemShow.Click += new System.EventHandler(this.menuItem6_Click);
			// 
			// menuItemBlank
			// 
			this.menuItemBlank.Index = 1;
			this.menuItemBlank.Text = "-";
			// 
			// menuItemExit
			// 
			this.menuItemExit.Index = 2;
			this.menuItemExit.Text = "E&xit";
			this.menuItemExit.Click += new System.EventHandler(this.menuItem5_Click);
			// 
			// statusBar1
			// 
			this.statusBar1.Location = new System.Drawing.Point(0, 257);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Size = new System.Drawing.Size(574, 41);
			this.statusBar1.TabIndex = 0;
			this.statusBar1.Text = "statusBar1";
			// 
			// toolBar1
			// 
			this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.toolBarTac,
            this.toolBarView,
            this.toolBarXML});
			this.toolBar1.DropDownArrows = true;
			this.toolBar1.ImageList = this.imgToolbar1;
			this.toolBar1.Location = new System.Drawing.Point(0, 0);
			this.toolBar1.Name = "toolBar1";
			this.toolBar1.ShowToolTips = true;
			this.toolBar1.Size = new System.Drawing.Size(574, 48);
			this.toolBar1.TabIndex = 1;
			this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
			// 
			// toolBarTac
			// 
			this.toolBarTac.Enabled = ((bool)(configurationAppSettings.GetValue("toolBarButton1.Enabled", typeof(bool))));
			this.toolBarTac.ImageIndex = ((int)(configurationAppSettings.GetValue("toolBarButton1.ImageIndex", typeof(int))));
			this.toolBarTac.Name = "toolBarTac";
			this.toolBarTac.Pushed = ((bool)(configurationAppSettings.GetValue("toolBarButton1.Pushed", typeof(bool))));
			this.toolBarTac.ToolTipText = "3 State Thumb tac when used with minimize.";
			// 
			// toolBarView
			// 
			this.toolBarView.Enabled = false;
			this.toolBarView.ImageIndex = 2;
			this.toolBarView.Name = "toolBarView";
			this.toolBarView.ToolTipText = "Enabled to view file after xml or html export.";
			// 
			// toolBarXML
			// 
			this.toolBarXML.Enabled = false;
			this.toolBarXML.ImageIndex = 4;
			this.toolBarXML.Name = "toolBarXML";
			this.toolBarXML.Visible = false;
			// 
			// imgToolbar1
			// 
			this.imgToolbar1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgToolbar1.ImageStream")));
			this.imgToolbar1.TransparentColor = System.Drawing.Color.Transparent;
			this.imgToolbar1.Images.SetKeyName(0, "");
			this.imgToolbar1.Images.SetKeyName(1, "");
			this.imgToolbar1.Images.SetKeyName(2, "");
			this.imgToolbar1.Images.SetKeyName(3, "");
			this.imgToolbar1.Images.SetKeyName(4, "XML.png");
			this.imgToolbar1.Images.SetKeyName(5, "Bin.png");
			this.imgToolbar1.Images.SetKeyName(6, "AutoHide.ico");
			this.imgToolbar1.Images.SetKeyName(7, "AutoHide2.ico");
			// 
			// cmTree
			// 
			this.cmTree.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem4,
            this.menuItem13,
            this.menuItem29,
            this.menuItem3,
            this.menuItem2,
            this.menuItem6,
            this.menuItem83,
            this.menuItem30,
            this.menuItem22,
            this.menuItem32,
            this.menuItem21,
            this.menuItem31,
            this.menuItem1,
            this.menuItem14,
            this.menuItem34,
            this.mnuImportXML,
            this.menuItem40,
            this.menuItem39,
            this.menuItem24,
            this.menuItem11});
			this.cmTree.Popup += new System.EventHandler(this.cmTree_Popup);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 0;
			this.menuItem4.Shortcut = System.Windows.Forms.Shortcut.CtrlA;
			this.menuItem4.Text = "Add To";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// menuItem13
			// 
			this.menuItem13.Index = 1;
			this.menuItem13.Shortcut = System.Windows.Forms.Shortcut.CtrlE;
			this.menuItem13.Text = "Edit";
			this.menuItem13.Click += new System.EventHandler(this.menuItem13_Click);
			// 
			// menuItem29
			// 
			this.menuItem29.Index = 2;
			this.menuItem29.Shortcut = System.Windows.Forms.Shortcut.CtrlI;
			this.menuItem29.Text = "Insert";
			this.menuItem29.Click += new System.EventHandler(this.menuItem29_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 3;
			this.menuItem3.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
			this.menuItem3.Text = "Copy";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click_1);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 4;
			this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem5});
			this.menuItem2.Text = "A&ttributes";
			this.menuItem2.Click += new System.EventHandler(this.menuItemAttribute_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 0;
			this.menuItem5.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftA;
			this.menuItem5.Text = "Add";
			this.menuItem5.Click += new System.EventHandler(this.menuItemAttributeAdd_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 5;
			this.menuItem6.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem36,
            this.menuItem37});
			this.menuItem6.Text = "Namespace";
			// 
			// menuItem36
			// 
			this.menuItem36.Index = 0;
			this.menuItem36.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftP;
			this.menuItem36.Text = "Set Prefix";
			this.menuItem36.Click += new System.EventHandler(this.menuItemNamespaceAddPrefix_Click);
			// 
			// menuItem37
			// 
			this.menuItem37.Index = 1;
			this.menuItem37.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftS;
			this.menuItem37.Text = "Set Suffix";
			this.menuItem37.Click += new System.EventHandler(this.menuItemNamespaceAddSuffix_Click);
			// 
			// menuItem83
			// 
			this.menuItem83.Index = 6;
			this.menuItem83.Text = "Find";
			this.menuItem83.Click += new System.EventHandler(this.menuItemFind_Click);
			// 
			// menuItem30
			// 
			this.menuItem30.Index = 7;
			this.menuItem30.Text = "-";
			// 
			// menuItem22
			// 
			this.menuItem22.Index = 8;
			this.menuItem22.Shortcut = System.Windows.Forms.Shortcut.CtrlG;
			this.menuItem22.Text = "Get Node";
			this.menuItem22.Click += new System.EventHandler(this.menuItemGetNode_Click);
			// 
			// menuItem32
			// 
			this.menuItem32.Index = 9;
			this.menuItem32.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
			this.menuItem32.Text = "Cut Node";
			this.menuItem32.Click += new System.EventHandler(this.menuCutNode_Click);
			// 
			// menuItem21
			// 
			this.menuItem21.Enabled = false;
			this.menuItem21.Index = 10;
			this.menuItem21.Shortcut = System.Windows.Forms.Shortcut.CtrlP;
			this.menuItem21.Text = "Put Node In";
			this.menuItem21.Click += new System.EventHandler(this.menuItem21_Click);
			// 
			// menuItem31
			// 
			this.menuItem31.Enabled = false;
			this.menuItem31.Index = 11;
			this.menuItem31.Shortcut = System.Windows.Forms.Shortcut.Ins;
			this.menuItem31.Text = "Insert Node";
			this.menuItem31.Click += new System.EventHandler(this.menuItem31_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Enabled = false;
			this.menuItem1.Index = 12;
			this.menuItem1.Text = "Encrypt Node";
			// 
			// menuItem14
			// 
			this.menuItem14.Index = 13;
			this.menuItem14.Shortcut = System.Windows.Forms.Shortcut.Del;
			this.menuItem14.Text = "Delete Node";
			this.menuItem14.Click += new System.EventHandler(this.menuItem14_Click);
			// 
			// menuItem34
			// 
			this.menuItem34.Index = 14;
			this.menuItem34.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem35,
            this.menuItem85});
			this.menuItem34.Text = "Export Node";
			// 
			// menuItem35
			// 
			this.menuItem35.Index = 0;
			this.menuItem35.Shortcut = System.Windows.Forms.Shortcut.F11;
			this.menuItem35.Text = "to XML/HTML";
			this.menuItem35.Click += new System.EventHandler(this.menuItem35_Click);
			// 
			// menuItem85
			// 
			this.menuItem85.Index = 1;
			this.menuItem85.Text = "XML->XSLT->Result";
			this.menuItem85.Click += new System.EventHandler(this.menuItem85_Click_1);
			// 
			// mnuImportXML
			// 
			this.mnuImportXML.Index = 15;
			this.mnuImportXML.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuImportNodeXML});
			this.mnuImportXML.Text = "Import Node";
			this.mnuImportXML.Click += new System.EventHandler(this.menuItem85_Click);
			// 
			// mnuImportNodeXML
			// 
			this.mnuImportNodeXML.Index = 0;
			this.mnuImportNodeXML.Text = "from XML/HTML";
			this.mnuImportNodeXML.Click += new System.EventHandler(this.mnuImportNodeXML_Click);
			// 
			// menuItem40
			// 
			this.menuItem40.Index = 16;
			this.menuItem40.Text = "-";
			// 
			// menuItem39
			// 
			this.menuItem39.Index = 17;
			this.menuItem39.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem46,
            this.menuItem74,
            this.menuItem42,
            this.menuItem38,
            this.menuItemViewErrors,
            this.menuItem86});
			this.menuItem39.Text = "Operations";
			// 
			// menuItem46
			// 
			this.menuItem46.Index = 0;
			this.menuItem46.Text = "Clear Operations";
			this.menuItem46.Click += new System.EventHandler(this.menuItemOperationsClear_Click);
			// 
			// menuItem74
			// 
			this.menuItem74.Index = 1;
			this.menuItem74.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem75});
			this.menuItem74.Text = "Language";
			// 
			// menuItem75
			// 
			this.menuItem75.Index = 0;
			this.menuItem75.Text = "To Latin";
			// 
			// menuItem42
			// 
			this.menuItem42.Index = 2;
			this.menuItem42.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem44,
            this.menuItem43,
            this.menuItem45,
            this.menuItem41,
            this.menuItem60,
            this.menuItem57,
            this.menuItem54,
            this.menuItem69,
            this.menuItem63,
            this.menuItem50});
			this.menuItem42.Text = "Math";
			// 
			// menuItem44
			// 
			this.menuItem44.Index = 0;
			this.menuItem44.Shortcut = System.Windows.Forms.Shortcut.CtrlD;
			this.menuItem44.Text = "Divide";
			this.menuItem44.Click += new System.EventHandler(this.menuItemMathDivide_Click);
			// 
			// menuItem43
			// 
			this.menuItem43.Index = 1;
			this.menuItem43.Shortcut = System.Windows.Forms.Shortcut.CtrlM;
			this.menuItem43.Text = "Multiply";
			this.menuItem43.Click += new System.EventHandler(this.menuItemMathMultiple_Click);
			// 
			// menuItem45
			// 
			this.menuItem45.Index = 2;
			this.menuItem45.Shortcut = System.Windows.Forms.Shortcut.CtrlM;
			this.menuItem45.Text = "Minus";
			this.menuItem45.Click += new System.EventHandler(this.menuItemMathSubtract_Click);
			// 
			// menuItem41
			// 
			this.menuItem41.Index = 3;
			this.menuItem41.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftS;
			this.menuItem41.Text = "Sum";
			this.menuItem41.Click += new System.EventHandler(this.menuItemMathSum_Click);
			// 
			// menuItem60
			// 
			this.menuItem60.Index = 4;
			this.menuItem60.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem61,
            this.menuItem62});
			this.menuItem60.Text = "Calculus";
			// 
			// menuItem61
			// 
			this.menuItem61.Index = 0;
			this.menuItem61.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftD;
			this.menuItem61.Text = "d/dx";
			// 
			// menuItem62
			// 
			this.menuItem62.Index = 1;
			this.menuItem62.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftI;
			this.menuItem62.Text = "Integral";
			// 
			// menuItem57
			// 
			this.menuItem57.Index = 5;
			this.menuItem57.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem58,
            this.menuItem59});
			this.menuItem57.Text = "Exponent";
			// 
			// menuItem58
			// 
			this.menuItem58.Index = 0;
			this.menuItem58.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftE;
			this.menuItem58.Text = "x^y";
			// 
			// menuItem59
			// 
			this.menuItem59.Index = 1;
			this.menuItem59.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftQ;
			this.menuItem59.Text = "Sqrt";
			// 
			// menuItem54
			// 
			this.menuItem54.Index = 6;
			this.menuItem54.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem55,
            this.menuItem56});
			this.menuItem54.Text = "Log";
			// 
			// menuItem55
			// 
			this.menuItem55.Index = 0;
			this.menuItem55.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftN;
			this.menuItem55.Text = "ln";
			// 
			// menuItem56
			// 
			this.menuItem56.Index = 1;
			this.menuItem56.Shortcut = System.Windows.Forms.Shortcut.CtrlL;
			this.menuItem56.Text = "log10";
			// 
			// menuItem69
			// 
			this.menuItem69.Index = 7;
			this.menuItem69.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem70,
            this.menuItem71,
            this.menuItem72,
            this.menuItem73});
			this.menuItem69.Text = "Logic";
			// 
			// menuItem70
			// 
			this.menuItem70.Index = 0;
			this.menuItem70.Shortcut = System.Windows.Forms.Shortcut.Alt0;
			this.menuItem70.Text = "Not";
			// 
			// menuItem71
			// 
			this.menuItem71.Index = 1;
			this.menuItem71.Shortcut = System.Windows.Forms.Shortcut.Alt1;
			this.menuItem71.Text = "And";
			// 
			// menuItem72
			// 
			this.menuItem72.Index = 2;
			this.menuItem72.Shortcut = System.Windows.Forms.Shortcut.Alt2;
			this.menuItem72.Text = "Or";
			// 
			// menuItem73
			// 
			this.menuItem73.Index = 3;
			this.menuItem73.Shortcut = System.Windows.Forms.Shortcut.Alt3;
			this.menuItem73.Text = "Xor";
			// 
			// menuItem63
			// 
			this.menuItem63.Index = 8;
			this.menuItem63.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem64,
            this.menuItem65,
            this.menuItem66,
            this.menuItem67,
            this.menuItem68});
			this.menuItem63.Text = "Statistics";
			// 
			// menuItem64
			// 
			this.menuItem64.Index = 0;
			this.menuItem64.Shortcut = System.Windows.Forms.Shortcut.Ctrl0;
			this.menuItem64.Text = "Avg";
			// 
			// menuItem65
			// 
			this.menuItem65.Index = 1;
			this.menuItem65.Shortcut = System.Windows.Forms.Shortcut.Ctrl1;
			this.menuItem65.Text = "Median";
			// 
			// menuItem66
			// 
			this.menuItem66.Index = 2;
			this.menuItem66.Shortcut = System.Windows.Forms.Shortcut.Ctrl2;
			this.menuItem66.Text = "Min";
			// 
			// menuItem67
			// 
			this.menuItem67.Index = 3;
			this.menuItem67.Shortcut = System.Windows.Forms.Shortcut.Ctrl3;
			this.menuItem67.Text = "Max";
			// 
			// menuItem68
			// 
			this.menuItem68.Index = 4;
			this.menuItem68.Shortcut = System.Windows.Forms.Shortcut.Ctrl4;
			this.menuItem68.Text = "Union";
			// 
			// menuItem50
			// 
			this.menuItem50.Index = 9;
			this.menuItem50.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem47,
            this.menuItem48,
            this.menuItem49,
            this.menuItem51,
            this.menuItem52,
            this.menuItem53});
			this.menuItem50.Text = "Trig";
			// 
			// menuItem47
			// 
			this.menuItem47.Index = 0;
			this.menuItem47.Text = "Sin";
			this.menuItem47.Click += new System.EventHandler(this.menuItemMathTrigSign_Click);
			// 
			// menuItem48
			// 
			this.menuItem48.Index = 1;
			this.menuItem48.Text = "Cos";
			this.menuItem48.Click += new System.EventHandler(this.menuItemMathTrigCos_Click);
			// 
			// menuItem49
			// 
			this.menuItem49.Index = 2;
			this.menuItem49.Text = "Tan";
			this.menuItem49.Click += new System.EventHandler(this.menuItemMathTrigTan_Click);
			// 
			// menuItem51
			// 
			this.menuItem51.Index = 3;
			this.menuItem51.Text = "Arcsine";
			// 
			// menuItem52
			// 
			this.menuItem52.Index = 4;
			this.menuItem52.Text = "Arccos";
			// 
			// menuItem53
			// 
			this.menuItem53.Index = 5;
			this.menuItem53.Text = "Arctan";
			// 
			// menuItem38
			// 
			this.menuItem38.Index = 3;
			this.menuItem38.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem76,
            this.menuItem77,
            this.menuItem80,
            this.menuItem78,
            this.menuItem81,
            this.menuItem82,
            this.menuItem79});
			this.menuItem38.Text = "RecordIP";
			// 
			// menuItem76
			// 
			this.menuItem76.Index = 0;
			this.menuItem76.Text = "Mouse";
			// 
			// menuItem77
			// 
			this.menuItem77.Index = 1;
			this.menuItem77.Text = "Keyboard";
			// 
			// menuItem80
			// 
			this.menuItem80.Index = 2;
			this.menuItem80.Text = "M and K";
			// 
			// menuItem78
			// 
			this.menuItem78.Index = 3;
			this.menuItem78.Text = "Audio";
			// 
			// menuItem81
			// 
			this.menuItem81.Index = 4;
			this.menuItem81.Text = "Video";
			// 
			// menuItem82
			// 
			this.menuItem82.Index = 5;
			this.menuItem82.Text = "A and V";
			// 
			// menuItem79
			// 
			this.menuItem79.Enabled = false;
			this.menuItem79.Index = 6;
			this.menuItem79.Text = "Click to Stop";
			// 
			// menuItemViewErrors
			// 
			this.menuItemViewErrors.Index = 4;
			this.menuItemViewErrors.Text = "View Errors";
			this.menuItemViewErrors.Click += new System.EventHandler(this.menuItemViewErrors_Click);
			// 
			// menuItem86
			// 
			this.menuItem86.Index = 5;
			this.menuItem86.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem87,
            this.menuItem88,
            this.menuItem89,
            this.menuItem90});
			this.menuItem86.Text = "XML";
			// 
			// menuItem87
			// 
			this.menuItem87.Index = 0;
			this.menuItem87.Text = "Mark Node as XML";
			// 
			// menuItem88
			// 
			this.menuItem88.Index = 1;
			this.menuItem88.Text = "Mark Node as XSLT";
			this.menuItem88.Click += new System.EventHandler(this.menuItem88_Click);
			// 
			// menuItem89
			// 
			this.menuItem89.Index = 2;
			this.menuItem89.Text = "Apply XSLT siblings to XML siblings";
			// 
			// menuItem90
			// 
			this.menuItem90.Index = 3;
			this.menuItem90.Text = "Apply XSLT to Child XML Nodes";
			// 
			// menuItem24
			// 
			this.menuItem24.Index = 18;
			this.menuItem24.Text = "-";
			// 
			// menuItem11
			// 
			this.menuItem11.Index = 19;
			this.menuItem11.Text = "Open Link [Dbl Lft Click]";
			this.menuItem11.Click += new System.EventHandler(this.menuItem11_Click);
			// 
			// imageTree1
			// 
			this.imageTree1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageTree1.ImageStream")));
			this.imageTree1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageTree1.Images.SetKeyName(0, "master1.ico");
			this.imageTree1.Images.SetKeyName(1, "master2.ico");
			this.imageTree1.Images.SetKeyName(2, "Sum.ico");
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem7,
            this.menuItem26,
            this.menuItem33,
            this.menuItem17});
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 0;
			this.menuItem7.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem8,
            this.menuItem9,
            this.menuItem10,
            this.menuItem15,
            this.menuItem23,
            this.menuItem25,
            this.menuItem16});
			this.menuItem7.Text = "&File";
			this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 0;
			this.menuItem8.Text = "&New";
			this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 1;
			this.menuItem9.Text = "&Open";
			this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click);
			// 
			// menuItem10
			// 
			this.menuItem10.Index = 2;
			this.menuItem10.Text = "&Save";
			this.menuItem10.Click += new System.EventHandler(this.menuSave_Click);
			// 
			// menuItem15
			// 
			this.menuItem15.Index = 3;
			this.menuItem15.Text = "-";
			// 
			// menuItem23
			// 
			this.menuItem23.Index = 4;
			this.menuItem23.Text = "&Export XML";
			this.menuItem23.Click += new System.EventHandler(this.menuItem23_Click);
			// 
			// menuItem25
			// 
			this.menuItem25.Index = 5;
			this.menuItem25.Text = "-";
			// 
			// menuItem16
			// 
			this.menuItem16.Index = 6;
			this.menuItem16.Text = "E&xit";
			this.menuItem16.Click += new System.EventHandler(this.menuItem16_Click);
			// 
			// menuItem26
			// 
			this.menuItem26.Index = 1;
			this.menuItem26.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem27,
            this.menuItem28,
            this.menuItem84,
            this.mnuNamespaces,
            this.mnuAttributes});
			this.menuItem26.Text = "&View";
			// 
			// menuItem27
			// 
			this.menuItem27.Index = 0;
			this.menuItem27.Text = "&Alphabetize a-Z";
			this.menuItem27.Click += new System.EventHandler(this.menuItem27_Click);
			// 
			// menuItem28
			// 
			this.menuItem28.Index = 1;
			this.menuItem28.Text = "Alphabetize &Off";
			this.menuItem28.Click += new System.EventHandler(this.menuItem28_Click);
			// 
			// menuItem84
			// 
			this.menuItem84.Index = 2;
			this.menuItem84.Text = "-";
			// 
			// mnuNamespaces
			// 
			this.mnuNamespaces.Checked = true;
			this.mnuNamespaces.Index = 3;
			this.mnuNamespaces.Text = "Namespaces";
			// 
			// mnuAttributes
			// 
			this.mnuAttributes.Checked = true;
			this.mnuAttributes.Index = 4;
			this.mnuAttributes.Text = "Attributes";
			// 
			// menuItem33
			// 
			this.menuItem33.Index = 2;
			this.menuItem33.Text = "&Undo";
			this.menuItem33.Visible = false;
			this.menuItem33.Click += new System.EventHandler(this.menuItem33_Click);
			// 
			// menuItem17
			// 
			this.menuItem17.Index = 3;
			this.menuItem17.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem12,
            this.menuItem18});
			this.menuItem17.MergeType = System.Windows.Forms.MenuMerge.Remove;
			this.menuItem17.Text = "&About";
			this.menuItem17.Click += new System.EventHandler(this.menuItem17_Click);
			// 
			// menuItem12
			// 
			this.menuItem12.Enabled = false;
			this.menuItem12.Index = 0;
			this.menuItem12.Text = " &Help";
			this.menuItem12.Click += new System.EventHandler(this.menuItem12_Click_1);
			// 
			// menuItem18
			// 
			this.menuItem18.Index = 1;
			this.menuItem18.Text = "&About pWord";
			this.menuItem18.Click += new System.EventHandler(this.menuItem18_Click);
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.Filter = "pWord files|*.pwd";
			this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.Filter = "pWord files|*.pwd";
			this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
			// 
			// txtValue
			// 
			this.txtValue.BackColor = System.Drawing.SystemColors.Info;
			this.txtValue.Cursor = System.Windows.Forms.Cursors.Default;
			this.txtValue.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtValue.ForeColor = System.Drawing.Color.Black;
			this.txtValue.Location = new System.Drawing.Point(3, 3);
			this.txtValue.Multiline = true;
			this.txtValue.Name = "txtValue";
			this.txtValue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtValue.Size = new System.Drawing.Size(552, 132);
			this.txtValue.TabIndex = 3;
			this.txtValue.TabStop = false;
			// 
			// cmMasters
			// 
			this.cmMasters.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem19,
            this.menuItem20});
			// 
			// menuItem19
			// 
			this.menuItem19.Index = 0;
			this.menuItem19.Text = "Add Master";
			this.menuItem19.Click += new System.EventHandler(this.menuItem19_Click);
			// 
			// menuItem20
			// 
			this.menuItem20.Index = 1;
			this.menuItem20.Text = "Delete Master";
			this.menuItem20.Click += new System.EventHandler(this.menuItem20_Click);
			// 
			// splitter1
			// 
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitter1.Location = new System.Drawing.Point(0, -171);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(574, 16);
			this.splitter1.TabIndex = 5;
			this.splitter1.TabStop = false;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Controls.Add(this.panel6);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 92);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(574, 165);
			this.panel1.TabIndex = 6;
			this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
			// 
			// panel2
			// 
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel2.Controls.Add(this.panel4);
			this.panel2.Controls.Add(this.panel3);
			this.panel2.Controls.Add(this.panel5);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(574, 192);
			this.panel2.TabIndex = 3;
			this.panel2.VisibleChanged += new System.EventHandler(this.panel2_VisibleChanged);
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.txtObject);
			this.panel4.Controls.Add(this.lblValue);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel4.Location = new System.Drawing.Point(0, 44);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(570, 75);
			this.panel4.TabIndex = 2;
			// 
			// txtObject
			// 
			this.txtObject.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtObject.Location = new System.Drawing.Point(112, 0);
			this.txtObject.Multiline = true;
			this.txtObject.Name = "txtObject";
			this.txtObject.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtObject.Size = new System.Drawing.Size(458, 73);
			this.txtObject.TabIndex = 1;
			// 
			// lblValue
			// 
			this.lblValue.Dock = System.Windows.Forms.DockStyle.Left;
			this.lblValue.Location = new System.Drawing.Point(0, 0);
			this.lblValue.Name = "lblValue";
			this.lblValue.Size = new System.Drawing.Size(112, 75);
			this.lblValue.TabIndex = 0;
			this.lblValue.Text = "Value:";
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.txtName);
			this.panel3.Controls.Add(this.lblName);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(570, 44);
			this.panel3.TabIndex = 0;
			// 
			// txtName
			// 
			this.txtName.Dock = System.Windows.Forms.DockStyle.Top;
			this.txtName.Location = new System.Drawing.Point(112, 0);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(458, 31);
			this.txtName.TabIndex = 1;
			this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
			// 
			// lblName
			// 
			this.lblName.Dock = System.Windows.Forms.DockStyle.Left;
			this.lblName.Location = new System.Drawing.Point(0, 0);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(112, 44);
			this.lblName.TabIndex = 0;
			this.lblName.Text = "Name:";
			// 
			// panel5
			// 
			this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel5.Controls.Add(this.chkClear);
			this.panel5.Controls.Add(this.btnAdd);
			this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel5.Location = new System.Drawing.Point(0, 127);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(570, 61);
			this.panel5.TabIndex = 4;
			// 
			// chkClear
			// 
			this.chkClear.Checked = true;
			this.chkClear.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkClear.Location = new System.Drawing.Point(176, 0);
			this.chkClear.Name = "chkClear";
			this.chkClear.Size = new System.Drawing.Size(516, 44);
			this.chkClear.TabIndex = 2;
			this.chkClear.TabStop = false;
			this.chkClear.Text = "Clear Name and Value fields?";
			this.chkClear.ThreeState = true;
			this.chkClear.CheckedChanged += new System.EventHandler(this.chkClear_CheckedChanged);
			this.chkClear.CheckStateChanged += new System.EventHandler(this.chkClear_CheckStateChanged);
			// 
			// btnAdd
			// 
			this.btnAdd.Location = new System.Drawing.Point(0, 0);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(150, 44);
			this.btnAdd.TabIndex = 0;
			this.btnAdd.Text = "Add";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// panel6
			// 
			this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel6.Controls.Add(this.treeView1);
			this.panel6.Controls.Add(this.splitter1);
			this.panel6.Controls.Add(this.tabs);
			this.panel6.Controls.Add(this.btnCancel);
			this.panel6.Location = new System.Drawing.Point(0, 192);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(574, 30);
			this.panel6.TabIndex = 5;
			// 
			// treeView1
			// 
			this.treeView1.AllowDrop = true;
			this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.treeView1.ContextMenu = this.cmTree;
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView1.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
			this.treeView1.FullRowSelect = true;
			this.treeView1.HideSelection = false;
			this.treeView1.HotTracking = true;
			this.treeView1.ImageIndex = 0;
			this.treeView1.ImageList = this.imageTree1;
			this.treeView1.ImeMode = System.Windows.Forms.ImeMode.Off;
			this.treeView1.Location = new System.Drawing.Point(0, 0);
			this.treeView1.Name = "treeView1";
			this.treeView1.Scrollable = ((bool)(configurationAppSettings.GetValue("treeView1.Scrollable", typeof(bool))));
			this.treeView1.SelectedImageIndex = 0;
			this.treeView1.Size = new System.Drawing.Size(574, 0);
			this.treeView1.TabIndex = 3;
			this.treeView1.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCollapse_1);
			this.treeView1.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterExpand_1);
			this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect_1);
			this.treeView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeView1_DragDrop_1);
			this.treeView1.DragOver += new System.Windows.Forms.DragEventHandler(this.treeView1_DragOver);
			this.treeView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeView1_KeyDown);
			this.treeView1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.treeView1_KeyPress);
			this.treeView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown_1);
			this.treeView1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseMove_1);
			this.treeView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseUp_1);
			// 
			// tabs
			// 
			this.tabs.Controls.Add(this.tabValue);
			this.tabs.Controls.Add(this.tabNamespaces);
			this.tabs.Controls.Add(this.tabAttributes);
			this.tabs.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tabs.Location = new System.Drawing.Point(0, -155);
			this.tabs.Name = "tabs";
			this.tabs.SelectedIndex = 0;
			this.tabs.Size = new System.Drawing.Size(574, 185);
			this.tabs.TabIndex = 7;
			// 
			// tabValue
			// 
			this.tabValue.AllowDrop = true;
			this.tabValue.Controls.Add(this.txtValue);
			this.tabValue.Location = new System.Drawing.Point(8, 39);
			this.tabValue.Name = "tabValue";
			this.tabValue.Padding = new System.Windows.Forms.Padding(3);
			this.tabValue.Size = new System.Drawing.Size(558, 138);
			this.tabValue.TabIndex = 0;
			this.tabValue.Text = "Value";
			this.tabValue.UseVisualStyleBackColor = true;
			// 
			// tabNamespaces
			// 
			this.tabNamespaces.Controls.Add(this.lstNamespaces);
			this.tabNamespaces.Location = new System.Drawing.Point(8, 39);
			this.tabNamespaces.Name = "tabNamespaces";
			this.tabNamespaces.Padding = new System.Windows.Forms.Padding(3);
			this.tabNamespaces.Size = new System.Drawing.Size(556, 138);
			this.tabNamespaces.TabIndex = 1;
			this.tabNamespaces.Text = "Namespaces";
			this.tabNamespaces.UseVisualStyleBackColor = true;
			// 
			// lstNamespaces
			// 
			this.lstNamespaces.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnPrefix,
            this.columnPrefixURI,
            this.columnSuffix});
			this.lstNamespaces.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstNamespaces.HideSelection = false;
			this.lstNamespaces.Location = new System.Drawing.Point(3, 3);
			this.lstNamespaces.Name = "lstNamespaces";
			this.lstNamespaces.Size = new System.Drawing.Size(550, 132);
			this.lstNamespaces.TabIndex = 0;
			this.lstNamespaces.UseCompatibleStateImageBehavior = false;
			this.lstNamespaces.View = System.Windows.Forms.View.Details;
			// 
			// columnPrefix
			// 
			this.columnPrefix.Text = "Prefix";
			this.columnPrefix.Width = 69;
			// 
			// columnPrefixURI
			// 
			this.columnPrefixURI.Text = "URI";
			this.columnPrefixURI.Width = 198;
			// 
			// columnSuffix
			// 
			this.columnSuffix.Text = "Suffix";
			this.columnSuffix.Width = 85;
			// 
			// tabAttributes
			// 
			this.tabAttributes.Controls.Add(this.lstAttributes);
			this.tabAttributes.Location = new System.Drawing.Point(8, 39);
			this.tabAttributes.Name = "tabAttributes";
			this.tabAttributes.Padding = new System.Windows.Forms.Padding(3);
			this.tabAttributes.Size = new System.Drawing.Size(556, 138);
			this.tabAttributes.TabIndex = 2;
			this.tabAttributes.Text = "Attributes";
			this.tabAttributes.UseVisualStyleBackColor = true;
			// 
			// lstAttributes
			// 
			this.lstAttributes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnAttributeName,
            this.columnAttributeValue});
			this.lstAttributes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstAttributes.HideSelection = false;
			this.lstAttributes.Location = new System.Drawing.Point(3, 3);
			this.lstAttributes.Name = "lstAttributes";
			this.lstAttributes.Size = new System.Drawing.Size(550, 132);
			this.lstAttributes.TabIndex = 1;
			this.lstAttributes.UseCompatibleStateImageBehavior = false;
			this.lstAttributes.View = System.Windows.Forms.View.Details;
			// 
			// columnAttributeName
			// 
			this.columnAttributeName.Text = "Attribute";
			this.columnAttributeName.Width = 91;
			// 
			// columnAttributeValue
			// 
			this.columnAttributeValue.Text = "Value";
			this.columnAttributeValue.Width = 198;
			// 
			// btnCancel
			// 
			this.btnCancel.Enabled = false;
			this.btnCancel.Location = new System.Drawing.Point(4, -4);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(150, 44);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.TabStop = false;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Visible = false;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// saveFileDialogHTML
			// 
			this.saveFileDialogHTML.Filter = "XML format|*.xml|HTML format|*.html|All Files|*.*";
			this.saveFileDialogHTML.Title = "Save As XML//HTML";
			this.saveFileDialogHTML.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialogHTML_FileOk);
			// 
			// notifyIcon2
			// 
			this.notifyIcon2.Text = "notifyIcon2";
			this.notifyIcon2.Visible = true;
			this.notifyIcon2.DoubleClick += new System.EventHandler(this.notifyIcon2_DoubleClick);
			this.notifyIcon2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon2_MouseClick);
			this.notifyIcon2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon2_MouseDoubleClick);
			// 
			// openFileDialog2
			// 
			this.openFileDialog2.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog2_FileOk);
			// 
			// userControl11
			// 
			this.userControl11.ContextMenu = this.cmMasters;
			this.userControl11.Dock = System.Windows.Forms.DockStyle.Top;
			this.userControl11.Location = new System.Drawing.Point(0, 48);
			this.userControl11.Name = "userControl11";
			this.userControl11.Size = new System.Drawing.Size(574, 44);
			this.userControl11.TabIndex = 4;
			this.userControl11.TabStop = false;
			this.userControl11.LeftClicked += new System.EventHandler(this.userControl11_LeftClicked);
			this.userControl11.RightClicked += new System.EventHandler(this.userControl11_RightClicked);
			this.userControl11.Load += new System.EventHandler(this.userControl11_Load);
			// 
			// pWord
			// 
			this.AccessibleDescription = "Enabled to view file after xml or html export.";
			this.AutoScaleBaseSize = new System.Drawing.Size(10, 24);
			this.ClientSize = new System.Drawing.Size(574, 298);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.userControl11);
			this.Controls.Add(this.toolBar1);
			this.Controls.Add(this.statusBar1);
			this.Enabled = ((bool)(configurationAppSettings.GetValue("pWord.Enabled", typeof(bool))));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Menu = this.mainMenu1;
			this.MinimumSize = new System.Drawing.Size(600, 369);
			this.Name = "pWord";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "0_0_6B";
			this.TopMost = ((bool)(configurationAppSettings.GetValue("pWord.TopMost", typeof(bool))));
			this.Deactivate += new System.EventHandler(this.pWord_Deactivate);
			this.Load += new System.EventHandler(this.pWord_Load);
			this.VisibleChanged += new System.EventHandler(this.pWord_VisibleChanged);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pWord_MouseDown);
			this.MouseLeave += new System.EventHandler(this.pWord_MouseLeave);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.panel5.ResumeLayout(false);
			this.panel6.ResumeLayout(false);
			this.tabs.ResumeLayout(false);
			this.tabValue.ResumeLayout(false);
			this.tabValue.PerformLayout();
			this.tabNamespaces.ResumeLayout(false);
			this.tabAttributes.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }
        #endregion
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new pWord());
#if debug
            MessageBox.Show("pWord has terminated itself.");
            Application.Exit();
#endif
        }

        private void menuItem5_Click(object sender, System.EventArgs e)
        {
            // EXIT
            this.Dispose(true);
            this.Close();
        }

        private void pWord_Load(object sender, System.EventArgs e)
        {
            this.Dock = System.Windows.Forms.DockStyle.Bottom;
            System.Drawing.Rectangle a = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
            this.DockRight(sender, e);
            this.UpdateTree();

            this.autoHide_flag = false;
            VIS = true;

            //			this.WindowState = FormWindowState.Normal;

            this.autoHide_flag = false;
            this.statusBar1.Text = "AutoHide Inactive";
            this.toolBarTac.ImageIndex = 1;
            this.Visible = true;

            this.WindowState = FormWindowState.Normal;
            this.DockRight(sender, e);
            this.actHook.Stop();
        }

        struct Masters
        {
            string Name;
            object Value;
        }



        public void MouseMoved(object sender, MouseEventArgs e)
        {
            try
            {
                if (this.WindowState == FormWindowState.Normal)
                {
                    notifyIcon2.Visible = true;
                    if ((this.autoHide_flag == true) && (this.Visible == false))
                    {
                        if (e.X >= Screen.PrimaryScreen.Bounds.Right - 1)
                        {
                            if ((e.Y >= (Screen.PrimaryScreen.Bounds.Top + 64)) && (e.Y <= (Screen.PrimaryScreen.Bounds.Bottom - 80)))
                            {
                                this.VIS = true;
                                this.Visible = true;

                            }
                        }
                        else if (e.X < (Screen.PrimaryScreen.Bounds.Right - this.Width))
                        {
                            this.VIS = false;
                            this.Visible = false;

                        }
                    }
                    else if ((this.autoHide_flag == true) && (this.Visible == true))
                    {
                        if (e.X < (Screen.PrimaryScreen.Bounds.Right - this.Width))
                        {
                            this.VIS = false;
                            this.Visible = false;

                        }

                    }
                }
                else
                {
                    //Console.WriteLine("How did I get here? Windows State: {0}", this.WindowState.ToString());
                    if (e.X < (Screen.PrimaryScreen.Bounds.Right - this.Width))
                    {
                        this.VIS = false;
                        this.Visible = false;
                        notifyIcon2.Visible = false;
                        notifyIcon1.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }


        private void pWord_VisibleChanged(object sender, System.EventArgs e)
        {
            try
            {
                invisible(VIS);
                this.Activate();
                this.treeView1.Focus();
                this.DockRight(sender, e);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Something bad happened. " + ex.ToString());
            }
        }

        private void invisible(bool vis)
        {
            if (VIS == true)
            {
                this.Show();
            }
            else
            {
                this.Hide();
            }
        }

        protected Screen HostingScreen
        {
            get { return Screen.FromRectangle(this.Bounds); }
        }

        private void DockCenter(object sender, EventArgs e)
        {
            this.Size = this.MinimumSize; // shrink to min size

            int centerX = HostingScreen.WorkingArea.Width / 2;
            int centerY = HostingScreen.WorkingArea.Height / 2;

            this.Location = new Point(
                centerX - this.Width / 2,
                centerY - this.Height / 2
                );
        }

        private void DockLeft(object sender, EventArgs e)
        {
            this.Height = HostingScreen.WorkingArea.Height;
            this.Location = new Point(0, 0);
        }

        private void DockRight(object sender, EventArgs e)
        {
            this.Height = HostingScreen.WorkingArea.Height;
            this.Location = new Point(
                HostingScreen.WorkingArea.Width - this.Width,
                0
                );
        }

        private void DockBottom(object sender, EventArgs e)
        {
            this.Height = this.MinimumSize.Height;
            this.Width = HostingScreen.WorkingArea.Width;

            this.Location = new Point(
                0,
                HostingScreen.WorkingArea.Height - this.Height
                );
        }

        private void DockTop(object sender, EventArgs e)
        {
            this.Height = this.MinimumSize.Height;
            this.Width = HostingScreen.WorkingArea.Width;

            this.Location = new Point(0, 0);
        }

        private void menuItem1_Click(object sender, System.EventArgs e)
        {
            VIS = true;
            this.Visible = true;
        }

        private void menuItem2_Click(object sender, System.EventArgs e)
        {

        }

        private void menuItem3_Click(object sender, System.EventArgs e)
        {
            VIS = true;
            this.Visible = true;
        }

        private void UpdateTree()
        {
            // Update Treeview




        }

        private void NewTree()
        {
            // Use NewTree when Loading a new TREEVIEW
            //masterNode = new pNode();
            //	subNode = new pNode();

            img = new pWordLib.Image();  // contains number's representing the items contained
            // in the image list.

        }

        private void menuItem8_Click(object sender, System.EventArgs e)
        {
            flag_file = false;  // notice... I don't want you saving new stuff over your old refined work.

            AYS ays = new AYS();
            // ARE YOU SURE? This operation will delete all of your work unless you have saved.
            // (Yes / No)

            ays.ShowDialog();
            if (ays.DialogResult == DialogResult.OK)
            {

                userControl11.Masters.Clear();
                userControl11.MastersValue.Clear();
                treeView1.Nodes.Clear();
                pNode masterNode;
                masterNode = new pNode("MASTER");
                TreePics apic = new TreePics("Master", img.GroupUp, img.GroupDown);
                masterNode.Tag = "MASTER";

                this.treeView1.Nodes.Add(masterNode);

                userControl11.index = 0;  // For some reason it loses track of index?
                userControl11.Masters.Add("MASTER");
                userControl11.MastersValue.Add(masterNode);
                userControl11.txtMaster.Text = (string)userControl11.Masters[userControl11.index];
                this.tmpNode = (pNode)treeView1.Nodes[0];
            }

        }


        private void autosave()
        {
            // autosave
            try
            {
                //	Nodes[0] = userControl11;
                int count = userControl11.Masters.Count;
                Nodes.Clear();
                Nodes.Add(count);

                for (int i = 0; i < count; i++)
                {
                    Nodes.Add((string)userControl11.Masters[i]);
                    Nodes.Add((pNode)userControl11.MastersValue[i]);
                }

                //this.saveFileDialog1.ShowDialog();
                //string filename = this.saveFileDialog1.FileName;
                if (filename != null)
                {
                    IFormatter formatter = new BinaryFormatter();
                    Stream stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                    formatter.Serialize(stream, Nodes);
                    stream.Close();
                }
                flag_file = true;
            }
            catch (Exception f)
            {
                MessageBox.Show("You had an error while saving. " + f.Message, "SAVE ERROR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }

        }

        private void menuItem4_Click(object sender, System.EventArgs e)
        {

            mode = nodeMode.addto;
            try
            {
                //				AddItem dlg = new AddItem();
                //				dlg.ShowDialog();
                //				LL.List l = dlg.group;
                //
                //				if (dlg.DialogResult == DialogResult.OK)
                //				{
                //					// Add the master node to Nodes
                //					pNode masterNode = treeView1.Nodes[0];
                //					pNode aNode;
                //					aNode = new pNode((string)l.RemoveFromFront());
                //					TreePics apic = new TreePics("aNode",img.GroupUp,img.GroupDown);
                //					aNode.Tag = l.RemoveFromFront();
                //
                //					treeView1.SelectedNode.Nodes.Add(aNode);
                //
                //					//Nodes[0] = masterNode;
                //					userControl11.MastersValue[userControl11.index] = masterNode;

                // Change from Add Dialog to local members for adding name and value

                //					if (flag_file == true)
                //					{
                //						autosave();
                //					}
                //	this.tmpNode.Nodes.Clear();
                this.tmpNode = (pNode)treeView1.SelectedNode;
                lblName.Text = "Name:";
                lblValue.Text = "Value:";
                this.txtName.Focus();
                this.statusBar1.Text = "Add to Node";
                if (this.chkClear.Checked == true)
                {
                    this.txtName.Clear();
                    this.txtObject.Clear();
                }
                //				}
            }

            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }

            // master node should be called by Nodes[0]...

            //			this.treeView1.Nodes.Add(Nodes[0]);
            //			Nodes[0].Nodes.Add(apic.PicNode);

        }

        private void menuItem6_Click(object sender, System.EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.autoHide_flag = false;
            VIS = true;

            this.autoHide_flag = false;
            this.statusBar1.Text = "AutoHide Inactive";
            this.toolBarTac.ImageIndex = 1;
            this.Visible = true;

            this.WindowState = FormWindowState.Normal;
            this.DockRight(sender, e);
            this.actHook.Stop();

        }

        private void treeView1_DragLeave(object sender, System.EventArgs e)
        {


        }

        private void treeView1_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            pNode a = new pNode("test", img.GroupUp, img.GroupDown);
            a.Tag = sender;

            this.treeView1.SelectedNode.Nodes.Add(a);
        }

        private void treeView1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                if (e.Clicks == 1)
                {

                    try
                    {
                        // if left mouse is clicked then do this
                        treeView1.DoDragDrop(treeView1.SelectedNode.Tag, DragDropEffects.Copy);
                    }
                    catch (Exception f)
                    {

                    }
                    this.drag_flag = true; // ok I'm down
                }
        }

        private void treeView1_LocationChanged(object sender, System.EventArgs e)
        {

        }

        private void treeView1_CursorChanged(object sender, System.EventArgs e)
        {
            int a = 1;
            int b = a;
        }

        private void menuSave_Click(object sender, System.EventArgs e)
        {
            try
            {
                //	Nodes[0] = userControl11;
                int count = userControl11.Masters.Count;
                Nodes.Clear();
                Nodes.Add(count);

                for (int i = 0; i < count; i++)
                {
                    Nodes.Add((string)userControl11.Masters[i]);
                    Nodes.Add((pNode)userControl11.MastersValue[i]);
                }

                // save the registry entry for the current user
                // the current user should save to his/her documents folder if multiple users on same computer or server
                // or the current user should save to his/her personal network location on a share set up for only that person
                // right now conflicts can occur which will not allow for a good user experience if multiple users share the same file
                // but, if the users use different files on the same server, it saves the registry settings for each and every user.

                //if (rm._pRegistry.Filename != null)
                //{
                //    this.saveFileDialog1.FileName = rm._pRegistry.Filename;
                //}
                this.saveFileDialog1.ShowDialog();
            }
            catch (Exception f)
            {
                MessageBox.Show("You had an error while saving. " + f.Message, "SAVE ERROR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
            //			}
        }

        private void menuItem9_Click(object sender, System.EventArgs e)
        {
            this.Nodes.Clear();
            this.userControl11.MastersValue.Clear();
            this.userControl11.Masters.Clear();
            try
            {

                this.openFileDialog1.FileName = filename;
                this.openFileDialog1.ShowDialog();
            }
            catch (Exception f)
            {
                MessageBox.Show("You had an error while loading. Please select the proper file. " + f.Message, "OPEN ERROR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }

        private void menuItem14_Click(object sender, System.EventArgs e)
        {
            try
            {
                // Add the master node to Nodes
                pNode masterNode;
                if (treeView1.SelectedNode.Parent != null)
                {
                    treeView1.SelectedNode.Remove();
                    masterNode = (pNode)treeView1.Nodes[0];
                    //					Nodes[0] = masterNode;
                    userControl11.MastersValue[userControl11.index] = masterNode;

                    if (flag_file == true)
                    {
                        autosave();
                    }
                }
                else
                    MessageBox.Show("You must not delete the master node.");
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }

        private void treeView1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                this.drag_flag = false;
        }

        private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
        {

            this.mode = nodeMode.xmlUpdate;
                try
                {
                    if (e.Button == this.toolBarTac)
                    {

                        if (this.autoHide_flag == true)
                        {
                            this.autoHide_flag = false;
                            this.statusBar1.Text = "AutoHide Inactive";
                            this.toolBarTac.ImageIndex = 1;
                            this.actHook.Stop();
                        }
                        else
                        {
                            this.autoHide_flag = true;
                            this.statusBar1.Text = "AutoHide Active";
                            this.toolBarTac.ImageIndex = 0;
                            this.actHook.Start();
                        }
                    }
                    else if (e.Button == this.toolBarView)
                    {
                        if (filenameHTML != null)
                        {
                            if (exportMode == ExportMode.treeview)
                            {
                                xml.Clear();  // clear out contents first.
                                //CallRecursive(xmlNode);
                                // xmlNode = (pNode)treeView1.SelectedNode;  // this assinged xmlNode to the selected node
                                //menuItem35_Click(sender, new EventArgs());
                            }
                            else if (exportMode == ExportMode.pNode)
                            {
                                xml.Clear(); // clear out contents first.
                                //CallRecursive(xmlNode);

                                try
                                {
                                    this.exportMode = ExportMode.pNode;  // what am I exporting?  A pNode
                                    xml.Clear();  // clear out contents first.

                                    //this.xmlNode = (pNode)treeView1.SelectedNode;  // xmlNode is what is being exported to xml
                                    this.xmlIndex = treeView1.SelectedNode.Index; // xmlIndex is the SelectedNodes index
                                    this.menuItem21.Enabled = true; // MenuItem21 is enabled... Todo: rename menuItem21
                                    this.menuItem31.Enabled = true;  // MneuItem21 is enabled... Todo: rename menuItem31
                                    this.nodeIndex = treeView1.SelectedNode.Index; // nodeInex is now equal to xmlIndex?
                                    this.statusBar1.Text = "Export Node XML Mode";
                                    //CallRecursive(xmlNode);  // disabled CallRecursive here... need to fix Call recursive
                                    // ToDo: fix CallRecursive(xmlNode)

                                    CallRecursive(xmlNode);  // treeview1 is a pView

                                    this.saveFileDialogHTML.FileName = filenameHTML;
                                    this.saveFileDialogHTML.Title = "Save the NODE to XML/HTML";
                                    //this.saveFileDialogHTML.ShowDialog();
                                    //this.filenameHTML = this.saveFileDialogHTML.FileName;

                                    if (this.filenameHTML != null)
                                    {

                                        //					IFormatter formatter = new BinaryFormatter();
                                        //					Stream stream = new FileStream(this.saveFileDialog1.FileName,FileMode.Create,FileAccess.Write,FileShare.ReadWrite);
                                        //					for (int i=0; i<xml.Count;i++)
                                        //					{
                                        //						formatter.Serialize(stream, xml[i].ToString());
                                        //					}
                                        //					this.filename = this.saveFileDialog1.FileName;
                                        //					stream.Close();
                                        //					FileStream fs = new FileStream(filename,
                                        //						FileMode.CreateNew, FileAccess.Write, FileShare.None);

                                        //StreamWriter swFromFile = new StreamWriter(filenameHTML);

                                        // may not have to do this with XmlDocument
                                        //swFromFile.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>"); 
                                        //for (int i=0; i<xml.Count;i++)					
                                        //{
                                        //	swFromFile.Write(xml[i]);
                                        //	//						formatter.Serialize(stream, xml[i].ToString());
                                        //}
                                        //swFromFile.Flush();
                                        //swFromFile.Close();
                                        if (xdoc != null)
                                        {
                                            xdoc.Save(filenameHTML);
                                            xdoc.RemoveAll();
                                            xdoc = null;
                                        }
                                        else
                                        {
                                            MessageBox.Show("The export was not able to save b/c it was empty.");
                                        }
                                    }

                                    this.toolBarView.Enabled = true;
                                }
                                catch (Exception f)
                                {
                                    MessageBox.Show("You had an error while exporting to XML. " + f.Message, "SAVE ERROR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                                }



                            }


                            //StreamWriter swFromFile = new StreamWriter(filenameHTML);

                            //swFromFile.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                            //for (int i = 0; i < xml.Count; i++)
                            //{
                            //    swFromFile.Write(xml[i]);
                            //}
                            //swFromFile.Flush();
                            //swFromFile.Close();
                            System.Diagnostics.Process.Start(filenameHTML);
                        }

                        else
                        {
                            MessageBox.Show("You must first export a NODE to XML or HTML.");
                        }
                    }
                    else if (e.Button == this.toolBarXML)
                    {

                        //XmlSerializer xs = new XmlSerializer(typeof(pNode));
                        //StringWriter sw = new StringWriter();
                        //xs.Serialize(sw, treeView1.Nodes[0]);
                    }
                }
                catch (Exception f)
                {
                    MessageBox.Show("You must first Export a NODE to XML or HTML format." + f.Message);
                }

        }

        private void eventLog1_EntryWritten(object sender, System.Diagnostics.EntryWrittenEventArgs e)
        {

        }

        private void pWord_MouseLeave(object sender, System.EventArgs e)
        {

        }

        private void pWord_Deactivate(object sender, System.EventArgs e)
        {
            if (this.autoHide_flag == true)
            {
                this.VIS = false;
                this.Visible = false;
            }
        }

        private void pWord_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {

        }

        private void contextMenu1_Popup(object sender, System.EventArgs e)
        {

        }

        private void menuItem3_Click_1(object sender, System.EventArgs e)
        {
            lblName.Text = "Name:";
            lblValue.Text = "Value:";

            Clipboard.SetDataObject(this.treeView1.SelectedNode.Tag, true);
            this.statusBar1.Text = "Copy Value Text Mode";
        }

        private void treeView1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            pNode a = (pNode)treeView1.GetNodeAt(e.X, e.Y);

            if (a != null)
            {
                treeView1.SelectedNode = a;
                this.txtValue.Text = (string)a.Tag;
            }

        }

        private void menuItem17_Click(object sender, System.EventArgs e)
        {
            frmAbout dlg = new frmAbout();
            dlg.ShowDialog();

        }

        private void menuItem12_Click(object sender, System.EventArgs e)
        {


        }

        private void treeView1_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
        {

        }

        private void treeView1_BeforeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
        {

        }

        private void treeView1_AfterExpand(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode != null)
                treeView1.SelectedNode.SelectedImageIndex = 1;
        }

        private void treeView1_AfterCollapse(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode != null)
                treeView1.SelectedNode.SelectedImageIndex = 0;
        }

        private void treeView1_ChangeUICues(object sender, System.Windows.Forms.UICuesEventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            treeView1.SelectedNode.SelectedImageIndex = img.GroupDown;
        }

        private void menuItem18_Click(object sender, System.EventArgs e)
        {
            // Help add support documentation
            // this will access the documentation portion of this project
            frmAbout dlg = new frmAbout();
            dlg.ShowDialog();
        }

        private void userControl11_Load(object sender, System.EventArgs e)
        {
            // Load what in registry else just show a new master
            // always check registry when starting pWord
            // be sure to check the Default version
            rm = new pWordLib.mgr.registryMgr(pWordSettings.Default.version.ToString());

            openFileDialog1.FileName = rm.AutoSavePathFromRegistry(pWordSettings.Default.version.ToString());
            if (rm.FileExist())
            {
                openFileDialog1_FileOk(null, null);
            }
            else
            {
                pNode masterNode;
                masterNode = new pNode("MASTER");
                TreePics apic = new TreePics("Master", img.GroupUp, img.GroupDown);
                masterNode.Tag = "MASTER";

                this.treeView1.Nodes.Add(masterNode);

                userControl11.Masters.Add("MASTER");
                userControl11.MastersValue.Add(masterNode);
                userControl11.txtMaster.Text = (string)userControl11.Masters[userControl11.index];
            }
            this.tmpNode = (pNode)treeView1.Nodes[0];
            // master node should be called by Nodes[0]...
        }			// Add the master node to Nodes


        private void menuItem19_Click(object sender, System.EventArgs e)
        {
            // Add master
            AddMaster dlg = new AddMaster();
            dlg.ShowDialog();

            if (dlg.DialogResult == DialogResult.OK)
            {
                this.treeView1.Nodes.Clear();
                pNode masterNode;
                masterNode = new pNode("MASTER");
                TreePics apic = new TreePics("masterNode", img.GroupUp, img.GroupDown);
                masterNode.Tag = "MASTER";

                this.treeView1.Nodes.Add(masterNode);

                userControl11.Masters.Add(dlg.txtMaster.Text);
                userControl11.MastersValue.Add(masterNode);
                userControl11.index++;
                userControl11.txtMaster.Text = (string)userControl11.Masters[userControl11.index];
                this.tmpNode = (pNode)treeView1.Nodes[0];  // Always start with master
            }
        }

        private void menuItem20_Click(object sender, System.EventArgs e)
        {

        }

        private void userControl11_LeftClicked(object sender, System.EventArgs e)
        {

            // This is the hardest of all
            this.treeView1.Nodes.Clear();
            pNode masterNode = (pNode)userControl11.MastersValue[userControl11.index];
            TreePics apic = new TreePics("masterNode", img.GroupUp, img.GroupDown);
            this.treeView1.Nodes.Add(masterNode);


        }

        private void userControl11_RightClicked(object sender, System.EventArgs e)
        {
            // This is the hardest of all
            this.treeView1.Nodes.Clear();
            pNode masterNode = (pNode)userControl11.MastersValue[userControl11.index];
            TreePics apic = new TreePics("masterNode", img.GroupUp, img.GroupDown);
            this.treeView1.Nodes.Add(masterNode);
        }

        private void menuItem16_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void menuItem11_Click(object sender, System.EventArgs e)
        {
            try
            {
                if ((this.treeView1.SelectedNode.Tag != null) && (((String)(this.treeView1.SelectedNode.Tag)).Length > 0))
                {
                    System.Diagnostics.Process.Start(this.treeView1.SelectedNode.Tag.ToString());
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("You must use an acceptable link contained in the value field!","DANGER",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                //txtValue.Text = "Value Field is can not process value.";
                txtValue.Text = ex.Message;
            }
        }

        private void menuItem12_Click_1(object sender, System.EventArgs e)
        {
            // help

        }

        private void panel2_VisibleChanged(object sender, System.EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            pNode masterNode = (pNode)treeView1.Nodes[0];
            if (mode == nodeMode.addto)
            {
                try
                {
                    pNode aNode;
                    aNode = new pNode(this.txtName.Text);
                    //TreePics apic = new TreePics("aNode", img.GroupUp, img.GroupDown);
                    aNode.Tag = this.txtObject.Text;
                    aNode.Text = this.txtName.Text;
                    if (tmpNode.Namespace != null)
                    {
                        aNode.Namespace = tmpNode.Namespace;  // trickle down namespaces
                    }
                    treeView1.SelectedNode = tmpNode;
                    treeView1.SelectedNode.Nodes.Add(aNode);
                    // after adding the new node, be sure the index is updated as well... this is not necessary
                    userControl11.MastersValue[userControl11.index] = masterNode;

                    // Change from Add Dialog to local members for adding name and value

                    // check box
                    if (this.chkClear.CheckState == CheckState.Checked)
                    {
                        this.txtName.Clear();
                        this.txtObject.Clear();
                        this.txtName.Focus();
                    }
                    else if (this.chkClear.CheckState == CheckState.Indeterminate)
                    {
                        this.txtObject.Clear();
                        this.txtObject.Focus();
                    }
                    else
                    {
                        this.btnAdd.Focus();
                    }

                    if (flag_file == true)
                    {
                        autosave();
                    }
                }
                catch (Exception f)
                {
                    MessageBox.Show(f.Message);
                }
            }
            else if (mode == nodeMode.edit)
            {
                try
                {
                    // EDIT MODE
                    // Only edit the current node
                    pNode aNode;
                    aNode = new pNode(this.txtName.Text);
                    TreePics apic = new TreePics("aNode", img.GroupUp, img.GroupDown);
                    aNode.Tag = this.txtObject.Text;
                    treeView1.SelectedNode = this.tmpNode;
                    treeView1.SelectedNode.Text = aNode.Text;
                    treeView1.SelectedNode.Tag = aNode.Tag;

                    // This is not necessary, when a save is committed this can be performed at that juncture
                    // However, it may be beneficial to know whether or not a node change was successfully saved
                    // at the iteration the event occurred.  This will prevent loss of work
                    userControl11.MastersValue[userControl11.index] = masterNode;

                    // Change from Add Dialog to local members for adding name and value
                    if (this.chkClear.Checked == true)
                    {
                        this.txtName.Clear();
                        this.txtObject.Clear();
                    }

                    if (flag_file == true)
                    {
                        autosave();
                    }
                    this.Refresh();
                }
                catch (Exception f)
                {
                    MessageBox.Show(f.Message);
                }
            }
            else if (mode == nodeMode.insert)
            {
                if (treeView1.SelectedNode.Parent == null)
                {
                    MessageBox.Show("You can not insert a sibling of the master node.");
                    return;
                }
                try
                {
                    // Insert Mode
                    // Only edit the current node
                    pNode aNode;
                    aNode = new pNode(this.txtName.Text);
                    TreePics apic = new TreePics("aNode", img.GroupUp, img.GroupDown);
                    aNode.Tag = this.txtObject.Text;
                    if (tmpNode.Namespace != null)
                    {
                        aNode.Namespace = tmpNode.Namespace;
                    }
                    treeView1.SelectedNode = tmpNode;
                    treeView1.SelectedNode.Nodes.Insert(modeIndex, aNode);
                    userControl11.MastersValue[userControl11.index] = masterNode;

                    // Change from Add Dialog to local members for adding name and value

                    // check box
                    if (this.chkClear.Checked == true)
                    {
                        this.txtName.Clear();
                        this.txtObject.Clear();
                    }

                    if (flag_file == true)
                    {
                        autosave();
                    }
                }
                catch (Exception f)
                {
                    MessageBox.Show(f.Message);
                }
            }
            else if (mode == nodeMode.addAttributeTo)
            {
                try
                {
                    // Add attribute to the selected treeNode
                    // Only edit the current node
                    //pNode aNode;
                    //aNode = new pNode(this.txtName.Text);
                    //TreePics apic = new TreePics("aNode", img.GroupUp, img.GroupDown);
                    //aNode.Tag = this.txtObject.Text;
                    tmpNode.AddAttribute(txtName.Text, txtObject.Text);
                    treeView1.SelectedNode = tmpNode;
                    userControl11.MastersValue[userControl11.index] = masterNode;

                    // Change from Add Dialog to local members for adding name and value

                    // check box
                    if (this.chkClear.Checked == true)
                    {
                        this.txtName.Clear();
                        this.txtObject.Clear();
                    }

                    if (flag_file == true)
                    {
                        autosave();
                    }
                }
                catch (Exception f)
                {
                    MessageBox.Show(f.Message);
                }
            }
            else if (mode == nodeMode.viewErrors)
            {
                // do nothing at all
            }
            else if (mode == nodeMode.addNamespacePrefix)
            {
                try
                {
                    // Add attribute to the selected treeNode
                    // Only edit the current node
                    //pNode aNode;
                    //aNode = new pNode(this.txtName.Text);
                    //TreePics apic = new TreePics("aNode", img.GroupUp, img.GroupDown);
                    //aNode.Tag = this.txtObject.Text;
                    tmpNode.Namespace = new pWordLib.dat.NameSpace();
                    tmpNode.Namespace.Prefix = txtName.Text;
                    tmpNode.Namespace.URI_PREFIX = txtObject.Text;
                    treeView1.SelectedNode = tmpNode;
                    userControl11.MastersValue[userControl11.index] = masterNode;

                    // Change from Add Dialog to local members for adding name and value

                    // check box
                    if (this.chkClear.Checked == true)
                    {
                        this.txtName.Clear();
                        this.txtObject.Clear();
                    }

                    if (flag_file == true)
                    {
                        autosave();
                    }
                }
                catch (Exception f)
                {
                    MessageBox.Show(f.Message);
                }
            }
            else if (mode == nodeMode.addNamespaceSuffix)
            {
                try
                {
                    // Add attribute to the selected treeNode
                    // Only edit the current node
                    //pNode aNode;
                    //aNode = new pNode(this.txtName.Text);
                    //TreePics apic = new TreePics("aNode", img.GroupUp, img.GroupDown);
                    //aNode.Tag = this.txtObject.Text;
                    tmpNode.Namespace = new pWordLib.dat.NameSpace();
                    tmpNode.Namespace.Suffix = txtName.Text;
                    tmpNode.Namespace.URI_SUFFIX = txtObject.Text;
                    treeView1.SelectedNode = tmpNode;
                    userControl11.MastersValue[userControl11.index] = masterNode;

                    // Change from Add Dialog to local members for adding name and value

                    // check box
                    if (this.chkClear.Checked == true)
                    {
                        this.txtName.Clear();
                        this.txtObject.Clear();
                    }

                    if (flag_file == true)
                    {
                        autosave();
                    }
                }
                catch (Exception f)
                {
                    MessageBox.Show(f.Message);
                }
            }

        }




        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            panel2.Visible = false;
        }

        private void menuItem7_Click(object sender, System.EventArgs e)
        {

        }

        private void panel1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

        }

        private void treeView1_MouseMove_1(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            pNode a = (pNode)treeView1.GetNodeAt(e.X, e.Y);

            if (a != null)
            {
                treeView1.SelectedNode = a;

                if (a.Text == "password")
                {
                    this.txtValue.Text = "password";
                }
                else
                {
                    this.txtValue.Text = (string)a.Tag;
                    this.lstNamespaces.Items.Clear();
                    this.lstAttributes.Items.Clear();
                    // for each namespace that exists at this node
                    pNode aParent = a;
                    do
                    {
                        if (aParent.Namespace == null)
                        {
                            aParent = (pNode)aParent.Parent;
                        }
                        else
                        {
                            // show only prefixes for now
                            ListViewItem item = new ListViewItem(aParent.Namespace.Prefix);
                            item.SubItems.Add(aParent.Namespace.URI_PREFIX);
                            item.SubItems.Add(aParent.Namespace.URI_SUFFIX);
                            lstNamespaces.Items.Add(item);
                            lstNamespaces.Refresh();
                            break;
                        }
                    } while (aParent != null);


                    // for each attribute that exists at this node
                    IList<String> keys = a.getKeys();
                    foreach (String key in keys)
                    {
                        ListViewItem item = new ListViewItem(key);
                        item.SubItems.Add(a.getValue(key));
                        lstAttributes.Items.Add(item);
                    }
                }
            }
        }

        private void treeView1_AfterCollapse_1(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode != null)
                treeView1.SelectedNode.SelectedImageIndex = 0;

        }

        private void treeView1_AfterExpand_1(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode != null)
                treeView1.SelectedNode.SelectedImageIndex = 1;
        }

        private void treeView1_AfterSelect_1(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            treeView1.SelectedNode.SelectedImageIndex = img.GroupDown;

        }

        private void treeView1_DragDrop_1(object sender, System.Windows.Forms.DragEventArgs e)
        {
            pNode a = new pNode("test", img.GroupUp, img.GroupDown);
            a.Tag = sender;

            this.treeView1.SelectedNode.Nodes.Add(a);
        }


        private void treeView1_MouseDown_1(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (e.Clicks == 1)
                {

                    try
                    {

                        this.StartPt.X = e.X;
                        this.StartPt.Y = e.Y;

                        ///
                        /// TODO:Fix Multiple Select Behavior in treeView1
                        /// 
                        /// 

                        // if left mouse is clicked then do this
                        //					    tmpNode = treeView1.SelectedNode;

                        /// TODO: Get rid of DoDragDrop behavior is possible
                        /// Try and save 
                        /// 
                        if (treeView1.SelectedNode.Tag == null)
                        {
                            treeView1.DoDragDrop("", DragDropEffects.Copy);
                            moveNode = (pNode)this.treeView1.SelectedNode;
                        }
                        else
                        {
                            treeView1.DoDragDrop(treeView1.SelectedNode.Tag, DragDropEffects.Copy);
                            moveNode = (pNode)this.treeView1.SelectedNode;
                        }
                    }
                    catch (Exception f)
                    {

                    }


                    this.drag_flag = true; // ok I'm down
                }
                else if (e.Clicks == 2)
                {
                    System.EventArgs generic = new System.EventArgs();
                    menuItem11_Click(sender, generic);
                }
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                // the context menu is being called... 
                // now record the node
                object s = sender;
                Console.WriteLine("Duh!");
            }

        }
        private void treeView1_MouseUp_1(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            /// TODO: Use StopPt and StartPt to select multiple nodes
            /// Place these nodes in an ArrayList and when the PutNode is used
            /// place All the nodes
            /// also behavior... start from StartPt and always end with end point so 
            /// that if things need to get flipped around it will be possible.
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                this.drag_flag = false;
        }

        private void menuItem13_Click(object sender, System.EventArgs e)
        {

            mode = nodeMode.edit;

            try
            {
                lblName.Text = "Name:";
                lblValue.Text = "Value:";
                this.modeIndex = treeView1.SelectedNode.Index;
                //				this.txtName.Text = treeView1.SelectedNode.Nodes[modeIndex].Text;
                //				this.txtObject.Text = treeView1.SelectedNode.Nodes[modeIndex].Text;
                this.tmpNode = (pNode)treeView1.SelectedNode;
                this.txtName.Text = tmpNode.Text;
                this.txtObject.Text = (string)tmpNode.Tag;
                this.statusBar1.Text = "Edit Mode";
                this.txtName.Focus();
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

        private void menuItem21_Click(object sender, System.EventArgs e)
        {
            // put in
            try
            {
                treeView1.SelectedNode.Nodes.Insert(treeView1.SelectedNode.Nodes.Count, this.getNode);
                this.getNode = (pNode)this.getNode.Clone();

                if (flag_file == true)
                {
                    autosave();
                }

            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }

        }

        private void menuItemGetNode_Click(object sender, System.EventArgs e)
        {

            // get
            try
            {

                this.getNode = (pNode)treeView1.SelectedNode.Clone();
                this.menuItem21.Enabled = true;
                this.menuItem31.Enabled = true;
                this.nodeIndex = treeView1.SelectedNode.Index;
                this.statusBar1.Text = "Get Node Mode";
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }

        private void menuItem23_Click(object sender, System.EventArgs e)
        {

            xml.Clear();  // clear out contents first.
            CallRecursive((pNode)treeView1.SelectedNode);
            try
            {
                exportMode = ExportMode.treeview;
                XmlDocument xdoc = new XmlDocument();

                this.saveFileDialogHTML.FileName = filenameHTML;
                this.saveFileDialogHTML.Title = "Save View to XML/HTML";
                this.saveFileDialogHTML.ShowDialog();
                filenameHTML = this.saveFileDialogHTML.FileName;
                if (filenameHTML != null)
                {

                    //					IFormatter formatter = new BinaryFormatter();
                    //					Stream stream = new FileStream(this.saveFileDialog1.FileName,FileMode.Create,FileAccess.Write,FileShare.ReadWrite);
                    //					for (int i=0; i<xml.Count;i++)
                    //					{
                    //						formatter.Serialize(stream, xml[i].ToString());
                    //					}
                    //					this.filename = this.saveFileDialog1.FileName;
                    //					stream.Close();
                    //					FileStream fs = new FileStream(filename,
                    //						FileMode.CreateNew, FileAccess.Write, FileShare.None);

                    this.toolBarView.Enabled = true;
                }


            }
            catch (Exception f)
            {
                MessageBox.Show("You had an error while exporting to XML. " + f.Message, "SAVE ERROR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }

        }

        //private void PrintRecursive(pNode pNode)
        //{
        //    string [] split = null;
        //    string delimStr = " ";
        //    char [] delimiter = delimStr.ToCharArray();

        //    // Print the node.
        //    // Print each node recursively.
        //    foreach (pNode tn in pNode.Nodes)
        //    {
        //        split = null;
        //        xml.Add("<"); xml.Add(tn.Text); xml.Add(">");
        //        xml.Add((string)tn.Tag);
        //        PrintRecursive(tn);
        //        split = tn.Text.Split(delimiter,2);
        //        xml.Add("</"); xml.Add(split[0]); xml.Add(">"); 
        //    }
        //}
        private XmlDocument xdoc;  // xdoc will be used to quickly load the xml content into a node
        // the goal will be to quickly close the open dialog box.
        // this will allow the open dialog box to close 
        // and then the pNode tree will be generated from the xDoc after
        // the open dialog box closes.
        private void CallRecursive(pNode node)
        {
            // MessageBox.Show("getnode:" + getNode.Name);
            // MessageBox.Show("pView top node:" + node.Text);
            // use c# 4.0 in a nut shell to construct XmlDocument for this xml stuff
            // page starts on 490
            xdoc = new XmlDocument();
            xdoc.AppendChild(xdoc.CreateXmlDeclaration("1.0", null, "yes"));

            if (xdoc == null)  // instantiate the first time through
            {
                xdoc = new XmlDocument();
                System.Xml.NameTable nt = new NameTable();
                nt.Add(node.Text);
                XmlNameTable xnt = (XmlNameTable)nt;
                System.Xml.XmlNamespaceManager xnsm = new XmlNamespaceManager(xnt);
                if (!(node.Namespace == null))
                {
                    if (node.Namespace.Prefix != null)
                    {
                        xnsm.AddNamespace(node.Namespace.Prefix, node.Namespace.URI_PREFIX);  // prefix will be like 'xs', and url will be like 'http://www.url.com/etc/'
                    }
                    if (node.Namespace.Suffix != null)
                    {
                        xnsm.AddNamespace(node.Namespace.Suffix, node.Namespace.URI_SUFFIX);
                    }
                }
                // todo:  iterate through all nodes in pNode and place namespaces into the namespace manager
                // for now only do the first one if it exists

                xdoc = new XmlDocument(xnt);
                xdoc.AppendChild(xdoc.CreateXmlDeclaration("1.0", null, "yes"));
                foreach (String key in xnsm.GetNamespacesInScope(XmlNamespaceScope.All).Keys)
                {
                    // this inserts the namespace into the xdoc from the name space manager
                    //xdoc.Schemas.XmlResolver resolve = 
                    //xdoc.Schemas.Add(key, xnsm.LookupNamespace(key));
                }
            }

            node.getXmlName();  // fix node attributes and todo: eventually namespaces

            XmlNode rootNode = xdoc.CreateElement(node.getXmlName());
            rootNode.InnerText = (String)node.Tag;
 
            if (node.Namespace != null)
            {
                //rootNode = xdoc.CreateNode(XmlNodeType.Element, node.Namespace.Prefix, node.Name, node.Namespace.URI_PREFIX);
            }
            else
            {
                // this takes a long time as well
                // see if we can't use an existing node and clone it
                //rootNode = xdoc.CreateNode(XmlNodeType.Element, node.getXmlName(), "");  // any time getXmlName is called ... 
                // it should be necessary to now recheck to see if its got attributes at this current node
            }
            foreach (String key in node.getKeys())
            {
                if (key != "")
                {
                    if (!(node.Namespace == null))
                    {
                        XmlNode attr = xdoc.CreateNode(XmlNodeType.Attribute, node.Namespace.Prefix, key, node.Namespace.URI_PREFIX);
                        attr.Value = node.getValue(key);
                        rootNode.Attributes.Append((XmlAttribute)attr);  // attr is an xmlNode object ;)
                    }
                    else
                    {
                        XmlNode attr;
                        if (node.Namespace != null)
                        {
                            if (node.Namespace.Prefix != null)
                            {
                                attr = xdoc.CreateNode(XmlNodeType.Attribute, key, node.Namespace.URI_PREFIX);
                            }
                            else
                            {
                                attr = xdoc.CreateNode(XmlNodeType.Attribute, key, "");
                            }
                        }
                        else
                        {
                            attr = xdoc.CreateNode(XmlNodeType.Attribute, key, "");
                        }
                        attr.Value = node.getValue(key);
                        rootNode.Attributes.Append((XmlAttribute)attr);  // attr is an xmlNode object ;)
                    }
                }
            }



            foreach (pNode p in node.Nodes)
            {
                p.getXmlName();  // fix any attributes (this has been completed in pNode.DetectComplexNodeName()
                                 // todo: fix any namespace ... 
            }

            foreach (pNode p in node.Nodes)
            {
                XmlNode xn;
                if (p.Namespace != null)
                {
                    if (p.Namespace.Prefix != null)
                    {
                        xn = xdoc.CreateNode(XmlNodeType.Element, p.Namespace.Prefix, p.Text, p.Namespace.URI_PREFIX);
                    }
                    else
                    {
                        // this takes a long time as well
                        // see if we can't use an existing node and clone it
                        xn = xdoc.CreateNode(XmlNodeType.Element, p.getXmlName(), "");  // any time getXmlName is called ... 
                        // it should be necessary to now recheck to see if its got attributes at this current node
                    }
                }
                else
                {
                    // this takes a long time as well
                    // see if we can't use an existing node and clone it
                    xn = xdoc.CreateNode(XmlNodeType.Element, p.getXmlName(), "");  // any time getXmlName is called ... 
                                                                                    // it should be necessary to now recheck to see if its got attributes at this current node
                }
                xn.InnerText = (String)p.Tag;
                foreach (String key in p.getKeys())
                {
                    if (p.Namespace != null)
                    {
                        if (p.Namespace.Prefix != null)
                        {
                            XmlNode attr = xdoc.CreateNode(XmlNodeType.Attribute, p.Namespace.Prefix, key, p.Namespace.URI_PREFIX);
                            attr.Value = p.getValue(key);
                            xn.Attributes.Append((XmlAttribute)attr);  // attr is an xmlNode object ;)
                        }
                        else
                        {
                            XmlNode attr;
                            attr = xdoc.CreateNode(XmlNodeType.Attribute, key, "");
                            attr.Value = p.getValue(key);
                            xn.Attributes.Append((XmlAttribute)attr);  // attr is an xmlNode object ;)
                        }
                    }
                    else
                    {
                        XmlNode attr;
                        if (p.Namespace != null)
                        {
                            if (p.Namespace.Prefix != null)
                            {
                                attr = xdoc.CreateNode(XmlNodeType.Attribute, key, p.Namespace.URI_PREFIX);
                            }
                            else
                            {
                                attr = xdoc.CreateNode(XmlNodeType.Attribute, key, "");
                            }
                        }
                        else
                        {
                            attr = xdoc.CreateNode(XmlNodeType.Attribute, key, "");
                        }
                        attr.Value = p.getValue(key);
                        xn.Attributes.Append((XmlAttribute)attr);  // attr is an xmlNode object ;)
                    }
                }

                rootNode.AppendChild(RecursiveChildren(ref xn, p.Nodes));
            }
            xdoc.AppendChild(rootNode);
        }

        private XmlNode RecursiveChildren(ref XmlNode node, TreeNodeCollection pNodes)
        {

            foreach (pNode p in pNodes)
            {
                p.getXmlName();
            }

            foreach (pNode p in pNodes)
            {
                XmlNode xn;
                if (p.Namespace != null)
                {
                    xn = xdoc.CreateNode(XmlNodeType.Element, p.Namespace.Prefix, p.getXmlName(), p.Namespace.URI_PREFIX);
                }
                else
                {
                    // Takes a long time to do this... why???? 
                    // Check to see if we cna not create a node, but use an available or existing node
                    xn = xdoc.CreateNode(XmlNodeType.Element, p.getXmlName().Replace(" ", ""), "");
                }
                xn.InnerText = (String)p.Tag;
                foreach (String key in p.getKeys())
                {
                    System.Xml.NameTable nt = new NameTable();
                    nt.Add(p.Text);
                    XmlNameTable xnt = (XmlNameTable)nt;
                    System.Xml.XmlNamespaceManager xnsm = new XmlNamespaceManager(xnt);
                    if (p.Namespace != null)
                    {
                        if (p.Namespace.Prefix != null)
                        {
                            xnsm.AddNamespace(p.Namespace.Prefix, p.Namespace.URI_PREFIX);  // prefix will be like 'xs', and url will be like 'http://www.url.com/etc/'
                        }
                        if (p.Namespace.Suffix != null)
                        {
                            xnsm.AddNamespace(p.Namespace.Suffix, p.Namespace.URI_SUFFIX);
                        }

                    }

                    if (p.Namespace != null)
                    {
                        if (p.Namespace.Prefix != null)
                        {
                            XmlNode attr = xdoc.CreateNode(XmlNodeType.Attribute, p.Namespace.Prefix, p.Namespace.URI_PREFIX);
                            attr.Value = p.getValue(key);
                            xn.Attributes.Append((XmlAttribute)attr);  // attr is an xmlNode object ;)
                        }
                        else
                        {
                            XmlNode attr;
                            attr = xdoc.CreateNode(XmlNodeType.Attribute, key, "");
                        }
                    }
                    else
                    {
                        XmlNode attr;
                        if (p.Namespace != null)
                        {
                            attr = xdoc.CreateNode(XmlNodeType.Attribute, key, p.Namespace.URI_PREFIX);
                        }
                        else
                        {
                            attr = xdoc.CreateNode(XmlNodeType.Attribute, key, "");
                        }
                        attr.Value = p.getValue(key);
                        xn.Attributes.Append((XmlAttribute)attr);  // attr is an xmlNode object ;)
                    }
                }
                node.AppendChild(RecursiveChildren(ref xn, p.Nodes));
            }
            return node;
        }

        //// Call the procedure using the TreeView.
        //private void CallRecursive(pView treeView)
        //{
        //    string [] split = null;
        //    string delimStr = " ";
        //    char [] delimiter = delimStr.ToCharArray();


        //    // Print each node recursively.
        //    TreeNodeCollection nodes = treeView.Nodes;
        //    foreach (pNode n in nodes)
        //    {
        //        split =null;
        //        xml.Add("<"); xml.Add(n.Text); xml.Add(">");
        //        xml.Add((string)n.Tag);
        //        PrintRecursive(n);
        //        split = n.Text.Split(delimiter,2);
        //        xml.Add("</"); xml.Add(split[0]); xml.Add(">"); 
        //    }
        //}

        //private void CallRecursive(pNode pNode)
        //{
        //    string [] split = null;
        //    string delimStr = " ";
        //    char [] delimiter = delimStr.ToCharArray();


        //    // Print each node recursively.
        //    TreeNodeCollection nodes = pNode.Nodes;
        //    foreach (pNode n in nodes)
        //    {
        //        split =null;
        //        xml.Add("<"); xml.Add(n.Text); xml.Add(">");
        //        xml.Add((string)n.Tag);
        //        PrintRecursive(n);
        //        split = n.Text.Split(delimiter,2);
        //        xml.Add("</"); xml.Add(split[0]); xml.Add(">"); xml.Add("\n");
        //    }
        //}

        private void menuItem27_Click(object sender, System.EventArgs e)
        {
            AYS dlg = new AYS();
            dlg.label1.Text = "ARE YOU SURE???\nSorting may change your structure.";
            dlg.ShowDialog();
            if (dlg.DialogResult == DialogResult.OK)
            {
                this.treeView1.Sorted = true;
            }


        }

        private void menuItem28_Click(object sender, System.EventArgs e)
        {
            this.treeView1.Sorted = false;
        }

        private void treeView1_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
        {
            //this.treeView1.Cursor = 
        }

        private void menuItem29_Click(object sender, System.EventArgs e)
        {
            lblName.Text = "Name:";
            lblValue.Text = "Value:";
            mode = nodeMode.insert;
            if (this.chkClear.Checked == true)
            {
                this.txtName.Clear();
                this.txtObject.Clear();
            }
            try
            {
                this.tmpNode = (pNode)this.treeView1.SelectedNode.Parent;
                this.modeIndex = treeView1.SelectedNode.Index;
                this.txtName.Focus();

            }

            catch (Exception f)
            {
                MessageBox.Show(f.Message);

            }
            this.statusBar1.Text = "Insert Mode";


        }

        private void menuItem31_Click(object sender, System.EventArgs e)
        {
            // Insert next to
            try
            {
                treeView1.SelectedNode.Parent.Nodes.Insert(treeView1.SelectedNode.Index, getNode);


                //Insert(treeView1.SelectedNode.Nodes.Count,this.getNode);
                this.getNode = (pNode)this.getNode.Clone();

                if (flag_file == true)
                {
                    autosave();
                }

            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }

        private void treeView1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
        }

        private void treeView1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Delete)
            {

                System.EventArgs f = new System.EventArgs();
                menuItem14_Click(sender, f);
            }
            else if (e.KeyCode == System.Windows.Forms.Keys.Insert)
            {
                EventArgs f = new EventArgs();
                if (menuItem29.Enabled == true)
                {
                    menuItem29_Click(sender, f);
                }
            }
        }

        private void chkClear_CheckedChanged(object sender, System.EventArgs e)
        {

        }

        private void chkClear_CheckStateChanged(object sender, EventArgs e)
        {
            // FIND OUT THE STATE...  
            // CHECKED: Clear the key and value fields.
            // CHECK-Disabled: Clear only the value field
            // Not Check: Clear no fields
            if (chkClear.CheckState == CheckState.Checked)
            {
                chkClear.Text = "Clear All Text";
            }
            else if (chkClear.CheckState == CheckState.Indeterminate)
            {
                chkClear.Text = "Clear Value Only";
            }
            else if (chkClear.CheckState == CheckState.Unchecked)
            {
                chkClear.Text = "Clear Disabled";
            }
        }


        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

            this.Nodes.Clear();
            this.userControl11.MastersValue.Clear();
            this.userControl11.Masters.Clear();
            filename = this.openFileDialog1.FileName;

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            Nodes = (ArrayList)formatter.Deserialize(stream);
            stream.Close();

            this.treeView1.Nodes.Clear();
            //				this.treeView1.Nodes.Add((pNode)Nodes[0]);

            //	Nodes[0] = userControl11;
            int count = (int)Nodes[0];
            int i = 1;
            while (i < Nodes.Count)
            {
                userControl11.Masters.Add((string)Nodes[i]);
                // check Nodes type
                ++i;
                if (Nodes[i].GetType() == typeof(pNode))
                {
                    userControl11.MastersValue.Add((pNode)Nodes[i]);
                }
                else if (Nodes[i].GetType() == typeof(TreeNode))
                {
                    // Compatibility with old version 6A
                    // convert TreeNode to a pNode
                    TreeNode a = (TreeNode)Nodes[i];

                    // p only gets the top node for a master... it doesn't delve in and get everything else
                    // Todo: get all other nodes in a proper TreeNode to pNode conversion
                    // I want to perform the conversion indie of the pNode class itself
                    try
                    {
                        pNode p = pNode.TreeNode2pNode(a);
                        //                    userControl11.MastersValue.Add((TreeNode)Nodes[i]);
                        userControl11.MastersValue.Add(p);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.ToString());
                    }
                }
                i++;
            }
            userControl11.index = 0;
            this.treeView1.Nodes.Add((pNode)userControl11.MastersValue[userControl11.index]);
            flag_file = true;

            // successful?  go ahead and make the open stick
            rm.SavePathInRegistry(pWordSettings.Default.version, filename);

        }

        private void menuItem33_Click(object sender, System.EventArgs e)
        {
            AYS ays = new AYS();
            ays.label1.Text = "This will load the file to the last insert, or addition only if you have saved or opened a file previously.";

            ays.ShowDialog();
            if (ays.DialogResult == DialogResult.OK)
            {
                try
                {
                    if (this.flag_file == true)
                    {
                        this.Nodes.Clear();
                        this.userControl11.MastersValue.Clear();
                        this.userControl11.Masters.Clear();


                        IFormatter formatter = new BinaryFormatter();
                        Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
                        Nodes = (ArrayList)formatter.Deserialize(stream);
                        stream.Close();

                        this.treeView1.Nodes.Clear();
                        //				this.treeView1.Nodes.Add((pNode)Nodes[0]);

                        //	Nodes[0] = userControl11;
                        int count = (int)Nodes[0];
                        int i = 1;
                        while (i < Nodes.Count)
                        {
                            userControl11.Masters.Add((string)Nodes[i]);
                            userControl11.MastersValue.Add((pNode)Nodes[++i]);
                            i++;
                        }
                        userControl11.index = 0;
                        this.treeView1.Nodes.Add((pNode)userControl11.MastersValue[userControl11.index]);
                        flag_file = true;
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

        /// <summary>
        /// Cuts a node only if node is not master node ie... has no parent node
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuCutNode_Click(object sender, System.EventArgs e)
        {
            // get
            try
            {

                this.getNode = (pNode)((pNode)treeView1.SelectedNode).Clone();
                if (treeView1.SelectedNode.Parent != null)
                {
                    this.menuItem21.Enabled = true;
                    this.menuItem31.Enabled = true;
                    this.nodeIndex = treeView1.SelectedNode.Index;
                    if (this.treeView1.SelectedNode.Parent != null)
                    {
                        this.treeView1.SelectedNode.Remove();
                    }
                    this.statusBar1.Text = "CUT Node Mode";
                    this.mode = nodeMode.cut;
                }
                else
                    MessageBox.Show("Cutting master node not allowed.", "Operation Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }
        private void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

            filename = this.saveFileDialog1.FileName;
            if (filename != null)
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(this.saveFileDialog1.FileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                formatter.Serialize(stream, Nodes);
                this.filename = this.saveFileDialog1.FileName;

                // use rm manager to set file name
                rm.SavePathInRegistry(pWordSettings.Default.version, filename);

                stream.Close();
                String path = rm.AutoSavePathFromRegistry(pWordSettings.Default.version.ToString());
                if (this.saveFileDialog1.FileName != path)
                {
                    // save the path to the registry
                    rm.SavePathInRegistry(pWordSettings.Default.version, this.saveFileDialog1.FileName);
                }
            }
            flag_file = true;
        }

        private void saveFileDialogHTML_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string filenameHTML = this.saveFileDialogHTML.FileName;
            if (filenameHTML != null)
            {
                StreamWriter swFromFile = new StreamWriter(filenameHTML);

                swFromFile.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                for (int i = 0; i < xml.Count; i++)
                {
                    swFromFile.Write(xml[i]);
                    //						formatter.Serialize(stream, xml[i].ToString());
                }
                swFromFile.Flush();
                swFromFile.Close();
            }
        }


        private void menuItem35_Click(object sender, System.EventArgs e)
        {
        
            try
            {
                this.exportMode = ExportMode.pNode;  // what am I exporting?  A pNode
                xml.Clear();  // clear out contents first.

                //this.xmlNode = (pNode)treeView1.SelectedNode;  // xmlNode is what is being exported to xml
                this.xmlIndex = treeView1.SelectedNode.Index; // xmlIndex is the SelectedNodes index
                this.menuItem21.Enabled = true; // MenuItem21 is enabled... Todo: rename menuItem21
                this.menuItem31.Enabled = true;  // MneuItem21 is enabled... Todo: rename menuItem31
                this.nodeIndex = treeView1.SelectedNode.Index; // nodeInex is now equal to xmlIndex?
                this.statusBar1.Text = "Export Node XML Mode";
                //CallRecursive(xmlNode);  // disabled CallRecursive here... need to fix Call recursive
                // ToDo: fix CallRecursive(xmlNode)
                xmlNode = null;
                xmlNode = (pNode)treeView1.SelectedNode;
                CallRecursive(xmlNode);  // treeview1 is a pView
                this.saveFileDialogHTML.FileName = filenameHTML;
                this.saveFileDialogHTML.Title = "Save the NODE to XML/HTML";
                this.saveFileDialogHTML.ShowDialog();
                this.filenameHTML = this.saveFileDialogHTML.FileName;

                if (this.filenameHTML != null)
                {

                    //					IFormatter formatter = new BinaryFormatter();
                    //					Stream stream = new FileStream(this.saveFileDialog1.FileName,FileMode.Create,FileAccess.Write,FileShare.ReadWrite);
                    //					for (int i=0; i<xml.Count;i++)
                    //					{
                    //						formatter.Serialize(stream, xml[i].ToString());
                    //					}
                    //					this.filename = this.saveFileDialog1.FileName;
                    //					stream.Close();
                    //					FileStream fs = new FileStream(filename,
                    //						FileMode.CreateNew, FileAccess.Write, FileShare.None);

                    //StreamWriter swFromFile = new StreamWriter(filenameHTML);

                    // may not have to do this with XmlDocument
                    //swFromFile.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>"); 
                    //for (int i=0; i<xml.Count;i++)					
                    //{
                    //	swFromFile.Write(xml[i]);
                    //	//						formatter.Serialize(stream, xml[i].ToString());
                    //}
                    //swFromFile.Flush();
                    //swFromFile.Close();
                    if (xdoc != null)
                    {
                        xdoc.Save(filenameHTML);
                        xdoc.RemoveAll();
                        xdoc = null;
                    }
                    else
                    {
                        MessageBox.Show("The export was not able to save b/c it was empty.");
                    }
                }

                this.toolBarView.Enabled = true;
            }
            catch (Exception f)
            {
                MessageBox.Show("You had an error while exporting to XML. " + f.Message, "SAVE ERROR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }

        }

        private void txtName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Return)
            {
                this.txtObject.Focus();
                e.Handled = true;
            }
        }

        private void menuItem36_Click(object sender, System.EventArgs e)
        {

        }

        private void cmTree_Popup(object sender, EventArgs e)
        {

        }


        // Notify Icon2 is is the one that goes on the task bar not the notification area
        private void notifyIcon2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Notify icon mouse double click
            Debug.WriteLine("Notify icon mouse double click.");
        }

        private void notifyIcon2_DoubleClick(object sender, EventArgs e)
        {
            // comment this doesn't really work.  When you click on this icon on the 
            // windows bar task bar, it basically does nothing when you double click it
            // notify icon double click
            // this moves it into the notification area.  Does exactly what the 
            // autohide + minimize does
            this.autoHide_flag = true;
            this.statusBar1.Text = "AutoHide Active";
            this.toolBarTac.ImageIndex = 0;
            this.actHook.Start();
            this.VIS = false;
            this.Visible = false;
            notifyIcon2.Visible = false;
            notifyIcon1.Visible = true;
        }

        private void notifyIcon2_MouseClick(object sender, MouseEventArgs e)
        {
            // notify icon mouse click
            // this event does nothing.  All the logic for controlling a window opening back
            // up appears to be coming from the windows taskbar and not this event

        }

        private void menuItemAttribute_Click(object sender, EventArgs e)
        {
            // add an attribute to a node
            Debug.WriteLine(e.ToString());
        }

        private void menuItemAttributeAdd_Click(object sender, EventArgs e)
        {
            mode = nodeMode.addAttributeTo;
            try
            {
                lblName.Text = "Attribute:";
                lblValue.Text = "Value:";
                this.modeIndex = treeView1.SelectedNode.Index;
                //				this.txtName.Text = treeView1.SelectedNode.Nodes[modeIndex].Text;
                //				this.txtObject.Text = treeView1.SelectedNode.Nodes[modeIndex].Text;
                this.tmpNode = (pNode)treeView1.SelectedNode;
                this.statusBar1.Text = "Add Attribute to Node";
                if (this.chkClear.CheckState == CheckState.Checked)
                {
                    this.txtName.Clear();
                    this.txtObject.Clear();
                    this.txtName.Focus();
                }
                else if (this.chkClear.CheckState == CheckState.Indeterminate)
                {
                    this.txtObject.Clear();
                    this.txtObject.Focus();
                }
                else
                {
                    this.btnAdd.Focus();
                }
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }


        private void menuItemNamespaceAddPrefix_Click(object sender, EventArgs e)
        {
            mode = nodeMode.addNamespacePrefix;
            try
            {
                lblName.Text = "Prefix:";
                lblValue.Text = "URI:";
                this.modeIndex = treeView1.SelectedNode.Index;
                //				this.txtName.Text = treeView1.SelectedNode.Nodes[modeIndex].Text;
                //				this.txtObject.Text = treeView1.SelectedNode.Nodes[modeIndex].Text;
                this.tmpNode = (pNode)treeView1.SelectedNode;
                this.statusBar1.Text = "Add Prefix to Node";
                if (this.chkClear.CheckState == CheckState.Checked)
                {
                    this.txtName.Clear();
                    this.txtObject.Clear();
                    this.txtName.Focus();
                }
                else if (this.chkClear.CheckState == CheckState.Indeterminate)
                {
                    this.txtObject.Clear();
                    this.txtObject.Focus();
                }
                else
                {
                    this.btnAdd.Focus();
                }
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }

        private void menuItemNamespaceAddSuffix_Click(object sender, EventArgs e)
        {
            mode = nodeMode.addNamespaceSuffix;
            try
            {
                lblName.Text = "Suffix:";
                lblValue.Text = "URI:";
                this.modeIndex = treeView1.SelectedNode.Index;
                //				this.txtName.Text = treeView1.SelectedNode.Nodes[modeIndex].Text;
                //				this.txtObject.Text = treeView1.SelectedNode.Nodes[modeIndex].Text;
                this.tmpNode = (pNode)treeView1.SelectedNode;
                this.statusBar1.Text = "Add Suffix to Node";
                if (this.chkClear.CheckState == CheckState.Checked)
                {
                    this.txtName.Clear();
                    this.txtObject.Clear();
                    this.txtName.Focus();
                }
                else if (this.chkClear.CheckState == CheckState.Indeterminate)
                {
                    this.txtObject.Clear();
                    this.txtObject.Focus();
                }
                else
                {
                    this.btnAdd.Focus();
                }
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }

        private void menuItemMathSum_Click(object sender, EventArgs e)
        {
            mode = nodeMode.sum;
            try
            {
                lblName.Text = "Name:";
                lblValue.Text = "Value:";
                this.modeIndex = treeView1.SelectedNode.Index;
                //				this.txtName.Text = treeView1.SelectedNode.Nodes[modeIndex].Text;
                //				this.txtObject.Text = treeView1.SelectedNode.Nodes[modeIndex].Text;
                Sum sum = new Sum();
                tmpNode = (pNode)treeView1.SelectedNode;
                tmpNode.AddOperation(new Sum(Resource1.Sum));
                treeView1.SelectedNode = tmpNode;
                this.statusBar1.Text = "Summation";
                this.txtObject.Text = (String)treeView1.SelectedNode.Tag;

                this.txtName.Focus();
                autosave();  // may need to hook up an event to save when ever new nodes are added or removed???
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }

        private void menuItemMathMultiple_Click(object sender, EventArgs e)
        {
            mode = nodeMode.multiply;
            try
            {
                lblName.Text = "Name:";
                lblValue.Text = "Value:";
                this.modeIndex = treeView1.SelectedNode.Index;
                //				this.txtName.Text = treeView1.SelectedNode.Nodes[modeIndex].Text;
                //				this.txtObject.Text = treeView1.SelectedNode.Nodes[modeIndex].Text;
                tmpNode = (pNode)treeView1.SelectedNode;
                tmpNode.AddOperation(new Multiply(Resource1.Multiplication));
                treeView1.SelectedNode = tmpNode;
                this.statusBar1.Text = "Mutliple";
                this.txtObject.Text = (String)treeView1.SelectedNode.Tag;

                this.txtName.Focus();
                autosave();  // may need to hook up an event to save when ever new nodes are added or removed???

            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }

        private void menuItemMathDivide_Click(object sender, EventArgs e)
        {
            mode = nodeMode.divide;
            try
            {
                lblName.Text = "Name:";
                lblValue.Text = "Value:";
                this.modeIndex = treeView1.SelectedNode.Index;
                //				this.txtName.Text = treeView1.SelectedNode.Nodes[modeIndex].Text;
                //				this.txtObject.Text = treeView1.SelectedNode.Nodes[modeIndex].Text;
                tmpNode = (pNode)treeView1.SelectedNode;
                tmpNode.AddOperation(new Divide(Resource1.Division));
                treeView1.SelectedNode = tmpNode;
                this.statusBar1.Text = "Divide";
                this.txtObject.Text = (String)treeView1.SelectedNode.Tag;
                this.txtName.Focus();
                autosave();  // may need to hook up an event to save when ever new nodes are added or removed???

            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }

        private void menuItemMathSubtract_Click(object sender, EventArgs e)
        {
            mode = nodeMode.divide;
            try
            {
                lblName.Text = "Name:";
                lblValue.Text = "Value:";
                this.modeIndex = treeView1.SelectedNode.Index;
                //				this.txtName.Text = treeView1.SelectedNode.Nodes[modeIndex].Text;
                //				this.txtObject.Text = treeView1.SelectedNode.Nodes[modeIndex].Text;
                tmpNode = (pNode)treeView1.SelectedNode;
                tmpNode.AddOperation(new Subtract(Resource1.Subtraction));
                treeView1.SelectedNode = tmpNode;
                this.statusBar1.Text = "Subtract";
                this.txtObject.Text = (String)treeView1.SelectedNode.Tag;
                this.txtName.Focus();
                autosave();  // may need to hook up an event to save when ever new nodes are added or removed???

            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }

        private void menuItemOperationsClear_Click(object sender, EventArgs e)
        {
            ((pNode)treeView1.SelectedNode).ClearOperations();
        }

        private void menuItemViewErrors_Click(object sender, EventArgs e)
        {
            // 
            lblName.Text = "Operations:";
            lblValue.Text = "Error Info:";

            txtName.Text = ((pNode)treeView1.SelectedNode).ListOperations();
            txtObject.Text = ((pNode)treeView1.SelectedNode).ErrorString;
        }

        private void menuItemMathTrigSign_Click(object sender, EventArgs e)
        {
            mode = nodeMode.trig;
            try
            {
                lblName.Text = "Name:";
                lblValue.Text = "Value:";
                this.modeIndex = treeView1.SelectedNode.Index;
                //				this.txtName.Text = treeView1.SelectedNode.Nodes[modeIndex].Text;
                //				this.txtObject.Text = treeView1.SelectedNode.Nodes[modeIndex].Text;
                tmpNode = (pNode)treeView1.SelectedNode;
                tmpNode.AddOperation(new Sin(Resource1.Sin));  // add new sin operation ... trying to make this a plug in.
                treeView1.SelectedNode = tmpNode;
                this.statusBar1.Text = "Sin";
                this.txtObject.Text = (String)treeView1.SelectedNode.Tag;
                this.txtName.Focus();
                autosave();  // may need to hook up an event to save when ever new nodes are added or removed???

            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }

        private void menuItemMathTrigCos_Click(object sender, EventArgs e)
        {
            mode = nodeMode.trig;
            try
            {
                lblName.Text = "Name:";
                lblValue.Text = "Value:";
                this.modeIndex = treeView1.SelectedNode.Index;
                //				this.txtName.Text = treeView1.SelectedNode.Nodes[modeIndex].Text;
                //				this.txtObject.Text = treeView1.SelectedNode.Nodes[modeIndex].Text;
                tmpNode = (pNode)treeView1.SelectedNode;
                tmpNode.AddOperation(new Cos(Resource1.Cos));  // add new sin operation ... trying to make this a plug in.
                treeView1.SelectedNode = tmpNode;
                this.statusBar1.Text = "Cos";
                this.txtObject.Text = (String)treeView1.SelectedNode.Tag;
                this.txtName.Focus();
                autosave();  // may need to hook up an event to save when ever new nodes are added or removed???

            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }

        private void menuItemMathTrigTan_Click(object sender, EventArgs e)
        {
            mode = nodeMode.trig;
            try
            {
                lblName.Text = "Name:";
                lblValue.Text = "Value:";
                this.modeIndex = treeView1.SelectedNode.Index;
                //				this.txtName.Text = treeView1.SelectedNode.Nodes[modeIndex].Text;
                //				this.txtObject.Text = treeView1.SelectedNode.Nodes[modeIndex].Text;
                tmpNode = (pNode)treeView1.SelectedNode;
                tmpNode.AddOperation(new Tan(Resource1.Tan));  // add new sin operation ... trying to make this a plug in.
                treeView1.SelectedNode = tmpNode;
                this.statusBar1.Text = "Tan";
                this.txtObject.Text = (String)treeView1.SelectedNode.Tag;
                this.txtName.Focus();
                autosave();  // may need to hook up an event to save when ever new nodes are added or removed???

            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);
            }
        }

        private void menuItemFind_Click(object sender, EventArgs e)
        {
            mode = nodeMode.find;
            try
            {
                lblName.Text = "Find Name:";
                lblValue.Text = "Find Value:";
                this.modeIndex = treeView1.SelectedNode.Index;
                tmpNode = (pNode)treeView1.SelectedNode;
            }
            catch (Exception ex)
            {

            }
        }

        private void menuItem85_Click(object sender, EventArgs e)
        {

        }

        private void mnuImportNodeXML_Click(object sender, EventArgs e)
        {
            
            importMode = ImportMode.treexml; // what am I exporting?  XML from previous exportNode
            // I'm importing into a node that i selected using import node treexml 

            xml.Clear();  // clear out contents first.

            //this.xmlNode = (pNode)treeView1.SelectedNode;  // xmlNode is what is being exported to xml
            this.xmlIndex = treeView1.SelectedNode.Index; // xmlIndex is the SelectedNodes index
            this.menuItem21.Enabled = true; // MenuItem21 is enabled... Todo: rename menuItem21
            this.menuItem31.Enabled = true;  // MneuItem21 is enabled... Todo: rename menuItem31
            this.nodeIndex = treeView1.SelectedNode.Index; // nodeInex is now equal to xmlIndex?
            this.statusBar1.Text = "Import Node XML Mode";
            //CallRecursive(xmlNode);  // disabled CallRecursive here... need to fix Call recursive
            // ToDo: fix CallRecursive(xmlNode)
            tmpNode = (pNode)treeView1.SelectedNode;
            this.openFileDialog2.ShowDialog();
            filenameHTML = this.openFileDialog2.FileName;
            if ((filenameHTML == null) || (filenameHTML == ""))
            {
                return;
            }
            else
            {
                try
                {
                    using (WebClient client = new WebClient())
                    using (Stream stream = client.OpenRead(filenameHTML))
                    {
                  //      byte[] buf = new byte[stream.Length];
                  //      stream.Read(buf, 0, (int)stream.Length);
                        xdoc = new XmlDocument();
                        try
                        {
                            xdoc.Load(stream);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    pNode masterNode = (pNode)treeView1.Nodes[0];
                    // now that we have the xdoc... now we need to stick it in the getNode
                    //                pNode pn = new pNode(xdoc.ChildNodes[1].Name);
                    //                pn.AddAttribute(xdoc.ch
                    //                getNode.Nodes.Add(
                    pNode pn = (pNode)treeView1.SelectedNode;
                    if (treeView1.SelectedNode.Tag == null)
                        treeView1.SelectedNode.Tag = "";
                    pn.Tag = treeView1.SelectedNode.Tag;
                    AddChildNodes(xdoc.ChildNodes, ref pn);
                    treeView1.SelectedNode = pn;
                    //                .Nodes.Add(pn);
                    //treeView1.SelectedNode.Nodes.Add(aNode);
                    // after adding the new node, be sure the index is updated as well... this is not necessary
                    userControl11.MastersValue[userControl11.index] = masterNode;

                    // Change from Add Dialog to local members for adding name and value

                    // check box
                    if (this.chkClear.CheckState == CheckState.Checked)
                    {
                        this.txtName.Clear();
                        this.txtObject.Clear();
                        this.txtName.Focus();
                    }
                    else if (this.chkClear.CheckState == CheckState.Indeterminate)
                    {
                        this.txtObject.Clear();
                        this.txtObject.Focus();
                    }
                    else
                    {
                        this.btnAdd.Focus();
                    }

                    if (flag_file == true)
                    {
                        //autosave();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred during xml document load.");
                }
            }
        }

        private void AddChildNodes(XmlNodeList children, ref pNode pn)
        {


                treeView1.SelectedNode = getNode;
                //pNode aNode = new pNode();
                //aNode = new pNode(this.txtName.Text);
                //TreePics apic = new TreePics("aNode", img.GroupUp, img.GroupDown);
                //aNode.Tag = this.txtObject.Text;
                //aNode.Text = this.txtName.Text;
                //if (tmpNode.Namespace != null)
                //{
                //    aNode.Namespace = tmpNode.Namespace;  // trickle down namespaces
                //}

                treeView1.SelectedNode = tmpNode;
                foreach (XmlNode xn in children)
                {
                    //AddChildNodes(xn.ChildNodes, ref pn);
                    if (!( xn.Name == "xml"))   // any tag except xml ... add to selected node
                    {
                        pNode aNode = new pNode();
                        if (xn.NodeType == XmlNodeType.Text)
                        {
                            // just getting the text
                            // this was already checked in the element
                            // skip for now
                            continue;
                                
                            // pn.Tag = xn.Value;
                        }
                        else if (xn.NodeType == XmlNodeType.Element)
                        {
                            aNode.Text = xn.LocalName;
                            aNode.Name = xn.LocalName;
                            if ((xn.Prefix != null) && (xn.Prefix != ""))
                            {
                                aNode.Namespace = new NameSpace();
                                // aNode.Namespace.Prefix = xn.Prefix;
                                aNode.Namespace.URI_PREFIX = xn.NamespaceURI;
                                if (xn.Attributes.Count > 0)
                                {
                                    foreach (XmlAttribute attr in xn.Attributes)
                                    {
                                        if (attr.Prefix == "xmlns") // its a namespace declaration
                                        {
                                            aNode.Namespace = new NameSpace();
                                            aNode.Namespace.Prefix = attr.LocalName;
                                            aNode.Namespace.URI_PREFIX = attr.Value;
                                        }
                                        else
                                        {
                                            aNode.AddAttribute(attr.LocalName, attr.Value);
                                        }
                                    }
                                }

                            }
                            else
                            {

                                if (xn.Attributes.Count > 0)
                                {
                                    foreach (XmlAttribute attr in xn.Attributes)
                                    {
                                            aNode.AddAttribute(attr.LocalName, attr.Value);
                                    }
                                }
                            }

                            // check first child to see if text
                            if ((xn.FirstChild != null) && (xn.FirstChild.NodeType == XmlNodeType.Text))
                            {
                                aNode.Tag = xn.InnerText;
                            }
                        }
                        else if (xn.NodeType == XmlNodeType.Attribute)
                        {
                            continue;
                            //aNode.AddAttribute(xn.Name, xn.Value);
                        }
                        else if (xn.NodeType == XmlNodeType.Entity)
                        {
                            aNode.Name = xn.Name;
                            aNode.Text = xn.Name;
                        }
                        else
                        {
                            MessageBox.Show("TypeOf: " + xn.NodeType.ToString());

                        }

                        AddChildNodes(xn.ChildNodes, ref aNode);
                        pn.Nodes.Add(aNode);
                    }
                }
        }

        private void openFileDialog2_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // file was accepted
            // open dialog box to import from

            try
            {
                filenameHTML = ((FileDialog)sender).FileName;
                if (filenameHTML != null)
                {

                    //					IFormatter formatter = new BinaryFormatter();
                    //					Stream stream = new FileStream(this.saveFileDialog1.FileName,FileMode.Create,FileAccess.Write,FileShare.ReadWrite);
                    //					for (int i=0; i<xml.Count;i++)
                    //					{
                    //						formatter.Serialize(stream, xml[i].ToString());
                    //					}
                    //					this.filename = this.saveFileDialog1.FileName;
                    //					stream.Close();
                    //					FileStream fs = new FileStream(filename,
                    //						FileMode.CreateNew, FileAccess.Write, FileShare.None);

                    //StreamWriter swFromFile = new StreamWriter(filenameHTML);

                    // may not have to do this with XmlDocument
                    //swFromFile.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>"); 
                    //for (int i=0; i<xml.Count;i++)					
                    //{
                    //	swFromFile.Write(xml[i]);
                    //	//						formatter.Serialize(stream, xml[i].ToString());
                    //}
                    //swFromFile.Flush();
                    //swFromFile.Close();

                    // check xdoc for size

                }
                //System.thr
                this.toolBarView.Enabled = true;
            }
            catch (Exception f)
            {
                MessageBox.Show("You had an error while exporting to XML. " + f.Message, "SAVE ERROR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }

        private void menuItem85_Click_1(object sender, EventArgs e)
        {

        }

        private void menuItem88_Click(object sender, EventArgs e)
        {

        }
    }
}
