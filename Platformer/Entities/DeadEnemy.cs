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
    public class DeadEnemy : Entity
    {
        int done = 0;

        float gravity = 0.9f;

        public override void Initialize(ContentManager content)
        {
            tag = "DeadEnemy";

            velocity.X = 4;
            velocity.Y = -15;

            Texture2D deadEnemySprite = content.Load<Texture2D>("sDead_strip2");

            AnimationController deadEnemyController = new AnimationController(new Dictionary<string, Animation>() 
            {
                {"Dead", new Animation(deadEnemySprite, 2, 0f, false, false)}
            });

            sprite = new Sprite(deadEnemyController, 1);

            sprite.Controller.Play("Dead");
        }

        public override void Update()
        {
            if (done == 0)
            {
                velocity.Y += gravity;

                if (PlaceMeeting(new Vector2(position.X + velocity.X, position.Y), "Block"))
                {
                    while (!PlaceMeeting(new Vector2(position.X + Math.Sign(velocity.X), position.Y), "Block"))
                    {
                        position.X += Math.Sign(velocity.X);
                    }

                    velocity.X = 0;
                }

                position.X += velocity.X;

                if (PlaceMeeting(new Vector2(position.X, position.Y + velocity.Y), "Block"))
                {
                    if (velocity.Y > 0)
                    {
                        done = 1;
                        sprite.Controller.SetFrame(1);
                    }

                    while (!PlaceMeeting(new Vector2(position.X, position.Y + Math.Sign(velocity.Y)), "Block"))
                    {
                        position.Y += Math.Sign(velocity.Y);
                    }

                    velocity.Y = 0;
                }

                position.Y += velocity.Y;
            }
        }
    }
}
