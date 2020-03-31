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

using Microsoft.Xna.Framework;

namespace AWorldDestroyed.Models.Components
{
    /// <summary>
    /// Define bounds for collision detection.
    /// </summary>
    class Collider : Component
    {
        public bool IsTrigger { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 Offset { get; set; }

        /// <summary>
        /// Creates a collider.
        /// </summary>
        /// <param name="size">Size.</param>
        public Collider(Vector2 size) :base()
        {
            Size = size;
            Offset = Vector2.Zero;
        }

        /// <summary>
        /// Get the bounds rectangle of this collider.
        /// </summary>
        /// <returns>Floating point 2D-rectangle.</returns>
        public RectangleF GetRectangle()
        {
            Vector2 position = AttachedTo.Transform.Position + Offset;
            return new RectangleF(position, Size);
        }

        /// <summary>
        /// Get a copy of this collider.
        /// </summary>
        /// <returns>Collider copy.</returns>
        public override Component Copy()
        {
            return new Collider(this.Size)
            {
                Offset = this.Offset
            };
        }
    }
}