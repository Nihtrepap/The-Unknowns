using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWorldDestroyed.Models
{
    /// <summary>
    /// Frame is a structure with Sprite.
    /// And will be used as a container for sprite sheets.
    /// </summary>
    public struct Frame
    {
        public Sprite Sprite { get; set; }
        public int Duration { get; set; }
        public delegate void Event();
    }
}