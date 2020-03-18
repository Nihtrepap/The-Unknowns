using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AWorldDestroyed
{
    public static class Paint
    {
        private static Texture2D pixel;

        public static void Init(GraphicsDevice graphicsDevice)
        {
            pixel = new Texture2D(graphicsDevice, 1, 1);
            
            Color[] data = { Color.White };
            pixel.SetData(data);
        }

        public static void DrawRect(SpriteBatch spriteBatch, Rectangle rect, Color color)
        {
            spriteBatch.Draw(pixel, rect, color);
        }

        public static void DrawOutlinedRect(SpriteBatch spriteBatch, Rectangle rect, int stroke, Color color)
        {
            Rectangle topRect    = new Rectangle(rect.X, rect.Y, rect.Width, stroke);
            Rectangle bottomRect = new Rectangle(rect.X, rect.Bottom - stroke, rect.Width, stroke);
            Rectangle leftRect   = new Rectangle(rect.X, rect.Y, stroke, rect.Height);
            Rectangle rightRect  = new Rectangle(rect.Right - stroke, rect.Y, stroke, rect.Height);

            DrawRect(spriteBatch, topRect, color);
            DrawRect(spriteBatch, bottomRect, color);
            DrawRect(spriteBatch, leftRect, color);
            DrawRect(spriteBatch, rightRect, color);
        }
    }
}
