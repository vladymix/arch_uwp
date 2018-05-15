using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace UWPCore.Converters
{
   public class CollectionEmptyToVisibilityConverter : IValueConverter
    {
        #region Interface Members

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var collectionEmpty = CollectionsTools.IsNullOrEmpty((IEnumerable<object>)targetType);

            return collectionEmpty ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
