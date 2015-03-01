﻿#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Audiotica.Core.Common;
using Audiotica.Data.Collection.Model;
using TagLib;

#endregion

namespace Audiotica.Data.Collection
{
    public interface ICollectionService
    {
        bool IsLibraryLoaded { get; }
        int ScaledImageSize { get; set; }
        OptimizedObservableCollection<Song> Songs { get; set; }
        OptimizedObservableCollection<Song> TempSongs { get; set; }
        OptimizedObservableCollection<Album> Albums { get; set; }
        OptimizedObservableCollection<Album> TempAlbums { get; set; }
        OptimizedObservableCollection<Artist> Artists { get; set; }
        OptimizedObservableCollection<Artist> TempArtists { get; set; }
        OptimizedObservableCollection<RadioStation> Stations { get; set; }
        OptimizedObservableCollection<Playlist> Playlists { get; set; }
        OptimizedObservableCollection<QueueSong> PlaybackQueue { get; }
        OptimizedObservableCollection<QueueSong> ShufflePlaybackQueue { get; }
        OptimizedObservableCollection<QueueSong> CurrentPlaybackQueue { get; }
        event EventHandler LibraryLoaded;

        /// <summary>
        ///     Loads all songs, albums, artist and playlists/queue.
        /// </summary>
        void LoadLibrary(bool loadEssentials = false);

        Task LoadLibraryAsync(bool loadEssentials = false);

        /// <summary>
        ///     Adds the song to the database and collection.
        /// </summary>
        Task AddSongAsync(Song song, Tag tags = null, bool temp = false);

        /// <summary>
        ///     Deletes the song from the database and collection.  Also all related files.
        /// </summary>
        Task DeleteSongAsync(Song song, bool temp = false);

        Task<List<HistoryEntry>> FetchHistoryAsync();
        bool SongAlreadyExists(string providerId, string name, string album, string artist);
        bool SongAlreadyExists(string localSongPath);

        #region Radio Stations

        Task AddStationAsync(RadioStation station);
        Task DeleteStationAsync(RadioStation station);

        #endregion

        #region Playback Queue

        void ShuffleModeChanged();
        Task ShuffleCurrentQueueAsync();

        /// <summary>
        ///     Erases all songs in the playback queue (database and  PlaybackQueue prop)
        /// </summary>
        /// <returns></returns>
        Task ClearQueueAsync();

        /// <summary>
        ///     Adds the current song to the end of the queue.
        /// </summary>
        Task<QueueSong> AddToQueueAsync(Song song, QueueSong position = null, bool shuffleInsert = true, bool temp = false);

        /// <summary>
        ///     Moves the queue items at the old index to the new index
        /// </summary>
        Task MoveQueueFromToAsync(int oldIndex, int newIndex);

        /// <summary>
        ///     Deletes the specify song from the queue (database and PlaybackQueue prop)
        /// </summary>
        /// <param name="songToRemove"></param>
        /// <returns></returns>
        Task DeleteFromQueueAsync(QueueSong songToRemove);

        #endregion

        #region Playlist

        Task<Playlist> CreatePlaylistAsync(string name);

        Task DeletePlaylistAsync(Playlist playlist);

        Task AddToPlaylistAsync(Playlist playlist, Song song);

        Task MovePlaylistFromToAsync(Playlist playlist, int oldIndex, int newIndex);

        Task DeleteFromPlaylistAsync(Playlist playlist, PlaylistSong songToRemove);

        #endregion
    }
}