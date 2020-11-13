using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Platformer.Entities;
using Platformer.Graphics;
using Platformer.Main;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.IO;
using System.ComponentModel;
using System.Reflection;

namespace Platformer.Main
{
    public class EntityManager
    {
        public static List<Entity> entities = new List<Entity>();

        public static ContentManager content;

        public static Camera camera = new Camera();

        public static Vector2 bounds;

        public void Update()
        {
            foreach (Entity entity in entities.ToArray())
            {
                entity.AnimationUpdating();
                entity.Update();
            }

            camera.Update();
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);

            foreach (Entity entity in entities)
            {
                if (entity != null)
                {
                    entity.Render(spriteBatch);

                    entity.Draw(spriteBatch);
                }
            }

            spriteBatch.End();
        }

        public static void AddObject(Entity entity)
        {
            entities.Add(entity);
        }

        public static void AddObject(Entity entity, Vector2 position)
        {
            entity.Position = position;

            entities.Add(entity);
        }

        public static void RemoveObject(Entity entity)
        {
            entities.Remove(entity);
        }

        public static Entity FindObjectWithTag(string tag)
        {
            foreach (Entity entity in entities)
            {
                if (entity.Tag == tag)
                {
                    return entity;
                }
            }

            return null;
        }

        public static List<Entity> FindObjectsWithTag(string tag)
        {
            List<Entity> entityList = new List<Entity>();

            foreach (Entity entity in entities)
            {
                if (entity.Tag == tag)
                {
                    entityList.Add(entity);
                }
            }

            return entityList;
        }

        public static void ResetList()
        {
            entities.Clear();
        }

        public static void LoadList(string roomName)
        {
            ResetList();

            string projectName = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;

            string fileAddress = projectName + @"\Levels\" + roomName + ".txt";

            if (File.Exists(fileAddress))
            {
                int howManyLines = File.ReadAllLines(fileAddress).Length - 4;

                using (StreamReader reader = File.OpenText(fileAddress))
                {
                    for (int i = 0; i < 4; i++) reader.ReadLine();

                    for (int linesLeft = 0; linesLeft < howManyLines; linesLeft++)
                    {
                        string[] entityInfo = reader.ReadLine().Split(' ');

                        var allEntities = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == "Platformer.Entities").ToArray();

                        foreach (var file in allEntities)
                        {
                            string path = "Platformer.Entities." + entityInfo[0];

                            if (file.ToString() == path)
                            {
                                Entity newEntity = (Entity)Activator.CreateInstance(file);

                                if (newEntity.Sprite.tilemap != null)
                                {
                                    newEntity.Sprite.tilemap.whichTileX = Convert.ToInt32(entityInfo[3]);
                                    newEntity.Sprite.tilemap.whichTileY = Convert.ToInt32(entityInfo[4]);
                                }

                                AddObject(newEntity, new Vector2(Convert.ToInt32(entityInfo[1]), Convert.ToInt32(entityInfo[2])));
                            }
                        }
                    }
                }
            }
        }
    }
}
