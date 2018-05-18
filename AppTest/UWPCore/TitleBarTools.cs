

using Windows.Foundation.Metadata;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Media;

namespace UWPCore
{
   public static class TitleBarTools
    {
        #region  Public Static Methods

        public static void SetColor(SolidColorBrush background, SolidColorBrush foreground)
        {
            //PC customization
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationView"))
            {
                var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                if (titleBar != null)
                {
                    titleBar.ButtonBackgroundColor = background.Color;
                    titleBar.ButtonForegroundColor = foreground.Color;
                    titleBar.BackgroundColor = background.Color;
                    titleBar.ForegroundColor = foreground.Color;
                }
            }

            //Mobile customization
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                var statusBar = StatusBar.GetForCurrentView();
                if (statusBar != null)
                {
                    statusBar.BackgroundOpacity = 1;
                    statusBar.BackgroundColor = background.Color;
                    statusBar.ForegroundColor = foreground.Color;
                }
            }
        }

        #endregion
    }
}
