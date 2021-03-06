﻿using Audiotica.Web.Extensions;
using HtmlAgilityPack;

namespace Audiotica.Web.Http.Requets.MatchEngine.Mp3Freex
{
    public class Mp3FreexSearchRequest : RestObjectRequest<HtmlDocument>
    {
        public Mp3FreexSearchRequest(string query)
        {
            this.Url("http://mp3freex.net/").QParam("inmp3", query).Get();
        }
    }
}