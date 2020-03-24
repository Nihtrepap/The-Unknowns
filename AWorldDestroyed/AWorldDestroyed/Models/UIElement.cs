// =============================================
//         Editor:     Daniel Abdulahad
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
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace AWorldDestroyed.Models
{
    public class UIElement : SceneObject
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

        public UIElement() : base()
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