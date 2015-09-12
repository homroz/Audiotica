﻿using Newtonsoft.Json;

namespace Audiotica.Web.Http.Requets.Metadata.Spotify.Models
{
    public class SearchItem : BasicModel
    {
        [JsonProperty("artists")]
        public Paging<FullArtist> Artists { get; set; }

        [JsonProperty("albums")]
        public Paging<SimpleAlbum> Albums { get; set; }

        [JsonProperty("tracks")]
        public Paging<FullTrack> Tracks { get; set; }
    }
}