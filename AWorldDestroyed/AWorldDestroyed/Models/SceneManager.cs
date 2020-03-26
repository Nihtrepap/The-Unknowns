// =============================================
//         Editor:     Lone Maaherra
//         Last edit:  2020-03-19 
// _-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_
//
//       (\                 >+{{{o)> - kvaouk
//    >+{{{{{0)> - kraouk      LL  
//       /_\_
//
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//                <333333><                     
//         <3333333><           <33333>< 

using System;
using System.Collections.Generic;

namespace AWorldDestroyed.Models
{
    /// <summary>
    /// Manages the Scenes in a program.
    /// </summary>
    public static class SceneManager
    {
        public static Scene ActiveScene { get; private set; }

        private static Dictionary<string, Scene> scenes;
        private static Stack<Scene> sceneHistory;

        /// <summary>
        /// Used to initialize the SceneManager class.
        /// </summary>
        static SceneManager()
        {
            ActiveScene = null;
            scenes = new Dictionary<string, Scene>();
            sceneHistory = new Stack<Scene>();
        }

        /// <summary>
        /// Add a Scene object to the SceneManager.
        /// </summary>
        /// <param name="name">A name by which to reference the Scene.</param>
        /// <param name="scene">The Scene to add.</param>
        public static void AddScene(string name, Scene scene)
        {
            if (ActiveScene == null) ActiveScene = scene;

            scenes.Add(name, scene);
        }

        /// <summary>
        /// Get a reference to a Scene managed by the SceneManager.
        /// </summary>
        /// <param name="name">A name that references the Scene.</param>
        /// <returns>A Scene object, or null if an invalid name was given.</returns>
        public static Scene GetScene(string name)
        {
            if (scenes.ContainsKey(name)) return scenes[name];
            return null;
        }

        /// <summary>
        /// Change the active Scene to the Scene referenced by the given name.
        /// </summary>
        /// <param name="name">A name that references the Scene.</param>
        public static void ChangeScene(string name)
        {
            if (scenes.ContainsKey(name))
            {
                sceneHistory.Push(scenes[name]);
                ActiveScene = scenes[name];
            }
        }

        /// <summary>
        /// Change the active Scene to the previous Scene.
        /// </summary>
        public static void PreviousScene()
        {
            if (sceneHistory.Count > 0) ActiveScene = sceneHistory.Pop();
        }

        /// <summary>
        /// Reset the Scene object referenced by the given name.
        /// </summary>
        /// <param name="name">A name that references the Scene.</param>
        public static void ResetScene(string name)
        {
            throw new NotImplementedException();
        }
    }
}
