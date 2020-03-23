using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWorldDestroyed.Models
{
    /// <summary>
    /// Check if objects are colliding.
    /// </summary>
    class Collider : Component
    {
        public Vector2 Size { get; set; }
        public Vector2 Offset { get; set; }

        public Collider(SceneObject sceneObject, Rectangle size) :base(sceneObject)
        {
        }

        public override Component Copy()
        {
            return this;
        }
    }
}
