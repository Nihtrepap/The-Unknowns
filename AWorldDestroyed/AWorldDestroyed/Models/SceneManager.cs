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
    class SceneManager
    {
        private Dictionary<string, Scene> scenes;
        private Stack<Scene> sceneHistory;

        /// <summary>
        /// Creates a new instance of the SceneManager class. 
        /// </summary>
        public SceneManager()
        {
            scenes = new Dictionary<string, Scene>();
            sceneHistory = new Stack<Scene>();
        }

        /// <summary>
        /// Add a Scene object to the SceneManager.
        /// </summary>
        /// <param name="name">A name by which to reference the Scene.</param>
        /// <param name="scene">The Scene to add.</param>
        public void AddScene(string name, Scene scene)
        {
            scenes.Add(name, scene);
        }

        /// <summary>
        /// Get a reference to a Scene managed by the SceneManager.
        /// </summary>
        /// <param name="name">A name that references the Scene.</param>
        /// <returns>A Scene object, or null if an invalid name was given.</returns>
        public Scene GetScene(string name)
        {
            if (scenes.ContainsKey(name)) return scenes[name];
            return null;
        }

        /// <summary>
        /// Change the active Scene to the Scene referenced by the given name.
        /// </summary>
        /// <param name="name">A name that references the Scene.</param>
        public void ChangeScene(string name)
        {
            if (scenes.ContainsKey(name)) sceneHistory.Push(scenes[name]);
        }

        /// <summary>
        /// Change the active Scene to the previous Scene.
        /// </summary>
        public void PreviousScene()
        {
            if (sceneHistory.Count > 0) sceneHistory.Pop();
        }

        /// <summary>
        /// Reset the Scene object referenced by the given name.
        /// </summary>
        /// <param name="name">A name that references the Scene.</param>
        public void ResetScene(string name)
        {
            throw new NotImplementedException();
        }
    }
}
