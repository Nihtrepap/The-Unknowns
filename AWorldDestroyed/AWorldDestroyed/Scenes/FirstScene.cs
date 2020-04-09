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
        private Player p = new Player();

        public override void Load()
        {
            Debug = false;

            MapData mapData = MapLoader.XmlMapReader(@"..\..\..\..\Content\Maps\Map_02.xml");

            Texture2D spriteSheet = ContentManager.Load<Texture2D>(@"..\..\..\..\Content\Sprites\Tiles\" + Path.GetFileNameWithoutExtension(mapData.TileSets[0].Source));
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

        protected override void OnObjectDraw(GameObject gameObject, float sortingOrder)
        {
            if (Debug)
            {
                if (gameObject is Player player)
                {
                    SpriteBatch.Draw(Game1.Pixel, new Rectangle(player.Transform.Position.ToPoint(), new Point(4)), null, Color.Black, 0f, Vector2.Zero, SpriteEffects.None, sortingOrder + 0.0001f);
                }
                if (gameObject is Enemy enemy)
                {
                    SpriteBatch.Draw(Game1.Pixel, new Rectangle(enemy.Transform.Position.ToPoint(), new Point(4)), null, Color.Black, 0f, Vector2.Zero, SpriteEffects.None, sortingOrder + 0.0001f);
                }
            }
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
            float healthWidth = (p.Health / p.MaxHealth) * 700f;
            Rectangle healthBar = new Rectangle(15, 15, (int)healthWidth, 20);
            //SpriteBatch.Draw(Game1.Pixel, new Rectangle(0, 0, 800, 30), null, Color.Orange);
            SpriteBatch.Draw(Game1.Pixel, healthBar, null, Color.Red);

            base.OnGUIDraw();
        }
    }

}
