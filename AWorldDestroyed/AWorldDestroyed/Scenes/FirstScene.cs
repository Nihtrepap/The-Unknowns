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

namespace AWorldDestroyed.Scenes
{
    class FirstScene : Scene
    {
        public FirstScene(SpriteBatch spriteBatch) : base(spriteBatch)
        {
            AddObject(new Player());
        }
    }

    class Player : GameObject
    {
        public override void Initialize()
        {
            AddComponent(new SpriteRenderer(Game1.Sprite));

            base.Initialize();
        }

        public override void Update(double deltaTime)
        {
            float speed = 1f;
            if (InputManager.IsKeyPressed(Keys.D))
                Transform.Translate(new Vector2(1, 0) * speed * (float)deltaTime);
            if (InputManager.IsKeyPressed(Keys.A))
                Transform.Translate(new Vector2(-1, 0) * speed * (float)deltaTime);
            if (InputManager.IsKeyPressed(Keys.W))
                Transform.Translate(new Vector2(0, -1) * speed * (float)deltaTime);
            if (InputManager.IsKeyPressed(Keys.S))
                Transform.Translate(new Vector2(0, 1) * speed * (float)deltaTime);

            if (InputManager.IsKeyPressed(Keys.Space))
                Transform.Translate(Transform.Forward * speed * (float)deltaTime);

            if (InputManager.IsKeyPressed(Keys.Left))
                Transform.Scale += new Vector2(0.01f, 0);
            if (InputManager.IsKeyPressed(Keys.Right))
                Transform.Scale += new Vector2(-0.01f, 0);
            if (InputManager.IsKeyPressed(Keys.Up))
                Transform.Scale += new Vector2(0, 0.01f);
            if (InputManager.IsKeyPressed(Keys.Down))
                Transform.Scale += new Vector2(0, -0.01f);

            if (InputManager.IsKeyPressed(Keys.PageDown))
                Transform.Rotation-=0.1f;
            if (InputManager.IsKeyPressed(Keys.PageUp))
                Transform.Rotation+=0.1f;

            base.Update(deltaTime);
        }
    }
}
