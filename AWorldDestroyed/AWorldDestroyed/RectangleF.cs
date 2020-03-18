using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace AWorldDestroyed
{
    public struct RectangleF
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public RectangleF(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public RectangleF(Vector2 position, Vector2 size)
        {
            X = position.X;
            Y = position.Y;
            Width = size.X;
            Height = size.Y;
        }

        public float Left => X;
        public float Right => X + Width;
        public float Top => Y;
        public float Bottom => Y + Height;

        public Vector2 Position
        {
            get
            {
                return new Vector2(X, Y);
            }

            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        public Vector2 Size
        {
            get
            {
                return new Vector2(Width, Height);
            }

            set
            {
                Width = value.X;
                Height = value.Y;
            }
        }

        public override string ToString()
        {
            return $"X: {X}, Y: {Y}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is RectangleF rectF)
                return this == rectF;
            if (obj is Rectangle rect)
                return this == (RectangleF)rect;

            return false;
        }

        public override int GetHashCode()
        {
            return ((int)Y ^ (int)X) + ((int)Width ^ (int)Height);
        }

        public bool Contains(Vector2 point)
        {
            return !((point.X < Left || point.X >= Right) || (point.Y < Top || point.Y >= Bottom));
        }

        public bool Intersects(RectangleF other)
        {
            return !((other.Right < Left || other.Left >= Right) || (other.Bottom < Top || other.Top >= Bottom));
        }

        //public static explicit operator Rectangle(RectangleF rect)
        //{
        //    return new Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
        //}

        public static implicit operator Rectangle(RectangleF rect)
        {
            return new Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
        }

        public static implicit operator RectangleF(Rectangle rect)
        {
            return new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static bool operator ==(RectangleF a, RectangleF b)
        {
            return a.Position == b.Position && a.Size == b.Size;
        }

        public static bool operator !=(RectangleF a, RectangleF b)
        {
            return !(a == b);
        }
    }
}
