using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OnionEngine
{
    /// <summary>
    /// The equivalent of World in FlashPunk. Add entities to a subclass of this.
    /// </summary>
    public abstract class Stage
    {
        private List<Entity> UpdateList = new List<Entity>();
        private List<Entity> RenderList = new List<Entity>();

        private List<Entity> AllEntities = new List<Entity>();

        private List<Emitter> Emitters = new List<Emitter>();

        protected Color bgColor = Color.Transparent;

        public void Add(Entity entity, bool render = true, bool update = true)
        {
            
            if (update && !UpdateList.Contains(entity))
                UpdateList.Add(entity);
            if (render && !RenderList.Contains(entity))
                RenderList.Add(entity);
            if (!AllEntities.Contains(entity))
            {
                AllEntities.Add(entity);
                entity.Init(this);
            }
            
            entity.Alive = true;
        }

        public void Add(IEnumerable<Entity> entities, bool render = true, bool update = true)
        {
            foreach (Entity entity in entities)
            {
                Add(entity, render, update);
            }
        }

        public void Add(Emitter e)
        {
            Emitters.Add(e);
        }
        public void Remove(Emitter e)
        {
            Emitters.Remove(e);
        }

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
            for (int i = 0; i < Emitters.Count; i++)
            {
                Emitters[i].Draw();
            }
            //Console.WriteLine(OE.Debug.FPS.Value);
        }

        public virtual void Update()
        {
            for (int i = 0; i < UpdateList.Count; i++)
            {
                UpdateList[i].Update();
            }
            for (int i = 0; i < Emitters.Count; i++)
            {
                Emitters[i].UpdateParticles();
            }
        }

        internal void DrawDebug()
        {
            //List<Entity> ents = RenderList.
            //foreach(Entity e in UpdateList)
            
            //set.
            //UpdateList.

            //var ents = UpdateList.Union(RenderList).ToList();
            for (int i = 0; i < AllEntities.Count; i++)
            {
                if (AllEntities[i].HasHitbox)
                {
                    OE.PrimBatch.DrawRectangle((RectangleF)AllEntities[i].Hitbox, Color.Red, OE.Debug.HitboxColor, false);
                }
            }

            
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
    }
}
