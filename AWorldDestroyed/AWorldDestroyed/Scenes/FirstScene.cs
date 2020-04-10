using AWorldDestroyed.GameObjects;
using AWorldDestroyed.Map;
using AWorldDestroyed.Models;
using AWorldDestroyed.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace AWorldDestroyed.Scenes
{
    /// <summary>
    /// This class is used to draw and load with SpriteBatch.
    /// Mainly a test class.
    /// </summary>
    class FirstScene : Scene
    {
        private Player p = new Player();

        /// <summary>
        /// Creates a new FirstScene, with the specified spriteBatch. 
        /// </summary>
        /// <param name="spriteBatch">A MonoGame SpriteBatch.</param>
        public FirstScene(SpriteBatch spriteBatch) : base(spriteBatch)
        {
        }

        /// <summary>
        /// Loads all GameObjects and UIElements in this Scene.
        /// </summary>
        public override void Load()
        {
            Debug = false;

            // Load content.
            MapData mapData = MapLoader.XmlMapReader(@"..\..\..\..\Content\Maps\Map_02.xml");
            Texture2D spriteSheet = ContentManager.Load<Texture2D>(@"..\..\..\..\Content\Sprites\Tiles\" + Path.GetFileNameWithoutExtension(mapData.TileSets[0].Source));

            // Load map.
            Point tileSize = new Point(mapData.TileWidth, mapData.TileHeight);
            Vector2 mapOffset = new Vector2(-2020, -300);
            Vector2 maxMapSize = new Vector2(mapData.Width, mapData.Height);
            for (int i = 0; i < mapData.Layers.Length; i++)
            {
                int soringOrder = i;
                bool solid = (i == 2);
                foreach (GameObject tile in MapLoader.LoadLayer(mapData.Layers[i], spriteSheet, tileSize, SortingLayer.Map, soringOrder, solid, mapOffset))
                    AddObject(tile);
            }

            CameraMin = mapOffset;
            CameraMax = maxMapSize * tileSize.ToVector2() + mapOffset;

            objectHandler.WorldSize = new RectangleF((Vector2)CameraMin, maxMapSize * tileSize.ToVector2());

            AddObject(p);
            CameraFollow = p;

            for (int i = 0; i < 6; i++)
            {
                Enemy e = new Enemy(new Vector2(-700 + 100 * i, 250));
                AddObject(e);
            }
        }

        /// <summary>
        /// Here we set what to happen with objects on draw. 
        /// </summary>
        /// <param name="gameObject">A gameobject.</param>
        /// <param name="sortingOrder">A sorting order.</param>
        protected override void OnObjectDraw(GameObject gameObject, float sortingOrder)
        {
            if (Debug)
            {
                if (gameObject.Tag == Tag.Player || gameObject.Tag == Tag.Enemy)
                {
                    SpriteBatch.Draw(ContentManager.Pixel,
                        new Rectangle(gameObject.Transform.Position.ToPoint(),
                        new Point(4)), 
                        null, 
                        Color.Black, 
                        0f, 
                        Vector2.Zero,
                        SpriteEffects.None, 
                        sortingOrder + 0.0001f);
                }
            }
        }

        /// <summary>
        /// Here we set what to happen with GUI on draw.
        /// </summary>
        protected override void OnGUIDraw()
        {
            float healthWidth = (p.Health / p.MaxHealth) * 200f;
            Rectangle healthBar = new Rectangle(15, 15, (int)healthWidth, 20);
            SpriteBatch.Draw(ContentManager.Pixel, healthBar, null, Color.Red);

            base.OnGUIDraw();
        }
    }

}
