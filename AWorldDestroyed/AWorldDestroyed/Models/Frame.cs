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

namespace AWorldDestroyed.Models
{
    /// <summary>
    /// To get frames for sprites.
    /// </summary>
    class Frame
    {
        public Sprite Sprite { get; set; }
        public int Duration { get; set; }
        public delegate void Event();
    }
}