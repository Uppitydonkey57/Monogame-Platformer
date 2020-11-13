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
    class Button : Entity
    {
        Action runOnClick;

        bool hasClicked;

        public bool FollowCamera { get; set; } = false;

        public Button(Action runOnClick)
        {
            this.runOnClick = runOnClick;
        }

        public override void Initialize(ContentManager content)
        {
            color = Color.SlateGray;

            tag = "UI";

            Texture2D square = content.Load<Texture2D>("Square");

            sprite = new Sprite(square);
        }

        public override void Update()
        {
            if (runOnClick != null)
            {
                MouseState state = Mouse.GetState();

                Rectangle mouseCollider = new Rectangle(state.X, state.Y, 1, 1);

                Rectangle thisRectangle = new Rectangle((int)position.X - ((int)collider.X / 2), (int)position.Y - ((int)collider.Y / 2), (int)collider.X, (int)collider.Y);

                if (thisRectangle.Intersects(mouseCollider) && state.LeftButton == ButtonState.Pressed && !hasClicked)
                {
                    runOnClick();

                    hasClicked = true;
                }

                if (state.LeftButton == ButtonState.Released)
                {
                    hasClicked = false;
                }
            }
        }
    }
}
