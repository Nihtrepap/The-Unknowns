using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private float walkSpeed = 0.04f;
        private float maxWalkSpeed = 4f;
        private float distanceTravelled = 0;
        private float maxDistance = 100;
        private int direction = 1;
        private bool attacking = false;


        public override void Update(double deltaTime)
        {
            if (rigidBody == null) rigidBody = AttachedTo.GetComponent<RigidBody>();
            if (animator == null) animator = AttachedTo.GetComponent<Animator>();

            float speed = walkSpeed * (float)deltaTime;

            // ATTKACK
            if (!attacking && InputManager.IsKeyJustPressed(Keys.E))
            {
                attacking = true;
                animator.ChangeAnimation("attack");


                //AttachedTo.GetComponent("Attack").Enabled
            }
            else if (attacking && animator.GetCurrentAnimation().Done)
            {
                attacking = false;
            }
            else if (!attacking)
            {

                if (distanceTravelled < maxDistance)
                {
                    if (direction > 0 && rigidBody.Velocity.X + speed < maxWalkSpeed)
                    {
                        AttachedTo.GetComponent<SpriteRenderer>().SpriteEffect = SpriteEffects.None;
                        rigidBody.Velocity += new Vector2(1, 0) * speed;
                    }
                    else if (direction < 0 && rigidBody.Velocity.X - speed > -maxWalkSpeed)
                    {
                        AttachedTo.GetComponent<SpriteRenderer>().SpriteEffect = SpriteEffects.FlipHorizontally;
                        rigidBody.Velocity += new Vector2(-1, 0) * speed;
                    }
                    distanceTravelled += Math.Abs(rigidBody.Velocity.X);
                }
                else
                {
                    direction *= -1;
                    distanceTravelled = 0;
                }

                if (InputManager.IsKeyHeld(Keys.E))
                {
                    animator.ChangeAnimation("attack");
                }
                else
                {
                    animator.ChangeAnimation("walk");
                }
            }
        }

        public override void OnCollision(GameObject other, Side side)
        {
            
        }

        public override void OnTrigger(GameObject other, Side side)
        {
            // AttachedTo.GetComponent<SpriteRenderer>().SpriteEffect

            if (side == Side.Left && AttachedTo.GetComponent<SpriteRenderer>().SpriteEffect == SpriteEffects.FlipHorizontally)
            {
                if (other.Tag == Tag.Player) other.Destroy();
            }
            else if (side == Side.Right && AttachedTo.GetComponent<SpriteRenderer>().SpriteEffect == SpriteEffects.None)
            {
                if (other.Tag == Tag.Player) other.Destroy();
            }

            base.OnTrigger(other, side);
        }

        public override Component Copy()
        {
            throw new NotImplementedException();
        }
    }
}
