using System;
using System.Xml.Serialization;

namespace AWorldDestroyed.Map
{
    /// <summary>
    /// Represents a game map with a number of object layers and tile sets.
    /// </summary>
    [Serializable]
    [XmlRoot("map")]
    public class MapData
    {
        // Width and Height in number of tile squares.
        [XmlAttribute("width")]
        public int Width;
        [XmlAttribute("height")]
        public int Height;
        [XmlAttribute("tilewidth")]
        public int TileWidth;
        [XmlAttribute("tileheight")]
        public int TileHeight;

        [XmlElement("layer")]
        public Layer[] Layers;

        [XmlElement("tileset")]
        public TileSet[] TileSets;
    }

    /// <summary>
    /// Represents a layer of maptile data in the form of ints that represent 
    /// a position in a spritesheet, read from left to right, top to bottom.
    /// </summary>
    [Serializable]
    [XmlRoot("layer")]
    public class Layer
    {
        [XmlAttribute("name")]
        public string Name;
        [XmlAttribute("id")]
        public int Id;
        // Width and Height in number of tile squares.
        [XmlAttribute("width")]
        public int Width;
        [XmlAttribute("height")]
        public int Height;
        // The map as a csv string of numbers.
        [XmlElement("data")]
        public string rawData;
        // The map as an array of ints.
        private int[] _data;

        /// <summary>
        /// Simulates a Data property. Translates the rawdata csv string to an array of ints.
        /// </summary>
        public int[] Data
        {
            get
            {
                if (_data == null) return _data = Array.ConvertAll(rawData.Split(','), s => int.Parse(s));
                else return _data;
            }
        }
    }

    /// <summary>
    /// Represents a spritesheet of tiles, with a first ID and a source path to the spritesheet image file.
    /// </summary>
    public class TileSet
    {
        [XmlAttribute("firstgid")]
        public int FirstGId;

        [XmlAttribute("source")]
        public string Source;
    }
}
