using System;
using AWorldDestroyed.GameObjects;
using AWorldDestroyed.Models;
using AWorldDestroyed.Models.Components;
using AWorldDestroyed.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AWorldDestroyed.Scripts
{

    /// <summary>
    /// This is used to handle all the Enemy object movements.
    /// </summary>
    public class EnemyMovement : Script
    {
        private RigidBody rigidBody;
        private Animator animator;

        private EnemyState state = EnemyState.Home;
        private Transform target = null;
        private float walkSpeed = 0.04f;
        private float maxWalkSpeed = 2f;
        private float distanceTravelled = 0;
        private float maxDistance = 100;
        private int direction = 1;

        /// <summary>
        /// Update EnemyMovement.
        /// </summary>
        /// <param name="deltaTime">Time in milliseconds since last update</param>
        public override void Update(double deltaTime)
        {
            if (rigidBody == null) rigidBody = AttachedTo.GetComponent<RigidBody>();
            if (animator == null) animator = AttachedTo.GetComponent<Animator>();

            float speed = walkSpeed * (float)deltaTime;

            if (state == EnemyState.Attacking)
            {
                if (animator.GetCurrentAnimation().Done)
                {
                    state = EnemyState.Aggro;
                    animator.GetCurrentAnimation().Reset();
                }
            }
            else if (state == EnemyState.Home)
            {
                if (distanceTravelled < maxDistance)
                {
                    Walk(direction > 0, speed);
                    distanceTravelled += Math.Abs(rigidBody.Velocity.X);
                }
                else
                {
                    direction *= -1;
                    distanceTravelled = 0;
                }
            }
            else if (state == EnemyState.Aggro)
            {
                Walk(target.Position.X >= AttachedTo.Transform.Position.X, speed);
            }
            else if (state == EnemyState.GoingHome)
            {
                if (!((Enemy)AttachedTo).IsHome)
                {
                    Walk(((Enemy)AttachedTo).HomePos.X >= AttachedTo.Transform.Position.X, speed);
                }
                else state = EnemyState.Home;
            }
        }

        /// <summary>
        /// Determines what happens when enemy hits other object.
        /// </summary>
        /// <param name="collider">A Collider.</param>
        /// <param name="other">A GameObject.</param>
        /// <param name="side">An enum Side.(Used to check objects side)</param>
        public void OnHit(Collider collider, GameObject other, Side side)
        {
            if (other.Tag == Tag.Player)
            {
                if(other is IDamageable player) { player.TakeDamage(15f); }
            }
        }

        /// <summary>
        /// Determines what happens when enemy attacks other object.
        /// </summary>
        /// <param name="collider">A Collider.</param>
        /// <param name="other">A GameObject.</param>
        /// <param name="side">An enum Side.(Used to check objects side)</param>
        public void OnPlayerInAttackRange(Collider collider, GameObject other, Side side)
        {
            if (other.Tag == Tag.Player)
            {
                state = EnemyState.Attacking;
                animator.ChangeAnimation("attack");
            }
        }

        /// <summary>
        /// Determines what happens when enemy notice other object.
        /// </summary>
        /// <param name="collider">A Collider.</param>
        /// <param name="other">A GameObject.</param>
        /// <param name="side">An enum Side.(Used to check objects side)</param>
        public void OnPlayerInSight(Collider collider, GameObject other, Side side)
        {
            if (other.Tag == Tag.Player)
            {
                state = EnemyState.Aggro;
                target = other.Transform;
                AttachedTo.GetComponent("AttackRange").Enabled = true;
            }
        }

        /// <summary>
        /// Determines what happens when other object get "out of sight" from enemy.
        /// </summary>
        /// <param name="collider">A Collider.</param>
        /// <param name="other">A GameObject.</param>
        /// <param name="side">An enum Side.(Used to check objects side)</param>
        public void OnPlayerOutOfSight(Collider collider, GameObject other, Side side)
        {
            if (other.Transform == target)
            {
                state = EnemyState.GoingHome;
                target = null;
                AttachedTo.GetComponent("AttackRange").Enabled = false;
            }
        }

        /// <summary>
        /// Determines what happens when other object walks.
        /// </summary>
        /// <param name="right">A bool(used to get enemies direction).</param>
        /// <param name="speed">A float for movement speed.</param>
        private void Walk(bool right, float speed)
        {
            animator.ChangeAnimation("walk");
            if (right && rigidBody.Velocity.X + speed < maxWalkSpeed)
            {
                AttachedTo.GetComponent<SpriteRenderer>().SpriteEffect = SpriteEffects.None;
                rigidBody.Velocity += new Vector2(1, 0) * speed;
            }
            else if (!right && rigidBody.Velocity.X - speed > -maxWalkSpeed)
            {
                AttachedTo.GetComponent<SpriteRenderer>().SpriteEffect = SpriteEffects.FlipHorizontally;
                rigidBody.Velocity += new Vector2(-1, 0) * speed;
            }
        }

        /// <summary>
        /// An enum used to determine state of the object.
        /// </summary>
        enum EnemyState
        {
            Home,
            GoingHome,
            Aggro,
            Attacking
        }

        /// <summary>
        /// Get a copy of this Component.
        /// </summary>
        /// <returns>EnemyMovement.</returns>
        public override Component Copy()
        {
            return new EnemyMovement();
        }
    }
}