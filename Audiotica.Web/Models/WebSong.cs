﻿using System;
using System.Collections.Generic;
using Audiotica.Core.Common;

namespace Audiotica.Web.Models
{
    public class WebSong : WebItem, IConvertibleObject
    {
        public WebSong(Type provider) : base(provider)
        {
        }

        public string Title { get; set; }
        public string Genres { get; set; }
        public List<WebArtist> Artists { get; set; }
        public WebAlbum Album { get; set; }
        public int TrackNumber { get; set; }
        public int DiscNumber { get; set; }
        public object PreviousConversion { get; set; }
    }
}