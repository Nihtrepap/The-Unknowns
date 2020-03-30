using AWorldDestroyed.Models;
using AWorldDestroyed.Models.Components;
using AWorldDestroyed.Scripts;
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
        }

        public override void Load()
        {
            GameObject o = new GameObject();
            o.AddComponent(new SpriteRenderer(Game1.Sprite));

            Player p = new Player();
            CameraFollow = p;

            AddObject(p);
        }
    }

    class Player : GameObject
    {
        public Player()
        {
            SpriteRenderer r = new SpriteRenderer(Game1.Sprite);
            r.Sprite.Origin = new Vector2(21, 0);

            AddComponent(r);
            AddComponent<PlayerMovement>();
        }
    }

    class Player2 : GameObject
    {
        public Player2()
        {
            SpriteRenderer r = new SpriteRenderer(Game1.Sprite);
            r.Sprite.Origin = new Vector2(21, 0);

            AddComponent(r);
            AddComponent<PlayerMovement>();
        }

    }

}
