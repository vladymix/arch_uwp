// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppEnvironment.cs" company="vlady-mix">
//    Fabricio Altamirano  2016
//  </copyright>
//  <summary>
//    The definition of  AppEnvironment.cs
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------

namespace UWPCore
{
    using Windows.System.Profile;

    public static class AppEnvironment
    {
        #region DeviceFamily enum

        public enum DeviceFamily
        {
            Desktop,
            Mobile,
            Other
        }

        #endregion

        #region Public Properties

        public static bool Desktop => Family.Equals(DeviceFamily.Desktop);
        public static DeviceFamily Family { get; } = AnalyticsInfo.VersionInfo.DeviceFamily.ToDeviceFamily();
        public static bool IsMobile => Family.Equals(DeviceFamily.Mobile);

        public static bool IsOther => Family.Equals(DeviceFamily.Other);

        #endregion

        #region Private Static Methods

        private static DeviceFamily ToDeviceFamily(this string value)
        {
            switch(value)
            {
                case "Windows.Desktop":
                    return DeviceFamily.Desktop;
                case "Windows.Mobile":
                    return DeviceFamily.Mobile;
                default:
                    return DeviceFamily.Other;
            }
        }

        #endregion
    }
}