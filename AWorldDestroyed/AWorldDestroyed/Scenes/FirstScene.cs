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
            Player p = new Player();
            AddObject(p);

            //Enemy e = new Enemy();
            //AddObject(e);
            CameraFollow = p;

            GameObject tile = new GameObject();
            tile.AddComponent(new Collider(new Vector2(32, 32)));
            SpriteRenderer renderer = new SpriteRenderer
            {
                SortingOrder = -1,
                Sprite = new Sprite(Game1.TestTileset, new Rectangle(103, 20, 32, 32))
            };
            tile.AddComponent(renderer);
            AddObject(tile);
        }
    }

    class Player : GameObject
    {
        Animator animator;
        SpriteRenderer renderer;
        RigidBody rb;

        public Player()
        {
            renderer = new SpriteRenderer(Game1.Sprite);
            renderer.Sprite.Origin = new Vector2(21, 0);
            AddComponent(renderer);

            rb = new RigidBody();
            AddComponent(rb);
            AddComponent<PlayerMovement>();
            AddComponent(new Collider(new Vector2(42, 72)));

            animator = new Animator();
            Sprite[] sprites = Sprite.Slice(Game1.Sprite.Texture, new Rectangle(0,0,42,72), new Point(8, 1));
            Animation walk = new Animation(sprites, 1000 / 10);
            animator.AddAnimation("walk", walk);
            //walk.Frames.Last().Event = () => { a.ChangeAnimation("something"); };

            AddComponent(animator);
        }

        public override void Update(double deltaTime)
        {

            base.Update(deltaTime);
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
    class Enemy : GameObject
    {
        Animator animator;
        SpriteRenderer renderer;

        public Enemy()
        {
            renderer = new SpriteRenderer(Game1.Sprite2);
            //renderer.Sprite.Origin = new Vector2(21, 0);
            AddComponent(renderer);

           // AddComponent<PlayerMovement>();
        
            animator = new Animator();
            Sprite[] sprites = Sprite.Slice(Game1.Sprite2.Texture, new Rectangle(0, 0, 85, 92), new Point(8, 1));
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i].Origin = sprites[i].SourceRectangle.Size.ToVector2() * new Vector2(0.5f, 0.5f);
            }
            Animation walk = new Animation(sprites, 1000 / 7);
            animator.AddAnimation("walk", walk);
            //walk.Frames.Last().Event = () => { a.ChangeAnimation("something"); };

            AddComponent(animator);

            AddComponent<PlayerMovement>();

            Transform.Scale = Vector2.One * 5f;
        }

        public override void Update(double deltaTime)
        {
            base.Update(deltaTime);
        }
    }

}
