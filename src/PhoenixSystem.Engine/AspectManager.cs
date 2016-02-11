﻿using System;
using System.Collections.Generic;

namespace PhoenixSystem.Engine
{
    public class AspectManager<AspectType> where AspectType : BaseAspect, new()
    {
        private readonly LinkedList<AspectType> _activeAspects = new LinkedList<AspectType>();
        private readonly LinkedList<AspectType> _availableAspects = new LinkedList<AspectType>();

        public int AvailableAspectCount => _availableAspects.Count;

        public AspectType Get(Entity e)
        {
            AspectType aspect;

            if (_availableAspects.Count > 0)
            {
                aspect = _availableAspects.First.Value;
                aspect.Reset();
                _availableAspects.RemoveFirst();
            }
            else
            {
                aspect = new AspectType();
            }

            aspect.Init(e);
            aspect.Deleted += Aspect_Deleted;

            _activeAspects.AddLast(aspect);

            return aspect;
        }

        protected virtual void Aspect_Deleted(object sender, EventArgs e)
        {
            var aspect = (AspectType) sender;

            aspect.Deleted -= Aspect_Deleted;
            _availableAspects.AddLast(aspect);

            _activeAspects.Remove(aspect);
        }

        public void ClearCache()
        {
            _availableAspects.Clear();
        }
    }
}