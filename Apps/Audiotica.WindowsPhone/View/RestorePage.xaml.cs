﻿#region

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Audiotica.Core.Common;
using Audiotica.Core.Utilities;
using Xamarin;

#endregion

namespace Audiotica.View
{
    public sealed partial class RestorePage
    {
        public RestorePage()
        {
            InitializeComponent();
        }

        public override async void NavigatedTo(object parameter)
        {
            base.NavigatedTo(parameter);
            
            var reset = AppSettingsHelper.Read<bool>("FactoryReset");

            var startingMsg = "Restoring (this may take a bit)...";
            if (reset)
                startingMsg = "Factory resetting...";

            using (Insights.TrackTime(reset ? "Factory Reset" : "Restore Collection"))
            {
                StatusBarHelper.ShowStatus(startingMsg);

                var file = reset ? null : await StorageHelper.GetFileAsync("_current_restore.autcp");

                //delete artowkr and mp3s
                var artworkFolder = await StorageHelper.GetFolderAsync("artworks");
                var artistFolder = await StorageHelper.GetFolderAsync("artists");
                var songFolder = await StorageHelper.GetFolderAsync("songs");

                if (artworkFolder != null)
                {
                    await artworkFolder.DeleteAsync();
                }

                if (artistFolder != null)
                {
                    await artistFolder.DeleteAsync();
                }

                if (songFolder != null)
                {
                    await songFolder.DeleteAsync();
                }

                if (!reset)
                {
                    using (var stream = await file.OpenStreamForWriteAsync())
                    {
                        await AutcpFormatHelper.UnpackBackup(ApplicationData.Current.LocalFolder, stream);
                    }

                    await file.DeleteAsync();

                    App.Locator.CollectionService.LibraryLoaded += async (sender, args) =>
                    {
                        await CollectionHelper.DownloadArtistsArtworkAsync(false);
                    };
                }
                else
                {
                    var dbs = (await ApplicationData.Current.LocalFolder.GetFilesAsync())
                    .Where(p => p.FileType == ".sqldb").ToList();

                    foreach (var db in dbs)
                    {
                        await db.DeleteAsync();
                    }

                    AppSettingsHelper.Write("FactoryReset", false);
                }

                StatusBarHelper.HideStatus();
            }

            (Application.Current as App).BootAppServicesAsync();
            App.Navigator.GoTo<HomePage, ZoomOutTransition>(null);
        }
    }
}