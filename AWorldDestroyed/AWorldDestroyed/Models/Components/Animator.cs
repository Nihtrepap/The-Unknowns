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
using System;
using System.Collections.Generic;


namespace AWorldDestroyed.Models.Components
{
    /// <summary>
    /// Animator takes care of what animation to run.
    /// </summary>
    class Animator : Component
    {
        private Dictionary<string, Animation> animations;
        private Animation currentAnimation;

        /// <summary>
        /// Create a new instance of the Animator class.
        /// </summary>
        public Animator() :base() 
        {
            animations = new Dictionary<string, Animation>();
        }

        /// <summary>
        /// This method is used to update the animation.
        /// </summary>
        /// <param name="deltaTime">Time in milliseconds since last update</param>
        public void Update(double deltaTime)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Start an animation or switch which one is playing.
        /// </summary>
        /// <param name="name">Name of animation to play.</param>
        public void Play(string name)
        {
            if (!animations.ContainsKey(name))
                throw new ArgumentException($"Animator has no Animation {name}.");
            currentAnimation = animations[name];
        }

        /// <summary>
        /// Adds animation into animator.
        /// </summary>
        /// <param name="animation">Animation object</param>
        public void Add(string name, Animation animation)
        {
            animations[name] = animation;
            if (currentAnimation == null) currentAnimation = animation;
        }

        /// <summary>
        /// Make a new Animator instance with the same attribute values as this instance.
        /// </summary>
        /// <returns>Animator object</returns>
        public override Component Copy()
        {
            return new Animator()
            {
                animations = new Dictionary<string, Animation>(this.animations),
                currentAnimation = this.currentAnimation
            };

        }
    }
}