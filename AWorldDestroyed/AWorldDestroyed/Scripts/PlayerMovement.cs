using AWorldDestroyed.Models;
using AWorldDestroyed.Models.Components;
using AWorldDestroyed.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace AWorldDestroyed.Scripts
{
    class PlayerMovement : Script
    {
        public override void Update(double deltaTime)
        {
            float speed = 1f;
            if (InputManager.IsKeyPressed(Keys.D))
                AttachedTo.Transform.Translate(new Vector2(1, 0) * speed * (float)deltaTime);
            if (InputManager.IsKeyPressed(Keys.A))
                AttachedTo.Transform.Translate(new Vector2(-1, 0) * speed * (float)deltaTime);
            if (InputManager.IsKeyPressed(Keys.W))
                AttachedTo.Transform.Translate(new Vector2(0, -1) * speed * (float)deltaTime);
            if (InputManager.IsKeyPressed(Keys.S))
                AttachedTo.Transform.Translate(new Vector2(0, 1) * speed * (float)deltaTime);

            if (InputManager.IsKeyPressed(Keys.Space))
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
