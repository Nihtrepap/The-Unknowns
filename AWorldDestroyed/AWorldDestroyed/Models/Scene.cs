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
    /// Holds information about a scene and connects a Camera and an ObjectHandler with the objects of SceneLayers.
    /// </summary>
    class Scene
    {
        private Camera camera;
        private ObjectHandler objectHandler;
        private List<SceneLayer> sceneLayers;

        /// <summary>
        /// Creates a new instance of the Scene class. 
        /// </summary>
        public Scene()
        {
            camera = new Camera();
            objectHandler = new ObjectHandler();
            sceneLayers = new List<SceneLayer>();
        }

        /// <summary>
        /// Creates a new instance of the Scene class, with the specified SceneLayers. 
        /// </summary>
        /// <params name="layers">A variable number of SceneLayers.</param>
        public Scene(params SceneLayer[] layers) : this()
        {   
            sceneLayers.AddRange(layers);
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Load SceneLayer contents.
        /// </summary>
        public void LoadContent()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add a SceneLayer to the list of SceneLayers.
        /// </summary>
        /// <param name="layer"></param>
        public void AddSceneLayer(SceneLayer layer)
        {
            sceneLayers.Add(layer);
        }

        /// <summary>
        /// Add a GameObject to the Scene.
        /// </summary>
        /// <param name="gameObject"></param>
        public void AddObject(GameObject gameObject)
        {
            throw new NotImplementedException();
        }


    }
}
