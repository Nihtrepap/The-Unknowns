using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AWorldDestroyed.Utility
{
    /// <summary>
    /// Static class that acts as a runtime library of 2D textures.
    /// Holds a reference to the MonoGame ContentManager that provides a link to the Monogame content pipeline tool.
    /// </summary>
    public static class ContentManager
    {
        public static Texture2D Pixel { get; private set; }

        private static Dictionary<string, Texture2D> textures;
        private static Microsoft.Xna.Framework.Content.ContentManager _content;

        /// <summary>
        /// Initiates the ContentManager.
        /// This method must be called before using the ContentManager for the first time.
        /// </summary>
        /// <param name="content">A MonoGame ContentManager.</param>
        /// <param name="graphicsDevice">A MonoGame GraphicsDevice.</param>
        public static void Init(Microsoft.Xna.Framework.Content.ContentManager content, GraphicsDevice graphicsDevice)
        {
            _content = content;

            textures = new Dictionary<string, Texture2D>();
            Pixel = new Texture2D(graphicsDevice, 1, 1);
            Color[] data = { Color.White };
            Pixel.SetData(data);
        }

        /// <summary>
        /// Add a Texture2D to the ContentManager storage of 2D textures.
        /// </summary>
        /// <param name="name">The name/key for later acces of the texture.</param>
        /// <param name="path">The path to the pipelined image file.</param>
        public static void AddTexture(string name, string path)
        {
            if (textures.ContainsKey(name)) return;
            
            textures.Add(name, _content.Load<Texture2D>(path));
        }

        /// <summary>
        /// Get a 2D texture from the ContentManager storage.
        /// Returns null if no texture is found.
        /// </summary>
        /// <param name="name">The name/key to acces the texture.</param>
        /// <returns>The Texture2D object associated with the given name.</returns>
        public static Texture2D GetTexture(string name)
        {
            if (!textures.ContainsKey(name)) return null;
            return textures[name];
        }

        /// <summary>
        /// Load content via the pipeline tool.
        /// Does not save the texture in the ContentManager storage.
        /// </summary>
        /// <typeparam name="T">The Type of the content to be loaded.</typeparam>
        /// <param name="path">The path to the pipelined content.</param>
        /// <returns></returns>
        public static T Load<T>(string path)
        {
            return _content.Load<T>(path);
        }
    }
}
