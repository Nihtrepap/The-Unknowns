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
        /// <param name="worldSize">A RectangleF with the size in pixels and top left position of the world.</param>
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
        /// Update GameObjects handled by the ObjectHandler, check for and handle collisions.
        /// </summary>
        /// <param name="deltaTime">Time in milliseconds since last update.</param>
        /// <param name="bounds"></param>
        public void Update(double deltaTime, RectangleF bounds)
        {
            // Rebuild quad tree.
            quadTree = new QuadTree<GameObject>(WorldSize, 3);
            for (int i = GameObjects.Count - 1; i >= 0; i--)
            {
                if (GameObjects[i].Destroyed) GameObjects.Remove(GameObjects[i]);
                else
                    quadTree.Insert(GameObjects[i].Transform.WorldPosition, GameObjects[i]);
            }

            //Get objects within range.
            GameObject[] objects = Query(bounds);

            // Update each GameObject
            foreach (GameObject obj in objects)
            {
                if (!obj.Enabled) continue;
                obj.Update(deltaTime);
            }

            // Collision handling and movement.
            foreach (GameObject mainObject in objects)
            {
                if (!mainObject.Enabled) continue;

                if (mainObject.HasCollider && mainObject.HasComponent<RigidBody>())
                {
                    foreach (Collider collider in mainObject.GetComponents<Collider>())
                    {
                        if (!collider.Enabled) continue;

                        //Find objects around the mainObject.
                        RectangleF objColRange = collider.GetRectangle();
                        List<GameObject> nearby = quadTree.Query(new RectangleF(
                            objColRange.X - (2 * 32),
                            objColRange.Y - (2 * 32),
                            objColRange.Width + (4 * 32),
                            objColRange.Height + (4 * 32)));

                        // Check for and handle collisions.
                        foreach (GameObject other in nearby)
                        {
                            if (mainObject == other
                                || !other.Enabled
                                || !other.HasCollider) continue;

                            foreach (Collider otherCollider in other.GetComponents<Collider>())
                            {
                                if (!otherCollider.Enabled) continue;
                                ResolveCollision(mainObject, other, collider, otherCollider);
                            }
                        }
                    }
                }

                // Move the mainObject.
                RigidBody rb = mainObject.GetComponent<RigidBody>();
                if (rb != null && rb.Enabled)
                    mainObject.Transform.Translate(rb.Velocity);
            }
        }

        /// <summary>
        /// Check if one RectagleF is touching another RectangleF's left side.
        /// </summary>
        /// <param name="obj">The RectangleF that touches the other.</param>
        /// <param name="objVelocity">Velocity of obj.</param>
        /// <param name="other">The RectangleF whose left the first RectangleF is touching.</param>
        /// <returns></returns>
        private bool IsTouchingLeft(RectangleF obj, Vector2 objVelocity, RectangleF other)
        {
            return obj.Right + objVelocity.X > other.Left
                && obj.Left < other.Left
                && obj.Bottom > other.Top
                && obj.Top < other.Bottom;
        }

        /// <summary>
        /// Check if one RectagleF is touching another RectangleF's right side.
        /// </summary>
        /// <param name="obj">The RectangleF that touches the other.</param>
        /// <param name="objVelocity">Velocity of obj.</param>
        /// <param name="other">The RectangleF whose right the first RectangleF is touching.</param>
        /// <returns></returns>
        private bool IsTouchingRight(RectangleF obj, Vector2 objVelocity, RectangleF other)
        {
            return obj.Left + objVelocity.X < other.Right
                && obj.Right > other.Right
                && obj.Bottom > other.Top
                && obj.Top < other.Bottom;
        }

        /// <summary>
        /// Check if one RectagleF is touching another RectangleF's top side.
        /// </summary>
        /// <param name="obj">The RectangleF that touches the other.</param>
        /// <param name="objVelocity">Velocity of obj.</param>
        /// <param name="other">The RectangleF whose top the first RectangleF is touching.</param>
        /// <returns></returns>
        private bool IsTouchingTop(RectangleF obj, Vector2 objVelocity, RectangleF other)
        {
            return obj.Bottom + objVelocity.Y > other.Top
                && obj.Top < other.Top
                && obj.Right > other.Left
                && obj.Left < other.Right;
        }

        /// <summary>
        /// Check if one RectagleF is touching another RectangleF's bottom side.
        /// </summary>
        /// <param name="obj">The RectangleF that touches the other.</param>
        /// <param name="objVelocity">Velocity of obj.</param>
        /// <param name="other">The RectangleF whose bottom the first RectangleF is touching.</param>
        /// <returns></returns>
        private bool IsTouchingBottom(RectangleF obj, Vector2 objVelocity, RectangleF other)
        {
            return obj.Top + objVelocity.Y < other.Bottom
                && obj.Bottom > other.Bottom
                && obj.Right > other.Left
                && obj.Left < other.Right;
        }

        /// <summary>
        /// Check for collisions on all four sides and determine what happens if they exist.
        /// </summary>
        /// <param name="obj">The object that collides with the other object.</param>
        /// <param name="other">The other object.</param>
        /// <param name="objCollider">The collider of the first object.</param>
        /// <param name="otherCollider">The collider of the second object.</param>
        private void ResolveCollision(GameObject obj, GameObject other, Collider objCollider, Collider otherCollider)
        {
            RigidBody objRigidbody = obj.GetComponent<RigidBody>();

            // When moving Down and hits another objects Top side.
            if (IsTouchingTop(objCollider.GetRectangle(), objRigidbody.Velocity, otherCollider.GetRectangle()))
            {
                if (!objCollider.IsTrigger && !otherCollider.IsTrigger)
                {
                    obj.Transform.Position = new Vector2(
                        obj.Transform.Position.X,
                        otherCollider.GetRectangle().Top - objCollider.Size.Y - objCollider.Offset.Y);

                    objCollider.Collide(other, Side.Bottom);
                    otherCollider.Collide(obj, Side.Top);

                    objRigidbody.Velocity *= Vector2.UnitX * otherCollider.Friction;
                    objRigidbody.Acceleration *= Vector2.UnitX;
                }

                if (objCollider.IsTrigger && !otherCollider.IsTrigger)
                    objCollider.Trigger(other, Side.Bottom);

                else if (otherCollider.IsTrigger && !objCollider.IsTrigger)
                    otherCollider.Trigger(obj, Side.Top);
            }
            // When moving Up and hits another objects Bottom side.
            else if (IsTouchingBottom(objCollider.GetRectangle(), objRigidbody.Velocity, otherCollider.GetRectangle()))
            {
                if (!objCollider.IsTrigger && !otherCollider.IsTrigger)
                {
                    obj.Transform.Position = new Vector2(
                        obj.Transform.Position.X,
                        otherCollider.GetRectangle().Bottom - objCollider.Offset.Y);

                    objCollider.Collide(other, Side.Top);
                    otherCollider.Collide(obj, Side.Bottom);

                    objRigidbody.Velocity *= Vector2.UnitX;
                    objRigidbody.Acceleration *= Vector2.UnitX;
                }

                if (objCollider.IsTrigger && !otherCollider.IsTrigger)
                    objCollider.Trigger(other, Side.Top);

                else if (otherCollider.IsTrigger && !objCollider.IsTrigger)
                    otherCollider.Trigger(obj, Side.Bottom);
            }

            // When moving Right and hits another objects Left side.
            if (IsTouchingLeft(objCollider.GetRectangle(), objRigidbody.Velocity, otherCollider.GetRectangle()))
            {
                if (!objCollider.IsTrigger && !otherCollider.IsTrigger)
                {
                    obj.Transform.Position = new Vector2(
                      otherCollider.GetRectangle().Left - objCollider.Size.X - objCollider.Offset.X,
                      obj.Transform.Position.Y);

                    objCollider.Collide(other, Side.Right);
                    otherCollider.Collide(obj, Side.Left);

                    objRigidbody.Velocity *= Vector2.UnitY;
                }

                if (objCollider.IsTrigger && !otherCollider.IsTrigger)
                    objCollider.Trigger(other, Side.Right);

                else if (otherCollider.IsTrigger && !objCollider.IsTrigger)
                    otherCollider.Trigger(obj, Side.Left);

            }
            // When moving Left and hits another objects Right side.
            else if (IsTouchingRight(objCollider.GetRectangle(), objRigidbody.Velocity, otherCollider.GetRectangle()))
            {
                if (!objCollider.IsTrigger && !otherCollider.IsTrigger)
                {
                    obj.Transform.Position = new Vector2(
                    otherCollider.GetRectangle().Right - objCollider.Offset.X,
                    obj.Transform.Position.Y);

                    objCollider.Collide(other, Side.Left);
                    otherCollider.Collide(obj, Side.Right);

                    objRigidbody.Velocity *= Vector2.UnitY;
                }

                if (objCollider.IsTrigger && !otherCollider.IsTrigger)
                    objCollider.Trigger(other, Side.Left);

                else if (otherCollider.IsTrigger && !objCollider.IsTrigger)
                    otherCollider.Trigger(obj, Side.Right);
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
            for (int i = GameObjects.Count - 1; i >= 0; i--)
            {
                if (GameObjects[i].Destroyed) GameObjects.Remove(GameObjects[i]);
            }
        }
    }

    /// <summary>
    /// Defines the sides of a four sided object.
    /// </summary>
    public enum Side
    {
        Top,
        Bottom,
        Left,
        Right,
        Unknown
    }
}
