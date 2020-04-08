using AWorldDestroyed.Models;
using AWorldDestroyed.Models.Components;
using AWorldDestroyed.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AWorldDestroyed.Scripts
{
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
        //private float maxRunSpeed = 8f;
       
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
                

                //AttachedTo.GetComponent("Attack").Enabled
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

        public override void OnCollision(GameObject other, Side side)
        {
            if (side == Side.Bottom && rigidBody.Velocity.Y > 0)
                canJump = true;
        }

        public override void OnTrigger(GameObject other, Side side)
        {
            // AttachedTo.GetComponent<SpriteRenderer>().SpriteEffect

            //if (side == Side.Left && AttachedTo.GetComponent<SpriteRenderer>().SpriteEffect == SpriteEffects.FlipHorizontally)
            //{
            //    if (other.Tag == Tag.Enemy) other.Destroy();
            //}
            //else if (side == Side.Right && AttachedTo.GetComponent<SpriteRenderer>().SpriteEffect == SpriteEffects.None)
            //{
            //    if (other.Tag == Tag.Enemy) other.Destroy();
            //}
            if (other.Tag == Tag.Enemy) other.Destroy();
            base.OnTrigger(other, side);
        }

        public override Component Copy()
        {
            return new PlayerMovement();
        }
    }
}
