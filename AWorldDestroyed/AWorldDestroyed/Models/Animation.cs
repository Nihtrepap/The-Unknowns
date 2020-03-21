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

namespace AWorldDestroyed
{
    /// <summary>
    /// Get the Sprite sheet to an animation. 
    /// Using the Frame to get the sprite "moving".
    /// </summary>
    class Animation
    {
        public string Name { get; set; }
        public bool Loop { get; set; }
        private Frame[] _frames;
        private double _timer;
        private int _currentFrameIndex;

        public void Animation(string name, Frame[] frames)
        {
            Name = name;
            _frames = frame;
            Loop = true;
        }
        /// <summary>
        /// This method is used to update the animation frame.
        /// It will check each millisecond and change frames after
        /// how you choose.
        /// </summary>
        /// <param name="deltaTime">Time in milliseconds since last update</param>
        public void Update(double deltaTime)
        {
           // _timer += deltaTime;
            //if(_timer > frames.)

            throw new NotImplementedException();
        }
    }
}