

namespace UWPCore.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using Windows.Foundation.Metadata;
    using Windows.UI.Core;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media.Animation;

    public class NavigationService
    {
        #region Private Fields

        private Dictionary<string, Type> dictionaryPages;
        private IMasterDetailPage framesMasterDetails;
        private Type LastDetail;
        private Type LastMaster;
        private Type MasterPage;
        private double size = 720;

        #endregion

        #region Constructors

        public NavigationService()
        {
            this.dictionaryPages = new Dictionary<string, Type>();
        }

        #endregion

        #region Public Methods

        public void ClearPages()
        {
            this.dictionaryPages = new Dictionary<string, Type>();
        }

        public void GoBack()
        {
            this.GoBackWindowsFrame();
        }

        public void NavigateTo(string pageKey)
        {
            var frame = (Frame)Window.Current.Content;
            if (this.dictionaryPages.ContainsKey(pageKey))
            {
                var typePage = this.dictionaryPages[pageKey];
                frame.Navigate(typePage);
                this.RegisterOrUnregisterButtons(frame.CanGoBack);
            }

            if (this.MasterPage != null && frame.CurrentSourcePageType != this.MasterPage)
            {
                frame.SizeChanged -= this.OnSizeChangedMasterDetailPage;
            }
        }

        public void NavigateToDetail(string pageKey)
        {
            if (this.dictionaryPages.ContainsKey(pageKey))
            {
                var typeDetailsPage = this.dictionaryPages[pageKey];
                this.LastDetail = typeDetailsPage;

                if (this.wideScreen)
                {
                    // Navigate  to details Frame
                    this.framesMasterDetails.DetailFrame.BackStack.Clear();
                    this.framesMasterDetails.DetailFrame.Navigate(typeDetailsPage, null, new SuppressNavigationTransitionInfo());
                }
                else
                {
                    // Navigate to master Frame
                    this.CurrentFrame.Navigate(this.LastDetail);
                }
            }
        }

        public void NavigateToMasterDetails(string pageKey)
        {
            this.InitializeMasterPage();

            if (this.dictionaryPages.ContainsKey(pageKey))
            {
                var master = this.dictionaryPages[pageKey];
                this.framesMasterDetails.MasterFrame.Navigate(master);
                this.LastMaster = master;
                this.LastDetail = null;
            }
        }

        public void RegisterFrames(IMasterDetailPage frames)
        {
            this.framesMasterDetails = frames;
        }

        public void RegisterMasterPage(Type masterPage, double withWideMaster)
        {
            this.MasterPage = masterPage;
            this.size = withWideMaster;
        }

        public void BackStackClear() {
            var frame = (Frame)Window.Current.Content;
            frame.BackStack.Clear();
        }

        public Type getSourcePage(string pageKey) {
          return this.dictionaryPages[pageKey];
        }

        public void RegisterPage(string key, Type page)
        {
            this.dictionaryPages.Add(key, page);
        }

        #endregion

        #region Private Methods

        private void GoBackWindowsFrame()
        {
            var frame = (Frame)Window.Current.Content;
            if (frame.CanGoBack)
            {
                frame.GoBack();
                this.RegisterOrUnregisterButtons(frame.CanGoBack);
            }

            if (frame.CurrentSourcePageType != this.MasterPage)
            {
                frame.SizeChanged -= this.OnSizeChangedMasterDetailPage;
            }
            else
            {
                this.LoadOldMasterDetailsPage();
            }
        }

        private void InitializeMasterPage()
        {
            var frame = (Frame)Window.Current.Content;
            if (frame.CurrentSourcePageType != this.MasterPage)
            {
                frame.Navigate(this.MasterPage);
                frame.SizeChanged += this.OnSizeChangedMasterDetailPage;
                this.RegisterOrUnregisterButtons(frame.CanGoBack);
            }
        }

        private void LoadOldMasterDetailsPage()
        {
            if (this.CurrentFrame.CurrentSourcePageType != this.MasterPage)
            {
                this.CurrentFrame.GoBack();
                if (this.CurrentFrame.CurrentSourcePageType == this.MasterPage)
                {
                    this.CurrentFrame.SizeChanged -= this.OnSizeChangedMasterDetailPage;
                    this.CurrentFrame.SizeChanged += this.OnSizeChangedMasterDetailPage;
                }
                Debug.WriteLine("[BackStackDepth Go back]" + this.CurrentFrame.BackStackDepth);
                Debug.WriteLine("[CurrentSourcePageType]" + this.CurrentFrame.CurrentSourcePageType);
            }

            if (this.framesMasterDetails.MasterFrame.CurrentSourcePageType != this.LastMaster)
            {
                this.framesMasterDetails.MasterFrame.Navigate(this.LastMaster, new SuppressNavigationTransitionInfo());
            }
            if (this.LastDetail != null && this.framesMasterDetails.DetailFrame.CurrentSourcePageType != this.LastDetail)
            {
                this.framesMasterDetails.DetailFrame.Navigate(this.LastDetail, new SuppressNavigationTransitionInfo());
            }
        }

        private void OnGoBackVirtualButton(object sender, BackRequestedEventArgs e)
        {
            if (!e.Handled)
            {
                e.Handled = true;
                this.GoBack();
            }
        }

        private void OnSizeChangedMasterDetailPage(object sender, SizeChangedEventArgs e)
        {
            if (this.wideScreen)
            {
                if (this.CurrentFrame.CurrentSourcePageType != this.MasterPage)
                {
                    this.LoadOldMasterDetailsPage();
                }
            }
            else
            {
                if (this.CurrentFrame.CurrentSourcePageType == this.MasterPage)
                {
                    if (this.LastDetail != null)
                    {
                        this.CurrentFrame.Navigate(this.LastDetail, null, new SuppressNavigationTransitionInfo());
                    }
                }
            }
        }

        private void RegisterOrUnregisterButtons(bool canGoBack)
        {
            if (canGoBack)
            {
                if (ApiInformation.IsTypePresent("Windows.UI.Core.SystemNavigationManager"))
                {
                    // Show UI in title bar if opted-in and in-app backstack is not empty.
                    SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
                    SystemNavigationManager.GetForCurrentView().BackRequested -= this.OnGoBackVirtualButton;
                    SystemNavigationManager.GetForCurrentView().BackRequested += this.OnGoBackVirtualButton;
                }
            }
            else
            {
                if (ApiInformation.IsTypePresent("Windows.UI.Core.SystemNavigationManager"))
                {
                    // Remove the UI from the title bar if in-app back stack is empty.
                    SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
                    SystemNavigationManager.GetForCurrentView().BackRequested -= this.OnGoBackVirtualButton;
                }
            }
        }

        #endregion

        private bool wideScreen => Window.Current.Bounds.Width > this.size;
        private Frame CurrentFrame => (Frame)Window.Current.Content;
    }
}
