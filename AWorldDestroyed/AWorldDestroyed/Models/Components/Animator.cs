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

using AWorldDestroyed.Utility;
using System;
using System.Collections.Generic;

namespace AWorldDestroyed.Models.Components
{
    /// <summary>
    /// Animator takes care of what animation to run.
    /// </summary>
    public class Animator : Component, IUpdateable
    {
        private Dictionary<string, Animation> animations;
        private Animation currentAnimation;

        /// <summary>
        /// Create a new instance of the Animator class.
        /// </summary>
        public Animator() : base() 
        {
            animations = new Dictionary<string, Animation>();
        }

        /// <summary>
        /// Updates the current animation and sets the sprite for the SpriteRenderer.
        /// </summary>
        /// <param name="deltaTime">Time in milliseconds since last update.</param>
        public void Update(double deltaTime)
        {
            if (currentAnimation != null)
            {
                currentAnimation.Update(deltaTime);
                if (!currentAnimation.Done)
                    AttachedTo.GetComponent<SpriteRenderer>().Sprite = currentAnimation.GetCurrentFrameSprite();
            }
        }

        /// <summary>
        /// Start an animation or switch which one is playing.
        /// </summary>
        /// <param name="name">Name of animation to play.</param>
        public void ChangeAnimation(string name)
        {
            if (!animations.ContainsKey(name))
                throw new ArgumentException($"Animator has no Animation {name}.");

            if (currentAnimation != animations[name])
                animations[name].Reset();

            currentAnimation = animations[name];
        }

        /// <summary>
        /// Adds animation into animator.
        /// </summary>
        /// <param name="animation">Animation object</param>
        public void AddAnimation(string name, Animation animation)
        {
            animations[name] = animation;
            if (currentAnimation == null) currentAnimation = animation;
        }

        /// <summary>
        /// Get the animation that is currently set to play in this Animator.
        /// </summary>
        /// <returns>The currently active Animation.</returns>
        public Animation GetCurrentAnimation()
        {
            return currentAnimation;
        }

        /// <summary>
        /// Make a new Animator instance with the same attribute values as this instance.
        /// </summary>
        /// <returns>A copy of this Animator instance.</returns>
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