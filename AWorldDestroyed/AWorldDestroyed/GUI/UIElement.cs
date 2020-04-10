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

using AWorldDestroyed.Models;
using AWorldDestroyed.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace AWorldDestroyed.GUI
{
    /// <summary>
    /// Base class for all UIElements.
    /// </summary>
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

        /// <summary>
        /// Create a new instance of the UIElement class.
        /// </summary>
        public UIElement() : base()
        {
            throw new NotImplementedException();
        }
    }
}