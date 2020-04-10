// =============================================
//         Editor:     Lone Maaherra
//         Last edit:  2020-04-02 
// _-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_
//
//       (\                 >+{{{o)> - kvaouk
//    >+{{{{{0)> - kraouk      LL  
//       /_\_
//
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//                <333333><                     
//         <3333333><           <33333>< 

using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using AWorldDestroyed.Map;
using AWorldDestroyed.Models;
using AWorldDestroyed.Models.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AWorldDestroyed.Utility
{
    /// <summary>
    /// Provides static methods to load an XML map (from the Tiled application), 
    /// with elements and attributes that match the MapData class properties.
    /// </summary>
    public static class MapLoader
    {
        /// <summary>
        /// Read an xml file and create an instance of the MapData class.
        /// </summary>
        /// <param name="path">The path to the xml file.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Translate Layers into GameObject tiles.
        /// </summary>
        /// <param name="layer">The Layer object to translate.</param>
        /// <param name="spriteSheet">The spritesheet that connects the Layer data to a tile with different looks.</param>
        /// <param name="tileSize">The width and hight of one tile.</param>
        /// <param name="sortingLayer">The sorting layer of the sprite.</param>
        /// <param name="sortingOrder">The sorting order of the sprite within the sorting layer.</param>
        /// <param name="isSolid">Indicates whether the GameObject tiles in this Layer should have a RigidBody or not.</param>
        /// <param name="worldOffset">The world offset of the top left corner of the layer.</param>
        /// <returns></returns>
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
