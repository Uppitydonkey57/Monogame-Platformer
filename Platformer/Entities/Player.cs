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

namespace Platformer.Entities
{
    public class Player : Entity
    {
        float speed = 5f;

        float gravity = 0.5f;

        bool hasJumped = false;

        public override void Initialize(ContentManager content)
        {
            tag = "Player";

            Texture2D playerSprite = content.Load<Texture2D>("sPlayer");

            Texture2D walkingSprites = content.Load<Texture2D>("sPlayer_r_strip4");
            Texture2D jumpingSprites = content.Load<Texture2D>("sPlayer_a_strip2");

            AnimationController playerAnimator = new AnimationController(new Dictionary<string, Animation>()
            {
                {"Walking", new Animation(walkingSprites, 4, 6f, true)},
                {"Idle", new Animation(playerSprite, 1, 0f, false)},
                {"Jump", new Animation(jumpingSprites, 2, 0f, false)}
            });

            sprite = new Sprite(playerSprite, playerAnimator);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public override void Update()
        {
            SetCameraPosition(position - (ScreenSize / 2));

            KeyboardState keyState = Keyboard.GetState();

            int keyLeft = keyState.IsKeyDown(Keys.A) ? 1 : 0;
            int keyRight = keyState.IsKeyDown(Keys.D) ? 1 : 0;
            bool keyJump = keyState.IsKeyDown(Keys.Space);

            float move = keyRight - keyLeft;

            velocity.X = move * speed;

            velocity.Y = velocity.Y + gravity;

            if (PlaceMeeting(new Vector2(position.X, position.Y + 1), "Block") && keyJump && !hasJumped)
            {
                velocity.Y = -10f;

                hasJumped = true;
            }

            if (keyState.IsKeyUp(Keys.Space))
            {
                hasJumped = false;
            }

            if (keyState.IsKeyDown(Keys.R))
            {
                ResetRoom();
            }
            
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
            } else
            {
                if (velocity.X == 0)
                {
                    sprite.Controller.Play("Idle");
                } else
                {
                    sprite.Controller.Play("Walking");
                }
            }

            if (velocity.X != 0) sprite.flipX = (Math.Sign(velocity.X) != 1);
        }
    }
}
