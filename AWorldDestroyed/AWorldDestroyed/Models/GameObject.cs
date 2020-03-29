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

using AWorldDestroyed.Models.Components;
using AWorldDestroyed.Utility;

namespace AWorldDestroyed.Models
{
    /// <summary>
    /// Base class for all objects related to the actual game, such as creatures and maptiles.
    /// </summary>
    public class GameObject : SceneObject
    {
        /// <summary>
        /// Initialize a new GameObject.
        /// </summary>
        public GameObject() : base()
        {
        }

        /// <summary>
        /// Initialize a new GameObject with a given Transform component.
        /// </summary>
        /// <param name="transform">A Transform component supplying transformation capabilites to this object.</param>
        public GameObject(Transform transform) : base(transform)
        {
        }

        /// <summary>
        /// Determines what happens when the object is out of scope of the camera.
        /// </summary>
        public virtual void OnOutOfScope()
        {
        }

        /// <summary>
        /// Determines what happens when this object collisions with another GameObject.
        /// </summary>
        /// <param name="other">The GameObject this object collided with.</param>
        public virtual void OnCollision(GameObject other)
        {
        }

        /// <summary>
        /// Determines what happens when another GameObject triggered this GameObject.
        /// </summary>
        /// <param name="other">The GameObject that triggered this object.</param>
        public virtual void OnTrigger(GameObject other)
        {
        }
    }
}
