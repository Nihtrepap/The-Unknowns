using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AWorldDestroyed.Map
{
    [Serializable]
    [XmlRoot("map")]
    public class MapData
    {
        [XmlAttribute("width")]
        public int Width;
        [XmlAttribute("height")]
        public int Height;
        [XmlAttribute("tilewidth")]
        public int Tilewidth;
        [XmlAttribute("tileheight")]
        public int Tileheight;

        [XmlElement("layer")]
        public Layer[] Layers;

        [XmlElement("tileset")]
        public TileSet[] TileSets;

        public MapData()
        {
        }
    }

    [Serializable]
    [XmlRoot("layer")]
    public class Layer
    {
        [XmlAttribute("name")]
        public string Name;
        [XmlAttribute("id")]
        public int Id;
        [XmlAttribute("width")]
        public int Width;
        [XmlAttribute("height")]
        public int Height;

        [XmlElement("data")]
        public string rawData;

        private int[] _data;

        public Layer()
        {
        }

        public int[] Data
        {
            get
            {
                if (_data == null) return _data = Array.ConvertAll(rawData.Split(','), s => int.Parse(s));
                else return _data;
            }
        }
    }

    public class TileSet
    {
        [XmlAttribute("firstgid")]
        public int FirstGId;

        [XmlAttribute("source")]
        public string Source;
    }
}
