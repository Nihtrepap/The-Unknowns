// =============================================
//         Editor:     Philip  Abrahamsson
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

namespace AWorldDestroyed
{
    /// <summary>
    /// Get the Sprite as animation.
    /// </summary>
    class Animation
    {
        public string Name { get; set; }
        public bool Loop { get; set; }
        private Frame[] _frames;
        private int _timer;
        private int _currentFrameIndex;

        /// <summary>
        /// This method will fix the animation update time.
        /// Updating per second/frame.
        /// </summary>
        /// <param name="deltaTime">This should be equivalent to GameTime.ElapsedGameTime?</param>
        public void Update(double deltaTime)
        {
            throw new NotImplementedException();
        }
    }
}