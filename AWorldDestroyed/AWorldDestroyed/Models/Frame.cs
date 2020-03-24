// =============================================
//         Editor:     Philip  Abrahamsson
//         Last edit:  2020-03-24 
// _-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_
//
//       (\                 >+{{{o)> - kvaouk
//    >+{{{{{0)> - kraouk      LL  
//       /_\_
//
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//                <333333><                     
//         <3333333><           <33333>< 

namespace AWorldDestroyed.Models
{
    /// <summary>
    /// A single frame of an animation.
    /// </summary>
    public struct Frame
    {
        public readonly Sprite Sprite;
        public readonly int Duration;
        public event FrameEvent Event;

        /// <summary>
        /// Create a new Frame with given Sprite and duration.
        /// </summary>
        /// <param name="sprite">The Sprite of the frame.</param>
        /// <param name="duration">The duration of the frame.</param>
        public Frame(Sprite sprite, int duration)
        {
            Sprite = sprite;
            Duration = duration;
            Event = null;
        }

        /// <summary>
        /// Invoke the Event attached to this Frame.
        /// </summary>
        public void InvokeEvent()
        {
            Event?.Invoke();
        }
    }

    public delegate void FrameEvent();
}