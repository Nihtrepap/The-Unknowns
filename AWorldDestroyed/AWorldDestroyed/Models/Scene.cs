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
using AWorldDestroyed.Utility;

namespace AWorldDestroyed.Models
{
    /// <summary>
    /// Holds information about a scene and connects a Camera and an ObjectHandler with the objects of SceneLayers.
    /// </summary>
    public class Scene
    {
        private Camera camera;
        private ObjectHandler objectHandler;
        private List<ISceneLayer> sceneLayers;

        /// <summary>
        /// Creates a new instance of the Scene class, with the specified SceneLayers. 
        /// </summary>
        /// <params name="layers">A variable number of SceneLayers.</param>
        public Scene(params ISceneLayer[] layers)
        {
            camera = new Camera();
            objectHandler = new ObjectHandler();
            sceneLayers = new List<ISceneLayer>();
            sceneLayers.AddRange(layers);
        }

        public void Initialize()
        {
            //foreach (ISceneLayer layer in sceneLayers)
            //    layer.Initialize();
        }

        /// <summary>
        /// Load SceneLayer contents.
        /// </summary>
        public void LoadContent()
        {
            objectHandler.GameObjects.Clear();

            //foreach (ISceneLayer layer in sceneLayers)
            //{
            //}
        }

        /// <summary>
        /// Unload SceneLayer contents.
        /// </summary>
        public void UnloadContent()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update the logic of the Scene.
        /// </summary>
        /// <param name="deltaTime">Time in milliseconds since last update.</param>
        public void Update(double deltaTime)
        {
            //foreach (ISceneLayer layer in sceneLayers)
            //    layer.Update(deltaTime);
        }

        /// <summary>
        /// Add a SceneLayer to the list of SceneLayers.
        /// </summary>
        /// <param name="layer">The SceneLayer to add.</param>
        public void AddSceneLayer(ISceneLayer layer)
        {
            sceneLayers.Add(layer);
        }

        /// <summary>
        /// Add a GameObject to the Scene.
        /// </summary>
        /// <param name="gameObject">The GameObject to add.</param>
        public void AddObject(GameObject gameObject)
        {
            throw new NotImplementedException();
        }
    }
}
