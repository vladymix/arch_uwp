// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProcessAsync.cs" company="vlady-mix">
//    Fabricio Altamirano  2016
//  </copyright>
//  <summary>
//    The definition of  ProcessAsync.cs
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------

namespace UWPCore.Controls
{
    using System;
    using System.Threading.Tasks;
    using Windows.UI;
    using Windows.UI.Core;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Controls.Primitives;
    using Windows.UI.Xaml.Media;

    public class ProcessAsync
    {
        #region Private Fields

        private readonly Task callback;
        private readonly Grid grid;
        private readonly Popup popup;

        #endregion

        #region Constructors

        public ProcessAsync(string message, Task asyncTask)
        {
            this.popup = new Popup();

            this.grid = new Grid
            {
                Height = Window.Current.Bounds.Height,
                Width = Window.Current.Bounds.Width,
                Background = new SolidColorBrush(Color.FromArgb(200, 30, 30, 30))
            };
            var stackPanel = new StackPanel
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Orientation = Orientation.Vertical,
                Children = {
                    new ProgressRing
                    {
                        Height = 35,
                        Width = 35,
                        Margin = new Thickness(10),
                        Foreground = new SolidColorBrush(Colors.White),
                        IsActive = true
                    },
                    new TextBlock
                    {
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        TextAlignment = TextAlignment.Center,
                        Text = message,
                        Foreground = new SolidColorBrush(Colors.White)
                    }
                }
            };
            this.grid.Children.Add(stackPanel);
            this.callback = asyncTask;
            this.popup.Child = this.grid;
        }

        #endregion

        #region Public Methods

        public async Task Execute()
        {
            this.popup.IsOpen = true;
            this.SubcribEvents();
            try
            {
                await this.callback;
            }
            catch(Exception ex)
            {
                Toast.MakeText(ex.Message, ToastLength.SHORT).Show(new SolidColorBrush(Colors.Crimson));
            }
            finally
            {
                this.UnSubcribEvents();
                this.popup.IsOpen = false;
            }
        }

        #endregion

        #region Private Methods

        private void OnSizeChangeWindows(object sender, WindowSizeChangedEventArgs e)
        {
            this.grid.Height = Window.Current.Bounds.Height;
            this.grid.Width = Window.Current.Bounds.Width;
        }

        private void SubcribEvents()
        {
            Window.Current.SizeChanged += this.OnSizeChangeWindows;
            Window.Current.CoreWindow.IsInputEnabled = false;
        }

        private void UnSubcribEvents()
        {
            Window.Current.SizeChanged -= this.OnSizeChangeWindows;
            Window.Current.CoreWindow.IsInputEnabled = true;
        }

        #endregion
    }
}