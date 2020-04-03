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
    public class RigidBody : Component
    {
        public static int PixelsPerUnit = 32;
        public static float DefaultGravity = 7f;

        public Vector2 Velocity { get; set; }
        public float Gravity { get; set; }
        public float Mass { get; set; }
        public float Friction { get; set; }
        public Vector2 Acceleration { get; set; }
        public float Power { get; set; }

        public RigidBody() : base()
        {
            Gravity = DefaultGravity;
            Mass = 1;
        }

        /// <summary>
        /// Adds velocity to the object.
        /// </summary>
        /// <param name="amount">Amount of velocity speed</param>
        public void AddVelocity(Vector2 amount)
        {
            Velocity += amount;
        }

        public void AddForce(Vector2 force)
        {
            Acceleration += force;
        }

        public void Update(double deltaTime)
        {
            float deltaSec = (float)deltaTime / 1000f;
            Velocity += Acceleration;
            AttachedTo.Transform.Position += Velocity;
            Acceleration += new Vector2(0, Gravity) * deltaSec;
        }

        public override Component Copy()=> throw new NotImplementedException();


        /// <summary>
        /// Creates a copy of Animator object.
        /// </summary>
        /// <returns>RigidBody object</returns>
        // public object Copy() => this.MemberwiseClone();

    }
}
