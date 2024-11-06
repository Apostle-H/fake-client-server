using Assets.Source.Entity;
using System;
using UnityEngine;

namespace Assets.Source.Effects.Continous.Runtime
{
    internal interface IContinousEffect
    {
        Sprite Icon { get; }
        uint LifeTime { get; }

        event Action<IEntity, IContinousEffect> OnCount;
        event Action<IEntity, IContinousEffect> OnFinished;

        void Apply(IEntity target);
        void Remove(IEntity target);
    }
}
