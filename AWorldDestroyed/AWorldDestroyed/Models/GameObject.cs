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
        public GameObject() : this(null){ }

        /// <summary>
        /// Initialize a new GameObject with a given Transform component.
        /// </summary>
        /// <param name="transform">A Transform component supplying transformation capabilites to this object.</param>
        public GameObject(Transform transform) : base(transform){ }

        /// <summary>
        /// Determines what happens when the object is out of scope of the camera.
        /// </summary>
        public virtual void OnOutOfScope()
        {
        }
    }
}
