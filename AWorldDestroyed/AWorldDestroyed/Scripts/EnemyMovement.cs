using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AWorldDestroyed.Models;
using AWorldDestroyed.Models.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AWorldDestroyed.Scripts
{
    public class EnemyMovement : Script
    {
        private RigidBody rigidBody;
        private Animator animator;

        private float walkSpeed = 0.04f;
        private float maxWalkSpeed = 4f;
        private float distanceTravelled = 0;
        private float maxDistance = 100;
        private int direction = 1;


        public override void Update(double deltaTime)
        {
            if (rigidBody == null) rigidBody = AttachedTo.GetComponent<RigidBody>();
            if (animator == null) animator = AttachedTo.GetComponent<Animator>();

            float speed = walkSpeed * (float)deltaTime;

            if (distanceTravelled < maxDistance)
            {
                if (direction > 0 && rigidBody.Velocity.X + speed < maxWalkSpeed)
                {
                    AttachedTo.GetComponent<SpriteRenderer>().SpriteEffect = SpriteEffects.None;
                    rigidBody.Velocity += new Vector2(1, 0) * speed;
                }
                else if (direction < 0 && rigidBody.Velocity.X - speed > -maxWalkSpeed)
                {
                    AttachedTo.GetComponent<SpriteRenderer>().SpriteEffect = SpriteEffects.FlipHorizontally;
                    rigidBody.Velocity += new Vector2(-1, 0) * speed;
                }
                distanceTravelled += Math.Abs(rigidBody.Velocity.X);
            }
            else
            {
                direction *= -1;
                distanceTravelled = 0;
            }
        }

        public override Component Copy()
        {
            throw new NotImplementedException();
        }
    }
}
