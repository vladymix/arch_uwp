// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ToastContent.cs" company="vlady-mix">
//    Fabricio Altamirano  2016
//  </copyright>
//  <summary>
//    The definition of  ToastContent.cs
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------

namespace UWPCore.Controls
{
    using Windows.UI;
    using Windows.UI.Core;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Controls.Primitives;
    using Windows.UI.Xaml.Input;
    using Windows.UI.Xaml.Media;

    /// <summary>
    ///     The toast content.
    /// </summary>
    internal class ToastContent
    {
        #region Private Static Fields

        /// <summary>
        ///     The accet color.
        /// </summary>
        private static SolidColorBrush accetColor = new SolidColorBrush(Colors.DodgerBlue);

        /// <summary>
        ///     The instance.
        /// </summary>
        private static ToastContent instance;

        #endregion

        #region Private Fields

        /// <summary>
        ///     The content.
        /// </summary>
        private readonly string content;

        /// <summary>
        ///     The is func.
        /// </summary>
        private readonly bool isFunc;

        /// <summary>
        ///     The grid toast func.
        /// </summary>
        private Border borderToast;

        /// <summary>
        ///     The grid content.
        /// </summary>
        private Grid gridContentPage;

        /// <summary>
        ///     The grid opacity.
        /// </summary>
        private Grid gridOpacity;

        /// <summary>
        ///     The grid toast.
        /// </summary>
        private Grid gridToast;

        /// <summary>
        ///     The popup.
        /// </summary>
        private Popup popup;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ToastContent" /> class.
        /// </summary>
        /// <param name="content">
        ///     The content.
        /// </param>
        /// <param name="isFunc">
        ///     The is Func.
        /// </param>
        private ToastContent(string content, bool isFunc = false)
        {
            this.content = content;
            this.isFunc = isFunc;
            this.CreateToastFunc();
            this.Initialized();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ToastContent" /> class.
        /// </summary>
        /// <param name="content">
        ///     The content.
        /// </param>
        private ToastContent(string content)
        {
            this.content = content;
            this.CreateToastDialog();
            this.Initialized();
        }

        #endregion

        #region  Public Static Methods

        /// <summary>
        ///     The make func.
        /// </summary>
        /// <param name="content">
        ///     The content.
        /// </param>
        /// <returns>
        ///     The <see cref="ToastContent" />.
        /// </returns>
        public static ToastContent MakeFunc(string content)
        {
            instance = new ToastContent(content, true);
            return instance;
        }

        /// <summary>
        ///     The make.
        /// </summary>
        /// <param name="content">
        ///     The content.
        /// </param>
        /// <returns>
        ///     The <see cref="ToastContent" />.
        /// </returns>
        public static ToastContent MakeText(string content)
        {
            instance = new ToastContent(content);
            return instance;
        }

        public static void SetAccentColor(SolidColorBrush solidColorBrush = null)
        {
            accetColor = solidColorBrush ?? new SolidColorBrush(Colors.DodgerBlue);
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     The on finalized.
        /// </summary>
        public void OnFinalized()
        {
            this.UnsubscribeEvents();
            this.popup.IsOpen = false;
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     The create message dialog.
        /// </summary>
        private void CreateToastDialog()
        {
            this.gridContentPage = new Grid
            {
                Height = Window.Current.Bounds.Height,
                Width = Window.Current.Bounds.Width
            };

            this.borderToast = new Border
            {
                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Center,
                Background = accetColor,
                CornerRadius = new CornerRadius(20d),
                Margin = new Thickness(0, 0, 0, 80d)
            };

            this.gridToast = new Grid
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                MinWidth = 220d,
                MaxWidth = 300d,
                Margin = new Thickness(20, 10, 20, 10)
            };

            var messageBox = new TextBlock
            {
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 15d,
                FontFamily = new FontFamily("Segoe UI Regular"),
                Text = this.content,
                TextWrapping = TextWrapping.NoWrap,
                TextTrimming = TextTrimming.CharacterEllipsis,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            this.gridToast.Children.Add(messageBox);

            this.borderToast.Child = this.gridToast;

            this.gridContentPage.Children.Add(this.borderToast);
        }

        /// <summary>
        ///     The create toast func.
        /// </summary>
        private void CreateToastFunc()
        {
            this.gridContentPage = new Grid
            {
                Height = Window.Current.Bounds.Height,
                Width = Window.Current.Bounds.Width
            };

            this.gridOpacity = new Grid
            {
                Height = Window.Current.Bounds.Height,
                Width = Window.Current.Bounds.Width
            };

            this.borderToast = new Border
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                BorderThickness = new Thickness(0.3d),
                CornerRadius = new CornerRadius(5d)
            };

            this.gridToast = new Grid
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                MinWidth = 300d,
                MaxWidth = 360d,
                Margin = new Thickness(20, 10, 20, 10)
            };

            // Progress Ring
            this.gridToast.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Auto)
            });

