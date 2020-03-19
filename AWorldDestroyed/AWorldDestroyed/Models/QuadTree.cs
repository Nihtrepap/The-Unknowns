// =============================================
//         Editor:     Daniel Abdulahad
//         Last edit:  2020-03-18 
// _-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_
//
//       (\                 >+{{{o)> - kvaouk
//    >+{{{{{0)> - kraouk      LL  
//       /_\_
//
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//                <333333><                     
//         <3333333><           <33333>< 

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace AWorldDestroyed.Models
{
    /// <summary>
    /// Are used to efficiently store data of points, witch can be used to check for collisions.
    /// </summary>
    /// <typeparam name="T">The type of object to store at every point.</typeparam>
    public class QuadTree<T>
    {
        public RectangleF Boundary { get; set; }
        public int Capacity { get; set; }
        public QuadTree<T> NorthWest { get; private set; }
        public QuadTree<T> NorthEast { get; private set; }
        public QuadTree<T> SouthWest { get; private set; }
        public QuadTree<T> SouthEast { get; private set; }

        private List<Tuple<Vector2, T>> points;
        private bool divided;

        /// <summary>
        /// Initializes a new instance of the QuadTree class with a given boundary and capacity.
        /// </summary>
        /// <param name="boundary">The size of the QuadTree</param>
        /// <param name="capacity">The maximum amount of points to insert to the QuadTree before subdividing</param>
        public QuadTree(RectangleF boundary, int capacity)
        {
            Boundary = boundary;
            Capacity = capacity;

            points = new List<Tuple<Vector2, T>>(capacity);
        }

        /// <summary>
        /// Insert a point into the QuadTree.
        /// </summary>
        /// <param name="point">Position where to insert the point.</param>
        /// <param name="obj">The object that is attached to the point.</param>
        /// <returns>Returns true if the point was successfully inserted into this QuadTree, otherwise false.</returns>
        public bool Insert(Vector2 point, T obj)
        {
            if (!Boundary.Contains(point)) return false;

            if (points.Count < Capacity)
            {
                // Inserted a point successfully.
                points.Add(new Tuple<Vector2, T>(point, obj));
                return true;
            }
            else if (!divided)
            {
                Subdivide();
            }

            if (NorthWest.Insert(point, obj))
                return true;
            if (NorthEast.Insert(point, obj))
                return true;
            if (SouthWest.Insert(point, obj))
                return true;
            if (SouthEast.Insert(point, obj))
                return true;

            return false;
        }

        /// <summary>
        /// Get all objects within the provided range.
        /// </summary>
        /// <param name="range">The area to get objects from.</param>
        /// <returns>Returns all objects that are within the provided range.</returns>
        public List<T> Query(RectangleF range)
        {
            List<T> found = new List<T>();

            if (Boundary.Intersects(range))
            {
                foreach (Tuple<Vector2, T> point in points)
                {
                    if (range.Contains(point.Item1))
                        found.Add(point.Item2);
                }
            }
            else
                return found;

            if (divided)
            {
                found = found.Concat(NorthWest.Query(range)).ToList();
                found = found.Concat(NorthEast.Query(range)).ToList();
                found = found.Concat(SouthWest.Query(range)).ToList();
                found = found.Concat(SouthEast.Query(range)).ToList();
            }

            return found;
        }

        /// <summary>
        /// Subdivides the QuadTree into four new QuadTrees.
        /// </summary>
        private void Subdivide()
        {
            Vector2 position = Boundary.Position;
            Vector2 halfSize = Boundary.Size / 2f;

            RectangleF nw = new RectangleF(position, halfSize);
            RectangleF ne = new RectangleF(position + new Vector2(halfSize.X, 0), halfSize);
            RectangleF sw = new RectangleF(position + new Vector2(0, halfSize.Y), halfSize);
            RectangleF se = new RectangleF(position + halfSize, halfSize);

            NorthWest = new QuadTree<T>(nw, Capacity);
            NorthEast = new QuadTree<T>(ne, Capacity);
            SouthWest = new QuadTree<T>(sw, Capacity);
            SouthEast = new QuadTree<T>(se, Capacity);

            divided = true;
        }
    }
}
