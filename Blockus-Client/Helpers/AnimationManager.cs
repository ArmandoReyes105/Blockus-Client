using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Blockus_Client.Helpers
{
    public static class AnimationManager
    {

        public static void FadeIn(UIElement element, double durationInSeconds)
        {

            DoubleAnimation animation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(durationInSeconds)
            };

            element.BeginAnimation(UIElement.OpacityProperty, animation);
        }

        public static void RotateImage(RotateTransform rotateTransform, double durationInSeconds)
        {
            DoubleAnimation rotationAnimation = new DoubleAnimation
            {
                From = 0,
                To = 360,
                Duration = TimeSpan.FromSeconds(durationInSeconds),
                RepeatBehavior = RepeatBehavior.Forever
            };

            rotateTransform.BeginAnimation(RotateTransform.AngleProperty, rotationAnimation);
        }

    }
}
