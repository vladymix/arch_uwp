﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWPCore;
using UWPCore.Converters;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace AppTest
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
       

    {
        List<String> items = new List<string>();


        public MainPage()
        {
            this.InitializeComponent();
        }

        private void onTappedButton(object sender, TappedRoutedEventArgs e)
        {
            CollectionEmptyToVisibilityConverter ol = new CollectionEmptyToVisibilityConverter();

         var obj=   ol.Convert(items,null, null, null);


            if (CollectionsTools.IsNullOrEmpty(items)) {
            }
        }
    }
}
