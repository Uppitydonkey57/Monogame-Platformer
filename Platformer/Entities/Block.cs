using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Platformer.Entities;
using Platformer.Graphics;
using Platformer.Main;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Platformer.Entities
{
    public class Block : Entity
    {

        public override void Initialize(ContentManager content)
        {
            tag = "Block";

            Texture2D image = content.Load<Texture2D>("Square");

            sprite = new Sprite(image);
        }

        public override void Update()
        {

        }
    }
}
