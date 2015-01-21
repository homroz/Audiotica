﻿#region

using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media.Animation;

#endregion

namespace Audiotica
{
    public static class ModalSheetUtility
    {
        private static IModalSheetPage _sheet;

        public static void Show(IModalSheetPage sheet)
        {
            if (_sheet != null) return;

            _sheet = sheet;
            var size = App.RootFrame;
            var element = sheet as FrameworkElement;

            element.Width = size.ActualWidth;
            element.Height = size.ActualHeight;
            App.RootFrame.SizeChanged += PageOnSizeChanged;

            var popup = new Popup
            {
                IsOpen = true,
                Child = element,
                VerticalOffset = element.Height
            };

            #region Slide up animation

            var slideAnimation = new DoubleAnimation
            {
                EnableDependentAnimation = true,
                From = popup.VerticalOffset,
                To = 0,
                Duration = new Duration(TimeSpan.FromMilliseconds(300))
            };

            var sb = new Storyboard();
            sb.Children.Add(slideAnimation);
            Storyboard.SetTarget(slideAnimation, popup);
            Storyboard.SetTargetProperty(slideAnimation, "VerticalOffset");

            sb.Begin();

            #endregion

            sheet.OnOpened(popup);
        }

        private static void PageOnSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
        {
            var size = App.RootFrame;
            (_sheet as FrameworkElement).Width = size.ActualWidth;
            (_sheet as FrameworkElement).Height = size.ActualHeight;
        }

        public static void Hide()
        {
            Hide(_sheet);
        }

        public static void Hide(IModalSheetPage sheet)
        {
            App.RootFrame.SizeChanged -= PageOnSizeChanged;

            #region Slide down animation

            var slideAnimation = new DoubleAnimation
            {
                EnableDependentAnimation = true,
                From = 0,
                To = (_sheet as FrameworkElement).Height,
                Duration = new Duration(TimeSpan.FromMilliseconds(200))
            };

            var sb = new Storyboard();
            sb.Children.Add(slideAnimation);
            Storyboard.SetTarget(slideAnimation, sheet.Popup);
            Storyboard.SetTargetProperty(slideAnimation, "VerticalOffset");

            sb.Completed += (sender, o) =>
            {
                sheet.Popup.IsOpen = false;
                sheet.OnClosed();
                _sheet = null;
            };

            sb.Begin();

            #endregion
        }
    }

    public interface IModalSheetPage
    {
        Popup Popup { get; }
        void OnOpened(Popup popup);
        void OnClosed();
    }

    public interface IModalSheetPageWithAction<T> : IModalSheetPage
    {
        Action<T> Action { get; set; }
    }
}