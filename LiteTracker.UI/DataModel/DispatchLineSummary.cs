﻿using System;
using LiteTracker.UI.Common;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace LiteTracker.UI.DataModel
{
    public class DispatchLineSummary : BindableBase
    {
        private static readonly Uri BaseUri = new Uri("ms-appx:///");

        public long DispatchNoteId { get; set; }
        public int Line { get; set; }
        public string ProductType { get; set; }
        public ImageSource ProductTypeImage { get; set; }
        public string Product { get; set; }
        public string QuantityDescription { get; set; }
        public string ClientDescription { get; set; }

        public void SetImage(string imagePath)
        {
            ProductTypeImage = new BitmapImage(new Uri(BaseUri, imagePath));            
            OnPropertyChanged("ProductTypeImage");
        }


        public string GetImagePath()
        {
            switch (ProductType)
            {
                case "Frozen":
                    return "/images/pin_big.png";
                case "Fresh":
                    return "/images/shark-icon.png";
                default:
                    return "/images/pending_big.png";
            }
        }
    }
}