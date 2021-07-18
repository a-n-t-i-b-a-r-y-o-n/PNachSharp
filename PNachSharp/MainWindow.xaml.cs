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
    /// Code behind MainWindow
    /// </summary>
    public partial class MainWindow: Window
    {
        // Whether or not the LeftPane contains unsynchronized updates
        public static readonly DependencyProperty LeftPaneUpdatedProperty = 
            DependencyProperty.Register("LeftPaneUpdated", typeof(bool), typeof(MainWindow));

        public bool LeftPaneUpdated
        {
            get => (bool) GetValue(LeftPaneUpdatedProperty);
            set => SetValue(LeftPaneUpdatedProperty, value);
        }

        // Whether or not the RightPane contains unsynchronized updates
        public static readonly DependencyProperty RightPaneUpdatedProperty =
            DependencyProperty.Register("RightPaneUpdated", typeof(bool), typeof(MainWindow));

        public bool RightPaneUpdated
        {
            get => (bool) GetValue(RightPaneUpdatedProperty);
            set => SetValue(RightPaneUpdatedProperty, value);
        }

        // Currently-open PNachFile
        public static readonly DependencyProperty CurrentFileProperty =
            DependencyProperty.Register("CurrentFile", typeof(PNachFile), typeof(MainWindow));

        public PNachFile CurrentFile
        {
            get => (PNachFile) GetValue(CurrentFileProperty);
            set => SetValue(CurrentFileProperty, value);
        }

        public MainWindow()
        {
            InitializeComponent();
            CurrentFile = new PNachFile(string.Empty, string.Empty);
            CurrentFile.PropertyChanged += CurrentFile_PropertyChanged;
        }

        private void CurrentFile_PropertyChanged(object content, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RightPane.OutputBox.Text = CurrentFile.ToString();
        }
    }
}