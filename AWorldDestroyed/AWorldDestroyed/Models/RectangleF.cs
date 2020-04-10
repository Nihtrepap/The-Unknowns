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

using Microsoft.Xna.Framework;

namespace AWorldDestroyed
{
    /// <summary>
    /// Describes a floating point 2D-rectangle.
    /// </summary>
    public struct RectangleF
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        /// <summary>
        /// Creates a new instance of RectangleF struct, with the specified position, width, and height. 
        /// </summary>
        /// <param name="x">The x coordinate of the top-left corner of this RectangleF.</param>
        /// <param name="y">The y coordinate of the top-left corner of this RectangleF.</param>
        /// <param name="width">The width of this RectangleF.</param>
        /// <param name="height">The height of this RectangleF.</param>
        public RectangleF(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Creates a new instance of RectangleF struct, with the specified position and size.
        /// </summary>
        /// <param name="position">The top-left position of this RectangleF.</param>
        /// <param name="size">The size of this RectangleF.</param>
        public RectangleF(Vector2 position, Vector2 size)
        {
            X = position.X;
            Y = position.Y;
            Width = size.X;
            Height = size.Y;
        }

        /// <summary>
        /// Returns the x coordinate of the left edge of this RectangleF.
        /// </summary>
        public float Left => X;
        /// <summary>
        /// Returns the x coordinate of the right edge of this RectangleF.
        /// </summary>
        public float Right => X + Width;
        /// <summary>
        /// Returns the y coordinate of the top edge of this RectangleF.
        /// </summary>
        public float Top => Y;
        /// <summary>
        /// Returns the y coordinate of the bottom edge of this RectangleF.
        /// </summary>
        public float Bottom => Y + Height;

        /// <summary>
        /// The top-left coordinates of this RectangleF.
        /// </summary>
        public Vector2 Position
        {
            get => new Vector2(X, Y);

            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        /// <summary>
        /// The width-height coordinates of this RectangleF.
        /// </summary>
        public Vector2 Size
        {
            get => new Vector2(Width, Height);

            set
            {
                Width = value.X;
                Height = value.Y;
            }
        }

        #region Overriding
        /// <summary>
        /// Returns a String representation of this RectangleF in the format: {X:[X] Y:[Y] Width:[Width] Height:[Height]}
        /// </summary>
        /// <returns>String representation of this RectangleF.</returns>
        public override string ToString()
        {
            return $"{{X:{X} Y:{Y} Width:{Width} Height:{Height}}}";
        }

        /// <summary>
        /// Compares whether current instance is equal to specified Object.
        /// </summary>
        /// <param name="obj">The Object to compare.</param>
        /// <returns>true if the instances are equal; false otherwise.</returns>
        public override bool Equals(object obj)
        {
            if (obj is RectangleF other) return this == other;

            return false;
        }

        /// <summary>
        /// Gets the hash code of this RectangleF.
        /// </summary>
        /// <returns>Hash code of this RectangleF.</returns>
        public override int GetHashCode()
        {
            return ((int)Y ^ (int)X) + ((int)Width ^ (int)Height);
        }
        #endregion

        /// <summary>
        /// Gets whether or not the provided Vector2 lies within the bounds of this RectangleF.
        /// </summary>
        /// <param name="point">The coordinates to check for inclusion in this RectangleF.</param>
        /// <returns>true if the provided Vector2 lies inside this RectangleF; false otherwise.</returns>
        public bool Contains(Vector2 point)
        {
            return !((point.X < Left || point.X >= Right) || (point.Y < Top || point.Y >= Bottom));
        }

        /// <summary>
        /// Gets whether or not the provided RectangleF intersects with this RectangleF.
        /// </summary>
        /// <param name="other">The RectangleF to check for inclusion in this RectangleF.</param>
        /// <returns>true if the provided RectangleF intersects with this RectangleF; false otherwise.</returns>
        public bool Intersects(RectangleF other)
        {
            return !((other.Right < Left || other.Left >= Right) || (other.Bottom < Top || other.Top >= Bottom));
        }

        #region Operator overloading
        /// <summary>
        /// Explicitly convert a RectangleF to a Rectangle.
        /// </summary>
        /// <param name="rect">The RectangleF to convert.</param>
        public static explicit operator Rectangle(RectangleF rect)
        {
            return new Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
        }

        /// <summary>
        /// Implicitly convert a Rectangle to a RectangleF.
        /// </summary>
        /// <param name="rect">The Rectangle to convert.</param>
        public static implicit operator RectangleF(Rectangle rect)
        {
            return new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);
        }
        /// <summary>
        /// Make it possible to + two RectangleF.
        /// Operator overloading
        /// </summary>
        /// <param name="a">The left-side RectangleF.</param>
        /// <param name="b">The right-side RectangleF.</param>
        /// <returns></returns>
        public static RectangleF operator +(RectangleF a, RectangleF b)
        {
            return new RectangleF(a.Position + b.Position, a.Size + b.Size);
        }
        /// <summary>
        /// Make it possible to - two RectangleF.
        /// Operator overloading
        /// </summary>
        /// <param name="a">The left-side RectangleF.</param>
        /// <param name="b">The right-side RectangleF.</param>
        /// <returns></returns>
        public static RectangleF operator -(RectangleF a, RectangleF b)
        {
            return new RectangleF(a.Position - b.Position, a.Size - b.Size);
        }

        /// <summary>
        /// Compares whether two RectangleF instances are equal.
        /// </summary>
        /// <param name="a">The first RectangleF.</param>
        /// <param name="b">The second RectangleF.</param>
        /// <returns></returns>
        public static bool operator ==(RectangleF a, RectangleF b)
        {
            return a.Position == b.Position && a.Size == b.Size;
        }

        /// <summary>
        /// Compares whether two Rectangle instances are not equal.
        /// </summary>
        /// <param name="a">The first RectangleF.</param>
        /// <param name="b">The second RectangleF.</param>
        /// <returns></returns>
        public static bool operator !=(RectangleF a, RectangleF b)
        {
            return !(a == b);
        }
        #endregion
    }
}
