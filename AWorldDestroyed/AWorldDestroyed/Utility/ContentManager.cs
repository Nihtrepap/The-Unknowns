using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWorldDestroyed.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AWorldDestroyed.Utility
{
    public static class ContentManager
    {
        public static Texture2D Pixel { get; private set; }

        private static Dictionary<string, Texture2D> textures;
        private static Microsoft.Xna.Framework.Content.ContentManager _content;

        public static void Init(Microsoft.Xna.Framework.Content.ContentManager content, GraphicsDevice graphicsDevice)
        {
            _content = content;

            textures = new Dictionary<string, Texture2D>();
            Pixel = new Texture2D(graphicsDevice, 1, 1);
            Color[] data = { Color.White };
            Pixel.SetData(data);
        }

        public static void AddTexture(string name, string path)
        {
            if (textures.ContainsKey(name)) return;
            
            textures.Add(name, _content.Load<Texture2D>(path));
        }

        public static Texture2D GetTexture(string name)
        {
            if (!textures.ContainsKey(name)) return null;

            return textures[name];
        }

        public static T Load<T>(string path)
        {
            return _content.Load<T>(path);
        }
    }
}
