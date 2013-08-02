using System;
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
using Xemio.GameLibrary.Rendering.GDIPlus;
using Xemio.GameLibrary.Rendering.SDL;

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

            float width = Screen.PrimaryScreen.Bounds.Width;
            float height = Screen.PrimaryScreen.Bounds.Height;

            float aspectRatio = width / height;

            MainForm mainForm = new MainForm();
            mainForm.ClientSize = new Size(800, 600);

            XGL.Initialize(new GDIGraphicsInitializer());
            XGL.Run(mainForm.Handle,
                mainForm.ClientSize.Width / 2,
                mainForm.ClientSize.Height / 2,
                60);

            XGL.Components.Add(new GameStateManager());
            
            var sceneManager = XGL.Components.Get<SceneManager>();
            var startScene = new LoadingScene();

            sceneManager.Add(new BackgroundScene());
            sceneManager.Add(new SplashScreen(startScene));
            sceneManager.Add(new DebugOverlay());
            
            Application.Run(mainForm);
        }
    }
}
