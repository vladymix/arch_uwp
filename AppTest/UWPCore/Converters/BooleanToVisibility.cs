// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BooleanToVisibility.cs" company="vlady-mix">
//    Fabricio Altamirano  2018
//  </copyright>
//  <summary>
//    The definition of  BooleanToVisibility.cs
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------

namespace UWPCore.Converters
{
    using System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    public class BooleanToVisibility : IValueConverter
    {
        #region Interface Members

        public object Convert(object value, Type targetType, object parameter, string language)
         =>
            (bool)value ^ (parameter as string ?? string.Empty).Equals("Reverse") ?
                Visibility.Visible : Visibility.Collapsed;

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
