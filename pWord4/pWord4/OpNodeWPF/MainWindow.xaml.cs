using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OpNodeWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Top = 0;
            this.Left = SystemParameters.PrimaryScreenWidth - this.Width;
            // fix the height so its the PrimaryScreenHeight minus the height of the window toolbar
            this.Height = SystemParameters.PrimaryScreenHeight - 40;

        }

        // create a method that detects when the windows toolbar height changes
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // fix the height so its the PrimaryScreenHeight minus the height of the window toolbar
            // explain why 40 is used
            this.Height = SystemParameters.PrimaryScreenHeight - 40;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = new TreeViewItem();
            item.Header = "New Item";
            // set the value to some test value
            var count = OpNode.Items.Count;
            item.Tag = $"Test Value{count}";
            item.MouseEnter += OpNode_TreeViewItem_MouseMove;
            var child = new TreeViewItem { Header = "Test Name", Tag = $"Test Value{count}" };
            child.MouseEnter += OpNode_TreeViewItem_MouseMove;
            item.Items.Add(child);
            OpNode.Items.Add(item);

        }

        // Hover over a TreeViewItem and show the value in teh TextBox
        private void OpNode_TreeViewItem_MouseMove(object sender, MouseEventArgs e)
        {
            // get the item that is being hovered over
            TreeViewItem item = (TreeViewItem)sender;
            // get the value of the item
            string value = item.Tag.ToString();
            // set the value of the textbox to the value of the item
            SummaryText.Text = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridSplitter_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            // This should all for consistent heigh with no strange behaviors between the two heights 500 and 45
            var delta = BottomDockPanel.Height - e.VerticalChange;
            if (BottomDockPanel.Height + delta <= 500 && BottomDockPanel.Height + delta >= 45)
            {
                    BottomDockPanel.Height = delta;
            }
            if (BottomDockPanel.Height < 45)
            {
                BottomDockPanel.Height = 45;
            }
            if (BottomDockPanel.Height > 500)
            {
                BottomDockPanel.Height = 300;
            }


            e.Handled = true;
        }
    }
}