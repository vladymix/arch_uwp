// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Toast.cs" company="vlady-mix">
//    Fabricio Altamirano  2016
//  </copyright>
//  <summary>
//    The definition of  Toast.cs
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------

namespace UWPCore.Controls
{
    using System;
    using System.Threading.Tasks;
    using Windows.UI.Xaml.Media;

    /// <summary>
    ///     The toast length.
    /// </summary>
    public enum ToastLength
    {
        /// <summary>
        ///     The short 1 second.
        /// </summary>
        SHORT,

        /// <summary>
        ///     The medium 3 seconds.
        /// </summary>
        MEDIUM,

        /// <summary>
        ///     The long  7 seconds.
        /// </summary>
        LONG
    }

    /// <summary>
    ///     The toast.
    /// </summary>
    public class Toast
    {
        #region Private Static Fields

        /// <summary>
        ///     The instance.
        /// </summary>
        private static Toast instance;

        #endregion

        #region Private Fields

        /// <summary>
        ///     The content.
        /// </summary>
        private readonly string content;

        /// <summary>
        ///     The lenght.
        /// </summary>
        private readonly ToastLength lenght;

        /// <summary>
        ///     The operation.
        /// </summary>
        private readonly Func<Task> operation;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Toast" /> class.
        /// </summary>
        /// <param name="content">
        ///     The content.
        /// </param>
        /// <param name="length">
        ///     The length.
        /// </param>
        private Toast(string content, ToastLength length)
        {
            this.content = content;
            this.lenght = length;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Toast" /> class.
        /// </summary>
        /// <param name="operation">
        ///     The operation.
        /// </param>
        /// <param name="content">
        ///     The content.
        /// </param>
        private Toast(Func<Task> operation, string content)
        {
            this.content = content;
            this.operation = operation;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets a value indicating whether is showed.
        /// </summary>
        public static bool IsShowed { get; set; }

        #endregion

        #region  Public Static Methods

        /// <summary>
        ///     The make func.
        /// </summary>
        /// <param name="operation">
        ///     The operation.
        /// </param>
        /// <param name="content">
        ///     The content.
        /// </param>
        /// <returns>
        ///     The <see cref="Toast" />.
        /// </returns>
        public static Toast MakeFunc(Func<Task> operation, string content)
        {
            instance = new Toast(operation, content);
            return instance;
        }

        /// <summary>
        ///     The make text.
        /// </summary>
        /// <param name="content">
        ///     The content.
        /// </param>
        /// <param name="length">
        ///     The length.
        /// </param>
        /// <returns>
        ///     The <see cref="Toast" />.
        /// </returns>
        public static Toast MakeText(string content, ToastLength length)
        {
            instance = new Toast(content, length);
            return instance;
        }

        #endregion

        #region Public Methods

        public async void Show(SolidColorBrush solidColorBrush)
        {
            ToastContent.SetAccentColor(solidColorBrush);

            ToastContent toask;

            IsShowed = true;

            if(this.operation != null)
            {
                toask = ToastContent.MakeFunc(this.content);
                await this.operation.Invoke();
            }
            else
            {
                toask = ToastContent.MakeText(this.content);
                await EndShowed(this.lenght);
            }

            toask.OnFinalized();
            IsShowed = false;
        }

        #endregion

        #region Private Static Methods

        /// <summary>
        ///     The await.
        /// </summary>
        /// <param name="length">
        ///     The length.
        /// </param>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        private static async Task EndShowed(ToastLength length)
        {
            switch(length)
            {
                case ToastLength.LONG:
                    await Task.Delay(TimeSpan.FromSeconds(5));
                    IsShowed = false;
                    break;
                case ToastLength.MEDIUM:
                    await Task.Delay(TimeSpan.FromSeconds(3));
                    IsShowed = false;
                    break;
                case ToastLength.SHORT:
                    await Task.Delay(TimeSpan.FromSeconds(1.5));
                    IsShowed = false;
                    break;
            }
        }

        #endregion
    }
}