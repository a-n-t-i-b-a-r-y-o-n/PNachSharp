using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Page = ModernWpf.Controls.Page;

namespace PNachSharp
{
    /// <summary>
    /// Interaction logic for EncodedPage.xaml
    /// </summary>
    public partial class EncodedPage : ICodePage
    {
        public EncodedPage()
        {
            InitializeComponent();
        }

        public ICodePage.PageType GetPageType() => ICodePage.PageType.Encoded;
        public string GetRawCode()
        {
            // TODO: Implement decoding/decryption with Omniconvert port
            Trace.WriteLine("[DECRYPTION NOT YET IMPLEMENTED]");
            return string.Empty;
        }

    }
}
