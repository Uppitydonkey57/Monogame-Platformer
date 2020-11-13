using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Graphics
{
    public class Tilemap
    {
        public Texture2D image;

        public int tileWidth;
        public int tileHeight;

        public int whichTileX = 0;
        public int whichTileY = 0;

        public Vector2 TileSize { get { return new Vector2(image.Width / tileWidth, image.Height / tileHeight); } }

        public Tilemap(Texture2D image, int tileWidth, int tileHeight)
        {
            this.image = image;

            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
        }
    }
}
