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
        public static float DefaultGravity = 0.0004f;

        public Vector2 Velocity { get; set; }
        public Vector2 MaxVelocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public Vector2 MaxAcceleration { get; set; }
        public float Gravity { get; set; }
        public float Mass { get; set; }
        public float Power { get; set; }

        /// <summary>
        /// Creates a new instance of the RigidBody class.
        /// </summary>
        public RigidBody() : base()
        {
            Gravity = DefaultGravity;
            Mass = 1;

            MaxVelocity = Vector2.One * 8f;
            MaxAcceleration = Vector2.One * 8f;
        }

        /// <summary>
        /// Adds velocity to the object.
        /// </summary>
        /// <param name="amount">Amount of velocity speed.</param>
        public void AddVelocity(Vector2 amount)
        {
            Velocity += amount;
        }

        /// <summary>
        /// Adds force/acceleration to the object.
        /// </summary>
        /// <param name="force">Amount of acceleration change.</param>
        public void AddForce(Vector2 force)
        {
            Acceleration += force;
        }

        /// <summary>
        /// Updates acceleration and velocity and makes sure that the values are within range.
        /// </summary>
        /// <param name="deltaTime">Time in milliseconds since last update.</param>
        public void Update(double deltaTime)
        {
            Acceleration += new Vector2(0, Gravity);
            Velocity += Acceleration * (float)deltaTime;

            Vector2 vel = Velocity;
            if (Velocity.X > MaxVelocity.X) vel.X = MaxVelocity.X;
            else if (Velocity.X < -MaxVelocity.X) vel.X = -MaxVelocity.X;
            if (Velocity.Y > MaxVelocity.Y) vel.Y = MaxVelocity.Y;
            else if (Velocity.Y < -MaxVelocity.Y) vel.Y = -MaxVelocity.Y;
            Velocity = vel;

            Vector2 acc = Acceleration;
            if (Acceleration.X > MaxAcceleration.X) acc.X = MaxAcceleration.X;
            else if (Acceleration.X < -MaxAcceleration.X) acc.X = -MaxAcceleration.X;
            if (Acceleration.Y > MaxAcceleration.Y) acc.Y = MaxAcceleration.Y;
            else if (Acceleration.Y < -MaxAcceleration.Y) acc.Y = -MaxAcceleration.Y;
            Acceleration = acc;
        }

        /// <summary>
        /// Creates a copy of the RigidBody instance with the same attribute values as this instance.
        /// </summary>
        /// <returns>A copy of this RigidBody instance.</returns>
        public override Component Copy() => throw new NotImplementedException();
    }
}
