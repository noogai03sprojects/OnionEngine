using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OnionEngine
{
    abstract class Stage
    {
        private List<Entity> UpdateList = new List<Entity>();
        private List<Entity> RenderList = new List<Entity>();

        private List<Entity> AllEntities = new List<Entity>();

        protected Color bgColor = Color.Transparent;

        public void Add(Entity entity, bool render = true, bool update = true)
        {
            if (entity.Alive)
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
            }
        }

        public void Add(IEnumerable<Entity> entities, bool render = true, bool update = true)
        {
            foreach (Entity entity in entities)
            {
                Add(entity, render, update);
            }
        }

        public void Remove(Entity entity)
        {
            if (UpdateList.Contains(entity))
                UpdateList.Remove(entity);
            if (RenderList.Contains(entity))
                RenderList.Remove(entity);
            if (AllEntities.Contains(entity))
                AllEntities.Remove(entity);
        }

        public virtual void Init()
        {
            
        }

        public void Draw()
        {
            OE.Device.Clear(bgColor);
            for (int i = 0; i < RenderList.Count; i++)
            {
                UpdateList[i].Draw();
            }
        }

        public virtual void Update()
        {
            for (int i = 0; i < UpdateList.Count; i++)
            {
                UpdateList[i].Update();
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
