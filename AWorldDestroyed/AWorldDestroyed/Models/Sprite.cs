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
    /// Defines a part of a Texture2D using a Rectangle.
    /// </summary>
    public class Sprite
    {
        public Texture2D Texture { get; set; }
        public Rectangle SourceRectangle { get; set; }
        public Vector2 Origin { get; set; }

        /// <summary>
        /// Initialize a new Sprite with a Texture2D.
        /// </summary>
        /// <param name="texture">A texture for the sprite.</param>
        public Sprite(Texture2D texture) 
        {
            Texture = texture;
            SourceRectangle = texture.Bounds;
        }

        /// <summary>
        /// Initialize a new Sprite with a Texture2D and Rectangle.
        /// </summary>
        /// <param name="texture">A texture for the sprite.</param>
        /// <param name="sourceRectangle">An optional region on the texture which will be rendered.</param>
        public Sprite(Texture2D texture, Rectangle sourceRectangle) 
        {
            Texture = texture;
            SourceRectangle = sourceRectangle;
        }

        /// <summary>
        /// Slice out many sprites from a Texture2D.
        /// </summary>
        /// <param name="texture">The spriteSheet to slice each Sprite from.</param>
        /// <param name="sourceRect">The first Sprite position and size. All other Sprite will use the same size.</param>
        /// <param name="frames">The number of Sprites to slice vertically and horizontally.</param>
        /// <param name="origin">An optional parameter that defines the origin of each sprite, if null, each sprite will have an origin of (0,0).</param>
        /// <returns>An array of sprites sliced from the texture.</returns>
        public static Sprite[] Slice(Texture2D texture, Rectangle sourceRect, Point frames, Vector2? origin = null)
        {
            Sprite[] sprites = new Sprite[frames.X * frames.Y];
            Vector2 orig = origin == null ? Vector2.Zero : (Vector2)origin;

            for (int row = 0; row < frames.Y; row++)
            {
                for (int col = 0; col < frames.X; col++)
                {
                    Point position = new Point(
                        sourceRect.X + sourceRect.Width * col,
                        sourceRect.Y + sourceRect.Height * row);

                    int i = row * frames.X + col;
                    sprites[i] = new Sprite(texture, new Rectangle(position, sourceRect.Size))
                    {
                        Origin = orig
                    };
                }
            }
            return sprites;
        }
    }
}
