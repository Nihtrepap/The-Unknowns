// =============================================
//         Editor:     Philip Abrahamsson
//         Last edit:  2020-03-18 
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

namespace AWorldDestroyed.Models
{
    /// <summary>
    /// A texture.
    /// </summary>
    public class Sprite
    {
        public Texture2D Texture { get; set; }
        public Rectangle SourceRectangle { get; set; }
        public Vector2 Origin { get; set; }

        /// <summary>
        /// Initialize a new Sprite with Texture.
        /// </summary>
        /// <param name="texture">A texture for the sprite.</param>
        public Sprite(Texture2D texture) 
        {
            Texture = texture;
            SourceRectangle = texture.Bounds;
        }

        /// <summary>
        /// Initialize a new Sprite with Texture and Rectangle.
        /// </summary>
        /// <param name="texture">A texture for the sprite.</param>
        /// <param name="sourceRectangle">An optional region on the texture which will be rendered.</param>
        public Sprite(Texture2D texture, Rectangle sourceRectangle) 
        {
            Texture = texture;
            SourceRectangle = sourceRectangle;
        }

        public static Sprite[] Slice(Texture2D texture, Rectangle sourceRect, Point frames)
        {
            Sprite[] sprites = new Sprite[frames.X * frames.Y];

            for (int row = 0; row < frames.Y; row++)
            {
                for (int col = 0; col < frames.X; col++)
                {
                    Point position = new Point(
                        sourceRect.X + sourceRect.Width * col,
                        sourceRect.Y + sourceRect.Height * row);

                    int i = row * frames.X + col;
                    sprites[i] = new Sprite(texture, new Rectangle(position, sourceRect.Size));
                }
            }
            return sprites;
        }
    }
}
