using System;
using System.Windows.Forms;
using Xemio.Adventure.Scenes;
using Xemio.Adventure.Worlds;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Game.Scenes;
using Xemio.GameLibrary.Rendering.GDIPlus;

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

            MainForm mainForm = new MainForm();

            XGL.Initialize(new GDIGraphicsInitializer());
            XGL.Run(mainForm.Handle, 400, 300, 60);
            
            var sceneManager = XGL.Components.Get<SceneManager>();
            var startScene = new LoadingScene();

            sceneManager.Add(new BackgroundScene());
            sceneManager.Add(new SplashScreen(startScene));

            Application.Run(mainForm);
        }
    }
}
