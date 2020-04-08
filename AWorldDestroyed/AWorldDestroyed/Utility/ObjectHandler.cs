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
        public RectangleF WorldSize { get; set; }

        private QuadTree<GameObject> quadTree;

        /// <summary>
        /// Initialize a new ObjectHandler.
        /// </summary>
        public ObjectHandler(RectangleF worldSize)
        {
            WorldSize = worldSize;

            GameObjects = new List<GameObject>();
            quadTree = new QuadTree<GameObject>(worldSize, 3);
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
            quadTree = new QuadTree<GameObject>(WorldSize, 3);
            for (int i = GameObjects.Count - 1; i >= 0; i--)
            {
                if (GameObjects[i].Destroyed) GameObjects.Remove(GameObjects[i]);
                else
                    quadTree.Insert(GameObjects[i].Transform.WorldPosition, GameObjects[i]);
            }
            
            GameObject[] objects = Query(bounds);

            // Update each GameObject
            foreach (GameObject obj in objects)
            {
                if (!obj.Enabled) continue;

                obj.Update(deltaTime);
            }
            
            // Collision handling.
            foreach (GameObject mainObject in objects)
            {
                if (!mainObject.Enabled) continue;

                if (mainObject.HasCollider && mainObject.HasComponent<RigidBody>())
                {
                    foreach (Collider collider in mainObject.GetComponents<Collider>())
                    {
                        if (!collider.Enabled) continue;

                        RectangleF objColRange = collider.GetRectangle();
                        List<GameObject> nearby = quadTree.Query(new RectangleF(
                            objColRange.X - (2 * 32),
                            objColRange.Y - (2 * 32),
                            objColRange.Width + (4 * 32),
                            objColRange.Height + (4 * 32)));
                        
                        foreach (GameObject other in nearby)
                        {
                            if (mainObject == other 
                                || !other.Enabled 
                                || !other.HasCollider) continue;

                            foreach (Collider otherCollider in other.GetComponents<Collider>())
                            {
                                if (!otherCollider.Enabled) continue;
                                ResolveCollision(mainObject, other, collider, otherCollider);
                                //else if (other.HasComponent<RigidBody>())
                                //    ResolveCollision(other, mainObject, otherCollider, collider);
                            }
                        }
                    }
                }

                RigidBody rb = mainObject.GetComponent<RigidBody>();
                if (rb != null && rb.Enabled)
                    mainObject.Transform.Translate(rb.Velocity);
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

        private void ResolveCollision(GameObject obj, GameObject other, Collider objCollider, Collider otherCollider)
        {
            //if (!obj.HasCollider || !other.colliderEnabled || !other.alive) return;
            //if (!objCollider.Enabled || !otherCollider.Enabled) return;

            RigidBody objRigidbody = obj.GetComponent<RigidBody>();
            //Collider objCollider = obj.GetComponent<Collider>();
            //Collider otherCollider = other.GetComponent<Collider>();

            // When moving Down and hits another objects Top side.
            if (/*objRigidbody.Velocity.Y > 0 &&*/ IsTouchingTop(objCollider.GetRectangle(), objRigidbody.Velocity, otherCollider.GetRectangle()))
            {
                if (!objCollider.IsTrigger && !otherCollider.IsTrigger)
                {
                    obj.Transform.Position = new Vector2(
                        obj.Transform.Position.X,
                        otherCollider.GetRectangle().Top - objCollider.Size.Y - objCollider.Offset.Y);

                    obj.OnCollision(other, Side.Bottom);
                    other.OnCollision(obj, Side.Top);

                    //objRigidbody.Velocity *= Vector2.UnitX;
                    objRigidbody.Velocity *= Vector2.UnitX * otherCollider.Friction;
                    objRigidbody.Acceleration *= Vector2.UnitX;
                }
          
                if (objCollider.IsTrigger)
                    obj.OnTrigger(other, Side.Bottom);

                if (otherCollider.IsTrigger)
                    other.OnTrigger(obj, Side.Top);
            }
            // When moving Up and hits another objects Bottom side.
            else if (/*objRigidbody.Velocity.Y < 0 && */IsTouchingBottom(objCollider.GetRectangle(), objRigidbody.Velocity, otherCollider.GetRectangle()))
            {
                if (!objCollider.IsTrigger && !otherCollider.IsTrigger)
                {
                    obj.Transform.Position = new Vector2(
                        obj.Transform.Position.X,
                        otherCollider.GetRectangle().Bottom - objCollider.Offset.Y);

                    obj.OnCollision(other, Side.Top);
                    other.OnCollision(obj, Side.Bottom);

                    objRigidbody.Velocity *= Vector2.UnitX;
                    objRigidbody.Acceleration *= Vector2.UnitX;
                }
             
                if (objCollider.IsTrigger)
                    obj.OnTrigger(other, Side.Top);

                if (otherCollider.IsTrigger)
                    other.OnTrigger(obj, Side.Bottom);
            }

            // When moving Right and hits another objects Left side.
            if (/*objRigidbody.Velocity.X > 0 && */IsTouchingLeft(objCollider.GetRectangle(), objRigidbody.Velocity, otherCollider.GetRectangle()))
            {
                if (!objCollider.IsTrigger && !otherCollider.IsTrigger)
                {
                    obj.Transform.Position = new Vector2(
                      otherCollider.GetRectangle().Left - objCollider.Size.X - objCollider.Offset.X,
                      obj.Transform.Position.Y);

                    obj.OnCollision(other, Side.Right);
                    other.OnCollision(obj, Side.Left);

                    objRigidbody.Velocity *= Vector2.UnitY;
                    //objRigidbody.Velocity *= new Vector2(otherCollider.Friction, 1);
                }
                
                if (objCollider.IsTrigger)
                    obj.OnTrigger(other, Side.Right);

                if (otherCollider.IsTrigger)
                    other.OnTrigger(obj, Side.Left);

            }
            // When moving Left and hits another objects Right side.
            else if (/*objRigidbody.Velocity.X < 0 && */IsTouchingRight(objCollider.GetRectangle(), objRigidbody.Velocity, otherCollider.GetRectangle()))
            {
                if (!objCollider.IsTrigger && !otherCollider.IsTrigger)
                {
                    obj.Transform.Position = new Vector2(
                    otherCollider.GetRectangle().Right - objCollider.Offset.X,
                    obj.Transform.Position.Y);

                    obj.OnCollision(other, Side.Left);
                    other.OnCollision(obj, Side.Right);

                    objRigidbody.Velocity *= Vector2.UnitY;
                    //objRigidbody.Acceleration *= otherCollider.Friction;
                }
                
                if (objCollider.IsTrigger)
                    obj.OnTrigger(other, Side.Left);

                if (otherCollider.IsTrigger)
                    other.OnTrigger(obj, Side.Right);
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

    public enum Side
    {
        Top,
        Bottom,
        Left,
        Right
    }
}
