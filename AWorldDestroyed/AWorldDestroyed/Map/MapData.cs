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
    public class Map
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

        public Map()
        {
        }
        //public MapData(string name, Model.Tile[,] mapdata)
        //{
        //    Name = name;

        //    Width = mapdata.GetLength(1);
        //    Height = mapdata.GetLength(0);

        //    Data = new string[Height][];
        //    for (int i = 0; i < Height; i++)
        //        Data[i] = new string[Width];

        //    for (int y = 0; y < Height; y++)
        //        for (int x = 0; x < Width; x++)
        //            Data[y][x] = mapdata[y, x].Name;
        //}
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
            get => Array.ConvertAll(rawData.Split(','), s => int.Parse(s));
        }
    }

    //[XmlRoot("data")]
    //public class Data
    //{
    //    [XmlElement("data")]
    //    public string rawData;
    //    public int[] 

    //    public Data()
    //    {

    //    }
    //}
}
