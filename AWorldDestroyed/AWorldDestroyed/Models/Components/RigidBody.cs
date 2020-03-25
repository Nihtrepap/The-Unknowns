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

namespace AWorldDestroyed.Models.Components
{
    /// <summary>
    /// Physics for our sprites.
    /// </summary>
    class RigidBody : Component
    {
        public Vector2 Velocity { get; set; }
        public float Gravity { get; set; }
        public float Mass { get; set; }
        public float Friction { get; set; }

        public RigidBody(GameObject gameObject) : base(gameObject) { }

        /// <summary>
        /// Adds velocity to the object.
        /// </summary>
        /// <param name="amount">Amount of velocity speed</param>
        public void AddVelocity(Vector2 amount)
        {
            Velocity += amount;
        }

        public override Component Copy()=> throw new NotImplementedException();

        /// <summary>
        /// Creates a copy of Animator object.
        /// </summary>
        /// <returns>RigidBody object</returns>
        // public object Copy() => this.MemberwiseClone();
                
    }
}
