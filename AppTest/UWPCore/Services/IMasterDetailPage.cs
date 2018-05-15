// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMasterDetailPage.cs" company="vlady-mix">
//    Fabricio Altamirano  2017
//  </copyright>
//  <summary>
//    The definition of  IMasterDetailPage.cs
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------

namespace UWPCore.Services
{
    using Windows.UI.Xaml.Controls;

    public interface IMasterDetailPage
    {
        #region Public Properties

        Frame DetailFrame { get; }
        Frame MasterFrame { get; }

        #endregion
    }
}
