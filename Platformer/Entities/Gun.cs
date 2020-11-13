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
    public class Gun : Entity
    {
        int fireDelay;

        float recoil = 4f;

        Player player;

        public override void Initialize(ContentManager content)
        {
            tag = "Gun";

            player = (Player)FindObjectWithTag("Player");

            Texture2D gunSprite = content.Load<Texture2D>("sGun");

            sprite = new Sprite(gunSprite);

            //sprite.origin = new Vector2(0, sprite.Image.Height);
        }

        public override void Update()
        {
            MouseState mouseState = Mouse.GetState();

            position = player.Position;
            position.Y += 10;

            PointTowards(position, new Vector2(mouseState.X, mouseState.Y));

            fireDelay--;

            recoil = Math.Max(0, recoil - 1);

            if (mouseState.LeftButton == ButtonState.Pressed && fireDelay <= 0)
            {
                fireDelay = 8;

                recoil = 4f;

                Bullet bullet = new Bullet()
                {
                    speed = 20,

                    Rotation = Rotation + ToRadian(new Random().Next(6) - 3)
                };

                Console.WriteLine(bullet.Rotation);

                Instantiate(bullet, position);
            }

            position = position + GetDirectionLength(recoil, Rotation);

            if (Rotation > 90 && Rotation < 270)
            {
                sprite.flipY = true;
            } else
            {
                sprite.flipY = false;
            }
        }
    }
}
