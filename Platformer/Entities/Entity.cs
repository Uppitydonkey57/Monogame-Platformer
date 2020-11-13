using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Platformer.Graphics;
using Platformer.Main;

namespace Platformer.Entities
{
    public abstract class Entity
    {
        protected Vector2 position = Vector2.Zero;

        public Vector2 Position { get { return position; } set { position = value; } }

        protected string tag = "";

        public string Tag { get { return tag; } }

        protected Sprite sprite;

        public Sprite Sprite { get { return sprite; } set { sprite = value; } }

        protected Color color = Color.White;

        protected Vector2 velocity;

        public Vector2 Velocity { get { return velocity; } set { velocity = value; } }

        protected Vector2 collider;

        protected Vector2 ScreenSize { get { return EntityManager.bounds; } }

        private float rotation;

        public float Rotation { get { return ToDegree(rotation); } set { rotation = ToRadian(value); } }

        public Vector2 Collider { get { return collider; } }
        public Rectangle ColliderRectangle { get { return new Rectangle((int)(position.X - (collider.X / 2)), (int)(position.Y - (collider.X / 2)), (int)collider.X, (int)collider.Y); } }

        public Entity()
        {
            Initialize(EntityManager.content);
            
            if (sprite.Image != null)
                collider = new Vector2(sprite.Image.Width, sprite.Image.Height);
        }

        //public abstract void Initialize();

        public abstract void Initialize(ContentManager content);

        public abstract void Update();

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        public void AnimationUpdating()
        {
            if (sprite.Controller != null)
                sprite.Controller.Update();

            sprite.rotation = Rotation;

            if (sprite != null && sprite.Controller != null && sprite.Controller.IsPlaying)
            {
                collider = new Vector2(sprite.imageRectangle.Width, sprite.imageRectangle.Height);
            }
        }

        public void Render(SpriteBatch spriteBatch)
        {
            sprite.Render(spriteBatch, position, color);
        }

        protected void Instantiate(Entity entity, Vector2 position)
        {
            EntityManager.AddObject(entity, position);
        }

        protected void Destroy(Entity entity)
        {
            EntityManager.RemoveObject(entity);
        }

        protected Entity FindObjectWithTag(string tag)
        {
            return EntityManager.FindObjectWithTag(tag);
        }

        protected List<Entity> FindObjectsWithTag(string tag)
        {
            return EntityManager.FindObjectsWithTag(tag);
        }

        protected bool PlaceMeeting(Vector2 position, string tag)
        {
            foreach (Entity entity in EntityManager.entities)
            {
                if (entity.tag == tag)
                {
                    Rectangle thisCollider = new Rectangle((int)position.X, (int)position.Y, (int)collider.X, (int)collider.Y);
                    Rectangle otherCollider = new Rectangle((int)entity.Position.X, (int)entity.Position.Y, (int)entity.Collider.X, (int)entity.Collider.Y);

                    if (thisCollider.Intersects(otherCollider))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        protected Entity PlaceMeetingObject(Vector2 position, string tag)
        {

            foreach (Entity entity in EntityManager.entities)
            {
                if (entity.tag == tag)
                {
                    Rectangle thisCollider = new Rectangle((int)position.X, (int)position.Y, (int)collider.X, (int)collider.Y);
                    Rectangle otherCollider = new Rectangle((int)entity.Position.X, (int)entity.Position.Y, (int)entity.Collider.X, (int)entity.Collider.Y);

                    if (thisCollider.Intersects(otherCollider))
                    {
                        return entity;
                    }
                }
            }

            return null;
        }

        protected void PointTowards(Vector2 position1, Vector2 position2)
        {
            Vector2 lookDirection = position2 - position1;
            float angle = (float)Math.Atan2(lookDirection.Y, lookDirection.X);

            Rotation = angle;
        }

        protected void MoveInDirection(float speed)
        {
            Vector2 direction = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));
            direction.Normalize();
            position += direction * speed;
        }

        protected Vector2 GetDirectionLength(float speed, float angle)
        {
            Vector2 direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            direction.Normalize();
            return direction * speed;
        }

        protected void ResetRoom()
        {
            EntityManager.ResetList();
        }

        protected void LoadRoom(string roomName)
        {
            EntityManager.LoadList(roomName);
        }

        protected float ToRadian(float degree)
        {
            return (float)(Math.PI / 180f) * degree;
        }

        protected float ToDegree(float radian)
        {
            return (float)(180f / Math.PI) * radian;
        }

        protected void SetCameraPosition(Vector2 position)
        {
            EntityManager.camera.cameraPosition.X = -position.X;
            EntityManager.camera.cameraPosition.Y = -position.Y;
        }
    }
}
