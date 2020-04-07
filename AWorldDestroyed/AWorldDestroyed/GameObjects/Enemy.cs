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
    public class Enemy : GameObject
    {
        public Enemy(Vector2 position) : this()
        {
            Transform.Position = position;
        }

        public Enemy()
        {
            //Transform.Scale = Vector2.One * 0.5f;

            Texture2D spriteSheet = ContentManager.GetTexture("Enemy"); //.Load<Texture2D>(@"..\..\..\..\Content\Sprites\Enemy");
            
            Vector2 origin = new Vector2(42.5f, 0);
            Sprite[] spriteWalk = Sprite.Slice(spriteSheet, new Rectangle(0, 0, 85, 92), new Point(8, 1), origin);

            Animator animator = new Animator();
            animator.AddAnimation("walk", new Animation(spriteWalk, 1000 / 10));

            AddComponent(animator);
            AddComponent(new Collider(new Vector2(45, 50)) { Offset = new Vector2(21, 36) - origin });
            AddComponent(new SpriteRenderer());
            AddComponent<EnemyMovement>();
            AddComponent(new RigidBody
            {
                Name = "EnemyRb",
                Mass = 109.6f,
                Power = 9000 ^ 9000 // OMG OVER 9000
            });
        }

        public override void Update(double deltaTime)
        {
            base.Update(deltaTime);
        }
    }
}
