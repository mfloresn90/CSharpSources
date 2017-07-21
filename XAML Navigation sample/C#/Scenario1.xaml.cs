//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
//
//*********************************************************

using SDKTemplate;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Navigation
{

	// This Scenario shows the basic usage of Navigation in XAML. 
    public sealed partial class Scenario1
    {
        MainPage rootPage = MainPage.Current;

        public Scenario1()
        {
            this.InitializeComponent();
			//This handler will modify some UI elements when the page is resized
            rootPage.MainPageResized += this.MainPageResized;
        }
                               
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
			//These two methods will show info about the state of the navigation stacks
			//and also enable or disable buttons from the User Interface.
            ShowInfo();
            UpdateUI();
        }
  			
        private void NavigateBtnClick(object sender, RoutedEventArgs e)
        {
			//Navigate method will display SimplePage content inside the MyFrame element.
            MyFrame.Navigate(typeof(SimplePage));
            ShowInfo();
            UpdateUI();
        }

        private void GoBackButtonClick(object sender, RoutedEventArgs e)
        {
			//We should verify if there are pages in the navigation back stack 
			//before navigating to the previous page.
            if (this.MyFrame != null && MyFrame.CanGoBack)
            {
				//Using the GoBack method, the frame navigates to the previous page.
                MyFrame.GoBack();
            }
            ShowInfo();
            UpdateUI();
        }

        private void GoForwardButtonClick(object sender, RoutedEventArgs e)
        {
			//We should verify if there are pages in the navigation forward stack 
			//before navigating to the forward page.
            if (this.MyFrame != null && MyFrame.CanGoForward)
            {
				//Using the GoForward method, the frame navigates to the forward page.
                MyFrame.GoForward();
            }
            ShowInfo();
            UpdateUI();
        }

        private void GoHomeButtonClick(object sender, RoutedEventArgs e)
        {
            // Use the navigation frame to return to the topmost page
            if (this.MyFrame != null)
            {
                while (this.MyFrame.CanGoBack) this.MyFrame.GoBack();
            }
            ShowInfo();
            UpdateUI();
        }

        private void ClearStacksButtonClick(object sender, RoutedEventArgs e)
        {
			//We can clear the navigation stacks using the Clear method of each stack.
            MyFrame.ForwardStack.Clear();
            MyFrame.BackStack.Clear();
            ShowInfo();
            UpdateUI();
        }

        private void ShowInfo()
        {
            if (MyFrame != null)
            {
                BackStackText.Text = "\nEntries in the navigation Back Stack: #" + MyFrame.BackStack.Count.ToString();
                ForwardStackText.Text = "\nEntries in the navigation Forward Stack: #" + MyFrame.ForwardStack.Count.ToString();

                BackStackListView.Items.Clear();
                //Add the pages from the back stack
                foreach (PageStackEntry pageStackEntry in MyFrame.BackStack)
                {
                    BackStackListView.Items.Add(pageStackEntry.SourcePageType.ToString());
                }

                ForwardStackListView.Items.Clear();
                //Add the pages from the forward stack
                foreach (PageStackEntry pageStackEntry in MyFrame.ForwardStack)
                {
                    ForwardStackListView.Items.Add(pageStackEntry.SourcePageType.ToString());
                }
            }
        }

        private void UpdateUI()
        {
            GoHomeBtn.IsEnabled = MyFrame.CanGoBack; 
            GoForwardBtn.IsEnabled = MyFrame.CanGoForward;
            GoBackBtn.IsEnabled = MyFrame.CanGoBack;
            ClearStacksBtn.IsEnabled = (MyFrame.BackStack.Count > 0 || MyFrame.ForwardStack.Count > 0) ? true : false;
        }

        private void MainPageResized(object sender, MainPageSizeChangedEventArgs e)
        {
            if(Window.Current.Bounds.Width >= 1024)
            {
                ButtonsContainer.Orientation = Orientation.Horizontal;
            }
            else
            {
                ButtonsContainer.Orientation = Orientation.Vertical;
            }
        }
    }
}
