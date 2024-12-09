using Blockus_Client.Helpers;
using Blockus_Client.View;
using log4net;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Blockus_Client
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            this.Closing += Window_Closing;

            NavigationManager.Instance.Initialize(Frame_Main, this);
            NavigationManager.Instance.NavigateTo(new LoginPage());

            PlayImageAnimation(Image_BlueEllipse, .5, 3);
            PlayImageAnimation(Image_YellowEllipse, .45, 4);
        }

        private void PlayImageAnimation(Image image, double animationValue, int time)
        {
            DoubleAnimation FadeOut = new DoubleAnimation
            {
                To = animationValue,
                Duration = TimeSpan.FromSeconds(time),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };

            image.BeginAnimation(Image.OpacityProperty, FadeOut);
        }

        private void Window_Closing (object sender, CancelEventArgs e)
        {
            SessionManager.Instance.LogOut(); 
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (NavigationManager.Instance.CurrentPage is GamePage gamePage)
            {
                gamePage.HandleKeyDown(e);
            }
        }
    }
}
