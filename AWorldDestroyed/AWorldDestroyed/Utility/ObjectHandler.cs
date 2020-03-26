// =============================================
//         Editor:     Lone Maaherra
//         Last edit:  2020-03-19 
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
using AWorldDestroyed.Models;

namespace AWorldDestroyed.Utility
{
    /// <summary>
    /// Handles a collection of GameObjects with the help of a QuadTree.
    /// </summary>
    public class ObjectHandler
    {
        public List<GameObject> GameObjects { get; set; }
        private QuadTree<GameObject> quadTree;

        /// <summary>
        /// Initialize a new ObjectHandler.
        /// </summary>
        public ObjectHandler()
        {
            GameObjects = new List<GameObject>();
            quadTree = new QuadTree<GameObject>(new RectangleF(-1000, -1000, 2000, 2000), 3); // TODO: Add boundary and capacitiy.
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bounds"></param>
        /// <returns></returns>
        public GameObject[] Query(RectangleF bounds) => quadTree.Query(bounds).ToArray();

        /// <summary>
        /// Update GameObjects handled by the ObjectHandler.
        /// </summary>
        /// <param name="deltaTime">Time in milliseconds since last update.</param>
        /// <param name="bounds"></param>
        public void Update(double deltaTime, RectangleF bounds)
        {
            quadTree = new QuadTree<GameObject>(new RectangleF(-1000, -1000, 2000, 2000), 3);
            foreach (var o in GameObjects)
            {
                quadTree.Insert(o.Transform.Position, o);
            }

            //TODO: Expand bounds
            GameObject[] objects = Query(bounds);

            foreach (GameObject obj in objects)
            {
                obj.Update(deltaTime);

                /*
                 * if (obj have Collider)
                 * {
                 *      CheckCollision(obj, objects);
                 * }
                 * 
                 * void CheckCollision(GameObject obj, GameObject[] otherObjects)
                 * {
                 *      QuadTree<GameObject> others = new QuadTree<GameObject>(?, ?);
                 *      
                 *      foreach (var o in otherObjects)
                 *          others.Insert(o);
                 *          
                 *       GameObject[] closeObjects = others.Quary(obj.ColliderBounds);
                 * }
                 */
            }
        }

        /// <summary>
        /// Add a new GameObject to the list of objects to be handled.
        /// </summary>
        /// <param name="gameObject">The new object to handle.</param>
        public void AddObject(GameObject gameObject)
        {
            if (!GameObjects.Contains(gameObject)) GameObjects.Add(gameObject);
        }

        /// <summary>
        /// Remove all objects flagged as Destroyed from the list of objects to handle.
        /// </summary>
        private void RemoveDestroyed()
        {
            foreach (GameObject gameObject in GameObjects)
            {
                if (gameObject.Destroyed) GameObjects.Remove(gameObject);
            }
        }
    }
}
