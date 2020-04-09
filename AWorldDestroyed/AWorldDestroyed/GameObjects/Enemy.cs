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
    public class Enemy : GameObject, IDamageable
    {
        public float Health { get; set; }
        public float MaxHealth { get; set; }
        public bool IsDead { get; private set; }

        public Enemy(Vector2 position) : this()
        {
            Transform.Position = position;
        }

        public Enemy()
        {
            //Transform.Scale = Vector2.One * 0.5f;
            Name = "Enemy";
            Tag = Tag.Enemy;
            MaxHealth = 100;
            Health = MaxHealth;
            Texture2D spriteSheet = ContentManager.GetTexture("EnemySprites"); //.Load<Texture2D>(@"..\..\..\..\Content\Sprites\Enemy");
            
            Vector2 origin = new Vector2(55, 0);
            Sprite[] spriteWalk = Sprite.Slice(spriteSheet, new Rectangle(0, 0, 110, 115), new Point(8, 1), origin);
            Sprite[] spriteAttack = Sprite.Slice(spriteSheet, new Rectangle(0, 115, 110, 115), new Point(7, 1), origin);

            Animation attackAnimation = new Animation(spriteAttack, 1000 / 30) { Loop = false };

            Animator animator = new Animator();
            animator.AddAnimation("walk", new Animation(spriteWalk, 1000 / 10));
            animator.AddAnimation("attack", attackAnimation);

            AddComponent(animator);
            AddComponent(new Collider(new Vector2(40, 50)) { Name = "Collider", Offset = new Vector2(36, 62) - origin });
            AddComponent(new Collider(new Vector2(53, 20)) { Name = "AttackLeft", Offset = new Vector2(2, 62) - origin, IsTrigger = true, Enabled = false });
            AddComponent(new Collider(new Vector2(53, 20)) { Name = "AttackRight", Offset = new Vector2(55, 62) - origin, IsTrigger = true, Enabled = false });
            AddComponent(new Collider(new Vector2(700, 50)) { Name = "Aggro", Offset = new Vector2(-300, 62) - origin, IsTrigger = true });
            AddComponent(new Collider(new Vector2(106, 20)) { Name = "AttackAggro", Offset = new Vector2(2, 62) - origin, IsTrigger = true, Enabled = false });

            AddComponent(new SpriteRenderer());
            AddComponent<EnemyMovement>();
            AddComponent(new RigidBody
            {
                Name = "EnemyRb",
                Mass = 109.6f,
                Power = 9000 ^ 9000 // OMG OVER 9000
            });

            attackAnimation.GetFrame(4).Event += () => 
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
            base.Update(deltaTime);
        }

        public void TakeDamage(float amount)
        {
            if (Health > 0) Health -= amount;
            if (Health <= 0) OnDeath();
        }

        public void OnDeath()
        {
            if (Health <= 0)
            {/*Death animation*/
                this.Destroy();
            }
        }
    }   
}
