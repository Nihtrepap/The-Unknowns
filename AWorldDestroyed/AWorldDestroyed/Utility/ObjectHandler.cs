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
using AWorldDestroyed.Models.Components;
using Microsoft.Xna.Framework;

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
        /// Get all objects in the world within the provided range.
        /// </summary>
        /// <param name="bounds">The area to get objects from.</param>
        /// <returns>Returns all objects in the world that are within the provided range.</returns>
        public GameObject[] Query(RectangleF bounds) => quadTree.Query(bounds).ToArray();

        /// <summary>
        /// Update GameObjects handled by the ObjectHandler.
        /// </summary>
        /// <param name="deltaTime">Time in milliseconds since last update.</param>
        /// <param name="bounds"></param>
        public void Update(double deltaTime, RectangleF bounds)
        {
            quadTree = new QuadTree<GameObject>(new RectangleF(-1000, -1000, 2000, 2000), 3);
            foreach (GameObject obj in GameObjects)
            {
                quadTree.Insert(obj.Transform.WorldPosition, obj);
            }

            //TODO: Expand bounds
            GameObject[] objects = Query(bounds);

            // Update each GameObject
            foreach (GameObject obj in objects)
            {
                obj.Update(deltaTime);
            }

            for (int i = 0; i < objects.Length; i++)
            {
                GameObject mainObject = objects[i];
                if (mainObject.HasCollider)
                {
                    for (int j = i; j < objects.Length; j++)
                    {
                        if (mainObject == objects[j] ||
                            !objects[j].HasCollider) continue;

                        if (mainObject.HasComponent<RigidBody>())
                        {
                            ResolveCollision(mainObject, objects[j]);
                        }
                        else if (objects[j].HasComponent<RigidBody>())
                        {
                            ResolveCollision(objects[j], mainObject);
                        }
                    }
                }

                RigidBody rb = mainObject.GetComponent<RigidBody>();
                if (rb != null)
                    mainObject.Transform.Translate(rb.Velocity * (float)deltaTime);
            }
        }

        private bool IsTouchingLeft(RectangleF obj, Vector2 objVelocity, RectangleF other)
        {
            return obj.Right + objVelocity.X > other.Left 
                && obj.Left < other.Left 
                && obj.Bottom > other.Top 
                && obj.Top < other.Bottom;
        }

        private bool IsTouchingRight(RectangleF obj, Vector2 objVelocity, RectangleF other)
        {
            return obj.Left + objVelocity.X < other.Right 
                && obj.Right > other.Right 
                && obj.Bottom > other.Top 
                && obj.Top < other.Bottom;
        }

        private bool IsTouchingTop(RectangleF obj, Vector2 objVelocity, RectangleF other)
        {
            return obj.Bottom + objVelocity.Y > other.Top 
                && obj.Top < other.Top 
                && obj.Right > other.Left 
                && obj.Left < other.Right;
        }

        private bool IsTouchingBottom(RectangleF obj, Vector2 objVelocity, RectangleF other)
        {
            return obj.Top + objVelocity.Y < other.Bottom 
                && obj.Bottom > other.Bottom 
                && obj.Right > other.Left 
                && obj.Left < other.Right;
        }

        private void ResolveCollision(GameObject obj, GameObject other)
        {
            //if (!obj.HasCollider || !other.colliderEnabled || !other.alive) return;

            RigidBody objRigidbody = obj.GetComponent<RigidBody>();
            Collider objCollider = obj.GetComponent<Collider>();
            Collider otherCollider = other.GetComponent<Collider>();

            // when moving Down and hits another objects Top side
            if (objRigidbody.Velocity.Y > 0 && IsTouchingTop(objCollider.GetRectangle(), objRigidbody.Velocity, otherCollider.GetRectangle()))
            {
                if (!objCollider.IsTrigger && !otherCollider.IsTrigger)
                {
                    obj.Transform.Position = new Vector2(
                        obj.Transform.Position.X,
                        otherCollider.GetRectangle().Top - objCollider.Size.Y - objCollider.Offset.Y);

                    objRigidbody.Velocity *= Vector2.UnitX;

                    obj.OnCollision(other);
                    other.OnCollision(obj);
                }
                else
                {
                    if (objCollider.IsTrigger)
                        obj.OnTrigger(other);

                    if (otherCollider.IsTrigger)
                        other.OnTrigger(obj);
                }
            }
            // when moving Up and hits another objects Bottom side
            else if (objRigidbody.Velocity.Y < 0 && IsTouchingBottom(objCollider.GetRectangle(), objRigidbody.Velocity, otherCollider.GetRectangle()))
            {
                if (!objCollider.IsTrigger && !otherCollider.IsTrigger)
                {
                    obj.Transform.Position = new Vector2(
                        obj.Transform.Position.X,
                        otherCollider.GetRectangle().Bottom - objCollider.Offset.Y);

                    objRigidbody.Velocity *= Vector2.UnitX;

                    obj.OnCollision(other);
                    other.OnCollision(obj);
                }
                else
                {
                    if (objCollider.IsTrigger)
                        obj.OnTrigger(other);

                    if (otherCollider.IsTrigger)
                        other.OnTrigger(obj);
                }
            }

            //// when moving Right and hits another objects Left side
            if (objRigidbody.Velocity.X > 0 && IsTouchingLeft(objCollider.GetRectangle(), objRigidbody.Velocity, otherCollider.GetRectangle()))
            {
                if (!objCollider.IsTrigger && !otherCollider.IsTrigger)
                {
                    obj.Transform.Position = new Vector2(
                      otherCollider.GetRectangle().Left - objCollider.Size.X - objCollider.Offset.X,
                      obj.Transform.Position.Y);

                    objRigidbody.Velocity *= Vector2.UnitY;

                    obj.OnCollision(other);
                    other.OnCollision(obj);
                }
                else
                {
                    if (objCollider.IsTrigger)
                        obj.OnTrigger(other);

                    if (otherCollider.IsTrigger)
                        other.OnTrigger(obj);
                }

            }
            // when moving Left and hits another objects Right side
            else if (objRigidbody.Velocity.X < 0 && IsTouchingRight(objCollider.GetRectangle(), objRigidbody.Velocity, otherCollider.GetRectangle()))
            {
                if (!objCollider.IsTrigger && !otherCollider.IsTrigger)
                {
                    obj.Transform.Position = new Vector2(
                    otherCollider.GetRectangle().Right - objCollider.Offset.X,
                    obj.Transform.Position.Y);

                    objRigidbody.Velocity *= Vector2.UnitY;

                    obj.OnCollision(other);
                    other.OnCollision(obj);
                }
                else
                {
                    if (objCollider.IsTrigger)
                        obj.OnTrigger(other);

                    if (otherCollider.IsTrigger)
                        other.OnTrigger(obj);
                }
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
