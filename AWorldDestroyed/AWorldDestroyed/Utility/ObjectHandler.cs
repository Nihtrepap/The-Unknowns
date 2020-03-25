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

namespace AWorldDestroyed.Utility
{
    /// <summary>
    /// Handles a collection of GameObjects with the help of a QuadTree.
    /// </summary>
    class ObjectHandler
    {
        public List<GameObject> GameObjects;
        private QuadTree<GameObject> quadTree;

        /// <summary>
        /// Initialize a new ObjectHandler.
        /// </summary>
        public ObjectHandler()
        {
            GameObjects = new List<GameObject>();
            quadTree = new QuadTree<GameObject>(); // TODO: Add boundary and capacitiy.
        }

        /// <summary>
        /// Update GameObjects handled by the ObjectHandler.
        /// </summary>
        public void Update()
        {
            throw new NotImplementedException();

            //foreach (GameObject gameObject in GameObjects)
            //{
            //    gameObject.Update();
            //}
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
