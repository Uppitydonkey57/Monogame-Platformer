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
    public class Bullet : Entity
    {
        public float speed;

        public override void Initialize(ContentManager content)
        {
            tag = "Bullet";

            Texture2D bulletSprite = content.Load<Texture2D>("sBullet_strip2");

            AnimationController bulletAnimator = new AnimationController(new Dictionary<string, Animation>()
            {
                { "Fire", new Animation(bulletSprite, 2, 0, false)}
            });

            sprite = new Sprite(bulletAnimator, 1);

            //sprite.Controller.Play("Fire");
        }

        public override void Update()
        {
            if (PlaceMeeting(position, "Block"))
            {
                Destroy(this);
            }

            if (PlaceMeeting(position, "Enemy"))
            {
                Enemy hitEnemy = (Enemy)PlaceMeetingObject(position, "Enemy");

                hitEnemy.hp--;

                hitEnemy.flash = 3;

                hitEnemy.hitFrom = Rotation;

                Destroy(this);
            }

            MoveInDirection(speed);

            if (sprite.Controller.CurrentAnimation == null)
                sprite.Controller.Play("Fire");
        }
    }
}
