// =============================================
//         Editor:     Lone Maaherra
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
using AWorldDestroyed.Models.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AWorldDestroyed.Utility
{
    /// <summary>
    /// A camera like object that determines what is displayed on the screen.
    /// </summary>

    public class Camera
    {
        public Transform Transform { get; private set; }
        public Vector2 ViewSize { get; set; }

        /// <summary>
        /// Creates a new instance of the Camera class.
        /// </summary>
        /// <param name="viewSize">The view size of this Camera.</param>
        public Camera(Vector2 viewSize) 
        {
            ViewSize = viewSize;
            Transform = new Transform();
        }

        /// <summary>
        /// Get the area this Camera sees.
        /// </summary>
        public RectangleF View => new RectangleF(Transform.Position / Transform.Scale, ViewSize);

        /// <summary>
        /// Get the translation matrix of this camera; containing information about the position, rotation and scale.
        /// </summary>
        /// <returns>A matrix that contains information about the position, rotation and scale of this camera.</returns>
        public Matrix GetTranslationMatrix()
        {
            Vector3 position = new Vector3(-Transform.Position.X, -Transform.Position.Y, 0);
            Vector3 zoom = new Vector3(Transform.Scale.X, Transform.Scale.Y, 0);

            return Matrix.CreateTranslation(position) * Matrix.CreateScale(zoom);
        }

        /// <summary>
        /// Zoom the Camera a given amount towards a given position.
        /// </summary>
        /// <param name="amount">How much to zoom.</param>
        /// <param name="towards">The position to zoom towards.</param>
        public void Zoom(float amount, Vector2 towards)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Convert a point on the screen (camera) to a point in the world.
        /// </summary>
        /// <param name="point">The camera point to convert.</param>
        /// <returns>The world point of the given screen point.</returns>
        public Vector2 ScreenToWorldPoint(Vector2 point)
        {
            //return Vector2((point.x + self.transform.position.x) / self.transform.scale.x,
            //           (point.y + self.transform.position.y) / self.transform.scale.y)

            return new Vector2();
        }

        /// <summary>
        /// Convert a point in the world to a point on the screen (camera).
        /// </summary>
        /// <param name="point">The world point to convert.</param>
        /// <returns>The screen point of the given world point.</returns>
        public Vector2 WorldToScreenPoint(Vector2 point)
        {
            //return Vector2((point.x * self.transform.scale.x - self.transform.position.x),
            //           (point.y * self.transform.scale.y - self.transform.position.y))

            return new Vector2();
        }
    }
}
