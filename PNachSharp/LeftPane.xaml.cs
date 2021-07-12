using ModernWpf.Controls;
using System;
using System.Linq;
using System.Windows.Controls;

namespace PNachSharp
{
    public partial class LeftPane : UserControl
    {
        public LeftPane()
        {
            InitializeComponent();

            // Navigate the view to the first item in the list
            NavView.SelectedItem = NavView.MenuItems.OfType<NavigationViewItem>().First();
            NavigateFrame(NavView.SelectedItem);
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                //Navigate(typeof(SettingsPage));
            }
            else
            {
                NavigateFrame(args.InvokedItemContainer);
            }
        }

        private void NavigateFrame(object item)
        {
            if (item is NavigationViewItem menuItem)
            {
                if(menuItem.Tag.ToString() == "ADD")
                {
                    // Prompt the user to add a new tab
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
    }
}