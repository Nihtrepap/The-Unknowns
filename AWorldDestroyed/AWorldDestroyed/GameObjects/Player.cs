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
    public class Player : GameObject, IDamageable
    {
        public float Health { get; set; }
        public float MaxHealth { get; set; }
        public bool IsDead { get; private set; }

        public Player()
        {
            Name = "Player";
            Tag = Tag.Player;
            MaxHealth = 100;
            Health = MaxHealth;
            IsDead = false;

            Texture2D spriteSheet = ContentManager.GetTexture("Player"); //Load<Texture2D>(@"..\..\..\..\Content\Sprites\Player\Player_spriteSheet");

            Vector2 origin = new Vector2(21, 0);
            Sprite[] spriteWalk = Sprite.Slice(spriteSheet, new Rectangle(0, 0, 42, 72), new Point(8, 1), origin);
            Sprite[] spriteRun = Sprite.Slice(spriteSheet, new Rectangle(0, 74, 42, 72), new Point(8, 1), origin);
            Sprite[] spriteIdle = Sprite.Slice(spriteSheet, new Rectangle(0, 148, 17, 68), new Point(8, 1), new Vector2(8, 0));
            Sprite[] spriteJump = Sprite.Slice(spriteSheet, new Rectangle(0, 218, 42, 72), new Point(2, 1), origin);
            Sprite[] spriteAttack = Sprite.Slice(spriteSheet, new Rectangle(0, 291, 84, 72), new Point(4, 2), origin);

            Animation attackAnimation = new Animation(spriteAttack, 1000 / 50) { Loop = false };

            Animator animator = new Animator
            {
                Name = "anmimatort"
            };
            animator.AddAnimation("walk", new Animation(spriteWalk, 1000 / 10));
            animator.AddAnimation("run", new Animation(spriteRun, 1000 / 30));
            animator.AddAnimation("idle", new Animation(spriteIdle, 1000 / 7));
            animator.AddAnimation("jump", new Animation(spriteJump, 1000 / 10));
            animator.AddAnimation("attack", attackAnimation);

            AddComponent(animator);
            AddComponent(new Collider(new Vector2(13, 62)) { Name = "Collider", Offset = new Vector2(16, 5) - origin });
            AddComponent(new Collider(new Vector2(30, 23)) { Name = "AttackLeft", Offset = new Vector2(-14, 25) - origin, IsTrigger = true, Enabled = false });
            AddComponent(new Collider(new Vector2(30, 23)) { Name = "AttackRight", Offset = new Vector2(29, 25) - origin, IsTrigger = true, Enabled = false });
            AddComponent(new SpriteRenderer());
            AddComponent<PlayerMovement>();
            AddComponent(new RigidBody
            {
                Name = "Rb",
                Mass = 78.6f,
                Power = 9001 ^ 9001 // OMG OVER 9000 
            });

            attackAnimation.GetFrame(2).Event += () =>
            {
                if (GetComponent<SpriteRenderer>().SpriteEffect == SpriteEffects.FlipHorizontally)
                    GetComponent("AttackLeft").Enabled = true;
                else
                    GetComponent("AttackRight").Enabled = true;
            };
            attackAnimation.GetFrame(5).Event += () =>
            {
                if (GetComponent<SpriteRenderer>().SpriteEffect == SpriteEffects.FlipHorizontally)
                    GetComponent("AttackLeft").Enabled = false;
                else
                    GetComponent("AttackRight").Enabled = false;
            };

        }

        public override void Update(double deltaTime)
        {
            //rb.Velocity += new Vector2(0, 0.5f);

            base.Update(deltaTime);
        }

        public void OnDeath()
        {
            throw new System.NotImplementedException();
        }

        public void TakeDamage(float amount)
        {
            throw new System.NotImplementedException();
        }
    }
}
