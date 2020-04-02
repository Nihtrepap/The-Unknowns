using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using AWorldDestroyed.Map;
using AWorldDestroyed.Models;
using AWorldDestroyed.Models.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AWorldDestroyed.Utility
{
    public static class MapLoader
    {
        public static MapData XmlMapReader(string path)
        {
            MapData map;
            XmlSerializer serializer = new XmlSerializer(typeof(MapData));
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                map = serializer.Deserialize(fs) as MapData;
            }

            return map;
        }
        
        public static IEnumerable<GameObject> LoadLayer(Layer layer, Texture2D spriteSheet, Point tileSize, SortingLayer sortingLayer, int sortingOrder, bool isSolid, Vector2 worldOffset)
        {
            for (int y = 0; y < layer.Height; y++)
            {
                for (int x = 0; x < layer.Width; x++)
                {
                    int i = y * layer.Width + x;
                    int id = layer.Data[i];
                    if (id != 0)
                    {

                        Vector2 position = new Vector2(worldOffset.X + x * tileSize.X, worldOffset.Y + y * tileSize.Y);
                        GameObject gameObject = new GameObject(new Transform(position));

                        Point tilePos = new Point(
                            (id - 1) % (spriteSheet.Width / tileSize.X),
                            (id - 1) / (spriteSheet.Width / tileSize.X));
                        Sprite sprite = new Sprite(spriteSheet, new Rectangle(tilePos * tileSize, tileSize));
                        SpriteRenderer renderer = new SpriteRenderer(sprite)
                        {
                            SortingLayer = sortingLayer,
                            SortingOrder = sortingOrder
                        };
                        gameObject.AddComponent(renderer);

                        if (isSolid)
                        {
                            Collider collider = new Collider(tileSize.ToVector2());
                            gameObject.AddComponent(collider);
                        }

                        yield return gameObject;
                    }
                    else
                        yield return null;
                }
            }
        }
    }
}
