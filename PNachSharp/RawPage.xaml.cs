﻿using System;
using System.Collections.Generic;
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
    /// Interaction logic for RawPage.xaml
    /// </summary>
    public partial class RawPage : ICodePage
    {
        public RawPage()
        {
            InitializeComponent();
        }

        public ICodePage.PageType GetPageType() => ICodePage.PageType.Raw;

        public string GetRawCode() => InputBox.Text.Trim();
    }
}
