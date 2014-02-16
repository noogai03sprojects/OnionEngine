using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OnionEngine
{
    /// <summary>
    /// The equivalent of World in FlashPunk. Add entities to a subclass of this.
    /// </summary>
    public abstract class Stage
    {
        private List<OnionBasic> UpdateList = new List<OnionBasic>();
        private List<OnionBasic> RenderList = new List<OnionBasic>();

        private List<Entity> AllEntities = new List<Entity>();

        //private List<Emitter> Emitters = new List<Emitter>();

        protected Color bgColor = Color.Transparent;

        public void Add(OnionBasic entity, bool render = true, bool update = true)
        {
            
            if (update && !UpdateList.Contains(entity))
                UpdateList.Add(entity);
            if (render && !RenderList.Contains(entity))
                RenderList.Add(entity);
            if (entity is Entity)
            {
                var e = entity as Entity;
                if (!AllEntities.Contains(entity))
                {
                    AllEntities.Add(e);
                    (e).Init(this);
                }

                (e).Alive = true;
            }
        }

        public void Add(IEnumerable<Entity> entities, bool render = true, bool update = true)
        {
            foreach (Entity entity in entities)
            {
                Add(entity, render, update);
            }
        }

        //public void Add(Emitter e)
        //{
        //    //Emitters.Add(e);
        //}
        //public void Remove(Emitter e)
        //{
        //    //Emitters.Remove(e);
        //}

        public void Remove(Entity entity)
        {
            if (UpdateList.Contains(entity))
                UpdateList.Remove(entity);
            if (RenderList.Contains(entity))
                RenderList.Remove(entity);
            if (AllEntities.Contains(entity))
                AllEntities.Remove(entity);

            entity.Alive = false;
        }

        public virtual void Init()
        {
            Add(OE.Debug.FPS, true, false);
        }

        public void Draw()
        {
            OE.Device.Clear(bgColor);
            for (int i = 0; i < RenderList.Count; i++)
            {
                
                RenderList[i].Draw();
            }
            //for (int i = 0; i < Emitters.Count; i++)
            {
                //Emitters[i].Draw();
            }
            //Console.WriteLine(OE.Debug.FPS.Value);
        }

        public virtual void Update()
        {
            for (int i = 0; i < UpdateList.Count; i++)
            {
                UpdateList[i].Update();
            }
            //for (int i = 0; i < Emitters.Count; i++)
            {
                //Emitters[i].Update();
            }
        }

        public virtual void DrawDebug()
        {
            //List<Entity> ents = RenderList.
            //foreach(Entity e in UpdateList)
            
            //set.
            //UpdateList.

            //var ents = UpdateList.Union(RenderList).ToList();
            OE.PrimBatch.Begin(PrimitiveType.LineList, OE.Camera.GetTransform());
            for (int i = 0; i < AllEntities.Count; i++)
            {
                if (AllEntities[i].HasHitbox)
                {
                    OE.PrimBatch.DrawRectangle((RectangleF)AllEntities[i].Hitbox, Color.Red, OE.Debug.HitboxColor, false);
                }
            }
            OE.PrimBatch.End();
            
        }

        /// <summary>
        /// Get all the entities of a certain type.
        /// </summary>
        /// <param name="type">The type to get entities of.</param>
        /// <returns></returns>
        public List<Entity> GetEntities(string type)
        {
            return AllEntities.Where(x => x.Type == type).Where(x => x.HasHitbox).ToList();
        }

        /// <summary>
        /// Query the stage for intersection with a CircularHitbox.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<Entity> QueryCircle(CircularHitbox box, string type)
        {
            var result = new List<Entity>();
            for (int i = 0; i < AllEntities.Count; i++)
            {
                if (AllEntities[i].HasHitbox && AllEntities[i].Type == type)
                {
                    if (Utils.Intersects(AllEntities[i].Hitbox, box))
                        result.Add(AllEntities[i]);
                }
            }
            return result;
        }
    }
}
