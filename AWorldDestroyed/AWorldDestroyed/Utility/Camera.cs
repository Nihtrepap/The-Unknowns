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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AWorldDestroyed.Utility
{
    /// <summary>
    /// A camera like object that determines what is displayed on the screen.
    /// </summary>

    class Camera
    {
        public Transform Transform { get; private set; }
        public SpriteBatch SpriteBatch { get; set; }

        /// <summary>
        /// Creates a new instance of the Camera class.
        /// </summary>
        public Camera()
        {
            Transform = new Transform();
        }

        /// <summary>
        /// Creates a new instance of the Camera class, connected to a given SpriteBatch.
        /// </summary>
        /// <param name="spriteBatch">A MonoGame SpriteBatch.</param>
        public Camera(SpriteBatch spriteBatch) : this()
        {
            SpriteBatch = spriteBatch;
        }

        /// <summary>
        /// Update the logic of the Camera.
        /// </summary>
        public void Update()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Zoom the Camera a given amount towards a given position.
        /// </summary>
        /// <param name="amount">How much to zoom.</param>
        /// <param name="towards">A position, given by a Vector2, to zoom towards.</param>
        public void Zoom(float amount, Vector2 towards)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Convert a point on the screen (camera) to a point in the world.
        /// </summary>
        /// <param name="point">The camera point to convert.</param>
        /// <returns></returns>
        public Vector2 ScreenToWorldPoint(Point point)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Convert a point in the world to a point on the screen (camera).
        /// </summary>
        /// <param name="point">The world point to convert.</param>
        /// <returns></returns>
        public Vector2 WorldToScreenPoint(Point point)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Render what the Camera sees to the screen.
        /// </summary>
        public void Render()
        {
            throw new NotImplementedException();
        }
    }
}
