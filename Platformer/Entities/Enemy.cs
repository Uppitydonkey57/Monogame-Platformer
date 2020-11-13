using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Platformer.Graphics;
using Platformer.Main;

namespace Platformer.Entities
{
    class Enemy : Entity
    {
        float gravity = 0.5f;

        public float hp = 5f;

        public float flash = 0f;

        public float hitFrom;

        float walkSpeed = 4f;

        public override void Initialize(ContentManager content)
        {
            velocity.X = walkSpeed;

            tag = "Enemy";

            Texture2D enemySprite = content.Load<Texture2D>("sEnemy");
            Texture2D enemyWalkingSprite = content.Load<Texture2D>("sEnemyR_strip4");
            Texture2D enemyJumpingSprite = content.Load<Texture2D>("sEnemyA_strip2");
            Texture2D enemyDeadSprite = content.Load<Texture2D>("sDead_strip2");

            AnimationController enemyController = new AnimationController(new Dictionary<string, Animation>() 
            {
                {"Idle", new Animation(enemySprite, 1, 0, false)},
                {"Walking", new Animation(enemyWalkingSprite, 4, 8f, true)},
                {"Jump", new Animation(enemyJumpingSprite, 2, 0f, false)},
                {"Dead", new Animation(enemyDeadSprite, 2, 0f, false)}
            });

            sprite = new Sprite(enemySprite, enemyController);
        }

        public override void Update()
        {
            if (hp <= 0)
            {
                DeadEnemy deadEnemy = new DeadEnemy();

                deadEnemy.Velocity = GetDirectionLength(3, hitFrom);

                Vector2 deadVelocity = deadEnemy.Velocity;

                deadEnemy.Velocity = deadVelocity;

                deadEnemy.Sprite.flipX = Math.Sign(deadEnemy.Velocity.X) < 0;

                Instantiate(deadEnemy, position);
                Destroy(this);
            }

            velocity.Y = velocity.Y + gravity;

            if (PlaceMeeting(new Vector2(position.X + velocity.X, position.Y), "Block"))
            {
                while (!PlaceMeeting(new Vector2(position.X + Math.Sign(velocity.X), position.Y), "Block"))
                {
                    position.X += Math.Sign(velocity.X);
                }

                velocity.X = -velocity.X;
            }

            position.X += velocity.X;

            if (PlaceMeeting(new Vector2(position.X, position.Y + velocity.Y), "Block"))
            {
                while (!PlaceMeeting(new Vector2(position.X, position.Y + Math.Sign(velocity.Y)), "Block"))
                {
                    position.Y += Math.Sign(velocity.Y);
                }

                velocity.Y = 0;
            }

            position.Y += velocity.Y;

            //Animation
            if (!PlaceMeeting(new Vector2(position.X, position.Y + 1), "Block"))
            {
                sprite.Controller.Play("Jump");

                if (Math.Sign(velocity.Y) > 0)
                    sprite.Controller.SetFrame(0);
                else
                    sprite.Controller.SetFrame(1);
            }
            else
            {
                if (velocity.X == 0)
                {
                    sprite.Controller.Play("Idle");
                }
                else
                {
                    sprite.Controller.Play("Walking");
                }
            }

            if (velocity.X != 0) sprite.flipX = (Math.Sign(velocity.X) != 1);
            Console.WriteLine(sprite.scale.X);
        }
    }
}
