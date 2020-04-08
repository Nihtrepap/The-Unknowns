using AWorldDestroyed.GameObjects;
using AWorldDestroyed.Map;
using AWorldDestroyed.Models;
using AWorldDestroyed.Models.Components;
using AWorldDestroyed.Scripts;
using AWorldDestroyed.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace AWorldDestroyed.Scenes
{
    class FirstScene : Scene
    {
        public FirstScene(SpriteBatch spriteBatch) : base(spriteBatch)
        {
        }

        public override void Load()
        {
            Debug = true;

            MapData mapData = MapLoader.XmlMapReader(@"..\..\..\..\Content\Maps\Map_02.xml");

            Texture2D spriteSheet = ContentManager.Load<Texture2D>(@"..\..\..\..\Content\Sprites\Tiles\" + Path.GetFileNameWithoutExtension(mapData.TileSets[0].Source));
            Point tileSize = new Point(mapData.TileWidth, mapData.TileHeight);
            for (int i = 0; i < mapData.Layers.Length; i++)
            {
                int soringOrder = i;
                bool solid = (i == 2);
                foreach (GameObject tile in MapLoader.LoadLayer(mapData.Layers[i], spriteSheet, tileSize, SortingLayer.Map, soringOrder, solid, new Vector2(-2020, -300)))
                {
                    AddObject(tile);
                }
            }

            Player p = new Player();
            AddObject(p);
            CameraFollow = p;

            for (int i = 0; i < 10; i++)
            {
            Enemy e = new Enemy(new Vector2(-700 + 100 * i, 250));
            AddObject(e);

            }
        }

        protected override void OnObjectDraw(GameObject gameObject, float sortingOrder)
        {
            //if (gameObject.HasComponent<RigidBody>())
            //{
            //    RectangleF objColRange = gameObject.GetComponent<Collider>().GetRectangle();
            //    RectangleF col = new RectangleF(
            //                objColRange.X - (2 * 32),
            //                objColRange.Y - (2 * 32),
            //                objColRange.Width + (4 * 32),
            //                objColRange.Height + (4 * 32));

            //    SpriteBatch.Draw(Game1.Pixel, (Rectangle)col, null, Color.BlueViolet * 0.6f, 0f, Vector2.Zero, SpriteEffects.None, sortingOrder);
            //}
        }

        protected override void OnGUIDraw()
        {
            //SpriteBatch.Draw(Game1.Pixel, new Rectangle(0, 0, 800, 30), null, Color.Orange);

            base.OnGUIDraw();
        }
    }

}
