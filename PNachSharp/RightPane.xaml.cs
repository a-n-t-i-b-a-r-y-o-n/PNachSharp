using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Threading;
using System.Windows.Documents;
using WinRT;

namespace PNachSharp
{
    public partial class RightPane : UserControl
    {
        public RightPane()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Copy output PNach to user's clipboard
        /// </summary>
        /// <param name="sender">CopyButton</param>
        /// <param name="e">RoutedEvents</param>
        private void CopyButton_OnClick(object sender, RoutedEventArgs e) => 
            Clipboard.SetData(DataFormats.StringFormat, OutputBox.Text);

        private void RefreshButton_OnClick(object sender, RoutedEventArgs e)
        {
            OutputBox.Text = ((MainWindow) ((Grid)Parent).Parent).CurrentFile.ToString();
        }
    }
}