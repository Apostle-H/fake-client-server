using Assets.Source.Entity;
using System;
using UnityEngine;

namespace Assets.Source.Effects.Continous.Runtime
{
    internal abstract class AContinousEffect : IContinousEffect
    {
        public Sprite Icon { get; private set; }
        public uint LifeTime { get; private set; }

        public event Action<IEntity, IContinousEffect> OnCount;
        public event Action<IEntity, IContinousEffect> OnFinished;

        protected AContinousEffect(Sprite icon, uint lifeTime)
        {
            Icon = icon;
            LifeTime = lifeTime;
        }

        public abstract void Apply(IEntity target);
        public abstract void Remove(IEntity target);

        protected void Count(IEntity target) => OnCount?.Invoke(target, this);
        protected void Finished(IEntity target) => OnFinished?.Invoke(target, this);
    }
}
