// =============================================
//         Editor:     Daniel Abdulahad
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
using Microsoft.Xna.Framework;

namespace AWorldDestroyed.Models.Components
{
    /// <summary>
    /// Position, rotation and scale of an object.
    /// </summary>
    public class Transform : Component
    {
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; }
        public float Rotation { get; set; }

        /// <summary>
        /// Creates a new instance of Transform.
        /// </summary>
        public Transform() : this(Vector2.Zero)
        {
        }

        /// <summary>
        /// Creates a new instance of Transform, with the specified position.
        /// </summary>
        /// <param name="position">The position of this Transform.</param>
        public Transform(Vector2 position) : this(position, Vector2.One, 0f)
        {
        }

        /// <summary>
        /// Creates a new instance of Transform, with the specified position, scale and rotation.
        /// </summary>
        /// <param name="position">The position of this Transform.</param>
        /// <param name="scale">The scale of this Transform.</param>
        /// <param name="rotation">The rotation of this Transform.</param>
        public Transform(Vector2 position, Vector2 scale, float rotation)
        {
            Position = position;
            Scale = scale;
            Rotation = rotation;
        }

        /// <summary>
        /// Get the normalized forward vector of this Transform.
        /// </summary>
        public Vector2 Forward
        {
            get
            {
                float x = (float)Math.Cos(Rotation);
                float y = (float)Math.Sin(Rotation);

                return new Vector2(x, y);
            }
        }

        /// <summary>
        /// Get the actual position in the world adjusted to the transform of the SceneObject parent.
        /// </summary>
        public Vector2 WorldPosition
        {
            get
            {
                if (AttachedTo?.Parent == null) return Position;
                else
                {
                    float angle = MathHelper.ToRadians(AttachedTo.Parent.Transform.WorldRotation);
                    Vector2 center = AttachedTo.Parent.Transform.WorldPosition;
                    Vector2 deltaPos =  Position;

                    float rotatedX = (float)(Math.Cos(angle) * deltaPos.X - Math.Sin(angle) * deltaPos.Y) + center.X;
                    float rotatedY = (float)(Math.Sin(angle) * deltaPos.X + Math.Cos(angle) * deltaPos.Y) + center.Y;

                    return new Vector2(rotatedX, rotatedY);
                }
            }
        }

        /// <summary>
        /// Get the actual rotation in the world adjusted to the rotation of the SceneObject parent.
        /// </summary>
        public float WorldRotation
        {
            get 
            { 
                if (AttachedTo?.Parent == null) return Rotation;
                else return Rotation + AttachedTo.Parent.Transform.WorldRotation;
            }
        }

        /// <summary>
        /// Get a copy of this Transform.
        /// </summary>
        /// <returns>A copy of this Transform.</returns>
        public override Component Copy()
        {
            return new Transform(Position, Scale, Rotation);
        }

        /// <summary>
        /// Rotates the transform so the forward vector points at target's current position.
        /// </summary>
        /// <param name="target">The Transform to point towards.</param>
        public void LookAt(Transform target)
        {
            LookAt(target.Position);
        }

        /// <summary>
        /// Rotates the transform so the forward vector points at the specified point.
        /// </summary>
        /// <param name="target">The point to point towards.</param>
        public void LookAt(Vector2 point)
        {
            Vector2 deltaPosition = Position - point;

            this.Rotation = -MathHelper.ToRadians((float)Math.Atan2(deltaPosition.Y, deltaPosition.X));
        }

        /// <summary>
        /// Moves the transform in the direction and distance of translation.
        /// </summary>
        /// <param name="translation">The distance to move.</param>
        public void Translate(Vector2 translation)
        {
            Position += translation;
        }

        /// <summary>
        /// Moves the transform by x along the x axis and y along the y axis.
        /// </summary>
        /// <param name="x">The amount to move along the x axis.</param>
        /// <param name="y">The amount to move along the y axis.</param>
        public void Translate(float x, float y)
        {
            Translate(new Vector2(x, y));
        }
    }
}
