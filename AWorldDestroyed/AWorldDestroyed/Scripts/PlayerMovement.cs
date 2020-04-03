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
        //SpriteRenderer renderer;
        RigidBody rb;

        //public override void Initialize()
        //{
        //}

        public override void Update(double deltaTime)
        {
            if (rb == null) rb = AttachedTo.GetComponent<RigidBody>();

            float speed = 0.004f;
            //rb.Velocity = Vector2.Zero;
            if (InputManager.IsKeyPressed(Keys.Right))
            {
                AttachedTo.GetComponent<SpriteRenderer>().SpriteEffect = SpriteEffects.None;
                rb.Velocity += new Vector2(1, 0) * speed;
            }
            if (InputManager.IsKeyPressed(Keys.Left))
            {
                if (!InputManager.IsKeyPressed(Keys.LeftShift))
                    AttachedTo.GetComponent<SpriteRenderer>().SpriteEffect = SpriteEffects.FlipHorizontally;
                rb.Velocity += new Vector2(-1, 0) * speed;
            }
            if (InputManager.IsKeyPressed(Keys.Up))
                rb.Velocity += new Vector2(0, -1) * speed;
            if (InputManager.IsKeyPressed(Keys.Down))
                rb.Velocity += new Vector2(0, 1) * speed;

            if (InputManager.IsKeyPressed(Keys.Space))
            {
                rb.AddVelocity(new Vector2(0, -0.08f));
            }
            //rb.Velocity += new Vector2(0, 1) * speed;

            if (InputManager.IsKeyPressed(Keys.RightShift))
                AttachedTo.Transform.Translate(AttachedTo.Transform.Forward * speed * (float)deltaTime);

            if (InputManager.IsKeyPressed(Keys.PageDown))
                AttachedTo.Transform.Rotation -= speed * 5;
            if (InputManager.IsKeyPressed(Keys.PageUp))
                AttachedTo.Transform.Rotation += speed * 5;
        }

        public override Component Copy()
        {
            return new PlayerMovement();
        }
    }
}
