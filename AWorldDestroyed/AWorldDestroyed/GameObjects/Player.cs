using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWorldDestroyed.Models;
using AWorldDestroyed.Models.Components;
using AWorldDestroyed.Scripts;
using AWorldDestroyed.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AWorldDestroyed.GameObjects
{
    public class Player : GameObject
    {
        public Player()
        {
            Name = "Player";


            Texture2D spriteSheet = ContentManager.GetTexture("Player"); //Load<Texture2D>(@"..\..\..\..\Content\Sprites\Player\Player_spriteSheet");

            Vector2 origin = new Vector2(21, 0);
            Sprite[] spriteWalk = Sprite.Slice(spriteSheet, new Rectangle(0, 0, 42, 72), new Point(8, 1), origin);
            Sprite[] spriteRun = Sprite.Slice(spriteSheet, new Rectangle(0, 72, 42, 72), new Point(8, 1), origin);
            Sprite[] spriteIdle = Sprite.Slice(spriteSheet, new Rectangle(0, 144, 42, 72), new Point(1, 1), origin);
            Sprite[] spriteJump = Sprite.Slice(spriteSheet, new Rectangle(0, 216, 42, 72), new Point(8, 1), origin);

            Animator animator = new Animator
            {
                Name = "anmimatort"
            };
            animator.AddAnimation("walk", new Animation(spriteWalk, 1000 / 10));
            animator.AddAnimation("run", new Animation(spriteRun, 1000 / 30));
            animator.AddAnimation("idle", new Animation(spriteIdle, 1000 / 10));
            animator.AddAnimation("jump", new Animation(spriteJump, 1000 / 10));

            AddComponent(animator);
            AddComponent(new Collider(new Vector2(13, 62)) { Name = "Collider", Offset = new Vector2(16, 5) - origin });
            AddComponent(new SpriteRenderer());
            AddComponent<PlayerMovement>();
            AddComponent(new RigidBody
            {
                Name = "Rb",
                Mass = 78.6f,
                Power = 9001 ^ 9001 // OMG OVER 9000
            });
        }

        public override void Update(double deltaTime)
        {
            //rb.Velocity += new Vector2(0, 0.5f);

            base.Update(deltaTime);
        }
    }
}
