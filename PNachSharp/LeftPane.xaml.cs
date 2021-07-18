using ModernWpf.Controls;
using ModernWpf.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PNachSharp
{
    public partial class LeftPane : UserControl
    {
        public ObservableCollection<ICodePage> CodePages = new ObservableCollection<ICodePage>();

        public LeftPane()
        {
            InitializeComponent();

            NavView.MenuItemsSource = CodePages;

            // Navigate the view to the first item in the list
            //NavView.SelectedItem = NavView.MenuItems.OfType<NavigationViewItem>().First();
            //NavigateFrame(NavView.SelectedItem);
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                //Navigate(typeof(SettingsPage));
            }
            else
            {
                Trace.WriteLine($"index: {sender.MenuItems.IndexOf(sender.SelectedItem)}\r\ntitle: {args.InvokedItem}\r\n\r\n");
                NavigateFrame(args.InvokedItemContainer);
            }
        }

        private void NavigateFrame(object item)
        {
            if (item is NavigationViewItem menuItem)
            {
                if(menuItem.Tag.ToString() == "ADD")
                {
                    ContentFrame.Navigate(typeof(AddCodePage));
                }
                else
                {
                    Type pageType = menuItem.Tag as Type;
                    if (ContentFrame.CurrentSourcePageType != pageType)
                    {
                        ContentFrame.Navigate(pageType);
                    }
                }
                
            }
        }

        private void GameTitleBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ((MainWindow) ((Grid) Parent).Parent).CurrentFile.Title = ((TextBox) sender).Text;
        }

        private void GameCRCBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ((MainWindow) ((Grid) Parent).Parent).CurrentFile.CRC = ((TextBox) sender).Text;
        }
    }
}