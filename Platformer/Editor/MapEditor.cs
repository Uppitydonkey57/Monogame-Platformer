using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer.Entities;
using Platformer.Graphics;
using Platformer.Main;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Platformer.Editor
{
    public class MapEditor : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public string levelFile;

        Entity selectedEntity;

        bool mousePressed;

        MouseState mouseState;

        Vector2 mousePosition;

        int previousScrollValue = 0;

        int scrollSelected = 0;

        public int tileSize;

        Vector2 previousSpot;

        Type[] allEntities = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == "Platformer.Entities").ToArray();

        KeyboardState previousKeyState = Keyboard.GetState();

        public MapEditor()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            EntityManager.content = Content;

            graphics.PreferredBackBufferWidth = 900;
            graphics.PreferredBackBufferHeight = 700;

            EntityManager.bounds.X = graphics.PreferredBackBufferWidth;
            EntityManager.bounds.Y = graphics.PreferredBackBufferHeight;

            IsMouseVisible = true;
            Window.Title = "Editor";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            EntityManager.AddObject(new Button(NewFile), new Vector2(60, 640));
            EntityManager.AddObject(new Button(OpenFile), new Vector2(120, 640));
            EntityManager.AddObject(new Button(SaveFile), new Vector2(180, 640));
            EntityManager.AddObject(new Button(ClearRoom), new Vector2(240, 640));

            selectedEntity = new Block();
        }

        public void NewFile()
        {
            new NewFile(this).Show();
        }

        public void OpenFile()
        {
            new OpenFile(this).Show();
        }

        public void SaveFile()
        {
            new SaveFile(this).Show();
        }
        public void ClearRoom()
        {
            new ClearRoom().Show();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

            float cameraSpeed = 4f;

            if (keyState.IsKeyDown(Keys.A))
            {
                EntityManager.camera.cameraPosition.X += cameraSpeed;
            } 
            if (keyState.IsKeyDown(Keys.D))
            {
                EntityManager.camera.cameraPosition.X -= cameraSpeed;
            } 
            if (keyState.IsKeyDown(Keys.W))
            {
                EntityManager.camera.cameraPosition.Y += cameraSpeed;
            } 
            if (keyState.IsKeyDown(Keys.S))
            {
                EntityManager.camera.cameraPosition.Y -= cameraSpeed;
            }

            EntityManager.camera.Update();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (Entity entity in EntityManager.entities.ToArray())
            {
                mouseState = Mouse.GetState();

                mousePosition = new Vector2(mouseState.X - EntityManager.camera.cameraPosition.X, mouseState.Y - EntityManager.camera.cameraPosition.Y);


                // Change the selected entity debend on the scroll value
                if (mouseState.ScrollWheelValue > previousScrollValue)
                {
                    scrollSelected++;

                    if (scrollSelected >= allEntities.Length)
                    {
                        scrollSelected = 0;
                    } else if (scrollSelected < 0)
                    {
                        scrollSelected = allEntities.Length - 1;
                    }

                    Console.WriteLine(mouseState.ScrollWheelValue + " " + scrollSelected);

                    bool usableCheck = (allEntities[scrollSelected].Name != "Button" && allEntities[scrollSelected].Name != "Entity");

                    if (usableCheck)
                        selectedEntity = (Entity)Activator.CreateInstance(allEntities[scrollSelected]);

                    previousScrollValue = mouseState.ScrollWheelValue;
                }
                else if (mouseState.ScrollWheelValue < previousScrollValue)
                {
                    scrollSelected--;

                    if (scrollSelected >= allEntities.Length)
                    {
                        scrollSelected = 0;
                    }
                    else if (scrollSelected < 0)
                    {
                        scrollSelected = allEntities.Length - 1;
                    }

                    Console.WriteLine(mouseState.ScrollWheelValue + " " + scrollSelected);

                    bool usableCheck = (allEntities[scrollSelected].Name != "Button" && allEntities[scrollSelected].Name != "Entity");

                    if (usableCheck)
                        selectedEntity = (Entity)Activator.CreateInstance(allEntities[scrollSelected]);

                    previousScrollValue = mouseState.ScrollWheelValue;
                }

                if (selectedEntity.Sprite.tilemap != null)
                {
                    Tilemap tilemap = selectedEntity.Sprite.tilemap;

                    if (keyState.IsKeyDown(Keys.Up) && !previousKeyState.IsKeyDown(Keys.Up))
                    {
                        tilemap.whichTileY++;

                        if (tilemap.whichTileY > tilemap.tileHeight)
                        {
                            tilemap.whichTileY = 0;
                        }

                        previousKeyState = keyState;
                    }

                    if (keyState.IsKeyDown(Keys.Down) && !previousKeyState.IsKeyDown(Keys.Down))
                    {
                        tilemap.whichTileY--;

                        if (tilemap.whichTileY < 0)
                        {
                            tilemap.whichTileY = tilemap.tileHeight;
                        }

                        previousKeyState = keyState;
                    }

                    if (keyState.IsKeyDown(Keys.Right) && !previousKeyState.IsKeyDown(Keys.Right))
                    {
                        tilemap.whichTileX++;

                        if (tilemap.whichTileX > tilemap.tileWidth)
                        {
                            tilemap.whichTileX = 0;
                        }

                        previousKeyState = keyState;
                    }

                    if (keyState.IsKeyDown(Keys.Left) && !previousKeyState.IsKeyDown(Keys.Left))
                    {
                        tilemap.whichTileX--;

                        if (tilemap.whichTileX < 0)
                        {
                            tilemap.whichTileX = tilemap.tileWidth;
                        }

                        previousKeyState = keyState;
                    }
                }

                if (levelFile != null && levelFile != "")
                {
                    if (mouseState.LeftButton == ButtonState.Pressed && !mousePressed)
                    {
                        mousePressed = true;

                        Entity placeEntity = (Entity)Activator.CreateInstance(selectedEntity.GetType());

                        Vector2 tiledPosition = GetTiledPosition(placeEntity);

                        previousSpot = tiledPosition;

                        placeEntity.Position = new Vector2(tiledPosition.X, tiledPosition.Y);

                        if (selectedEntity.Sprite.tilemap != null)
                        {
                            placeEntity.Sprite.tilemap.whichTileX = selectedEntity.Sprite.tilemap.whichTileX;
                            placeEntity.Sprite.tilemap.whichTileY = selectedEntity.Sprite.tilemap.whichTileY;
                        }

                        EntityManager.AddObject(placeEntity);
                    }
                }

                if (mouseState.RightButton == ButtonState.Pressed)
                {
                    foreach (Entity deleteEntity in EntityManager.entities.ToArray())
                    {
                        Rectangle mouseRectangle = new Rectangle((int)mousePosition.X, (int)mousePosition.Y, 1, 1);

                        if (deleteEntity.ColliderRectangle.Intersects(mouseRectangle))
                        {
                            if (deleteEntity.Tag != "UI")
                                EntityManager.RemoveObject(deleteEntity);
                        }
                    }
                }

                if (mousePressed)
                {
                    Vector2 mouseSpot = GetTiledPosition((Entity)Activator.CreateInstance(selectedEntity.GetType()));

                    if (mouseSpot != previousSpot)
                    {
                        mousePressed = false;

                        mousePressed = false;
                    }
                }

                if (entity != null)
                {
                    if (entity.Tag == "UI")
                    {
                        entity.Update();
                    }
                }
            }

            previousScrollValue = mouseState.ScrollWheelValue;

            previousKeyState = Keyboard.GetState();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            EntityManager.Draw(spriteBatch);

            base.Draw(gameTime);

            spriteBatch.Begin();

            //if (selectedEntity)
            Animation selectedAnimation = null;

            if (selectedEntity != null)
            {
                if (selectedEntity.Sprite.Image == null && selectedEntity.Sprite.Controller != null)
                    foreach (Animation animation in selectedEntity.Sprite.Controller.animations.Values)
                    {
                        selectedAnimation = animation;
                    }

                if (selectedEntity.Sprite.Image != null)
                    spriteBatch.Draw(selectedEntity.Sprite.Image, new Vector2(GetTiledPosition(selectedEntity).X, GetTiledPosition(selectedEntity).Y), Color.White);
                else if (selectedEntity.Sprite.Controller != null)
                    spriteBatch.Draw(selectedAnimation.sprites, new Vector2(GetTiledPosition(selectedEntity).X, GetTiledPosition(selectedEntity).Y), new Rectangle(0, 0, selectedAnimation.sprites.Width / selectedAnimation.howManyFrames, selectedAnimation.sprites.Height), Color.White);
                else if (selectedEntity.Sprite.tilemap != null) 
                {
                    Tilemap tilemap = selectedEntity.Sprite.tilemap;

                    Vector2 tilePos = new Vector2(tilemap.TileSize.X * tilemap.whichTileX, tilemap.TileSize.Y * tilemap.whichTileY);

                    spriteBatch.Draw(selectedEntity.Sprite.tilemap.image, new Vector2(GetTiledPosition(selectedEntity).X, GetTiledPosition(selectedEntity).Y), new Rectangle((int)tilePos.X, (int)tilePos.Y, (int)tilemap.TileSize.X, (int)tilemap.TileSize.Y), Color.White); 
                }
            }
            spriteBatch.End();
        }

        Vector2 GetTiledPosition(Entity entity)
        {
            if (entity.Sprite.Image != null)
            {
                return new Vector2((float)Math.Round((double)(mouseState.Position.X / entity.Sprite.Image.Width)) * entity.Sprite.Image.Width, (float)Math.Round((double)(mouseState.Position.Y / entity.Sprite.Image.Height)) * entity.Sprite.Image.Height);
            } else if (entity.Sprite.Controller != null)
            {
                float howManyFrames = entity.Sprite.Controller.CurrentAnimation.howManyFrames;

                Texture2D selectedTexture = null;

                foreach (Animation animation in selectedEntity.Sprite.Controller.animations.Values)
                {
                    selectedTexture = animation.sprites;
                    break;
                }

                return new Vector2((float)Math.Round(mousePosition.X / (selectedTexture.Width / howManyFrames)) * (selectedTexture.Width / howManyFrames), (float)Math.Round((double)(mousePosition.Y / selectedTexture.Height)) * selectedTexture.Height);
            } else if (entity.Sprite.tilemap != null)
            {
                Tilemap tilemap = entity.Sprite.tilemap;

                return new Vector2((float)Math.Round((double)(mousePosition.X / (tilemap.image.Width / tilemap.tileWidth))) * (tilemap.image.Width / tilemap.tileWidth), (float)Math.Round((double)(mousePosition.Y / (tilemap.image.Height / tilemap.tileHeight))) * (tilemap.image.Height / tilemap.tileHeight));
            }

            return new Vector2(mousePosition.X, mousePosition.Y);
        }
    }
}
