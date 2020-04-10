using AWorldDestroyed.Models;
using AWorldDestroyed.Models.Components;
using AWorldDestroyed.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AWorldDestroyed.Scripts
{
    /// <summary>
    /// This is used to handle all the Enemy object movements.
    /// </summary>
    class PlayerMovement : Script
    {
        private RigidBody rigidBody;
        private Animator animator;
        private bool canJump;
        private bool isRunning;
        private bool attacking = false;

        private float walkSpeed = 0.04f;
        private float runBoost = 2f;
        private float maxWalkSpeed = 4f;


        /// <summary>
        /// Update PlayerMovement.
        /// </summary>
        /// <param name="deltaTime">Time in milliseconds since last update</param>
        public override void Update(double deltaTime)
        {
            if (rigidBody == null) rigidBody = AttachedTo.GetComponent<RigidBody>();
            if (animator == null) animator = AttachedTo.GetComponent<Animator>();

            float speed = walkSpeed * (float)deltaTime;

            // ATTKACK
            if (!attacking && InputManager.IsKeyJustPressed(Keys.Z))
            {
                attacking = true;
                animator.ChangeAnimation("attack");
                
            }else if (attacking && animator.GetCurrentAnimation().Done)
            {
                attacking = false;
            }
            else if (!attacking)
            { 
            isRunning = InputManager.IsKeyPressed(Keys.LeftShift);

            // Walk
            if (InputManager.IsKeyPressed(Keys.Right))
            {
                AttachedTo.GetComponent<SpriteRenderer>().SpriteEffect = SpriteEffects.None;
                if (canJump && isRunning)
                    rigidBody.Velocity += new Vector2(1, 0) * speed * runBoost;
                else if (rigidBody.Velocity.X + speed < maxWalkSpeed)
                    rigidBody.Velocity += new Vector2(1, 0) * speed;
                if (canJump)
                {
                    if (rigidBody.Velocity.X > maxWalkSpeed)
                        animator.ChangeAnimation("run");
                    else animator.ChangeAnimation("walk");
                }
            }
           
            if (InputManager.IsKeyPressed(Keys.Left))
            {
                AttachedTo.GetComponent<SpriteRenderer>().SpriteEffect = SpriteEffects.FlipHorizontally;
                if (canJump && isRunning)
                    rigidBody.Velocity += new Vector2(-1, 0) * speed * runBoost;
                else if (rigidBody.Velocity.X - speed > - maxWalkSpeed)
                    rigidBody.Velocity += new Vector2(-1, 0) * speed;
                if (canJump)
                {
                    if (rigidBody.Velocity.X < -maxWalkSpeed)
                        animator.ChangeAnimation("run");
                    else animator.ChangeAnimation("walk");
                }
            }

            // Jump
            if (canJump && InputManager.IsKeyJustPressed(Keys.Up) && rigidBody.Velocity.Y < 0.1f)
            {
                rigidBody.Velocity += new Vector2(0, -0.3f) * (float)deltaTime;
                canJump = false;
            }
            
            if (!canJump)
            {
                rigidBody.Velocity *= new Vector2(0.99f, 1f);
                animator.ChangeAnimation("jump");
            }
            else if (rigidBody.Velocity.LengthSquared() <= 0.8f)
            {
                animator.ChangeAnimation("idle");
            }


            ////////////////////////////////
            //if (InputManager.IsKeyPressed(Keys.RightShift))
            //    AttachedTo.Transform.Translate(AttachedTo.Transform.Forward * speed * (float)deltaTime);

            if (InputManager.IsKeyPressed(Keys.PageDown))
                AttachedTo.Transform.Rotation -= speed * 5;
            if (InputManager.IsKeyPressed(Keys.PageUp))
                AttachedTo.Transform.Rotation += speed * 5;
            //////////////////////////////////

            }
        }

        /// <summary>
        /// Determine what happens when player touches other objects.
        /// </summary>
        /// <param name="collider">A Collider.</param>
        /// <param name="other">A GameObject.</param>
        /// <param name="side">An enum Side.(Used to check objects side)</param>
        public void OnCollision(Collider collider, GameObject other, Side side)
        {
            if (side == Side.Bottom && rigidBody.Velocity.Y > 0)
                canJump = true;
        }

        /// <summary>
        /// Determine what happens when player trigger/pokes other objects.
        /// </summary>
        /// <param name="collider">A Collider.</param>
        /// <param name="other">A GameObject.</param>
        /// <param name="side">An enum Side.(Used to check objects side)</param>
        public void OnTriggerEnter(Collider collider, GameObject other, Side side)
        {
            if (other.Tag == Tag.Enemy)
            {
                if (other is IDamageable enemy) { enemy.TakeDamage(34f); }
            }
        }

        /// <summary>
        /// Get a copy of this Component.
        /// </summary>
        /// <returns>PlayerMovement.</returns>
        public override Component Copy()
        {
            return new PlayerMovement();
        }
    }
}
