using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace AWorldDestroyed
{
    /// <summary>
    /// Are used to efficiently store data of points, witch can be used to check for collisions.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class QuadTree<T>
    {
        public Rectangle Boundary { get; set; }
        public int Capacity { get; set; }

        private List<Tuple<Vector2, T>> points;
        private bool divided;

        private QuadTree<T> northWest;
        private QuadTree<T> northEast;
        private QuadTree<T> southWest;
        private QuadTree<T> southEast;

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
            if (Boundary.Contains(point)) return false;

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

            if (northWest.Insert(point, obj)) return true;
            if (northEast.Insert(point, obj)) return true;
            if (southWest.Insert(point, obj)) return true;
            if (southEast.Insert(point, obj)) return true;

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
                found = found.Concat(northWest.Query(range)).ToList();
                found = found.Concat(northEast.Query(range)).ToList();
                found = found.Concat(southWest.Query(range)).ToList();
                found = found.Concat(southEast.Query(range)).ToList();
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

            northWest = new QuadTree<T>(nw, Capacity);
            northEast = new QuadTree<T>(ne, Capacity);
            southWest = new QuadTree<T>(sw, Capacity);
            southEast = new QuadTree<T>(se, Capacity);
            
            divided = true;
        }
    }
}