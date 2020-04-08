// =============================================
//         Editor:     Lone Maaherra
//         Last edit:  2020-04-07
// _-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_
//
//       (\                 >+{{{o)> - kvaouk
//    >+{{{{{0)> - kraouk      LL  
//       /_\_
//
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//                <333333><                     
//         <3333333><           <33333>< 

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
            Tag = Tag.Player;

            Texture2D spriteSheet = ContentManager.GetTexture("Player"); //Load<Texture2D>(@"..\..\..\..\Content\Sprites\Player\Player_spriteSheet");

            Vector2 origin = new Vector2(21, 0);
            Sprite[] spriteWalk = Sprite.Slice(spriteSheet, new Rectangle(0, 0, 42, 72), new Point(8, 1), origin);
            Sprite[] spriteRun = Sprite.Slice(spriteSheet, new Rectangle(0, 72, 42, 72), new Point(8, 1), origin);
            Sprite[] spriteIdle = Sprite.Slice(spriteSheet, new Rectangle(0, 144, 42, 72), new Point(1, 1), origin);
            Sprite[] spriteJump = Sprite.Slice(spriteSheet, new Rectangle(0, 288, 42, 72), new Point(2, 1), origin);
            Sprite[] spriteAttack = Sprite.Slice(spriteSheet, new Rectangle(0, 216, 42, 72), new Point(8, 1), origin);

            Animation attackAnimation = new Animation(spriteAttack, 1000 / 50) { Loop = false };

            Animator animator = new Animator
            {
                Name = "anmimatort"
            };
            animator.AddAnimation("walk", new Animation(spriteWalk, 1000 / 10));
            animator.AddAnimation("run", new Animation(spriteRun, 1000 / 30));
            animator.AddAnimation("idle", new Animation(spriteIdle, 1000 / 10));
            animator.AddAnimation("jump", new Animation(spriteJump, 1000 / 10));
            animator.AddAnimation("attack", attackAnimation);

            AddComponent(animator);
            AddComponent(new Collider(new Vector2(59, 23)) { Name = "Attack", Offset = new Vector2(-8, 25) - origin , IsTrigger = true, Enabled = false });
            AddComponent(new Collider(new Vector2(13, 62)) { Name = "Collider", Offset = new Vector2(16, 5) - origin });
            AddComponent(new SpriteRenderer());
            AddComponent<PlayerMovement>();
            AddComponent(new RigidBody
            {
                Name = "Rb",
                Mass = 78.6f,
                Power = 9001 ^ 9001 // OMG OVER 9000 
            });

            attackAnimation.GetFrame(2).Event += () => { GetComponent("Attack").Enabled = true; };
            attackAnimation.GetFrame(5).Event += () => { GetComponent("Attack").Enabled = false; };

        }

        

        public override void Update(double deltaTime)
        {
            //rb.Velocity += new Vector2(0, 0.5f);

            base.Update(deltaTime);
        }
    }
}
