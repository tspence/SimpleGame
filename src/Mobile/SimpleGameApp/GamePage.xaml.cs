﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLib;
using GameLib.Messages;
using GameLib.Rules;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace SimpleGameApp
{
    public partial class MainPage : ContentPage
    {
        public GameViewController Controller { get; set; }

        public MainPage(Board game)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            // Construct new board
            Controller = new GameViewController()
            {
                GameBoard = game,
            };

            // Start timer for automatic updates
            Xamarin.Forms.Device.StartTimer(TimeSpan.FromMilliseconds(32), TimerFunc);
        }

        private void OnPainting(object sender, SKPaintSurfaceEventArgs e)
        {
            Controller.Paint(sender, e.Surface.Canvas);
        }

        /// <summary>
        /// If we're playing a bot, take one turn every 250 ms
        /// </summary>
        bool TimerFunc()
        {
            try
            {
                // If an animation is playing, render
                if (Controller.Playing != null)
                {
                    cvGameCanvas.InvalidateSurface();
                }

                // Clear all status
                //Controller.CurrentAttack = null;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            return true;
        }

        //private void HandleResult(GameViewController.GameAttackResult r)
        //{
        //    switch (r)
        //    {
        //        case GameViewController.GameAttackResult.GameOver:
        //            var pg = new GameOverPage(Controller.GameBoard);
        //            Navigation.PushModalAsync(pg);
        //            break;
        //        case GameViewController.GameAttackResult.Invalid:
        //            // What to do here?
        //            break;
        //        default:
        //            cvGameCanvas.InvalidateSurface();
        //            break;
        //    }
        //}

        private void CvGameCanvas_Touch(object sender, SKTouchEventArgs e)
        {
            Controller.HandleTouch(sender, e.Location);
        }
    }
}
