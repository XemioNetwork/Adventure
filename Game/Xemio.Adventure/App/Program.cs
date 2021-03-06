﻿using System;
using System.Drawing;
using System.Windows.Forms;
using Xemio.Adventure.Game;
using Xemio.Adventure.Game.States;
using Xemio.Adventure.Scenes;
using Xemio.Adventure.Scenes.Debug;
using Xemio.Adventure.Worlds;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Game.Scenes;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.GDIPlus;
using Xemio.GameLibrary.Rendering.Xna;

namespace Xemio.Adventure.App
{
    static class Program
    {
        /// <summary>
        /// Main entrance point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            Size size = new Size(800, 600);
            MainForm mainForm = new MainForm();
            mainForm.ClientSize = size;

            var initializer = new PriorizedInitializer();

            initializer.Add(new XnaGraphicsInitializer());
            initializer.Add(new GDIGraphicsInitializer());

            var config = XGL.Configure()
                .FrameRate(60)
                .BackBuffer(size.Width / 2, size.Height / 2)
                .WithDefaultComponents()
                .WithDefaultInput()
                .Components(new GameStateManager())
                .Graphics(initializer)
                .Scenes(new BackgroundScene(),
                        new SplashScreen(new LoadingScene()),
                        new DebugOverlay())
                .BuildConfiguration();
            
            XGL.Run(mainForm.Handle, config);

            Application.Run(mainForm);
        }
    }
}
