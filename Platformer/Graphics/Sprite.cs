using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Platformer.Graphics
{
    public class Sprite
    {
        Texture2D image;

        public Texture2D Image { get { return image; } set { image = value; } }

        public AnimationController Controller { get; }

        public bool flipX = false;
        public bool flipY = false;

        public Vector2 scale = new Vector2(1, 1);

        public Vector2 origin;

        public float rotation = 0f;

        public Rectangle imageRectangle;

        public Tilemap tilemap;

        #region Constructors
        public Sprite(Texture2D image)
        {
            this.image = image;

            origin = new Vector2(image.Width / 2, image.Height / 2);
        }
        public Sprite(AnimationController Controller, int whichAnimation)
        {
            this.Controller = Controller;

            Animation originAnimation = null;

            int animationCount = 0;

            foreach (Animation animation in Controller.animations.Values)
            {
                animationCount++;

                if (animationCount == whichAnimation)
                {
                    originAnimation = animation;
                }
            }

            if (originAnimation == null)
            {
                throw new NullReferenceException("This animation controller does not have " + whichAnimation + " animations.");
            }

            origin = new Vector2(originAnimation.sprites.Width / originAnimation.howManyFrames / 2, originAnimation.sprites.Height / 2);
        }

        public Sprite(Texture2D image, AnimationController Controller)
        {
            this.image = image;

            this.Controller = Controller;

            origin = new Vector2(image.Width / 2, image.Height / 2);
        }

        public Sprite(Tilemap tilemap)
        {
            this.tilemap = tilemap;
        }
        #endregion

        public void Render(SpriteBatch spriteBatch, Vector2 position, Color color)
        {
            if ((Controller == null || !Controller.IsPlaying) && image != null)
            {
                spriteBatch.Draw(image, position, null, color, rotation, origin, scale, (flipX ? SpriteEffects.FlipHorizontally : SpriteEffects.None) | (flipY ? SpriteEffects.FlipVertically : SpriteEffects.None), 0);
            } else if (Controller != null && Controller.IsPlaying)
            {
                Animation animation = Controller.CurrentAnimation;

                imageRectangle = new Rectangle(animation.sprites.Width / animation.howManyFrames * animation.currentFrame, 0, animation.sprites.Width / animation.howManyFrames, animation.sprites.Height);
                
                if (animation.sprites != null)
                    spriteBatch.Draw(animation.sprites, position, imageRectangle, color, rotation, origin, scale, (flipX ? SpriteEffects.FlipHorizontally : SpriteEffects.None) | (flipY ? SpriteEffects.FlipVertically : SpriteEffects.None), 0);
            } else if (tilemap != null)
            {
                imageRectangle = new Rectangle(tilemap.image.Width / tilemap.tileWidth * tilemap.whichTileX, tilemap.image.Height / tilemap.tileHeight * tilemap.whichTileY, tilemap.image.Width / tilemap.tileWidth, tilemap.image.Height / tilemap.tileHeight);

                spriteBatch.Draw(tilemap.image, position, imageRectangle, color, rotation, origin, scale, (flipX ? SpriteEffects.FlipHorizontally : SpriteEffects.None) | (flipY ? SpriteEffects.FlipVertically : SpriteEffects.None), 0);
            }
        }
    }
}
