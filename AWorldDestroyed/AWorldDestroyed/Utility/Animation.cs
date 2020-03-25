// =============================================
//         Editor:     Philip  Abrahamsson
//         Last edit:  2020-03-21 
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

namespace AWorldDestroyed.Utility
{
    /// <summary>
    /// Used to simulate a movement by changing a series of frames.
    /// </summary>
    class Animation
    {
        public string Name { get; set; }
        public bool Loop { get; set; }
        private Frame[] frames;
        private double timer;
        private int currentFrameIndex;

        /// <summary>
        /// Create new instance of Animation class with given animation frames.
        /// </summary>
        /// <param name="frames">The animation frames that make up an animation.</param>
        public void Animation(Frame[] frames)
        {
            this.frames = frames;
            Loop = true;
        }

        /// <summary>
        /// Creates a new instance of Animation from a given array of Sprites and a varable number of durations.
        /// </summary>
        /// <param name="sprites">An array of Sprites to use in the animation.</param>
        /// <param name="durations">The durations each frame should play, if sprites excede durations remaning sprites get the last duration value.</param>
        public Animation(Sprite[] sprites, params int[] durations)
        {
            frames = new Frame[sprites.Length];
            if (durations.Length == 0) durations = new int[] { 1000/12 };

            for (int i = 0; i < sprites.Length; i++)
            {
                frames[i] = new Frame(sprites[i], (durations.Length > i ? durations[i] : durations[durations.Length - 1]));
            }
            
        }

        /// <summary>
        /// This method is used to update the animation.
        /// </summary>
        /// <param name="deltaTime">Time in milliseconds since last update</param>
        public void Update(double deltaTime)
        {
           // _timer += deltaTime;
            //if(_timer > frames.)

            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the Sprite of the current Frame.
        /// </summary>
        /// <returns>The Sprite of the current Frame.</returns>
        public Sprite GetCurrentFrameSprite()
        {
            return frames[currentFrameIndex].Sprite;
        }
    }
}