            this.gridToast.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            });

            var progress = new ProgressRing
            {
                IsActive = true,
                Height = 45d,
                Width = 45d,
                Foreground = accetColor,
                Margin = new Thickness(0, 0, 20, 0)
            };

            Grid.SetColumn(progress, 0);

            var textBlock = new TextBlock
            {
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 15d,
                FontFamily = new FontFamily("Segoe UI Regular"),
                Text = this.content,
                TextWrapping = TextWrapping.NoWrap,
                TextTrimming = TextTrimming.CharacterEllipsis
            };

            Grid.SetColumn(textBlock, 1);

            this.SetColorChildrenFunc(ref this.gridOpacity, ref this.borderToast, ref progress, ref textBlock);

            this.gridToast.Children.Add(progress);
            this.gridToast.Children.Add(textBlock);

            this.borderToast.Child = this.gridToast;

            this.gridContentPage.Children.Add(this.gridOpacity);
            this.gridContentPage.Children.Add(this.borderToast);
        }

        /// <summary>
        ///     The initialized.
        /// </summary>
        private void Initialized()
        {
            this.popup = new Popup
            {
                Child = this.gridContentPage
            };

            if(this.popup.Child != null)
            {
                this.SubscribeEvents();
                this.popup.IsOpen = true;
            }
        }

        /// <summary>
        ///     The on key down.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The e.
        /// </param>
        private void OnKeyDown(object sender, KeyRoutedEventArgs e)
        {
            this.OnFinalized();
        }

        /// <summary>
        ///     The on window activated.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="windowActivatedEventArgs">
        ///     The window activated event args.
        /// </param>
        private void OnWindowActivated(object sender, WindowActivatedEventArgs windowActivatedEventArgs)
        {
            Window.Current.Activated -= this.OnWindowActivated;
        }

        /// <summary>
        ///     The set color children func.
        /// </summary>
        /// <param name="contentOpacity">
        ///     The content opacity.
        /// </param>
        /// <param name="borderToastRef">
        ///     The border toast ref.
        /// </param>
        /// <param name="progressRing">
        ///     The progress ring.
        /// </param>
        /// <param name="label">
        ///     The label.
        /// </param>
        private void SetColorChildrenFunc(ref Grid contentOpacity, ref Border borderToastRef, ref ProgressRing progressRing, ref TextBlock label)
        {
            // If application is mode light theme
            if(Application.Current.RequestedTheme == ApplicationTheme.Dark)
            {
                contentOpacity.Background = new SolidColorBrush(Colors.Black);
                contentOpacity.Opacity = 0.45D;

                borderToastRef.Background = new SolidColorBrush(Color.FromArgb(255, 31, 31, 31));
                borderToastRef.BorderBrush = new SolidColorBrush(Colors.Black);

                label.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
                progressRing.Background = new SolidColorBrush(Colors.Transparent);
            }
            else
            {
                contentOpacity.Background = new SolidColorBrush(Colors.White);
                contentOpacity.Opacity = 0.45D;

                borderToastRef.Background = new SolidColorBrush(Colors.GhostWhite);
                borderToastRef.BorderBrush = new SolidColorBrush(Colors.DarkGray);

                label.Foreground = new SolidColorBrush(Colors.DimGray);
                progressRing.Background = new SolidColorBrush(Colors.Transparent);
            }
        }

        /// <summary>
        ///     The size change.
        /// </summary>
        /// <param name="coreWindow">
        ///     The core window.
        /// </param>
        /// <param name="args">
        ///     The args.
        /// </param>
        private void SizeChange(CoreWindow coreWindow, WindowSizeChangedEventArgs args)
        {
            this.gridContentPage.Height = args.Size.Height;
            this.gridContentPage.Width = args.Size.Width;

            if(this.isFunc)
            {
                this.gridOpacity.Height = args.Size.Height;
                this.gridOpacity.Width = args.Size.Width;
            }

            this.popup.IsOpen = false;
            this.popup.Child = this.gridContentPage;
            this.popup.IsOpen = true;
        }

        /// <summary>
        ///     The subscribe events.
        /// </summary>
        private void SubscribeEvents()
        {
            if(this.isFunc)
            {
                Window.Current.CoreWindow.IsInputEnabled = false;
            }

            var frame = Window.Current.Content as FrameworkElement;

            if(frame == null)
            {
                // The dialog is being shown before content has been created for the window
                Window.Current.Activated += this.OnWindowActivated;
                return;
            }

            Window.Current.CoreWindow.SizeChanged += this.SizeChange;
            Window.Current.Content.KeyDown += this.OnKeyDown;
        }

        /// <summary>
        ///     The unsubscribe events.
        /// </summary>
        private void UnsubscribeEvents()
        {
            if(this.isFunc)
            {
                Window.Current.CoreWindow.IsInputEnabled = true;
            }

            Window.Current.CoreWindow.SizeChanged -= this.SizeChange;
            Window.Current.Content.KeyDown -= this.OnKeyDown;
            instance = null;
        }

        #endregion
    }
}