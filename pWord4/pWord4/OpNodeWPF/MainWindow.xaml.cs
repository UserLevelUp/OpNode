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
            this.Height = SystemParameters.PrimaryScreenHeight;
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
            OpNode.Items.Add(item);
        }

        private void GridSplitter_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            // fix this

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