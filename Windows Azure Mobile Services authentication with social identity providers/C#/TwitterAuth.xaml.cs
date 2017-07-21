//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
//
//*********************************************************

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System;
using Microsoft.WindowsAzure.MobileServices;

namespace AzureMobileAuthentication
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TwitterAuth
    {
        // A pointer back to the main page.  This is needed if you want to call methods in MainPage such
        // as NotifyUser()
        MainPage rootPage = MainPage.Current;

        public TwitterAuth()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void DebugPrint(string trace)
        {
            TwitterDebugArea.Text += trace + "\r\n";
        }

        private void OutputPrint(string message)
        {
            TwitterOutputArea.Text = message;
        }

        /// <summary>
        /// Click handler of Twitter auth button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void buttonTwitterAuth_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MobileServiceUser user = await App.MobileService.LoginAsync(MobileServiceAuthenticationProvider.Twitter);
                this.OutputPrint(string.Format("You are now logged in - {0}", user.UserId));
            }
            catch (InvalidOperationException)
            {
                this.DebugPrint("An error occurred during login. Login Required.");
            }
        }
    }
}
