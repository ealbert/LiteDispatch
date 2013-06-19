using System;
using System.Collections.ObjectModel;
using LiteDispatch.Core.DTOs;
using LiteTracker.UI.Common;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace LiteTracker.UI.DataModel
{
    public class DispatchNoteSummary : BindableBase
    {
        private static readonly Uri BaseUri = new Uri("ms-appx:///");
        public long DispatchNoteId { get; set; }
        public string Haulier { get; set; }
        public string Truck { get; set; }
        public string TrackingInfo { get; set; }
        public string LastActivity { get; set; }
        public StatusEnum Status { get; set; }
        public ImageSource StatusImage { get; set; }
        public ObservableCollection<DispatchLineSummary> DispatchLineSummaries { get; set; }

        public void SetImage(string imagePath, ImageEnum imageEnum)
        {
            var image = new BitmapImage(new Uri(BaseUri, imagePath));
            UpdateImageProperty(imageEnum, image);
        }

        protected void UpdateImageProperty(ImageEnum imageEnum, BitmapImage image)
        {
            switch (imageEnum)
            {
                case ImageEnum.Main:
                    StatusImage = image;
                    OnPropertyChanged("MainImage");
                    break;
            }
        }

        public string StatusImageName()
        {
            switch (Status)
            {
                case StatusEnum.New:
                    return "/images/new_128.png";

                case StatusEnum.InTransit:
                    return "/images/truck_ex_128.png";

                case StatusEnum.Received:
                    return "/images/received_128.png";
            }
            return "new_128.png";
        }

        public enum ImageEnum
        {
            Main,
            Pending,
            Overdue,
            ToDo
        }

        public static DispatchNoteSummary Create(DispatchNoteDto dto)
        {
            var instance = new DispatchNoteSummary();
            instance.DispatchNoteId = dto.Id;
            instance.Haulier = dto.Haulier;
            instance.Truck = dto.TruckReg;
            instance.Status = dto.DispatchStatus.Equals("New") ? StatusEnum.New : StatusEnum.InTransit;
            instance.LastActivity = dto.LastUpdate.ToString("dd-MMM-yyyy @ hh:mm");
            if (instance.Status == StatusEnum.InTransit)
            {
                instance.LastActivity += " - Tracking Event";
                instance.TrackingInfo = dto.LastTrackingNotification;
            }
            else
            {
                instance.LastActivity += " - Dispatch was created";
                instance.TrackingInfo = "Has not started trip yet";
            }
            instance.SetImage(instance.StatusImageName(), ImageEnum.Main);
            instance.DispatchLineSummaries = new ObservableCollection<DispatchLineSummary>();
            foreach (var dispatchLineDto in dto.DispatchLineSet)
            {
                var line = new DispatchLineSummary();
                line.DispatchNoteId = instance.DispatchNoteId;
                line.Line = 0;
                line.Product = dispatchLineDto.Product;
                line.ProductType = dispatchLineDto.ProductType;
                line.SetImage(line.GetImagePath());
                line.QuantityDescription = string.Format("{0} {1}{2}",
                                                         dispatchLineDto.Quantity,
                                                         dispatchLineDto.Metric,
                                                         dispatchLineDto.Quantity > 1
                                                             ? "s"
                                                             : string.Empty);

                line.ClientDescription = string.Format("{0} (Shop: {1}{2})",
                                                       dispatchLineDto.Client,
                                                       dispatchLineDto.ShopId,
                                                       dispatchLineDto.ShopLetter);

                instance.DispatchLineSummaries.Add(line);

            }

            return instance;
        }
    }
}