using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWorldDestroyed
{
    /// <summary>
    /// Sprite class for Sprites ;)
    /// </summary>
    public class Sprite
    {
        public Texture2D Texture;
        public Rectangle SourceRectangle;
        public Vector2 Origin;

        /// <summary>
        /// Initialize a new Sprite with Texture
        /// </summary>
        /// <param name="texture"></param>
        public Sprite(Texture2D texture) 
        {
            Texture = texture;
        }
        /// <summary>
        /// Initialize a new Sprite with Texture and Rectangle
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="sourceRectangle"></param>
        public Sprite(Texture2D texture, Rectangle sourceRectangle) 
        {
            Texture = texture;
            SourceRectangle = sourceRectangle;
        }

    }
}
