using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Platformer.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Platformer.Entities
{
    public class GrassTile : Entity
    {
        public override void Initialize(ContentManager content)
        {
            tag = "Block";

            Texture2D grassTile = content.Load<Texture2D>("sTiles");

            Tilemap tilemap = new Tilemap(grassTile, 8, 6);

            sprite = new Sprite(tilemap);
        }

        public override void Update()
        {
            
        }
    }
}
