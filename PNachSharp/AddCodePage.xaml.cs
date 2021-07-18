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

namespace PNachSharp
{
    /// <summary>
    /// Interaction logic for AddCodePage.xaml
    /// </summary>
    public partial class AddCodePage
    {
        public AddCodePage()
        {
            InitializeComponent();
        }

        private void AddRawButton_OnClick(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine(((Grid)((ContentPresenter)VisualParent).Parent).Name);
        }
    }
}
