using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteTracker.UI.DataModel;
using Microsoft.WindowsAzure.MobileServices;
using Windows.Networking.PushNotifications;

namespace LiteTracker.UI.Services
{
  internal class MobileServices
  {
    public MobileServices()
    {
      MobileServiceClient = new MobileServiceClient(
        "https://litetracker.azure-mobile.net/",
        "xJJfEdiYjrioWvkvaQoUgRtlTUpyBp52"
        );

      AcquirePushChannel();
    }

    public MobileServiceClient MobileServiceClient { get; private set; }
    public PushNotificationChannel CurrentChannel { get; private set; }

    private async void AcquirePushChannel()
    {
      CurrentChannel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
      var channelTable = MobileServiceClient.GetTable<Channel>();
      var channel = new Channel { Uri = CurrentChannel.Uri };
      await channelTable.InsertAsync(channel);
    }
  }
}
