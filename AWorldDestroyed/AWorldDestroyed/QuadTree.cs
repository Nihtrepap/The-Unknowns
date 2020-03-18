using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace AWorldDestroyed
{
    /// <summary>
    /// Are used to efficiently store data of points, witch can be used to check for collisions.
    /// </summary>
    /// <typeparam name="T">The data to store.</typeparam>
    public class QuadTree<T>
    {
        public Rectangle Boundary { get; set; }
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
        public QuadTree(Rectangle boundary, int capacity)
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

            if (NorthWest.Insert(point, obj)) return true;
            if (NorthEast.Insert(point, obj)) return true;
            if (SouthWest.Insert(point, obj)) return true;
            if (SouthEast.Insert(point, obj)) return true;

            return false;
        }

        /// <summary>
        /// Get all objects within the provided range.
        /// </summary>
        /// <param name="range">The area to get objects from.</param>
        /// <returns>Returns all objects that are within the provided range.</returns>
        public List<T> Query(Rectangle range)
        {
            List<T> found = new List<T>();

            if (Boundary.Intersects(range))
            {
                foreach (Tuple<Vector2, T> point in points)
                {
                    if (range.Contains(point.Item1)) found.Add(point.Item2);
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
            Point position = Boundary.Location;
            Point halfSize = new Point(Boundary.Width / 2, Boundary.Height / 2);

            Rectangle nw = new Rectangle(position, halfSize);
            Rectangle ne = new Rectangle(position + new Point(halfSize.X, 0), halfSize);
            Rectangle sw = new Rectangle(position + new Point(0, halfSize.Y), halfSize);
            Rectangle se = new Rectangle(position + halfSize, halfSize);

            NorthWest = new QuadTree<T>(nw, Capacity);
            NorthEast = new QuadTree<T>(ne, Capacity);
            SouthWest = new QuadTree<T>(sw, Capacity);
            SouthEast = new QuadTree<T>(se, Capacity);
            
            divided = true;
        }
    }
}