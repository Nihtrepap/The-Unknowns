// =============================================
//         Editor:     Daniel Abdulahad
//         Last edit:  2020-03-30 
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

namespace AWorldDestroyed.Models.Components
{
    public abstract class Script : Component, IUpdateable
    {
        /// <summary>
        /// Defines what happens when the script is updated.
        /// </summary>
        /// <param name="deltaTime">Time in milliseconds since last update.</param>
        public abstract void Update(double deltaTime);

        /// <summary>
        /// Defines a way to make a copy of the Script instance with the 
        /// same attribute values as this instance.
        /// </summary>
        /// <returns>A copy of this Script instance.</returns>
        public abstract override Component Copy();
    }
}
