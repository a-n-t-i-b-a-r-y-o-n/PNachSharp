using System.Windows;
using System.Windows.Controls;

namespace PNachSharp
{
    public partial class RightPane : UserControl
    {
        public RightPane()
        {
            InitializeComponent();
        }

        private void CopyButton_OnClick(object sender, RoutedEventArgs e)
        {
            OutputBox.Text = new PNachFile("Game Title!", "CRC----").ToString();
        }
    }
}