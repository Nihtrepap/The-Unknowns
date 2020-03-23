// =============================================
//         Editor:     Philip  Abrahamsson
//         Last edit:  2020-03-23 
// _-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_
//
//       (\                 >+{{{o)> - kvaouk
//    >+{{{{{0)> - kraouk      LL  
//       /_\_
//
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//                <333333><                     
//         <3333333><           <33333>< 

using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace AWorldDestroyed.Models
{
    /// <summary>
    /// Animator takes care of what animation to run.
    /// </summary>
    class Animator : Component
    {
        List<Animation> animations { get; set; }

        public void Animator(GameObject gameObject, Vector2 position) :base(sceneObject) { }

        /// <summary>
        /// This method is used to update the object.
        /// </summary>
        /// <param name="deltaTime">Time in milliseconds since last update</param>
        public void Update(double deltaTime)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">Name of animation to play</param>
        public void Play(string name)
        {

        }

        /// <summary>
        /// Adds animation into animator.
        /// </summary>
        /// <param name="animation">Animation object</param>
        public void Add(Animation animation)
        {
            animations.Add(animation);
        }

        /// <summary>
        /// Creates a copy of Animator object.
        /// </summary>
        /// <returns>Animator object</returns>
        public object Copy() => this.MemberwiseClone();
    }
}