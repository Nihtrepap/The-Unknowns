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

namespace AWorldDestroyed.Models
{
    /// <summary>
    /// Base class for all objects related to the actual game, such as creatures and maptiles.
    /// </summary>
    class GameObject : SceneObject
    {
        /// <summary>
        /// Initialize a new GameObject within the context of a given SceneLayer.
        /// </summary>
        /// <param name="sceneLayer">The SceneLayer related to this object.</param>
        public GameObject(SceneLayer sceneLayer) : base(sceneLayer)
        {
        }

        /// <summary>
        /// Initialize a new GameObject within the context of a given SceneLayer, with a given Transform component.
        /// </summary>
        /// <param name="sceneLayer">The SceneLayer related to this object.</param>
        /// <param name="transform">A Transform component supplying transformation capabilites to this object.</param>
        public GameObject(SceneLayer sceneLayer, Transform transform) : base(sceneLayer, transform)
        {
        }

        /// <summary>
        /// Determines what happens when the object is out of scope of the camera.
        /// </summary>
        public void OnOutOfScope()
        {
            Enabled = false;
        }

        /// <summary>
        /// Determines what happens when the object collisions with another GameObject.
        /// </summary>
        public void OnCollision(GameObject other)
        {
            Destroy();
        }

        /// <summary>
        /// Determines what happens when a GameObject triggers another GameObject.
        /// </summary>
        public void OnTrigger(GameObject other)
        {
            throw new NotImplementedException();
        }
    }
}
