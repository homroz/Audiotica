﻿using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Audiotica.Database.Models;

namespace Audiotica.Windows.Tools.Converters
{
    public class TrackStatusToContentConverter : IValueConverter
    {
        public TrackStatus DesiredStatus { get; set; }
        public bool NotDesired { get; set; }
        public object TrueContent { get; set; }
        public object FalseContent { get; set; }

        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            if (!(value is TrackStatus))
                return FalseContent;

            var status = (TrackStatus)value;
            var visible = (NotDesired && status != DesiredStatus) || (!NotDesired && status == DesiredStatus);
            return visible ? TrueContent : FalseContent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TrackStatusConverter : IValueConverter
    {
        public TrackStatus DesiredStatus { get; set; }
        public bool NotDesired { get; set; }

        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            if (!(value is TrackStatus))
                return Visibility.Collapsed;

            var status = (TrackStatus) value;
            var visible = (NotDesired && status != DesiredStatus) || (!NotDesired && status == DesiredStatus);
            return visible ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TrackTypeConverter : IValueConverter
    {
        public TrackType DesiredStatus { get; set; }
        public bool NotDesired { get; set; }

        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            if (!(value is TrackType))
                return Visibility.Collapsed;

            var status = (TrackType)value;
            var visible = (NotDesired && status != DesiredStatus) || (!NotDesired && status == DesiredStatus);
            return visible ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }
}