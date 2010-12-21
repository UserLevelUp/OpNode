using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace myPword
{
    public class pView : TreeView
    {
        public event DrawTreeNodeEventHandler DrawNode;

        // Create a Font object for the node tags.
        Font tagFont = new Font("Helvetica", 8, FontStyle.Bold);
        public pView()
        {
            InitializeComponent();
        }

        public void InitializeComponent()
        {
            //this.CheckBoxes = true;
            base.DrawMode = TreeViewDrawMode.OwnerDrawAll;
            base.DrawNode += new DrawTreeNodeEventHandler(pView_DrawNode);
        }



        void parentNodeReference(DrawTreeNodeEventArgs e, Rectangle b)
        {
            if (e.Node.Text == "viper")
            {
                Debug.WriteLine("Viper caught.");
            }
            DrawTreeNodeEventArgs eParent = new DrawTreeNodeEventArgs(e.Graphics, e.Node.Parent, e.Bounds, e.State);
            Rectangle b2 = new Rectangle(b.Left - 19, b.Top, 16, 16);
            if (e.Node.NextNode != null)
            {
                try
                {

                    parentNodeReference(eParent, b2);
                    e.Graphics.DrawIcon(Resource1.Empty, b);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            }
            else if (e.Node.Parent != null)
            {
                parentNodeReference(eParent, b2);
            }
            

        }

        void pView_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if (e.Node.IsVisible)
            {
                //if (e.Node.Parent == null)
                //{
                //    e.Graphics.Clear(Color.White);
                //}
                //else
                //{

                //}
                Rectangle r = NodeBounds(e.Node);
                Rectangle b = new Rectangle(r.Left - 38, r.Top, 16, 16);
                Rectangle b2 = new Rectangle(r.Left - 19, r.Top, 16, 16);

                if (e.Node.Parent != null)
                {
                    // start a loop
                    DrawTreeNodeEventArgs eParent = new DrawTreeNodeEventArgs(e.Graphics, e.Node.Parent, e.Bounds, e.State);
                    parentNodeReference(eParent, b);
                    if (e.Node.Index == 0)
                    {
                        e.Graphics.DrawIcon(Resource1.ExpandedLink, b);
                    }

                }
                if (e.Node.Nodes.Count > 0)
                {
                    if (e.Node.IsExpanded)
                    {
                        e.Graphics.DrawIcon(Resource1.Minus, b);
                    }
                    else
                    {
                        e.Graphics.DrawIcon(Resource1.Plus, b);
                    }
                }
                //if ((e.Node.Nodes != null) && (e.Node.Nodes.Count > 0))
                //{
                //    if (e.Node.IsExpanded)
                //    {
                //        if (e.Node.Parent == null)
                //        {
                //            e.Graphics.DrawIcon(Resource1.Minus, b);
                //            getExpandedLink(e, b);

                //        }
                //        else if (e.Node.Parent != null)
                //        {
                //            e.Graphics.DrawIcon(Resource1.SubMinus, b);
                //            getExpandedLink(e, b);

                //        }

                //    }
                //    else
                //    {

                //        // draw a plus sign
                //        getExpandedLink(e, b);
                //        e.Graphics.DrawIcon(Resource1.Plus, b);
                //    }
                //}
                //else
                //{
                //    Debug.WriteLine("Do nothing.");
                //    getExpandedLink(e, b2);
                //    //e.Graphics.DrawIcon(Resource1.ExpandedLink, b);

                //}
                e.Graphics.DrawImage(this.ImageList.Images[0], b2);
                // Draw the background and node text for a selected node.
                if ((e.State & TreeNodeStates.Selected) != 0)
                {
                    e.Graphics.DrawImage(this.ImageList.Images[1], b2);
                    // Draw the background of the selected node. The NodeBounds
                    // method makes the highlight rectangle large enough to
                    // include the text of a node tag, if one is present.
                    SolidBrush sbBackground = new SolidBrush(pWordSettings.Default.SelectedNodeBackground);
                    e.Graphics.FillRectangle(sbBackground, r );
                    // Retrieve the node font. If the node font has not been set,
                    // use the TreeView font.
                    Font nodeFont = e.Node.NodeFont;
                    if (nodeFont == null) nodeFont = ((TreeView)sender).Font;

                    SolidBrush sbForeground = new SolidBrush(pWordSettings.Default.SelectedNodeForeground);
                    // Draw the node text.
                    e.Graphics.DrawString(e.Node.Text, nodeFont, sbForeground,
                        Rectangle.Inflate(r, -5, 0));
                }

                // Use the default background and node text.
                else
                {
//                    e.DrawDefault = true;
                    // Draw the background of the selected node. The NodeBounds
                    // method makes the highlight rectangle large enough to
                    // include the text of a node tag, if one is present.
                    e.Graphics.FillRectangle(Brushes.White, NodeBounds(e.Node));

                    // Retrieve the node font. If the node font has not been set,
                    // use the TreeView font.
                    Font nodeFont = e.Node.NodeFont;
                    if (nodeFont == null) nodeFont = ((TreeView)sender).Font;

                    // Draw the node text.
                    e.Graphics.DrawString(e.Node.Text, nodeFont, Brushes.Black,
                        Rectangle.Inflate(NodeBounds(e.Node), 2, 0));
                }

                // If a node tag is present, draw its string representation 
                // to the right of the label text.
                if (e.Node.Tag != null)
                {
                    e.Graphics.DrawString(e.Node.Tag.ToString(), tagFont,
                        Brushes.Red, e.Bounds.Right + 30, e.Bounds.Top);
                }

                // If the node has focus, draw the focus rectangle large, making
                // it large enough to include the text of the node tag, if present.
                if ((e.State & TreeNodeStates.Focused) != 0)
                {
                    using (Pen focusPen = new Pen(Color.Black))
                    {
                        focusPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                        Rectangle focusBounds = NodeBounds(e.Node);
                        focusBounds.Size = new Size(focusBounds.Width - 1,
                        focusBounds.Height - 1);
                        e.Graphics.DrawRectangle(focusPen, focusBounds);
                    }
                }

            }

        }

        private Rectangle NodeBounds(TreeNode treeNode)
        {
            // Set the return value to the normal node bounds.
            Rectangle bounds = treeNode.Bounds;
            if (treeNode.Text != null)
            {
                // Retrieve a Graphics object from the TreeView handle
                // and use it to calculate the display width of the tag.
                Graphics g = this.CreateGraphics();
                int textWidth = (int)g.MeasureString
                    (treeNode.Text.ToString(), tagFont).Width + 6;

                // Adjust the node bounds using the calculated value.
                bounds.Offset(textWidth / 2, 0);
                bounds = Rectangle.Inflate(bounds, textWidth / 2, 0);
                g.Dispose();
            }

            return bounds;

        }

        protected override void OnPaint(PaintEventArgs e)
        {

            //Graphics g = e.Graphics;
            //Brush b = Brushes.Black;
            //Pen p = new Pen(b, 2.3f);
            //g.DrawLine(p, 0, 0, 300, 300);
            //base.OnPaint(e);
        }

        


    }
}
