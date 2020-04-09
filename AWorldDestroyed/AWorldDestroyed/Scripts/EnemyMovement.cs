using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWorldDestroyed.GameObjects;
using AWorldDestroyed.Models;
using AWorldDestroyed.Models.Components;
using AWorldDestroyed.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AWorldDestroyed.Scripts
{
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
        //private bool attacking = false;


        public override void Update(double deltaTime)
        {
            if (rigidBody == null) rigidBody = AttachedTo.GetComponent<RigidBody>();
            if (animator == null) animator = AttachedTo.GetComponent<Animator>();

            float speed = walkSpeed * (float)deltaTime;

            // ATTKACK
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

        public void OnHit(Collider collider, GameObject other, Side side)
        {
            if (other.Tag == Tag.Player)
            {
                if(other is IDamageable player) { player.TakeDamage(15f); }
            }
        }

        public void OnPlayerInAttackRange(Collider collider, GameObject other, Side side)
        {
            if (other.Tag == Tag.Player)
            {
                state = EnemyState.Attacking;
                animator.ChangeAnimation("attack");
            }
        }

        public void OnPlayerInSight(Collider collider, GameObject other, Side side)
        {
            if (other.Tag == Tag.Player)
            {
                state = EnemyState.Aggro;
                target = other.Transform;
                AttachedTo.GetComponent("AttackRange").Enabled = true;
            }
        }

        public void OnPlayerOutOfSight(Collider collider, GameObject other, Side side)
        {
            if (other.Transform == target)
            {
                state = EnemyState.GoingHome;
                target = null;
                AttachedTo.GetComponent("AttackRange").Enabled = false;
            }
        }

        public override Component Copy()
        {
            throw new NotImplementedException();
        }


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

        enum EnemyState
        {
            Home,
            GoingHome,
            Aggro,
            Attacking
        }
    }
}