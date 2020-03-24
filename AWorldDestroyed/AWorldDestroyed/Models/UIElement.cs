using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWorldDestroyed.Models
{
    public class UIElement
    {
        public static SpriteFont DefaultFont { get; set; }
        public static Color DefaultForeColor { get; set; }
        public static Color DefaultBackColor { get; set; }

        protected virtual Vector2 DefaultSize { get; }

        public SpriteFont Font { get; set; }
        public Color ForeColor { get; set; }
        public Color BackColor { get; set; }
        public Rectangle Bounds { get; set; }
        public Rectangle Margin { get; set; }

        public event MouseEventHandler MouseClick;
        public event MouseEventHandler MouseDown;
        public event MouseEventHandler MouseUp;
        public event KeyEventHandler KeyPress;
        public event KeyEventHandler KeyDown;
        public event KeyEventHandler KeyUp;

        public UIElement()
        {

        }
    }

    public delegate void MouseEventHandler(object sender, MouseEventArgs e);
    public delegate void KeyEventHandler(object sender, KeyEventArgs e);

    public class KeyEventArgs : EventArgs
    {
        public Keys Key { get; }

        public KeyEventArgs(Keys key)
        {
            Key = key;
        }
    }

    public class MouseEventArgs : EventArgs
    {
        public Point Position { get; }
        public int X { get; }
        public int Y { get; }
        public int Wheel { get; }

        public MouseEventArgs(Point position) : this(position, 0)
        {
        }

        public MouseEventArgs(Point position, int wheel)
        {
            Position = position;
            Wheel = wheel;

            X = position.X;
            Y = position.Y;
        }
    }
}